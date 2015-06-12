
/* 
 Focuser - controlling of the telescope focuser with stepper motor. 
 
 The motor driver is attached to digital pins 8, 9, 10, 11, 12, 13 of the Arduino.
 The Left and Right buttons conected to pins 2 and 4. 
 The Release button - pin 7.
 A speed control (potentiometer) is connected to analog input 0.
  
 The motor will rotate in a clockwise or opposite direction depends on the button pressed. 
 The higher the potentiometer value, the faster the motor speed. 
 Because setSpeed() sets the delay between steps, 
 you may notice the motor is less responsive to changes in the sensor value at 
 low speeds.
 
 In case of zero speed (potentimetsr gives 0), it works in step-by-step mode. 
 This mean then with each button press only one step comes, holding button do nothing. Its important for final turning of focus 
 (disabled with micro-step driver).
 
 Created 20.04.2014
 by Alexey V. Popov
 St.Petersbrug 
 9141866@gmail.com
 
 */

//#include <Stepper.h>
#include <StepperClass.h>
#include <EEPROM.h>
#include <Metro.h> // Include the Metro library

const char SketchVersion[] = "1.4";

union TInt {
  long l;
  int i[2];
  byte b[4];
};


// A4988 connection pins
#define DirectionPin 9                      //Direction Pin - Initial State is ZERO
#define StepPin 8                           //Step Pin - Pulse this to step the motor in the direction selected by the Direction Pin
#define MS1 12
#define MS2 11
#define MS3 10
#define A4988_ENABLES 3

// speed limitations as a delay between steps and steps by revolution
#define MIN_SPEED_DELAY 100000
#define MAX_SPEED_DELAY 250
#define DEFAULT_MS MICROSTEP16
#define STEP_PER_REVOLUTION 200

//  Communication protocol
// 168 - first byte, #13#10 - end of the command
const int FOCUSER_CMD_START = 168;
const int FOCUSER_CMD_STOP_1 = 13;
const int FOCUSER_CMD_STOP_2 = 10;
const int FOCUSER_HANDSHAKE = 254;
const int FOCUSER_GET_VERSION = 253;
const int FOCUSER_PING = 255;
const int FOCUSER_MSG_DEBUG = 250;
const int FOCUSER_DEBUG = 251;
const int FOCUSER_RC = 252;

const int FOCUSER_STOP = 210;
const int FOCUSER_STEP_RIGHT = 211;
const int FOCUSER_ROLL_RIGHT = 212;
const int FOCUSER_RANGE_CHECK = 215;
const int FOCUSER_STEP_LEFT = 209;
const int FOCUSER_ROLL_LEFT = 208;
const int FOCUSER_ROLLING = 202;
const int FOCUSER_GET_SPEED = 240;  
const int FOCUSER_SET_SPEED = 241;  
const int FOCUSER_RELEASE = 220; 
const int FOCUSER_GET_RELEASE_TIME = 221; 
const int FOCUSER_SET_RELEASE_TIME = 222; 
const int FOCUSER_POWER_ON = 223;
const int FOCUSER_GET_MIN_SPEED_DELAY = 239;
const int FOCUSER_GET_MAX_SPEED_DELAY = 238;
const int FOCUSER_SET_MICROSTEP = 231;
const int FOCUSER_GET_MICROSTEP = 230;
const int FOCUSER_GET_POSITION = 225;
const int FOCUSER_SET_POSITION = 229;
const int FOCUSER_GO_TO_POSITION = 226;
const int FOCUSER_SET_MIN_POSITION = 227;
const int FOCUSER_SET_MAX_POSITION = 228;
const int FOCUSER_GET_MIN_POSITION = 233;
const int FOCUSER_GET_MAX_POSITION = 224;

#define DeviceType 101

int SerialBuf[255];
int BufLength = 0;

//  Remote controls pins
const int buttonPinLeft = 2;     // 
const int buttonPinRight = 4;     // 
const int buttonPinRelease = 7;
const int speedled = 6;           // speed led
const int motorled = 5;           // motor motion led

int buttonStateLeft = 0;         // variable for reading the pushbutton status
int buttonOldStateLeft = LOW;
int buttonStateRight = 0;         // variable for reading the pushbutton status
int buttonOldStateRight = LOW;

int buttonPinState = LOW;

// initialize the stepper library on pins 8 through 11, SeeedStudio MotorShield:
//Stepper myStepper(stepsPerRevolution, 8,11,12,13);            

//initialize the stepper library AccelStepper on pins 8 (STEP) and 9 (DIR) , A4988:
//AccelStepper myStepper(AccelStepper::DRIVER, StepPin, DirectionPin); // Defaults to AccelStepper::FULL4WIRE (4 pins) on 2, 3, 4, 5


//  motor`s variable and some parameters
StepperClass FocuserStepper(StepPin, DirectionPin, A4988_ENABLES, MS1, MS2, MS3, STEP_PER_REVOLUTION, A4988);

long OldMotorSpeedDU = -100;
long old_position = 0;


enum MOTOR_STATE {ROLLING_LEFT = -1, HOLD = 0, ROLLING_RIGHT = 1};

/*const int ROLLING_LEFT = -1;
const int ROLLING_RIGHT = 1;
const int HOLD = 0;*/
volatile int IsRolling = HOLD;  // Rolling status
volatile int IsRollingToNewPos = 0;  // Rolling to the new position status

unsigned long LastAction=0, LastPosCheck = 0, LastSpeedCheck = 0;

#define RELEASE_TIME_ADDRESS  1
#define CUR_POS_ADDRESS       4
#define MIN_POS_ADDRESS       8
#define MAX_POS_ADDRESS       12
volatile int ReleaseTime = 2;   //  Idle time to release motor, in seconds
int CheckSpeedTime = 100;  //  

Metro SavePosTime = Metro(60000);  // Instantiate an instance
long SavedPosition = 0;

// Some flags
volatile int IsDebug = false;
volatile int IsRC = true;


// --------------- Initialization ------------------------------------


void setup() {
    
    FocuserStepper.Init(MIN_SPEED_DELAY, MAX_SPEED_DELAY);   
    FocuserStepper.SetMicroStep(DEFAULT_MS);
    FocuserStepper.SetSpeed(500);
    FocuserStepper.fMaxPosition = 0;    
    FocuserStepper.fMaxPosition = 100000;    
    FocuserStepper.fPosition = 20000;       
    
    if ( EEPROM.read(0) != 127){     
       EEPROM.write(0, 127);       
       ReleaseTime = 300;
       EEPROM.write(RELEASE_TIME_ADDRESS, highByte(ReleaseTime));              
       EEPROM.write(RELEASE_TIME_ADDRESS+1, lowByte(ReleaseTime));                
       SavePosition(CUR_POS_ADDRESS, FocuserStepper.fPosition);
       SavePosition(MIN_POS_ADDRESS, FocuserStepper.fMinPosition);
       SavePosition(MAX_POS_ADDRESS, FocuserStepper.fMaxPosition);       
    } else {
       ReleaseTime = word(EEPROM.read(RELEASE_TIME_ADDRESS),EEPROM.read(RELEASE_TIME_ADDRESS+1));
       FocuserStepper.fPosition = LoadPosition(CUR_POS_ADDRESS);
       FocuserStepper.fMinPosition = LoadPosition(MIN_POS_ADDRESS);
       FocuserStepper.fMaxPosition = LoadPosition(MAX_POS_ADDRESS);
    }

    SavedPosition = FocuserStepper.fPosition;

    pinMode(buttonPinLeft, INPUT);
    pinMode(buttonPinRight, INPUT);
    pinMode(buttonPinRelease, INPUT);
    pinMode(speedled, OUTPUT);
    pinMode(motorled, OUTPUT);

    
    Serial.begin(115200);
    Serial.println("Started!");
    
    GetMotorSpeed();  
    
    LastAction = millis();
}

void SavePosition(int addr, long pos)
{
  TInt sPos;
  sPos.l = pos;
  EEPROM.write(addr, sPos.b[0]);              
  EEPROM.write(addr+1, sPos.b[1]);              
  EEPROM.write(addr+2, sPos.b[2]);              
  EEPROM.write(addr+3, sPos.b[3]);                  
}

long LoadPosition(int addr)
{
  TInt sPos;
  sPos.b[0] = EEPROM.read(addr);
  sPos.b[1] = EEPROM.read(addr+1);
  sPos.b[2] = EEPROM.read(addr+2);
  sPos.b[3] = EEPROM.read(addr+3);  
  return sPos.l;
}

// ----------------   Communications with PC --------------------------------

void SendCmd(int cmd)
{
       Serial.write(FOCUSER_CMD_START);
       Serial.write(cmd);
       Serial.println("");
}

void SendCmd(int cmd, int par)
{
       Serial.write(FOCUSER_CMD_START);
       Serial.write(cmd);
       Serial.write(par);       
       Serial.println("");
}

void SendCmd(int cmd, String str)
{
       Serial.write(FOCUSER_CMD_START);
       Serial.write(cmd);
       Serial.println(str);
}

int FindCmdStart(int *buff, int length)
{
    for (int i=0; i<length; i++){
       if (buff[i]==FOCUSER_CMD_START) 
           return i;       
    }
    return -1;
}

int FindCmdEnd(int *buff, int length)
{
    for (int i=0; i<length-1; i++){
       if ((buff[i]==13)&&(buff[i+1]==10))
           return i;       
    }
    return -1;
}

int ShiftBuffer(int *SerialBuffer, int length, int x)
{
    for (int i=x; i!=length; i++)
      SerialBuffer[i-x] = SerialBuffer[i];

    for (int i=length-x; i!=length; i++)
      SerialBuffer[i] = 0;
      
    return length-x;
}

void serialEvent(){
   if (Serial.available()>0){

    if (IsDebug)
      SendCmd(FOCUSER_MSG_DEBUG, "serial event, buff length: " + String(Serial.available()));
    int command = -1;
    int cmd_end = -1;
    
    do{ 
      command = Serial.read();
      SerialBuf[BufLength] = command;
      BufLength++;
    } while (Serial.available()>0);   
       
    if (IsDebug)
      SendCmd(FOCUSER_MSG_DEBUG, "data recieved: " + String(BufLength));
 
    while (BufLength>0){
    
        int x = FindCmdStart(SerialBuf, BufLength);   
                           
        if (x>0){
           if (IsDebug)           
             SendCmd(FOCUSER_MSG_DEBUG, "cmd start >0 - shift");
           BufLength = ShiftBuffer(SerialBuf, BufLength, x);
           continue;
        }
        
        if (x==-1){
           BufLength = 0;
           if (IsDebug)
             SendCmd(FOCUSER_MSG_DEBUG, "cmd not found");
           return;
        }       
        
        cmd_end = FindCmdEnd(SerialBuf, BufLength);          
        if (cmd_end ==-1){
           if (IsDebug)          
             SendCmd(FOCUSER_MSG_DEBUG, "cmd end not found");
           return;
        }
                             
        LastAction = millis();    
        command = SerialBuf[1];
        String param = "";
        for (int i=2; i<cmd_end; i++){
          param = param + char(SerialBuf[i]);
        }       
        if (IsDebug)       
          SendCmd(FOCUSER_MSG_DEBUG, "cmd found: " + String(command));
        
        switch (command){
          case FOCUSER_PING:   //  ping back;
               SendCmd(FOCUSER_PING);
             break;                       
             
          case FOCUSER_HANDSHAKE:  // handshake
              SendCmd(FOCUSER_HANDSHAKE, DeviceType);
            break; 
            
          case FOCUSER_GET_VERSION:  // handshake
              SendCmd(FOCUSER_GET_VERSION, SketchVersion);
            break; 
            
          case FOCUSER_STOP:  // stop
             SendCmd(FOCUSER_STOP);             
             IsRolling = HOLD;    
             SendCmd(FOCUSER_GET_POSITION, String(FocuserStepper.fPosition));                          
             if  (IsRollingToNewPos==HIGH){
                IsRollingToNewPos = 0;
                SendCmd(FOCUSER_GO_TO_POSITION, "0");
                SendCmd(FOCUSER_ROLLING, IsRollingToNewPos);                
             }
            break;             
            
          case FOCUSER_STEP_RIGHT: // step right
             FocuserStepper.Step(STEP_BACKWARD);  
             SendCmd(FOCUSER_STEP_RIGHT);
             SendCmd(FOCUSER_POWER_ON);
            break;      
            
          case FOCUSER_STEP_LEFT: //  step left
             FocuserStepper.Step(STEP_FORWARD);
             SendCmd(FOCUSER_STEP_LEFT);
             SendCmd(FOCUSER_POWER_ON);             
            break;

          case FOCUSER_ROLL_RIGHT: // rolling right
            IsRolling = ROLLING_RIGHT;
            SendCmd(FOCUSER_ROLL_RIGHT);            
            SendCmd(FOCUSER_POWER_ON);                         
            break;      
                       
          case FOCUSER_ROLL_LEFT: //  rolling left     
            IsRolling = ROLLING_LEFT;      
            SendCmd(FOCUSER_ROLL_LEFT);            
            SendCmd(FOCUSER_POWER_ON);                         
            break;      
            
          case FOCUSER_RELEASE:
              SendCmd(FOCUSER_RELEASE);
              if  (IsRollingToNewPos==HIGH){
                IsRollingToNewPos = 0;
                SendCmd(FOCUSER_GO_TO_POSITION, "0");
                SendCmd(FOCUSER_ROLLING, IsRollingToNewPos);                
              }
              FocuserStepper.EnablePower(false);
              UpdatePosition();                                                  
            break;  
            
          case FOCUSER_GET_SPEED:
             SendCmd(FOCUSER_GET_SPEED, String(FocuserStepper.GetSpeed()));         
            break;
            
          case FOCUSER_GET_MIN_SPEED_DELAY:
             SendCmd(FOCUSER_GET_MIN_SPEED_DELAY, String(MIN_SPEED_DELAY));
            break; 
           
          case FOCUSER_GET_MAX_SPEED_DELAY:
             SendCmd(FOCUSER_GET_MAX_SPEED_DELAY, String(MAX_SPEED_DELAY));
            break; 
           
          case FOCUSER_SET_MICROSTEP:
                FocuserStepper.SetMicroStep((byte)SerialBuf[2]);                
                SendCmd(FOCUSER_GET_MICROSTEP, FocuserStepper.fMicroStep);                
              break;
              
          case FOCUSER_GET_MICROSTEP:  
               SendCmd(FOCUSER_GET_MICROSTEP, FocuserStepper.fMicroStep);
              break; 
                            
        case FOCUSER_GET_POSITION:
              SendCmd(FOCUSER_GET_POSITION, String(FocuserStepper.fPosition));
              break;
              
        case FOCUSER_GO_TO_POSITION:{
               long new_position = param.toInt();
               SendCmd(FOCUSER_GO_TO_POSITION, String(new_position));  
               FocuserStepper.fRelativePosition = new_position-FocuserStepper.fPosition; 
               if(IsDebug)
                 SendCmd(FOCUSER_MSG_DEBUG, "Going to new relative position:" + String(FocuserStepper.fRelativePosition));          
               FocuserStepper.fTargetPosition = new_position;
               IsRollingToNewPos = HIGH;
               SendCmd(FOCUSER_POWER_ON);       
               SendCmd(FOCUSER_ROLLING, IsRollingToNewPos);               
              break;
        }
        case FOCUSER_SET_MAX_POSITION:
                FocuserStepper.fMaxPosition = param.toInt();               
                SavePosition(MAX_POS_ADDRESS, FocuserStepper.fMaxPosition);       
                FocuserStepper.fMaxPosition = LoadPosition(MAX_POS_ADDRESS);                
                SendCmd(FOCUSER_SET_MAX_POSITION, String(FocuserStepper.fMaxPosition) ); //String(max_position));        
              break;                      
        case FOCUSER_SET_MIN_POSITION:
                FocuserStepper.fMinPosition = param.toInt();               
                SavePosition(MIN_POS_ADDRESS, FocuserStepper.fMinPosition);
                FocuserStepper.fMinPosition = LoadPosition(MIN_POS_ADDRESS);
                SendCmd(FOCUSER_SET_MIN_POSITION, String(FocuserStepper.fMaxPosition) ); //String(max_position));        
              break;                            
              
        case FOCUSER_GET_MAX_POSITION:
                SendCmd(FOCUSER_SET_MAX_POSITION, String(FocuserStepper.fMaxPosition) ); //String(max_position));        
              break;                      
        case FOCUSER_GET_MIN_POSITION:                
                SendCmd(FOCUSER_SET_MIN_POSITION, String(FocuserStepper.fMinPosition) ); //String(max_position));        
              break;                                          

        case FOCUSER_SET_POSITION:
              FocuserStepper.fPosition = param.toInt();
              SendCmd(FOCUSER_GET_POSITION, String(FocuserStepper.fPosition));
             break;        
              
        case FOCUSER_RANGE_CHECK:
               FocuserStepper.fRangeCheck = SerialBuf[2];
               SendCmd(FOCUSER_RANGE_CHECK, SerialBuf[2]);
             break;
        case FOCUSER_SET_SPEED:{
               long new_speed = param.toInt();
               SetSpeed(new_speed);
            break; }               
         case FOCUSER_ROLLING:
              SendCmd(FOCUSER_ROLLING, IsRollingToNewPos);
            break;                      
         case FOCUSER_DEBUG:
             IsDebug = SerialBuf[2];
             SendCmd(FOCUSER_DEBUG, IsDebug);
            break;          
         case FOCUSER_RC:
             IsRC = SerialBuf[2];
             SendCmd(FOCUSER_RC, IsRC);
            break;              
         case FOCUSER_GET_RELEASE_TIME:
            SendCmd(FOCUSER_GET_RELEASE_TIME, String(ReleaseTime));
            break; 
         case FOCUSER_SET_RELEASE_TIME:
            ReleaseTime = param.toInt();         
            EEPROM.write(RELEASE_TIME_ADDRESS, highByte(ReleaseTime));              
            EEPROM.write(RELEASE_TIME_ADDRESS+1, lowByte(ReleaseTime));              
            SendCmd(FOCUSER_SET_RELEASE_TIME, String(ReleaseTime));            
            break;          
         case FOCUSER_POWER_ON:
            LastAction = millis();
            FocuserStepper.EnablePower(true);      
            SendCmd(FOCUSER_POWER_ON);
            break;

     }         
       BufLength = ShiftBuffer(SerialBuf, BufLength, cmd_end+2);
    }    
  } 
}

// ----------------------------------     Some extra fucntion ------------------------------

void GetMotorSpeed()   // Read potentiometer
{
// read the sensor value:
  int sensorReading = analogRead(A0);
  int new_motorSpeed = sensorReading; //map(sensorReading, 0, 1023, 1, 200);
  int speed_diff = new_motorSpeed-OldMotorSpeedDU;
  if ( abs(speed_diff)>4){ 
      if (IsDebug)
        SendCmd(FOCUSER_MSG_DEBUG, "change motor speed: " + String(OldMotorSpeedDU) + " -> " + String(new_motorSpeed));
      OldMotorSpeedDU = new_motorSpeed;      
      SetSpeed(map(new_motorSpeed, 0, 1023, MAX_SPEED_DELAY, MIN_SPEED_DELAY));
  }
}

void SetSpeed(long new_motorSpeed)    // Apply new spped value 
{ 
  if (FocuserStepper.GetSpeed() != new_motorSpeed){              
    FocuserStepper.SetSpeed(new_motorSpeed);
    int led_brightness = 0;
    led_brightness = 201 - map(FocuserStepper.GetSpeed(), MAX_SPEED_DELAY, MIN_SPEED_DELAY, 0, 200);
    analogWrite(speedled, led_brightness);    
    SendCmd(FOCUSER_GET_SPEED, String(FocuserStepper.GetSpeed()));
   }  
}

boolean UpdatePosition(){   // Send new positio to PC if it has been changed
   if (old_position != FocuserStepper.fPosition){
      old_position = FocuserStepper.fPosition;
      SendCmd(FOCUSER_GET_POSITION, String(FocuserStepper.fPosition));
      return true;
   }   
   return false; 
}

void Roll(int dir){    // Blinking with LED during while rolling
      analogWrite(motorled, 100);       
      FocuserStepper.Roll(dir);      
      analogWrite(motorled, 0);        
}


//   -----------------------------     Main loop   -------------------------

void loop() {      

  if (IsRC){
  
    if (millis() - LastSpeedCheck > CheckSpeedTime){   // Potentiometer check
       GetMotorSpeed();
       LastSpeedCheck = millis();
    }
       
    buttonStateLeft = digitalRead(buttonPinLeft);    // read buttons
    buttonStateRight = digitalRead(buttonPinRight);       
       
    buttonPinState = digitalRead(buttonPinRelease);                 
   }
          
  if (IsRollingToNewPos==HIGH){   // if Rolling to a new specified form PC position 
    
    FocuserStepper.fRelativePosition = FocuserStepper.fTargetPosition-FocuserStepper.fPosition;    
    if (abs(FocuserStepper.fRelativePosition)>=FocuserStepper.fMicroSteps[FocuserStepper.fMicroStep]){
//       FocuserStepper.fSpeed = 250;
       if (FocuserStepper.fRelativePosition>0)        
            Roll(STEP_FORWARD);
       else if (FocuserStepper.fRelativePosition<0)
            Roll(STEP_BACKWARD);
    }
     
    if (abs(FocuserStepper.fRelativePosition)<FocuserStepper.fMicroSteps[FocuserStepper.fMicroStep]){
        UpdatePosition();         
        IsRollingToNewPos=LOW;
        SendCmd(FOCUSER_GO_TO_POSITION, "0");
        SendCmd(FOCUSER_ROLLING, IsRollingToNewPos);        
    }    
   LastAction = millis();              
           
    if (IsDebug)
       SendCmd(FOCUSER_MSG_DEBUG, "distance:" + String(FocuserStepper.fRelativePosition));                                  
  } else {

    if ((buttonStateLeft==HIGH)||(IsRolling==ROLLING_LEFT)){  // and roll if pressed
       Roll(STEP_FORWARD);
       LastAction = millis();              
    } else if ((buttonStateRight==HIGH)||(IsRolling==ROLLING_RIGHT)) {  
       Roll(STEP_BACKWARD);
       LastAction = millis();          
    }
  }  
 
  if (FocuserStepper.fEnabled){   // if out of use for the ReleaseTime - realse motor
    int time_diff = (millis() - LastAction)/1000;
    if ((buttonPinState==HIGH)||((time_diff>ReleaseTime)&&(ReleaseTime>0))){    
       FocuserStepper.EnablePower(false);      
       SendCmd(FOCUSER_RELEASE);      
    }       
  }
 
  int time_diff = millis() - LastPosCheck;  // if necassery - check position and send it to PC
  if (time_diff>200){
      UpdatePosition();
      LastPosCheck = millis();
  }

  if ((SavePosTime.check())&&(SavedPosition!=FocuserStepper.fPosition))
  {
      SavedPosition = FocuserStepper.fPosition;
      SavePosition(CUR_POS_ADDRESS, SavedPosition);
      SendCmd(FOCUSER_MSG_DEBUG, "Saved position: " + String(SavedPosition));      
  }

  buttonOldStateRight = buttonStateRight; 
  buttonOldStateLeft = buttonStateLeft;  
  
}





