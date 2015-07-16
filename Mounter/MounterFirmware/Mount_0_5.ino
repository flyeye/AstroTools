/* 
 Mounter - controlling of the telescope mount with stepper motors. 

 
 The motors rotates in a clockwise or opposite direction depends on the button pressed. 
 The higher the potentiometer value, the faster the motor speed. 

 
 Created 01.10.2014
 by Alexey Popov
 
 */

#include <StepperClass.h>
#include <Wire.h>
#include <LiquidCrystal_I2C.h>
//#include "RTClib.h"
#include <DS3232RTC.h>
#include <Time.h>
#include <EEPROM.h>
#include <Metro.h> // Include the Metro library
#include <FlexiTimer2.h>
#include <floattostring.h>

const char SketchVersion[] = "0.5";

//  Communication protocol
// 168 - first byte, <command> - 1 byte, <params> - a few byte of command parameters, #13#10 - end of the command

const int MOUNTER_STOP = 210;
const int MOUNTER_STEP = 211;
const int MOUNTER_ROLL = 212;

const int FOCUSER_PING = 255;
const int FOCUSER_HANDSHAKE = 254;
const int FOCUSER_RANGE_CHECK = 215;
const int MOUNTER_GET_NAV_SPEED = 240;  
const int MOUNTER_SET_NAV_SPEED = 241;  
const int MOUNTER_GET_DAILY_SPEED = 242;  
const int MOUNTER_SET_DAILY_SPEED = 243;
const int MOUNTER_GET_POWER = 220; //
const int MOUNTER_SET_POWER = 223;
const int FOCUSER_GET_RELEASE_TIME = 221; 
const int FOCUSER_SET_RELEASE_TIME = 222; 
const int FOCUSER_CMD_START = 168;
const int MOUNTER_GET_NAV_MAX_SPEED = 239;
const int MOUNTER_GET_NAV_MIN_SPEED = 238;
const int MOUNTER_GET_DAILY_MAX_SPEED = 237;
const int MOUNTER_GET_DAILY_MIN_SPEED = 236;
const int FOCUSER_SET_MICROSTEP = 231;
const int FOCUSER_GET_MICROSTEP = 230;
const int FOCUSER_GET_POSITION = 225;
const int FOCUSER_GO_TO_POSITION = 226;
const int FOCUSER_SET_MIN_POSITION = 227;
const int FOCUSER_SET_MAX_POSITION = 228;
const int FOCUSER_SET_POSITION = 229;
const int FOCUSER_CMD_STOP_1 = 13;
const int FOCUSER_CMD_STOP_2 = 10;
const int MOUNTER_MSG_DEBUG = 250;
const int MOUNTER_SET_DROTATION = 206;
const int MOUNTER_GET_DROTATION = 213;
const int MOUNTER_DUBUG = 251;
const int MOUNTER_RC = 252;
const int FOCUSER_GET_VERSION = 253;

const int FBC_RTC_GET = 90;
const int FBC_RTC_SET = 91;
const int FBC_RTC_TEMP = 92;

#define DeviceType 102 

// ----------------   Motors   -----------------------------

#define RA_ENABLES 30
#define MS1_RA 32
#define MS2_RA 34
#define MS3_RA 36
#define StepPinRA 38                           //Step Pin - Pulse this to step the motor in the direction selected by the Direction Pin
#define DirectionPinRA 40                      //Direction Pin - Initial State is ZERO

#define DE_ENABLES 31
#define MS1_DE 33
#define MS2_DE 35
#define MS3_DE 37
#define StepPinDE 39                           //Step Pin - Pulse this to step the motor in the direction selected by the Direction Pin
#define DirectionPinDE 41                      //Direction Pin - Initial State is ZERO

StepperClass DEStepper(StepPinDE, DirectionPinDE, DE_ENABLES, MS1_DE, MS2_DE, MS3_DE, 1036, DRV8825);
StepperClass RAStepper(StepPinRA, DirectionPinRA, RA_ENABLES, MS1_RA, MS2_RA, MS3_RA, 1036, DRV8825);

const int MOTOR_RA = 1;
const int MOTOR_DE = 2;

// ----------------   Buttons fot remote   -----------------------------

#define buttonPinLeft 4     // the number of the pushbutton pin
#define buttonPinRight 5     // the number of the pushbutton pin
#define btnPinLeftDE 6     // the number of the pushbutton pin
#define btnPinRightDE 7     // the number of the pushbutton pin
#define buttonPinRelease 3
#define buttonPinTurn 2

#define speedled 8           // speed led
#define turnled 9 
#define speedled_nav 10
#define motorled 11           // motor led


int buttonStateLeft = 0;         // variable for reading the pushbutton status
int buttonOldStateLeft = LOW;
int buttonStateRight = 0;         // variable for reading the pushbutton status
int buttonOldStateRight = LOW;
int buttonStateTurn = 0;         // variable for reading the pushbutton status
int buttonOldStateTurn = LOW;

int btnStateLeftDE = 0;         // variable for reading the pushbutton status
int btnOldStateLeftDE = LOW;
int btnStateRightDE = 0;         // variable for reading the pushbutton status
int btnOldStateRightDE = LOW;
int btnPinState = 0;

// ------------------------------------------

// for your motor
long MINSPEED_DAILY_RA_D = 25000;
long MAXSPEED_DAILY_RA_D = 16500;
long MINSPEED_DAILY_DE_D = 1000000000;
long MAXSPEED_DAILY_DE_D = -1000000000;
long MINSPEED_NAV_D = 30000;
long MAXSPEED_NAV_D = 125;
const int stepsPerRevolutionDefault = 33137;  //  for motor with gear 5*(2/11) and 200 steps/turn with 32 micro-step drive


const int MODE_DAILY = 1;
const int MODE_NAV = 2;

volatile long daily_speed_RA = MINSPEED_DAILY_RA_D;
volatile long daily_speed_DE = MINSPEED_DAILY_DE_D;
volatile long nav_speed = MAXSPEED_NAV_D;
volatile int nav_micro_step = MICROSTEP8;
volatile int daily_micro_step = MICROSTEP32;
int OldMotorSpeedDU_Daily = -100;
int OldMotorSpeedDU_Nav = -100;

long RA_old_position = 0;
long DE_old_position = 0;

unsigned long LastActionRA=0, LastActionDE=0,LastPosCheck = 00;
unsigned long PositionCheckTime = 250;
Metro CheckSpeedTime = Metro(200);  // Instantiate an instance
 

#define RELEASE_TIME_ADDRESS  1024
int ReleaseTime = 300;

const int HOLD = 0;
const int TURNING = 1;

volatile int IsRollingRA = HOLD;
volatile int IsRollingDE = HOLD;
volatile int IsTurningRA = HOLD;
volatile int IsTurningDE = HOLD;
volatile int IsRollingToNewPos = false;
volatile int IsAutoStop = 0;

volatile boolean IsDebug = false;
volatile boolean IsRC = true;

volatile int IsRangeCheck = false;

int SerialBuf[512];
int BufLength = 0;

LiquidCrystal_I2C lcd(0x27,16,2);  // set the LCD address to 0x27 for a 16 chars and 2 line display
unsigned long MainLoopTime = 0;
unsigned long MainLoopRepeatTime = 0;
unsigned long LastTimeCheck = 0;
unsigned long OpTime1 = 0, OpTime2 = 0, OpTime3 = 0;
unsigned long LoopTime1 = 0,  LoopTime2 = 0; 

union TInt {
  long l;
  int i[2];
  byte b[4];
  float f;
};


int StepCount = 0;
long StepSum = 0;
long StepMean = 0;


//  ----------   Implemenataion -------------------------------------------------------------------------------------------------


void MotorStop()
{
    if (IsAutoStop == MOTOR_RA){
        IsRollingRA = HOLD;
        LastPosCheck = 0;
        RAStepper.Stop();
        SendCmd(MOUNTER_STOP, MOTOR_RA);
    } else if (IsAutoStop == MOTOR_DE){
        IsRollingDE = HOLD;      
        LastPosCheck = 0;        
        DEStepper.Stop();       
        SendCmd(MOUNTER_STOP, MOTOR_DE);
    }
    FlexiTimer2::stop(); 
    IsAutoStop = 0;
}

void GetMotorSpeedDaily()
{
// read the sensor value:
     int sensorReading = analogRead(A0);
     int new_motorSpeed = map(sensorReading, 0, 1023, 1, 200);
     int speed_diff = new_motorSpeed-OldMotorSpeedDU_Daily;
     if ( abs(speed_diff)>1){ 
//      SendCmd(MOUNTER_MSG_DEBUG, "change motor speed: " + String(OldMotorSpeedDU) + " -> " + String(new_motorSpeed));
        OldMotorSpeedDU_Daily = new_motorSpeed;      
//      if (new_motorSpeed>0)
//      new_motorSpeed = map(new_motorSpeed, 1, 200, 1, MAXSPEED);        
        SetDailySpeed( map(new_motorSpeed, 1, 200, MAXSPEED_DAILY_RA_D, MINSPEED_DAILY_RA_D) );
      }     
} 


void GetMotorSpeedNav()
{
     int sensorReading = analogRead(A1);
     int new_motorSpeed = map(sensorReading, 0, 1023, 1, 200);
     int speed_diff = new_motorSpeed-OldMotorSpeedDU_Nav;
     if ( abs(speed_diff)>1){ 
//      SendCmd(MOUNTER_MSG_DEBUG, "change motor speed: " + String(OldMotorSpeedDU) + " -> " + String(new_motorSpeed));
        OldMotorSpeedDU_Nav = new_motorSpeed;      
//      if (new_motorSpeed>0)
//      new_motorSpeed = map(new_motorSpeed, 1, 200, 1, MAXSPEED);
        SetNavSpeed( map(new_motorSpeed, 1, 200, MAXSPEED_NAV_D, MINSPEED_NAV_D) );        
     }   
}

void SetDailySpeed(int new_motorSpeed)
{ 
   if (daily_speed_RA!=new_motorSpeed){
            daily_speed_RA=new_motorSpeed;
            int led_brightness = 0;            
            led_brightness = 201 - map(daily_speed_RA, MAXSPEED_DAILY_RA_D, MINSPEED_DAILY_RA_D, 0, 200);            
            analogWrite(speedled, led_brightness);            
            SendCmd(MOUNTER_GET_DAILY_SPEED, MOTOR_RA, String(daily_speed_RA));
    } 
}

void SetNavSpeed(int new_motorSpeed)
{
    if (nav_speed!=new_motorSpeed){
          nav_speed=new_motorSpeed;
            int led_brightness = 0;            
            led_brightness = 201 - map(nav_speed, MAXSPEED_NAV_D, MINSPEED_NAV_D, 0, 200);            
            analogWrite(speedled_nav, led_brightness);            
          SendCmd(MOUNTER_GET_NAV_SPEED, String(nav_speed));            
    }       
}


void UpdatePosition(){

   if (RA_old_position!=RAStepper.fPosition){
      RA_old_position = RAStepper.fPosition;    
      
//      SendCmd(FOCUSER_GET_POSITION, MOTOR_RA, String(RAStepper.fPosition));
      SendCmd(FOCUSER_GET_POSITION, (byte)MOTOR_RA, (long)(RAStepper.fPosition));
   } 
   
   if (DE_old_position!=DEStepper.fPosition){
      DE_old_position = DEStepper.fPosition;
//      SendCmd(FOCUSER_GET_POSITION, MOTOR_DE, String(DEStepper.fPosition));
      SendCmd(FOCUSER_GET_POSITION, (byte)MOTOR_DE, (long)(DEStepper.fPosition));
   } 
}



long Roll(int motor, int dir){     

      unsigned long t1 = micros();             
      
      long res = 0;
  
      analogWrite(motorled, HIGH);  
      
      
      
      if (motor== MOTOR_RA){ 
        if (!RAStepper.fEnabled){
           RAStepper.EnablePower(true);                                    
           SendCmd(MOUNTER_SET_POWER, (byte) MOTOR_RA, (byte) RAStepper.fEnabled);
        }        
        res = RAStepper.Roll(dir);                   
      }
      else if (motor==MOTOR_DE){
        if (!DEStepper.fEnabled){
           DEStepper.EnablePower(true);                                  
           SendCmd(MOUNTER_SET_POWER, (byte) MOTOR_DE, (byte) DEStepper.fEnabled);
        }        
        res = DEStepper.Roll(dir);        
      }
    
     long t = millis();
     if (  t - LastPosCheck > PositionCheckTime){
        UpdatePosition(); 
        LastPosCheck = t;     
     }         

      analogWrite(motorled, LOW);        
      
      OpTime1 = micros()-t1;                          
      
      return res;
}



void SendCmd(byte cmd)
{
       Serial.write(FOCUSER_CMD_START);
       Serial.write(cmd);
       Serial.write(13);
       Serial.write(10);       
}

void SendCmd(byte cmd, byte par)
{
       Serial.write(FOCUSER_CMD_START);
       Serial.write(cmd);
       Serial.write(par);       
       Serial.write(13);
       Serial.write(10);       
}

void SendCmd(byte cmd, byte par, byte data)
{
       Serial.write(FOCUSER_CMD_START);
       Serial.write(cmd);
       Serial.write(par);       
       Serial.write(data);              
       Serial.write(13);
       Serial.write(10);       
}

void SendCmd(byte cmd, byte par, long data)
{
       TInt d;
       d.l = data;
       Serial.write(FOCUSER_CMD_START);
       Serial.write(cmd);
       Serial.write(par);       
       Serial.write(d.b[0]);              
       Serial.write(d.b[1]);              
       Serial.write(d.b[2]);              
       Serial.write(d.b[3]);                     
       Serial.write(13);
       Serial.write(10);       
}

void SendCmd(byte cmd, char *buf, int lng)
{
       Serial.write(FOCUSER_CMD_START);
       Serial.write(cmd);
       for (int i=0; i<lng; i++)      
          Serial.write(buf[i]);       
       Serial.write(13);
       Serial.write(10);       
}

void SendCmd(byte cmd, String str)
{
       Serial.write(FOCUSER_CMD_START);
       Serial.write(cmd);
       Serial.println(str);
}

void SendCmd(byte cmd, byte par, String str)
{
       Serial.write(FOCUSER_CMD_START);
       Serial.write(cmd);
       Serial.write(par);       
//       byte buf [3] = {FOCUSER_CMD_START, cmd, par};
//       Serial.write(buf, 3);
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
  
//  unsigned long t1 = micros();

  unsigned long tm1 = millis();  
  
  if (Serial.available()>0){

//   if (IsDebug)
//      SendCmd(MOUNTER_MSG_DEBUG, "serial event, buff length: " + String(Serial.available()));
    int command = -1;
    int cmd_end = -1;
    
    do{ 
      command = Serial.read();
      SerialBuf[BufLength] = command;
      BufLength++;
    } while (Serial.available()>0);   

//    if (IsDebug)       
//        SendCmd(MOUNTER_MSG_DEBUG, "data recieved: " + String(BufLength));
 
    while (BufLength>0){
    
        int x = FindCmdStart(SerialBuf, BufLength);   
        
//        String s="";
//        for (int i=0; i<BufLength; i++)        
//           s = s + char(SerialBuf[i]);
//         SendCmd(MOUNTER_MSG_DEBUG, s);
                   
        if (x>0){
//           if (IsDebug)
//             SendCmd(MOUNTER_MSG_DEBUG, "cmd start >0 - shift");
           BufLength = ShiftBuffer(SerialBuf, BufLength, x);
           continue;
        }
        
        if (x==-1){
           BufLength = 0;
//           if (IsDebug)
//              SendCmd(MOUNTER_MSG_DEBUG, "cmd not found");
           return;
        }       
        
        cmd_end = FindCmdEnd(SerialBuf, BufLength);          
        if (cmd_end ==-1){
//           if (IsDebug)
//              SendCmd(MOUNTER_MSG_DEBUG, "cmd end not found");
           return;
        }
                             
        String str = SketchVersion;
        command = SerialBuf[1];
        String param = "";
        for (int i=2; i<cmd_end; i++){
          param = param + char(SerialBuf[i]);
        }       
       
        if (IsDebug)
          SendCmd(MOUNTER_MSG_DEBUG, "cmd found: " + String(command));
        
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
            
          case MOUNTER_STOP:  // stop
             if  (IsRollingToNewPos==HIGH){
                IsRollingToNewPos = 0;
                SendCmd(FOCUSER_GO_TO_POSITION, "0");
             }
             if (SerialBuf[2]==MOTOR_RA){        
                 SendCmd(MOUNTER_STOP, MOTOR_RA);
                 IsRollingRA = false;         
                 RAStepper.Stop();
             } else if (SerialBuf[2]==MOTOR_DE){    
                 SendCmd(MOUNTER_STOP, MOTOR_DE);
                 IsRollingDE = false;                      
                 DEStepper.Stop();
             }
             UpdatePosition();                      
          break;             
            
         case MOUNTER_GET_POWER:
              if (SerialBuf[2]==MOTOR_RA){            
                  SendCmd(MOUNTER_GET_POWER, (byte) MOTOR_RA, (byte) RAStepper.fEnabled);
               } else if (SerialBuf[2]==MOTOR_DE){                               
                  SendCmd(MOUNTER_GET_POWER, (byte) MOTOR_DE, (byte) DEStepper.fEnabled);
               }    
              break;           
               

         case MOUNTER_SET_POWER:
            if (SerialBuf[3] == 1){
               if (SerialBuf[2]==MOTOR_RA){            
                  RAStepper.EnablePower(true);                    
                  SendCmd(MOUNTER_SET_POWER, (byte) MOTOR_RA, (byte) RAStepper.fEnabled);
                  LastActionRA = millis();                  
               } else if (SerialBuf[2]==MOTOR_DE){
                  DEStepper.EnablePower(true);                                  
                  SendCmd(MOUNTER_SET_POWER, (byte) MOTOR_DE, (byte) DEStepper.fEnabled);
                  LastActionDE = millis();                  
               }               
            } else {
                if  (IsRollingToNewPos==HIGH){
                  IsRollingToNewPos = 0;
                  SendCmd(FOCUSER_GO_TO_POSITION, "0");
                }
                if (SerialBuf[2]==MOTOR_RA){        
                  RAStepper.Stop();
                  IsRollingRA = false;
                  IsTurningRA= HOLD;                            
                  RAStepper.EnablePower(false);
                  analogWrite(turnled, LOW);     
                  SendCmd(MOUNTER_STOP, MOTOR_RA);  
                  SendCmd(MOUNTER_GET_DROTATION, (byte) MOTOR_RA, (byte) 0);                                                        
                  SendCmd(MOUNTER_SET_POWER, (byte) MOTOR_RA, (byte) RAStepper.fEnabled);
                } else if (SerialBuf[2]==MOTOR_DE){
                  DEStepper.Stop();      
                  IsRollingDE = false;        
                  IsTurningDE= HOLD;                                                        
                  DEStepper.EnablePower(false);
                  SendCmd(MOUNTER_STOP, MOTOR_DE);                    
                  SendCmd(MOUNTER_SET_POWER, (byte) MOTOR_DE, (byte) DEStepper.fEnabled);
                  SendCmd(MOUNTER_GET_DROTATION, (byte) MOTOR_DE, (byte) 0);                                                                  
                }
                UpdatePosition();                                                  
                break;               
            }
            
            
          case MOUNTER_STEP: // step right       
             if (SerialBuf[2]==MOTOR_RA){        
                 char buf[2] = {MOTOR_RA, SerialBuf[3]};
                 RAStepper.SetMicroStep(nav_micro_step);                             
                 RAStepper.Step(buf[1]);               
                 SendCmd(MOUNTER_STEP, buf, 2);
                 SendCmd(MOUNTER_SET_POWER, (byte) MOTOR_RA, (byte) RAStepper.fEnabled);
                 LastActionRA = millis();    
             } else if (SerialBuf[2]==MOTOR_DE){
                 char buf[2] = {MOTOR_DE, SerialBuf[3]};               
                 DEStepper.SetMicroStep(nav_micro_step);                             
                 DEStepper.Step(buf[1]);         
                 SendCmd(MOUNTER_STEP, buf, 2);           
                 SendCmd(MOUNTER_SET_POWER, (byte) MOTOR_DE, (byte) DEStepper.fEnabled);
                 LastActionDE = millis();    
             }
             UpdatePosition();                                   
            break;      
            
          case MOUNTER_ROLL:{ // rolling right          
             if (IsRollingToNewPos)
                break;
             int timeout = word(SerialBuf[4], SerialBuf[5]);
             if (SerialBuf[2]==MOTOR_RA){                 
                 char buf[2] = {MOTOR_RA, SerialBuf[3]};                 
                 SendCmd(MOUNTER_ROLL, buf, 2);                
                 
                 if ((IsRollingRA == buf[1])&&(!IsRollingRA))
                   break;
                 if (IsRollingRA == buf[1]){
                   IsRollingRA = HOLD;      
                   RAStepper.Stop();
                   SendCmd(MOUNTER_STOP, MOTOR_RA);
                 }                   
                 else 
                   IsRollingRA = buf[1];                                                                      
                   
                 if ((IsRollingRA)&&(timeout>0)){
                   if (IsAutoStop)
                      MotorStop();                   
                   IsAutoStop = MOTOR_RA;
                   FlexiTimer2::set(timeout, MotorStop); // MsTimer2 style is also supported
                   FlexiTimer2::start();                 
                 } else if(!IsRollingRA){
                   RAStepper.Stop();                   
                 };                
                 
                 if ((IsRollingRA)&&(IsRollingDE)){
                    IsRollingDE = HOLD;
                    DEStepper.Stop();                    
                    SendCmd(MOUNTER_STOP, MOTOR_DE);
                 }
             } else if (SerialBuf[2]==MOTOR_DE){
                 char buf[2] = {MOTOR_DE, SerialBuf[3]};
                 SendCmd(MOUNTER_ROLL, buf, 2);        
                 
                 if ((IsRollingDE == buf[1])&&(!IsRollingDE))
                   break;
                 if (IsRollingDE == buf[1]){
                   IsRollingDE = HOLD;      
                   DEStepper.Stop();
                   SendCmd(MOUNTER_STOP, MOTOR_DE);
                 }                   
                 else 
                   IsRollingDE = buf[1];                                                   
                 
                 if ((IsRollingDE)&&(timeout>0)){
                   if (IsAutoStop)
                      MotorStop();                                      
                   IsAutoStop = MOTOR_DE;
                   FlexiTimer2::set(timeout, MotorStop); // MsTimer2 style is also supported
                   FlexiTimer2::start();                 
                 } else if(!IsRollingDE)                             {
                   DEStepper.Stop();
                 }
                 if ((IsRollingRA)&&(IsRollingDE)){
                    IsRollingRA = HOLD;             
                    RAStepper.Stop();
                    SendCmd(MOUNTER_STOP, MOTOR_RA);
                 }    
             }
             UpdatePosition();                             
             
          } break;                                                  
            
          case MOUNTER_GET_NAV_SPEED: SendCmd(MOUNTER_GET_NAV_SPEED, String(nav_speed));  break;
          
          case MOUNTER_GET_DAILY_SPEED:{
                if (SerialBuf[2]==MOTOR_RA)                              
                   SendCmd(MOUNTER_GET_DAILY_SPEED, MOTOR_RA, String(daily_speed_RA)); 
                else if (SerialBuf[2]==MOTOR_DE)             
                   SendCmd(MOUNTER_GET_DAILY_SPEED, MOTOR_DE, String(daily_speed_DE));                 
          } break; 
             
          
          
          case MOUNTER_GET_DAILY_MAX_SPEED:   
            if (SerialBuf[2]==MOTOR_RA) 
                SendCmd(MOUNTER_GET_DAILY_MAX_SPEED, MOTOR_RA, String(MINSPEED_DAILY_RA_D));
            else if (SerialBuf[2]==MOTOR_DE) 
                SendCmd(MOUNTER_GET_DAILY_MAX_SPEED, MOTOR_DE, String(MINSPEED_DAILY_DE_D));                
          break; 
          
          case MOUNTER_GET_DAILY_MIN_SPEED:  
            if (SerialBuf[2]==MOTOR_RA) 
                SendCmd(MOUNTER_GET_DAILY_MIN_SPEED, MOTOR_RA, String(MAXSPEED_DAILY_RA_D));
            else if (SerialBuf[2]==MOTOR_DE) 
                SendCmd(MOUNTER_GET_DAILY_MIN_SPEED, MOTOR_DE, String(MAXSPEED_DAILY_DE_D));                
          break;                     
          
          case MOUNTER_GET_NAV_MAX_SPEED:    
                SendCmd(MOUNTER_GET_NAV_MAX_SPEED, String(MINSPEED_NAV_D));                
          break;                    

          case MOUNTER_GET_NAV_MIN_SPEED:    
                SendCmd(MOUNTER_GET_NAV_MIN_SPEED, String(MAXSPEED_NAV_D));                
          break;                              
           
          case FOCUSER_SET_MICROSTEP:
                if (SerialBuf[2]==MODE_DAILY){  //  daily ms
                   daily_micro_step = (byte)SerialBuf[3]; //param.toInt();                                   
                   SendCmd(FOCUSER_SET_MICROSTEP, (byte)MODE_DAILY, (byte)daily_micro_step);                                      
                } else if (SerialBuf[2]==MODE_NAV){ 
                   nav_micro_step = (byte)SerialBuf[3]; //param.toInt();                                     
                   SendCmd(FOCUSER_SET_MICROSTEP, (byte)MODE_NAV, (byte)nav_micro_step);                   
                }                               
              break;        
              
        case FOCUSER_GET_MICROSTEP:  
                if (SerialBuf[2]==MODE_DAILY){  //  daily ms
                   SendCmd(FOCUSER_GET_MICROSTEP, (byte)MODE_DAILY, (byte)daily_micro_step);                   
                } else if (SerialBuf[2]==MODE_NAV){ 
                   SendCmd(FOCUSER_GET_MICROSTEP, (byte)MODE_NAV, (byte)nav_micro_step);                   
                }               
              break; 
              
/*        case FOCUSER_RESET_POSITION:
                SendCmd(FOCUSER_RESET_POSITION);
                if (SerialBuf[2]==MOTOR_RA){                 
                   RAStepper.fPosition = 0;
                   SendCmd(FOCUSER_GET_POSITION, (byte)MOTOR_RA, (long)0);
                } else if (SerialBuf[2]==MOTOR_DE){
                   DEStepper.fPosition = 0;
                   SendCmd(FOCUSER_GET_POSITION, (byte)MOTOR_DE, (long)0);
                }
              break;    */
              
        case FOCUSER_GET_POSITION:
              SendCmd(FOCUSER_GET_POSITION, (byte)MOTOR_RA, (long)(RAStepper.fPosition));
              SendCmd(FOCUSER_GET_POSITION, (byte)MOTOR_DE, (long)(DEStepper.fPosition));              
              break;
              
        case FOCUSER_GO_TO_POSITION:{               
               long new_position_ra = (param.substring(0, param.indexOf(";"))).toInt();
               long new_position_de = (param.substring(param.indexOf(";")+1)).toInt();
               
               SendCmd(FOCUSER_GO_TO_POSITION, String(new_position_ra) + "," + String(new_position_de));  

               RAStepper.fRelativePosition = new_position_ra-RAStepper.fPosition;
               DEStepper.fRelativePosition = new_position_de-DEStepper.fPosition;
//               if (IsDebug) 
//                  SendCmd(MOUNTER_MSG_DEBUG, "Going to new relative position:  (" + String(RAStepper.fRelativePosition) + ";" + String(DEStepper.fRelativePosition));          
               RAStepper.fTargetPosition = new_position_ra;
               DEStepper.fTargetPosition = new_position_de;               
               IsRollingToNewPos = HIGH;
              break;
        }
        case FOCUSER_SET_MAX_POSITION:
//                max_position = param.toInt();               
//                SendCmd(FOCUSER_SET_MAX_POSITION, param ); //String(max_position));        
              break;                      

        case FOCUSER_SET_POSITION:
                 TInt d;                   
                 d.b[0] = SerialBuf[3];
                 d.b[1] = SerialBuf[4];
                 d.b[2] = SerialBuf[5];
                 d.b[3] = SerialBuf[6];                   
                 if (SerialBuf[2]==MOTOR_RA){                                                             
                   RAStepper.fPosition = d.l;                  
                   SendCmd(FOCUSER_GET_POSITION, (byte)MOTOR_RA, (long)(RAStepper.fPosition));
                } else if (SerialBuf[2]==MOTOR_DE){
                   DEStepper.fPosition = d.l;                  
//                   lcd.setCursor(0,0);
//                   lcd.print(String(d.l));
//                   lcd.setCursor(0,1);                   
//                   lcd.print(String(d.b[0]) + String(d.b[1]) + String(d.b[2]) + String(d.b[3]));
                   SendCmd(FOCUSER_GET_POSITION, (byte)MOTOR_DE, (long)(DEStepper.fPosition));
                }       
             break;        
              
        case FOCUSER_RANGE_CHECK:
               IsRangeCheck = SerialBuf[2];
               SendCmd(FOCUSER_RANGE_CHECK, SerialBuf[2]);
             break;
        case MOUNTER_DUBUG:
               IsDebug = SerialBuf[2];
               SendCmd(MOUNTER_DUBUG, SerialBuf[2]);        
             break;        
        case MOUNTER_RC:
               IsRC = SerialBuf[2];
               SendCmd(MOUNTER_RC, SerialBuf[2]);        
             break;                               
        case MOUNTER_SET_DAILY_SPEED:{  
           param = param.substring(1); 
           long new_speed = param.toInt();
           if (SerialBuf[2]==MOTOR_RA){                                          
               if (new_speed>MINSPEED_DAILY_RA_D)
                  new_speed=MINSPEED_DAILY_RA_D;
               if (new_speed<MAXSPEED_DAILY_RA_D)
                  new_speed = MAXSPEED_DAILY_RA_D;
               SetDailySpeed(new_speed);
               SendCmd(MOUNTER_GET_DAILY_SPEED, MOTOR_RA, String(daily_speed_RA));               
           } else if (SerialBuf[2]==MOTOR_DE){
               if (new_speed>MINSPEED_DAILY_DE_D)
                  new_speed=MINSPEED_DAILY_DE_D;
               if (new_speed<MAXSPEED_DAILY_DE_D)
                  new_speed = MAXSPEED_DAILY_DE_D;
               daily_speed_DE = new_speed;
               SendCmd(MOUNTER_GET_DAILY_SPEED, MOTOR_DE, String(daily_speed_DE));                            
           }
            break; }
        case MOUNTER_SET_NAV_SPEED:{
               long new_speed = param.toInt();
               if (new_speed>MINSPEED_NAV_D)
                  new_speed=MINSPEED_NAV_D;
               if (new_speed<MAXSPEED_NAV_D)
                  new_speed = MAXSPEED_NAV_D;
               SetNavSpeed(new_speed);
               SendCmd(MOUNTER_GET_NAV_SPEED, String(nav_speed));               
            break; }    
        case MOUNTER_SET_DROTATION:
           if (SerialBuf[2]==MOTOR_RA){                                                    
             if (SerialBuf[3]){
               RAStepper.SetCompensation(true);                              
               IsTurningRA = TURNING;
               analogWrite(turnled, HIGH);           
               StepCount = 0;
               StepSum = 0; 
             } else {
               RAStepper.SetCompensation(false);                              
               IsTurningRA = HOLD;
               analogWrite(turnled, LOW);           
               RAStepper.Stop();               
             }            
             SendCmd(MOUNTER_GET_DROTATION, (byte) MOTOR_RA, (byte)IsTurningRA);                       
           } else if (SerialBuf[2]==MOTOR_DE){
             if (SerialBuf[3]){
               DEStepper.SetCompensation(false);                                             
               IsTurningDE = TURNING;
             }               
             else {
               DEStepper.SetCompensation(false);                                             
               IsTurningDE = HOLD;
             }               
             SendCmd(MOUNTER_GET_DROTATION, (byte)MOTOR_DE, (byte)IsTurningDE);                                      
           }           
        break;        
        case MOUNTER_GET_DROTATION:
            if (SerialBuf[2]==MOTOR_RA) 
                SendCmd(MOUNTER_GET_DROTATION, (byte)MOTOR_RA, (byte)IsTurningRA);                       
            else if (SerialBuf[2]==MOTOR_DE) 
                SendCmd(MOUNTER_GET_DROTATION, (byte)MOTOR_DE, (byte)IsTurningDE);                                       
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
       case FBC_RTC_GET:{       
            time_t t = RTC.get();
            tmElements_t tm;
            breakTime( t, tm);            
            SendCmd(FBC_RTC_GET, String(1970 + tm.Year) + '/' + String(tm.Month) + '/' + String(tm.Day) + ' ' + String(tm.Hour) + ':' + String(tm.Minute) + ':' + String(tm.Second));
       } break;
       case FBC_RTC_SET:{                  
            tmElements_t tm;            
            param.trim();
            int y = ParseStrToInt(param, '/');
            if (y >= 1000)
                tm.Year = CalendarYrToTm(y);
            else    //(y < 100)
                tm.Year = y2kYearToTm(y);                                        
            tm.Month = ParseStrToInt(param, '/');
            tm.Day = ParseStrToInt(param, ' ');
            tm.Hour = ParseStrToInt(param, ':');
            tm.Minute = ParseStrToInt(param, ':');
            tm.Second = param.toInt();            
            time_t t = makeTime(tm);
            
            String s1 = String(1970 + tm.Year) + '/' + String(tm.Month) + '/' + String(tm.Day);
            String s2 = String(tm.Hour) + ':' + String(tm.Minute) + ':' + String(tm.Second) + ';';
            RTC.set(t);
            SendCmd(FBC_RTC_SET, s1 + ' ' + s2);            
       }break;       
       case FBC_RTC_TEMP:{
            char buffer[64];
            floatToString(buffer, RTC.temperature() / 4., 1, ',');              
//            lcd.clear();
//            lcd.setCursor(0,0);
//            TInt d;                   
//            d.f = RTC.temperature();
//            lcd.print(buffer);                       
//            SendCmd(FBC_RTC_TEMP, 0, d.l);                        
//              SendCmd(FBC_RTC_TEMP, (char*) &(d.l), 4);                        
            SendCmd(FBC_RTC_TEMP, String(buffer));            
       } break;       
       }       
       BufLength = ShiftBuffer(SerialBuf, BufLength, cmd_end+2);
    }    
  } 
 //  OpTime3 = micros()-t1;
  OpTime2 = millis()-tm1;  
  
}

int ParseStrToInt(String &str, char delimiter)
{ 
   int i = str.indexOf(delimiter);
   if (i<0) 
     i = str.length();
   String s = str.substring(0, i);
   str.remove(0, i+1);
   return s.toInt();
}


void setup() {
  
    if ( EEPROM.read(1) != 127){     
       EEPROM.write(1, 127);       
       ReleaseTime = 300;
       EEPROM.write(RELEASE_TIME_ADDRESS, highByte(ReleaseTime));              
       EEPROM.write(RELEASE_TIME_ADDRESS+1, lowByte(ReleaseTime));         
    } else        
       ReleaseTime = word(EEPROM.read(RELEASE_TIME_ADDRESS),EEPROM.read(RELEASE_TIME_ADDRESS+1));
  
    pinMode(buttonPinLeft, INPUT);
    pinMode(buttonPinRight, INPUT);
    pinMode(btnPinLeftDE, INPUT);
    pinMode(btnPinRightDE, INPUT);    
    pinMode(buttonPinRelease, INPUT);
    pinMode(buttonPinTurn, INPUT);
    pinMode(speedled, OUTPUT);
    pinMode(speedled_nav, OUTPUT);    
    pinMode(motorled, OUTPUT);
    pinMode(turnled, OUTPUT);        

    DEStepper.Init(MINSPEED_DAILY_DE_D, MAXSPEED_NAV_D);
    DEStepper.SetMicroStep(nav_micro_step);
    DEStepper.SetSpeed(1000);
    
    RAStepper.Init(MINSPEED_DAILY_RA_D, MAXSPEED_NAV_D);
    RAStepper.SetMicroStep(nav_micro_step);
    RAStepper.SetSpeed(500);
//    RAStepper.fIsDelayCompensation = true;
      
    Serial.begin(115200);
    Serial.println("Started!");

    GetMotorSpeedDaily();
    GetMotorSpeedNav();
    LastActionRA = millis();
    LastActionDE = millis();

    setTime(RTC.get());
    setSyncProvider(RTC.get);     
    setSyncInterval(60);
 
    lcd.init();                      // initialize the lcd 
    lcd.backlight();  
    lcd.print("Mounter started!");
    lcd.setCursor(0,1);
    lcd.print("Ver: " + String(SketchVersion));              
    delay(1000); 

    time_t t = RTC.get();
    tmElements_t tm;
    breakTime( t, tm);                    
   
    lcd.clear();
    lcd.setCursor(0,0);
    lcd.print(String(1970 + tm.Year) + '/' + String(tm.Month) + '/' + String(tm.Day));
    lcd.setCursor(0,1); 
    lcd.print(String(tm.Hour) + ':' + String(tm.Minute) + ':' + String(tm.Second) + ';');                    
    delay(1000);
    
    lcd.clear();    
    
    lcd.clear();
    lcd.setCursor(0,0);
    char buffer[64];
    floatToString(buffer, RTC.temperature() / 4., 1, ',');              
    lcd.print(buffer);    
    delay(1000);
    
    lcd.clear();    
 
}

void loop() {     
 
  LoopTime1 = micros(); 
  
  if (IsRC){
       
     if (CheckSpeedTime.check()){   
       GetMotorSpeedDaily();
       GetMotorSpeedNav();  
     }    
 
     buttonStateLeft = digitalRead(buttonPinLeft);  
     buttonStateRight = digitalRead(buttonPinRight);
     buttonStateTurn = digitalRead(buttonPinTurn);   
  
     btnStateLeftDE = digitalRead(btnPinLeftDE);  
     btnStateRightDE = digitalRead(btnPinRightDE);             
     
     btnPinState = digitalRead(buttonPinRelease);  
        
     
     if ((buttonStateTurn==HIGH)&&(buttonStateTurn!=buttonOldStateTurn)){
       if (IsTurningRA==HOLD){
           IsTurningRA = TURNING;
           analogWrite(turnled, 255);           
       } else {
           IsTurningRA = HOLD;
           analogWrite(turnled, LOW);           
       }
       SendCmd(MOUNTER_GET_DROTATION, (byte)MOTOR_RA, (byte)IsTurningRA);          
     }  
    
     if ((buttonStateLeft==HIGH)&&(buttonOldStateLeft==LOW)){
       IsRollingRA=STEP_BACKWARD;
     } 
     if ((buttonStateLeft==LOW)&&(buttonOldStateLeft==HIGH)){
       IsRollingRA=HOLD;    
       RAStepper.Stop();
       LastPosCheck = 0;
     }
     if ((buttonStateRight==HIGH)&&(buttonOldStateRight==LOW)){
       IsRollingRA=STEP_FORWARD;
     }
     if ((buttonStateRight==LOW)&&(buttonOldStateRight==HIGH)){
       IsRollingRA=HOLD;    
       RAStepper.Stop();       
       LastPosCheck = 0;
     }
      
    if ((btnStateLeftDE==HIGH)&&(btnOldStateLeftDE==LOW)){
      IsRollingDE=STEP_BACKWARD;
    }  
     if ((btnStateLeftDE==LOW)&&(btnOldStateLeftDE==HIGH)){
       IsRollingDE=HOLD;   
       DEStepper.Stop();       
       LastPosCheck = 0;
     }
     if ((btnStateRightDE==HIGH)&&(btnOldStateRightDE==LOW)){
       IsRollingDE=STEP_FORWARD;
     }  
     if ((btnStateRightDE==LOW)&&(btnOldStateRightDE==HIGH)){
       IsRollingDE=HOLD;   
       DEStepper.Stop();              
       LastPosCheck = 0;
     }
 } else {  
     if (digitalRead(buttonPinRelease) && digitalRead(buttonPinRight) && digitalRead(btnPinRightDE)){
         IsRC = true;
         SendCmd(MOUNTER_RC, IsRC);
     }
 }  
     
         
  if (IsRollingToNewPos==HIGH){

    RAStepper.fRelativePosition = RAStepper.fTargetPosition-RAStepper.fPosition;    
    if (abs(RAStepper.fRelativePosition)>=RAStepper.fMicroSteps[nav_micro_step]){
       RAStepper.SetSpeed(MAXSPEED_NAV_D);
       RAStepper.SetMicroStep(4);   
       if (RAStepper.fRelativePosition>0)        
            Roll(MOTOR_RA, STEP_FORWARD);
       else if (RAStepper.fRelativePosition<0)
            Roll(MOTOR_RA, STEP_BACKWARD);
    }

    DEStepper.fRelativePosition = DEStepper.fTargetPosition-DEStepper.fPosition;        
    if (abs(DEStepper.fRelativePosition)>=DEStepper.fMicroSteps[nav_micro_step]){
       DEStepper.SetSpeed(MAXSPEED_NAV_D);
       DEStepper.SetMicroStep(4);   
       if (DEStepper.fRelativePosition>0)        
            Roll(MOTOR_DE, STEP_FORWARD);
      else if (DEStepper.fRelativePosition<0)
            Roll(MOTOR_DE, STEP_BACKWARD);
    }
//    SendCmd(MOUNTER_MSG_DEBUG, "distance:" + String(RelativePosition));                           

    if ((abs(RAStepper.fRelativePosition)<RAStepper.fMicroSteps[nav_micro_step])&&(abs(DEStepper.fRelativePosition)<DEStepper.fMicroSteps[nav_micro_step])){
        RAStepper.Stop();
        RAStepper.Stop();        
        LastPosCheck = 0;
        IsRollingToNewPos=LOW;
        SendCmd(FOCUSER_GO_TO_POSITION, "0");
    }
    
 } else {
    
    if (IsRollingRA==STEP_BACKWARD){
      LastActionRA = millis();  
      RAStepper.SetSpeed(nav_speed);
      RAStepper.SetMicroStep(nav_micro_step);
//      long l = RAStepper.fLastStep;
//      String s = "f1=" + String(RAStepper.fLastStep);
      Roll(MOTOR_RA, STEP_BACKWARD);
//      if (RAStepper.fLastStep != l){
//          s += "f2=" + String(RAStepper.fLastStep);
//          SendCmd(MOUNTER_MSG_DEBUG, s);             
//      }
    } else if (IsRollingRA==STEP_FORWARD) {  
      LastActionRA = millis();    
      RAStepper.SetSpeed(nav_speed);    
      RAStepper.SetMicroStep(nav_micro_step);    
//      long l = RAStepper.fLastStep;
//      String s = "f1=" + String(RAStepper.fLastStep);
      Roll(MOTOR_RA, STEP_FORWARD);
//      if (RAStepper.fLastStep != l){
//          s += "f2=" + String(RAStepper.fLastStep);
//          SendCmd(MOUNTER_MSG_DEBUG, s);             
//      }  
    } else if ((IsTurningRA)&&(IsRollingToNewPos==false)){
      LastActionRA = millis();    
      RAStepper.SetSpeed(daily_speed_RA*MICROSTEPS[daily_micro_step]);     //  на случай изменения режима микрошага
      RAStepper.SetMicroStep(daily_micro_step);    
      long res = Roll(MOTOR_RA, STEP_FORWARD);
      long dt = (long)RAStepper.fRealStepTime - (long)RAStepper.GetSpeed();                
        if (res>0)
        {
          StepCount++;
          StepSum += dt;
          if (IsDebug)
             if (abs(dt)>20) SendCmd(MOUNTER_MSG_DEBUG, "rst=" + String(dt) + "ms=" + String(daily_micro_step) + "speed=" + String((long)RAStepper.GetSpeed()));   
//          SendCmd(MOUNTER_MSG_DEBUG, "sum rst=" + String(StepSum));   
          if (StepCount == 3000){
              StepMean = StepSum / 3000;
              if (IsDebug) 
                 SendCmd(MOUNTER_MSG_DEBUG, "mean rst=" + String(StepMean) + "sum rts=" + String(StepSum) +  "---------------------------------");   
              StepCount = 0;
              StepSum = 0;
          }
          
        }          
    } else if ( (RAStepper.fEnabled) && ( (ReleaseTime>0)||(btnPinState==HIGH) )){
      int time_diff = (millis() - LastActionRA)/1000;
      if ((time_diff>ReleaseTime)||(btnPinState==HIGH)){
        RAStepper.EnablePower(false);      
        SendCmd(MOUNTER_SET_POWER, (byte) MOTOR_RA, (byte) RAStepper.fEnabled);
      }
  }
  

    if (IsRollingDE==STEP_BACKWARD){
      analogWrite(motorled, HIGH);        
      LastActionDE = millis();    
      DEStepper.SetSpeed(nav_speed*2);    
      DEStepper.SetMicroStep(nav_micro_step);    
      Roll(MOTOR_DE, STEP_BACKWARD);        
//    DEStepper.Roll(STEP_BACKWARD);
//    DEStepper.Step(STEP_BACKWARD);
      analogWrite(motorled, LOW);       
    } else if (IsRollingDE==STEP_FORWARD) {  
      analogWrite(motorled, HIGH);            
      LastActionDE = millis();    
      DEStepper.SetSpeed(nav_speed*2);    
      DEStepper.SetMicroStep(nav_micro_step);       
      Roll(MOTOR_DE, STEP_FORWARD);        
//    DEStepper.Roll(STEP_FORWARD);
//    DEStepper.Step(STEP_FORWARD);
      analogWrite(motorled, LOW);           
    } else if ((IsTurningDE)&&(IsRollingToNewPos==false)){
      LastActionDE = millis();    
      long de_speed = daily_speed_DE*2;      
      DEStepper.SetSpeed(abs(de_speed));     
      DEStepper.SetMicroStep(daily_micro_step);          
      if (daily_speed_DE>0)
        Roll(MOTOR_DE, STEP_FORWARD);
      else if (daily_speed_DE<0)  
        Roll(MOTOR_DE, STEP_BACKWARD);
    } else if ( (DEStepper.fEnabled) && ( (ReleaseTime>0)||(btnPinState==HIGH) )){ 
      int time_diff = (millis() - LastActionDE)/1000;
      if ((time_diff>ReleaseTime)||(btnPinState==HIGH)){
        DEStepper.EnablePower(false);
        SendCmd(MOUNTER_SET_POWER, (byte) MOTOR_DE, (byte) DEStepper.fEnabled);
      }
    } 
 }

    buttonOldStateRight = buttonStateRight; 
  buttonOldStateLeft = buttonStateLeft;
  buttonOldStateTurn = buttonStateTurn;
  
  btnOldStateRightDE = btnStateRightDE; 
  btnOldStateLeftDE = btnStateLeftDE;             
   
// if (MainLoopTime > 3000){
//    SendCmd(MOUNTER_MSG_DEBUG, "lt=" + String(MainLoopTime));
// }
 
 unsigned long t = micros();  
 unsigned long dt = t - MainLoopRepeatTime;
 if (  dt > 3000){
    SendCmd(MOUNTER_MSG_DEBUG, "ltr=" + String(dt));
//   lcd.setCursor(6,0);                            
//   lcd.print(String(dt));             
 } 
 MainLoopRepeatTime = t;


// t = micros();  
 
//   lcd.clear();  

//   lcd.setCursor(0,0);                            
//   lcd.print(String(OpTime1));         
//   lcd.setCursor(6,0);                            
//   lcd.print(String(MainLoopTime));         
//   lcd.setCursor(0,1);                            
//   lcd.print(String(OpTime3));   
//   lcd.setCursor(6,1);                     
//   lcd.print(String(OpTime2)); 

//  OpTime3 = micros()-t;     
 
/* int time_diff = millis() - LastTimeCheck; 
 if (time_diff > 5){
   LastTimeCheck = millis();        
   
   lcd.clear();  
   lcd.setCursor(0,0);                            
   lcd.print(String(OpTime1));      
   lcd.setCursor(6,0);                            
   lcd.print(String(OpTime3));         
   lcd.setCursor(0,1);                            
   lcd.print(String(MainLoopTime));   
   lcd.setCursor(6,1);                     
   lcd.print(String(OpTime2)); 
   */
  

/*   time_t t_rtc = RTC.get();  
   time_t t_ard = now();  
   time_t dt = abs(t_rtc - t_ard);
   
   String s1, s2;
   if (IsTurningRA){
     s1 = String(hour(t_rtc)) + ':' + String(minute(t_rtc)) + ':' + String(second(t_rtc));               
     s2 = String(hour(t_ard)) + ':' + String(minute(t_ard)) + ':' + String(second(t_ard));                    
   } else {
     s1 = String(dt);
     s2 = "";
   }            
   
   if (timeStatus() != timeSet){
      s1 = "RTC Error!";
   }
   
   lcd.setCursor(0,0);
   lcd.print(s1);   
 
   lcd.setCursor(0,1);    
   lcd.print(s2);      */       
// }  

 LoopTime2 = micros();   
 MainLoopTime = LoopTime2-LoopTime1;   

 
}






