
// ‘айл "Focuser.pas"
// —оержит описание реализацию класса TFocuser,
// предназначенного дл€ управлени€ электрофокусером через COM-порт.
//
// јвтор - ѕопов јлексей ¬ладирович, 9141866@gmail.com
// 27.04.2014, —анкт-ѕетербург.


unit Focuser;

interface
uses windows, sysutils, messages, FConnection;


 const FOCUSER_STOP = 210;             // остановитьс€
 const FOCUSER_STEP_RIGHT = 211;       // шаг вправо
 const FOCUSER_ROLL_RIGHT = 212;       // вращение вправо с заданной скоростью
 const FOCUSER_STEP_LEFT = 209;        // шаг влево
 const FOCUSER_ROLL_LEFT = 208;        // вращение влево с заданной скоростью
 const FOCUSER_STEPPED = 203;          // нотификаци€ о том, что мотор шагнул
 const FOCUSER_ROLLING = 202;          // нотификаци€ о вращении с заданной скоростью
 const FOCUSER_GET_SPEED = 240;        // получить текущую скорость
 const FOCUSER_SET_SPEED = 241;        // установить текущую скорость
 const FOCUSER_GET_MIN_SPEED_DELAY = 239;// получить максимальную скорость
 const FOCUSER_GET_MAX_SPEED_DELAY = 238;    // получить минимальную скорость
 const FOCUSER_RELEASE = 220;          // сн€ть напр€жение с двигател€
 const FOCUSER_GET_RELAESE_TIME = 221;  // таймаут автоматического сн€ти€ напр€жение с двигател, 0 - никогда
 const FOCUSER_SET_RELAESE_TIME = 222;  // таймаут автоматического сн€ти€ напр€жение с двигател, 0 - никогда
 const FOCUSER_POWER_ON = 223;         // ѕринудительно подать питание на мотор
 const FOCUSER_SET_MICROSTEP = 231;    // установить режим микрошага
 const FOCUSER_GET_MICROSTEP = 230;    // получить режим микрошага
 const FOCUSER_GET_POSITION = 225;     // получить текущее положение
 const FOCUSER_GO_TO_POSITION = 226;   // вращатьс€ до заданной позиции
 const FOCUSER_RESET_POSITION = 227;   // сбросить текущее положение в 0
 const FOCUSER_SET_MAX_POSITION = 228; // установить максимальное положение
 const FOCUSER_SET_POSITION = 229;     // установить текущее положение
 const FOCUSER_RANGE_CHECK=215;        // вкл/выкл проверку выхода за границы по положению


// 168 - превый байт команды, #13#10 - конец комнды


 const WM_FOCUSER_SPEED = WM_APP + FOCUSER_GET_SPEED;
 const WM_FOCUSER_SET_SPEED = WM_APP + FOCUSER_SET_SPEED;
 const WM_FOCUSER_GET_MIN_SPEED = WM_APP + FOCUSER_GET_MIN_SPEED_DELAY;
 const WM_FOCUSER_GET_MAX_SPEED = WM_APP + FOCUSER_GET_MAX_SPEED_DELAY;
 const WM_FOCUSER_STOP = WM_APP + FOCUSER_STOP;
 const WM_FOCUSER_STEP_RIGHT = WM_APP + FOCUSER_STEP_RIGHT;
 const WM_FOCUSER_ROLL_RIGHT = WM_APP + FOCUSER_ROLL_RIGHT;
 const WM_FOCUSER_STEP_LEFT = WM_APP + FOCUSER_STEP_LEFT;
 const WM_FOCUSER_ROLL_LEFT = WM_APP + FOCUSER_ROLL_LEFT;
 const WM_FOCUSER_STEPPED = WM_APP + FOCUSER_STEPPED;
 const WM_FOCUSER_ROLLING = WM_APP + FOCUSER_ROLLING;
 const WM_FOCUSER_RELEASE = WM_APP + FOCUSER_RELEASE;
 const WM_FOCUSER_MICROSTEP = WM_APP + FOCUSER_GET_MICROSTEP;
 const WM_FOCUSER_GET_POSITION = WM_APP + FOCUSER_GET_POSITION;
 const WM_FOCUSER_GO_TO_POSITION = WM_APP + FOCUSER_GO_TO_POSITION;
 const WM_FOCUSER_RESET_POSITION = WM_APP + FOCUSER_RESET_POSITION;
 const WM_FOCUSER_SET_MAX_POSITION = WM_APP + FOCUSER_SET_MAX_POSITION;
 const WM_FOCUSER_RANGE_CHECK= WM_APP+FOCUSER_RANGE_CHECK;
 const WM_FOCUSER_RELEASE_TIME = WM_APP + FOCUSER_GET_RELAESE_TIME;
 const WM_FOCUSER_POWER_ON = WM_APP + FOCUSER_POWER_ON;

 const FOCUSER_MESSAGES: array[1..20] of string = (
     'Firmware version: ',  //1
     'Motor speed: ',       //2
     'Motor Stopped!',      //3
     'Stepped!',            //4
     'One step right..',    //5
     'One step left..',     //6
     'Roll left..',         //7
     'Roll right..',        //8
     'Rolling..',           //9
     'Motor released!',     //10
     '+',                     //11
     'Unknown message has been recieved: ',  //12
     'Motor max speed: ',      //13
     'Microstepping mode: ',   //14
     'Focuser position: ',     //15
     'Reset position!',        //16
     'Go to position: ',        //17
     'Debug msg:',              //18
     'Set Max pos:',             //19
     'Range check:'                 // 20
     );

 const MICROSTEPS:array [0..4] of integer = (16,8,4,2,1);

 const MICROSTEP1 = 0;
 const MICROSTEP2 = 1;
 const MICROSTEP4 = 2;
 const MICROSTEP8 = 3;
 const MICROSTEP16 = 4;
 const MICROSTEP32 = 5;


 type TFocuser = class(TFBasicCom)
    private
    protected
      fSpeed, fMaxSpeedDelay, fMinSpeedDelay:integer;
      fMicroStep:integer;
      fPosition, fMaxPosition:integer;
      fRangeCheck:boolean;
      fReleaseTime:integer;   // in seconds

      procedure ParseData(data:TCommandData; size:integer); override;

      procedure fSetSpeed(speed:integer);
      procedure fSetMicroStep(step:integer);

      procedure fSetMaxPosition(pos:integer);
      procedure fSetPosition(pos:integer);

      procedure fSetRangeCheck(check:boolean);
      procedure fSetReleaseTime(timeout:integer);

    public

      Constructor Init; override;
      function Connect(port:string; handle:HWND):integer; override;
      procedure Disconnect; override;

      property MaxSpeed:integer read fMaxSpeedDelay;
      property MinSpeed:integer read fMinSpeedDelay;
      property CurrentSpeed:integer read fSpeed write fSetSpeed;
      property MicroStep:integer read fMicroStep write fSetMicroStep;
      property Position:integer read fPosition write fSetPosition;
      property MaxPosition:integer read fMaxPosition write fSetMaxPosition;
      property RangeCheck:boolean read fRangeCheck write fSetRangeCheck;
      property ReleaseTime:integer read fReleaseTime write fSetReleaseTime;

      procedure StepLeft;
      procedure StepRight;
      Procedure RollLeft;
      Procedure RollRight;
      Procedure Stop;
      Procedure Release;
      Procedure PowerOn;
      Procedure ResetPosition;
      procedure GotoPosition(pos:integer);

 end;

 type TProcedure = procedure of object;


implementation

Constructor TFocuser.Init;
begin
   Inherited;

   fReleaseTime := 3;
   fDeviceType := FD_FOCUSER;
end;


procedure TFocuser.fSetSpeed(speed:integer);
begin
   SendData(FOCUSER_SET_SPEED, IntToStr(speed));
end;

procedure TFocuser.fSetMicroStep(step:integer);
begin
    SendData(FOCUSER_SET_MICROSTEP, IntToStr(step));
end;


function TFocuser.Connect(port:string; handle:HWND):integer;
begin

   result := Inherited Connect(port, handle);

   if (result<>0) then
       exit;

   SendData(FOCUSER_GET_MICROSTEP);  //  Get microstep mode
   SendData(FOCUSER_GET_MIN_SPEED_DELAY);   // Get motor max speed
   SendData(FOCUSER_GET_MAX_SPEED_DELAY);   // Get motor min speed
   SendData(FOCUSER_GET_RELAESE_TIME);  // Get motor release delay
   SendData(FOCUSER_GET_SPEED);   // Get motor current speed
   SendData(FOCUSER_GET_POSITION);   // Get motor current position
end;

procedure TFocuser.Disconnect;
begin
    SendData(FOCUSER_STOP);
    SendData(FOCUSER_RELEASE);

    Inherited Disconnect;
end;


procedure TFocuser.StepLeft;
begin
  SendData(FOCUSER_STEP_LEFT);
end;

procedure TFocuser.StepRight;
begin
  SendData(FOCUSER_STEP_RIGHT);
end;

Procedure TFocuser.RollLeft;
begin
  SendData(FOCUSER_ROLL_LEFT);
end;

Procedure TFocuser.RollRight;
begin
  SendData(FOCUSER_ROLL_RIGHT);
end;

Procedure TFocuser.Stop;
begin
  SendData(FOCUSER_STOP);
end;

Procedure TFocuser.Release;
begin
  SendData(FOCUSER_RELEASE);
end;

Procedure TFocuser.PowerOn;
begin
  SendData(FOCUSER_POWER_ON);
end;

procedure TFocuser.GotoPosition(pos:integer);
begin
  SendData(FOCUSER_GO_TO_POSITION, IntToStr(pos));
end;

Procedure TFocuser.ResetPosition;
begin
  SendData(FOCUSER_RESET_POSITION);
end;

procedure TFocuser.fSetReleaseTime(timeout:integer);
begin
     SendData(FOCUSER_SET_RELAESE_TIME, IntToStr(timeout));
end;

procedure TFocuser.fSetRangeCheck(check:boolean);
begin
  if (check) then
     SendData(FOCUSER_RANGE_CHECK, 1)
    else
     SendData(FOCUSER_RANGE_CHECK, 0);
end;

procedure TFocuser.fSetMaxPosition(pos:integer);
begin
  SendData(FOCUSER_SET_MAX_POSITION, IntToStr(pos));
end;

procedure TFocuser.fSetPosition(pos:integer);
begin
  SendData(FOCUSER_SET_POSITION, IntToStr(pos));
end;


procedure TFocuser.ParseData(data:TCommandData; size:integer);
begin
      case data[1] of
        FOCUSER_GET_SPEED:
          begin
            fSpeed := StrToInt(ByteArrayToStr(data, 2, size-1));
            PostMessage(ParentHandle, WM_FOCUSER_SPEED, fSpeed, 0);
            exit;
          end;
        FOCUSER_STOP:
          begin
            PostMessage(ParentHandle, WM_FOCUSER_STOP, 0, 0);
            exit;
          end;
        FOCUSER_STEP_RIGHT:
          begin
            PostMessage(ParentHandle, WM_FOCUSER_STEP_RIGHT, 0, 0);
            exit;
          end;
        FOCUSER_STEP_LEFT:
          begin
            PostMessage(ParentHandle, WM_FOCUSER_STEP_LEFT, 0, 0);
            exit;
          end;
        FOCUSER_STEPPED:
          begin
            PostMessage(ParentHandle, WM_FOCUSER_STEPPED, 0, 0);
            exit;
          end;
       FOCUSER_ROLL_RIGHT:
          begin
            PostMessage(ParentHandle, WM_FOCUSER_ROLL_RIGHT, 0, 0);
            exit;
          end;
       FOCUSER_ROLL_LEFT:
          begin
            PostMessage(ParentHandle, WM_FOCUSER_ROLL_LEFT, 0, 0);
            exit;
          end;
       FOCUSER_ROLLING:
          begin
            PostMessage(ParentHandle, WM_FOCUSER_ROLLING, 0, 0);
            exit;
          end;
       FOCUSER_RELEASE:
          begin
            PostMessage(ParentHandle, WM_FOCUSER_RELEASE, 0, 0);
            exit;
          end;
       FOCUSER_SET_SPEED: ;
//          PostMessage(ParentHandle, WM_FOCUSER_SPEED, data[2], 0);
       FOCUSER_GET_MICROSTEP:
          begin
            fMicroStep := data[2];
            PostMessage(ParentHandle, WM_FOCUSER_MICROSTEP, fMicroStep, 0);
            exit;
          end;
       FOCUSER_GET_MIN_SPEED_DELAY:
          begin
            fMinSpeedDelay := StrToInt(ByteArrayToStr(data, 2, size-1));
            PostMessage(ParentHandle, WM_FOCUSER_GET_MAX_SPEED, fMinSpeedDelay, 0);
            exit;
          end;
       FOCUSER_GET_MAX_SPEED_DELAY:
          begin
            fMaxSpeedDelay := StrToInt(ByteArrayToStr(data, 2, size-1));
            PostMessage(ParentHandle, WM_FOCUSER_GET_MIN_SPEED, fMaxSpeedDelay, 0);
            exit;
          end;
       FOCUSER_GET_POSITION:
          begin
            fPosition := StrToInt(ByteArrayToStr(data, 2, size-1));
            PostMessage(ParentHandle, WM_FOCUSER_GET_POSITION, fPosition, 0);
            exit;
          end;
       FOCUSER_SET_MAX_POSITION:
          begin
            fMaxPosition := StrToInt(ByteArrayToStr(data, 2, size-1));
            PostMessage(ParentHandle, WM_FOCUSER_SET_MAX_POSITION, 0, 0);
            exit;
          end;
       FOCUSER_RESET_POSITION:
          begin
            PostMessage(ParentHandle, WM_FOCUSER_RESET_POSITION, 0, 0);
            exit;
          end;
       FOCUSER_RANGE_CHECK:
          begin
            if (data[2]<>0) then
               fRangeCheck := true
              else
               fRangeCheck := false;
            PostMessage(ParentHandle, WM_FOCUSER_RANGE_CHECK, 0, 0);
            exit;
          end;
       FOCUSER_GO_TO_POSITION:
          begin
           PostMessage(ParentHandle, WM_FOCUSER_GO_TO_POSITION, StrToInt(ByteArrayToStr(data, 2, size-1)), 0);
           exit;
          end;
       FOCUSER_GET_RELAESE_TIME, FOCUSER_SET_RELAESE_TIME:
          begin
            fReleaseTime := StrToInt(ByteArrayToStr(data, 2, size-1));
            PostMessage(ParentHandle, WM_FOCUSER_RELEASE_TIME, fReleaseTime, 0);
            exit;
          end;
       FOCUSER_POWER_ON:
          begin
            PostMessage(ParentHandle, WM_FOCUSER_POWER_ON, 0, 0);
            exit;
          end;
      end;

     Inherited;

end;

   function FindCmdStart(data:TBuffer; size:integer):integer;
   var i:integer;
   begin
     result := -1;
     for i := 0 to size-1 do
       if data[i]=168 then
         begin
           result := i;
           break;
         end;
   end;
   procedure ShiftBuffer(var Buffer:TBuffer; size, shift:integer);
   var i:integer;
   begin
    for i := shift to size-1 do
      Buffer[i-shift] := Buffer[i];

    for i:= size-shift to size-1 do
      Buffer[i] := 0;
   end;

   function FindCmdEnd(data:TBuffer; size:integer):integer;
   var i:integer;
   begin
     result := -1;
     for i := 0 to size-2 do
       if (data[i]=13)and(data[i+1]=10) then
         begin
           result := i;
           break;
         end;
   end;

end.
