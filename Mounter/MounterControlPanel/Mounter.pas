
// Файл "Focuser.pas"
// Соержит описание реализацию класса TMounter,
// предназначенного для управления электрофокусером через COM-порт.
//
// Автор - Попов Алексей Владирович, 9141866@gmail.com
// 27.04.2014, Санкт-Петербург.


unit Mounter;

interface
uses windows, sysutils, messages, FConnection;


 const FOCUSER_RANGE_CHECK=215;

 const MOUNTER_STEP = 211;
 const MOUNTER_ROLL = 212;
 const MOUNTER_RELEASE = 220;
 const FOCUSER_GET_RELAESE_TIME = 221;  // таймаут автоматического снятия напряжение с двигател, 0 - никогда
 const FOCUSER_SET_RELAESE_TIME = 222;  // таймаут автоматического снятия напряжение с двигател, 0 - никогда
 const MOUNTER_STOP = 210;

 const MOUNTER_GET_NAV_SPEED = 240;
 const MOUNTER_GET_DAILY_SPEED = 242;
 const MOUNTER_SET_NAV_SPEED = 241;
 const MOUNTER_SET_DAILY_SPEED = 243;

 const MOUNTER_GET_NAV_MAX_SPEED = 239;
 const MOUNTER_GET_NAV_MIN_SPEED = 238;
 const MOUNTER_GET_DAILY_MAX_SPEED = 237;
 const MOUNTER_GET_DAILY_MIN_SPEED = 236;

 const MOUNTER_SET_NAV_MAX_SPEED = 235;
 const MOUNTER_SET_NAV_MIN_SPEED = 234;
 const MOUNTER_SET_DAILY_MAX_SPEED = 233;
 const MOUNTER_SET_DAILY_MIN_SPEED = 232;

 const FOCUSER_SET_MICROSTEP = 231;
 const FOCUSER_GET_MICROSTEP = 230;

 const FOCUSER_GET_POSITION = 225;
 const FOCUSER_GO_TO_POSITION = 226;
 const FOCUSER_SET_MIN_POSITION = 227;     //  Не реализовано!
 const FOCUSER_SET_MAX_POSITION = 228;
 const FOCUSER_SET_POSITION = 229;
 const FOCUSER_GET_MIN_POSITION = 223;
 const FOCUSER_GET_MAX_POSITION = 224;

 const MOUNTER_SET_DROTATION = 206;
 const MOUNTER_GET_DROTATION = 213;

//  Windows messages

 const WM_MOUNTER_NAV_SPEED = WM_APP + MOUNTER_GET_NAV_SPEED;
 const WM_MOUNTER_DAILY_SPEED = WM_APP + MOUNTER_GET_DAILY_SPEED;
 const WM_MOUNTER_SET_NAV_SPEED = WM_APP + MOUNTER_SET_NAV_SPEED;
 const WM_MOUNTER_SET_DAILY_SPEED = WM_APP + MOUNTER_SET_DAILY_SPEED;

 const WM_MOUNTER_GET_NAV_MAX_SPEED = WM_APP + MOUNTER_GET_NAV_MAX_SPEED;
 const WM_MOUNTER_GET_NAV_MIN_SPEED = WM_APP + MOUNTER_GET_NAV_MIN_SPEED;
 const WM_MOUNTER_GET_DAILY_MAX_SPEED = WM_APP + MOUNTER_GET_DAILY_MAX_SPEED;
 const WM_MOUNTER_GET_DAILY_MIN_SPEED = WM_APP + MOUNTER_GET_DAILY_MIN_SPEED;

 const WM_MOUNTER_SET_NAV_MAX_SPEED = WM_APP + MOUNTER_SET_NAV_MAX_SPEED;
 const WM_MOUNTER_SET_NAV_MIN_SPEED = WM_APP + MOUNTER_SET_NAV_MIN_SPEED;
 const WM_MOUNTER_SET_DAILY_MAX_SPEED = WM_APP + MOUNTER_SET_DAILY_MAX_SPEED;
 const WM_MOUNTER_SET_DAILY_MIN_SPEED = WM_APP + MOUNTER_SET_DAILY_MIN_SPEED;

 const WM_MOUNTER_STEP = WM_APP + MOUNTER_STEP;
 const WM_MOUNTER_ROLL = WM_APP + MOUNTER_ROLL;
 const WM_MOUNTER_RELEASE = WM_APP + MOUNTER_RELEASE;
 const WM_MOUNTER_STOP = WM_APP + MOUNTER_STOP;

 const WM_FOCUSER_CMD_KEEP_ROLLING = 207;  // not in use
 const WM_FOCUSER_MICROSTEP = WM_APP + FOCUSER_GET_MICROSTEP;
 const WM_FOCUSER_GET_POSITION = WM_APP + FOCUSER_GET_POSITION;
 const WM_FOCUSER_GO_TO_POSITION = WM_APP + FOCUSER_GO_TO_POSITION;
 const WM_FOCUSER_SET_MAX_POSITION = WM_APP + FOCUSER_SET_MAX_POSITION;
 const WM_FOCUSER_RANGE_CHECK=WM_APP+FOCUSER_RANGE_CHECK;
 const WM_FOCUSER_RELEASE_TIME = WM_APP + FOCUSER_GET_RELAESE_TIME;
 const WM_MOUNTER_DIURNAL_ROTATION = WM_APP + MOUNTER_SET_DROTATION;

 const MOUNTER_MESSAGES: array[1..20] of string = (
     'Firmware version: ',  //1
     'Motor speed: ',       //2
     'Motor Stopped!',      //3
     '---',            //4
     'One step',    //5
     '---',     //6
     '---',         //7
     'Roll ',        //8
     '-----',           //9
     'Motor released!',     //10
     '+',                     //11
     'Unknown message has been recieved: ',  //12
     'Motor max speed: ',      //13
     'Microstepping mode: ',   //14
     'Mounter position: ',     //15
     'Reset position!',        //16
     'Go to position: ',        //17
     'Debug msg:',              //18
     'Set Max pos:',             //19
     'Range check:'                 // 20
     );

{ const MICROSTEPS:array [0..5] of integer = (32,16,8,4,2,1);

 const MICROSTEP1 = 0;
 const MICROSTEP2 = 1;
 const MICROSTEP4 = 2;
 const MICROSTEP8 = 3;
 const MICROSTEP16 = 4;
 const MICROSTEP32 = 5; }

 const MOTOR_RA = 1;
 const MOTOR_DE = 2;

 const STEP_BACKWARD = -1;
 const STEP_FORWARD = 1;
 const STEP_HOLD = 0;

 const DAILY = 1;
 const NAVIGATION = 2;

// const GEAR_RATIO_RA = 192981.82;  // исходная, рассчитана по времени полного оборота вала, полученного экмпериментом с покупным часовы механизмом
// const GEAR_RATIO_RA = 187990;  // рассчитана, исходя из полученной на практике скорости часового ведения и теоретическом линейном смещении прямого восхождения секунда за секунду (что не верно на практике).
 const GEAR_RATIO_RA_DEF = 180885;  // рассчитана исходя из эксперимента - количество шагов двигателя на фактический угол поворота монтировки по оси прямого восхождения.

 const GEAR_RATIO_DE_DEF = 6000;
 const EARTH_TURN_SEC = 0.0002785383083;  //Звездная скорость в угловых часах за секунду звездного времени
// const EARTH_TURN = 86164.0905;   // звезные сутки, время полного оборота Земли в секундах


 type TMounter = class(TFBasicCom)
    private
    protected

      fNavSpeed, fNavMinSpeed, fNavMaxSpeed:integer;
      fDailySpeedRA, fDailyMinSpeedRA, fDailyMaxSpeedRA:integer;
      fDailySpeedDE, fDailyMinSpeedDE, fDailyMaxSpeedDE:integer;
      fMicroStepDaily, fMicroStepNav:integer;

      fPosRA,fPosDE :integer;
      fPosRA_EQ, fPosDE_EQ:double;
      fOffset, fPosTime:TDateTime;
      fIsTime: boolean;

      fDuirnalRotationRA:boolean;
      fDECompensastion:boolean;

      fMaxPosition:integer;
      fRangeCheck:boolean;
      fReleaseTime:integer;   // in seconds

      procedure ParseData(data:TCommandData; size:integer); override;

      procedure fSetNavSpeed(speed:double);
      procedure fSetNavMaxSpeed(speed:double);
      procedure fSetNavMinSpeed(speed:double);
      function fGetNavSpeed:double;
      function fGetNavMaxSpeed:double;
      function fGetNavMinSpeed:double;

      procedure fSetDailySpeedRA(speed:double);
      procedure fSetDailyMinSpeedRA(speed:double);
      procedure fSetDailyMaxSpeedRA(speed:double);
      function fGetDailySpeedRA:double;
      function fGetDailyMinSpeedRA:double;
      function fGetDailyMaxSpeedRA:double;

      procedure fSetDailySpeedDE(speed:double);
      procedure fSetDailyMinSpeedDE(speed:double);
      procedure fSetDailyMaxSpeedDE(speed:double);
      function fGetDailySpeedDE:double;
      function fGetDailyMinSpeedDE:double;
      function fGetDailyMaxSpeedDE:double;

      procedure fSetMicroStepNav(step:integer);
      procedure fSetMicroStepDaily(step:integer);

      procedure fSetMaxPosition(pos:integer);
      procedure fSetPosRA(pos:integer);
      procedure fSetPosDE(pos:integer);
      procedure fSetPosRA_EQ(pos:double);
      procedure fSetPosDE_EQ(pos:double);

      procedure fSetRangeCheck(check:boolean);

      procedure fDuirnalRotationSet(rotation:boolean);
      procedure fDECompensatioSet(rotation:boolean);

      procedure fSetReleaseTime(timeout:integer);

      procedure OnHandshake; virtual;

    public

      fGearRatioRA, fGearRatioDE : integer;

      Constructor Init; Override;
      function Connect(port:string; handle:HWND):integer; Override;
      procedure Disconnect; Override;

      property NavMaxSpeed:double read fGetNavMaxSpeed write fSetNavMaxSpeed;
      property NavMinSpeed:double read fGetNavMinSpeed write fSetNavMinSpeed;
      property NavCurrentSpeed:double read fGetNavSpeed write fSetNavSpeed;

      property DailyMaxSpeedRA:double read fGetDailyMaxSpeedRA write fSetDailyMaxSpeedRA;
      property DailyMinSpeedRA:double read fGetDailyMinSpeedRA write fSetDailyMinSpeedRA;
      property DailyMaxSpeedDE:double read fGetDailyMaxSpeedDE write fSetDailyMaxSpeedDE;
      property DailyMinSpeedDE:double read fGetDailyMinSpeedDE write fSetDailyMinSpeedDE;
      property DailyCurrentSpeedRA:double read fGetDailySpeedRA write fSetDailySpeedRA;
      property DailyCurrentSpeedDE:double read fGetDailySpeedDE write fSetDailySpeedDE;
      property DailySpeedRA:integer read fDailySpeedRA;
      property DailySpeedDE:integer read fDailySpeedDE;
      property NavMicroStep:integer read fMicroStepNav write fSetMicroStepNav;
      property DailyMicroStep:integer read fMicroStepDaily write fSetMicroStepDaily;
      property PosRA:integer read fPosRA write fSetPosRA;
      property PosDE:integer read fPosDE write fSetPosDE;
      property PosRA_EQ:double read fPosRA_EQ write fSetPosRA_EQ;
      property PosDE_EQ:double read fPosDE_EQ write fSetPosDE_EQ;
      property MaxPosition:integer read fMaxPosition write fSetMaxPosition;
      property DuirnalRotation: boolean read fDuirnalRotationRA write fDuirnalRotationSet;
      property DECompensation:boolean read fDECompensastion write fDECompensatioSet;

      property RangeCheck:boolean read fRangeCheck write fSetRangeCheck;
      property IsTime:boolean read fIsTime write fIsTime;
      property ReleaseTime:integer read fReleaseTime write fSetReleaseTime;

      procedure Step(motor:byte; dir:shortint);
      Procedure Roll(motor:byte; dir:shortint); overload;
      Procedure Roll(motor:byte; dir:shortint; autostop:word); overload;
      Procedure Stop(motor:byte);
      Procedure Release;
      Procedure ResetPosition;

      procedure GotoPosition(ra, de:integer);
      procedure GotoPositionEQ(ra, de:double);
      procedure UpdatePosition;


 end;

 function GetMotorName(motor:integer):string;


implementation

uses DateUtils;


function GetMotorName(motor:integer):string;
begin
  if motor=MOTOR_RA then Result := '(RA)'
  else if motor=MOTOR_DE then Result := '(DE)';
end;


Constructor TMounter.Init;
begin

  Inherited;

  fDeviceType := FD_MOUNTER;

  fMicroStepDaily := 0;
  fMicroStepNav := 0;
  fDuirnalRotationRA := false;
  fDECompensastion := false;
  fIsTime := true;
  fDailySpeedRA := 1; fDailyMinSpeedRA := 1; fDailyMaxSpeedRA:= 100;
  fDailySpeedDE := 1; fDailyMinSpeedDE := 1; fDailyMaxSpeedDE:= 100;
  fNavSpeed := 1; fNavMinSpeed := 1; fNavMaxSpeed := 100;
  fPosRA := 0; fPosDE := 0;
  fPosRA_EQ :=0; fPosDE_EQ := 0;
  fOffset := Now;
  fPosTime := 0;
  fGearRatioRA := GEAR_RATIO_RA_DEF;
  fGearRatioDE := GEAR_RATIO_DE_DEF;

end;

procedure TMounter.fSetNavSpeed(speed:double);
var sp:integer;
begin
   sp := round((1000000.0/speed)/(fGearRatioDE/3600.0));
   SendData(MOUNTER_SET_NAV_SPEED, IntToStr(sp));
end;

procedure TMounter.fSetNavMaxSpeed(speed:double);
begin
  fNavMinSpeed := round((1000000.0/speed)/(fGearRatioDE/3600.0));
end;

procedure TMounter.fSetNavMinSpeed(speed:double);
begin
  fNavMaxSpeed := round((1000000.0/speed)/(fGearRatioDE/3600.0));
end;

function TMounter.fGetNavMaxSpeed:double;
begin
   result := (1000000.0/fNavMinSpeed)/(fGearRatioDE/3600.0);
end;

function TMounter.fGetNavMinSpeed:double;
begin
   result := (1000000.0/fNavMaxSpeed)/(fGearRatioDE/3600.0);
end;

function TMounter.fGetNavSpeed:double;
begin
   result := (1000000.0/fNavSpeed)/(fGearRatioDE/3600.0);
end;

procedure TMounter.UpdatePosition;
begin
   SendData(FOCUSER_GET_POSITION);   // Get motor current position
end;

procedure TMounter.fSetReleaseTime(timeout:integer);
begin
     SendData(FOCUSER_SET_RELAESE_TIME, IntToStr(timeout));
end;

procedure TMounter.fSetDailySpeedRA(speed:double);
var sp:integer;
begin
   sp := round(((1000000.0/speed)/(fGearRatioRA/54000.0)){*MICROSTEPS[fMicroStepDaily]});
   SendData(MOUNTER_SET_DAILY_SPEED, char(MOTOR_RA) + IntToStr(sp));
end;

procedure TMounter.fSetDailyMinSpeedRA(speed:double);
begin
  fDailyMaxSpeedRA := round(((1000000.0/speed)/(fGearRatioRA/54000.0)){*MICROSTEPS[fMicroStepDaily]});
end;

procedure TMounter.fSetDailyMaxSpeedRA(speed:double);
begin
 fDailyMinSpeedRA := round(((1000000.0/speed)/(fGearRatioRA/54000.0)){*MICROSTEPS[fMicroStepDaily]});
end;

function TMounter.fGetDailySpeedRA:double;   /// тут правильно
begin
   result := ((1000000.0/fDailySpeedRA)/(fGearRatioRA/54000.0)){*MICROSTEPS[fMicroStepDaily]};
end;

function TMounter.fGetDailyMinSpeedRA:double;
begin
   result := (1000000.0/fDailyMaxSpeedRA)/(fGearRatioRA/54000.0);
end;

function TMounter.fGetDailyMaxSpeedRA:double;
begin
   result := (1000000.0/fDailyMinSpeedRA)/(fGearRatioRA/54000.0);
end;

procedure TMounter.fSetDailySpeedDE(speed:double);
var sp:integer;
begin
   if abs(speed)<0.00001 then
     sp := 1000000000
   else
     sp := round((1000000.0/speed)/(fGearRatioDE/3600.0));
   SendData(MOUNTER_SET_DAILY_SPEED, char(MOTOR_DE) + IntToStr(sp));
end;

procedure TMounter.fSetDailyMinSpeedDE(speed:double);
begin
  fDailyMaxSpeedDE := round((1000000.0/speed)/(fGearRatioDE/3600.0));
end;

procedure TMounter.fSetDailyMaxSpeedDE(speed:double);
begin
  fDailyMinSpeedDE := round((1000000.0/speed)/(fGearRatioDE/3600.0));
end;

function TMounter.fGetDailySpeedDE:double;
begin
   result := (1000000.0/fDailySpeedDE)/(fGearRatioDE/3600.0);
end;

function TMounter.fGetDailyMinSpeedDE:double;
begin
   result := (1000000.0/fDailyMaxSpeedDE)/(fGearRatioDE/3600.0);
end;

function TMounter.fGetDailyMaxSpeedDE:double;
begin
   result := (1000000.0/fDailyMinSpeedDE)/(fGearRatioDE/3600.0);
end;

procedure TMounter.fSetMicroStepDaily(step:integer);
begin
    SendData(FOCUSER_SET_MICROSTEP, byte(DAILY), byte(step));
end;

procedure TMounter.fSetMicroStepNav(step:integer);
begin
    SendData(FOCUSER_SET_MICROSTEP, byte(NAVIGATION), byte(step));
end;

procedure TMounter.fDuirnalRotationSet(rotation:boolean);
begin
  if (rotation) then
    SendData(MOUNTER_SET_DROTATION, MOTOR_RA, 1)
  else
    SendData(MOUNTER_SET_DROTATION, MOTOR_RA, 0);
end;

procedure TMounter.fDECompensatioSet(rotation:boolean);
begin
  if (rotation) then
    SendData(MOUNTER_SET_DROTATION, MOTOR_DE, 1)
  else
    SendData(MOUNTER_SET_DROTATION, MOTOR_DE, 0);
end;

function TMounter.Connect(port:string; handle:HWND):integer;
begin

   result := Inherited Connect(port, handle);

   if (result<>0) then
       exit;
end;


procedure TMounter.OnHandshake;
begin

   inherited OnHandshake;

   SendData(FOCUSER_GET_MICROSTEP, DAILY);  //  Get microstep mode
   SendData(FOCUSER_GET_MICROSTEP, NAVIGATION);  //  Get microstep mode
//   SendData(MOUNTER_GET_NAV_MAX_SPEED);   // Get motor max speed
//   SendData(MOUNTER_GET_NAV_MIN_SPEED);   // Get motor min speed
//   SendData(MOUNTER_GET_DAILY_MAX_SPEED, MOTOR_DE);   // Get motor max speed
//   SendData(MOUNTER_GET_DAILY_MIN_SPEED, MOTOR_DE);   // Get motor min speed
//   SendData(MOUNTER_GET_DAILY_MAX_SPEED, MOTOR_RA);   // Get motor max speed
//   SendData(MOUNTER_GET_DAILY_MIN_SPEED, MOTOR_RA);   // Get motor min speed
   SendData(MOUNTER_GET_NAV_SPEED);   // Get motor current speed
   SendData(MOUNTER_GET_DAILY_SPEED, MOTOR_RA);   // Get motor current speed
   SendData(MOUNTER_GET_DAILY_SPEED, MOTOR_DE);   // Get motor current speed
   SendData(FOCUSER_GET_POSITION);   // Get motor current position
   SendData(MOUNTER_GET_DROTATION, MOTOR_RA);   // Get motor current position
   SendData(MOUNTER_GET_DROTATION, MOTOR_DE);   // Get motor current position
   SendData(FOCUSER_GET_RELAESE_TIME);  // Get motor release delay

   GetTemperature;
   GetDateTime;

end;

procedure TMounter.Disconnect;
begin
    SendData(MOUNTER_STOP, MOTOR_RA);
    SendData(MOUNTER_STOP, MOTOR_DE);
    SendData(MOUNTER_RELEASE, MOTOR_RA);
    SendData(MOUNTER_RELEASE, MOTOR_DE);

    Inherited Disconnect;
end;


procedure TMounter.Step(motor:byte; dir:shortint);
begin
  SendData(MOUNTER_STEP, char(motor)+char(dir));
end;

Procedure TMounter.Roll(motor:byte; dir:shortint);
begin
  SendData(MOUNTER_ROLL, char(motor)+char(dir)+char(0)+char(0));
end;

Procedure TMounter.Roll(motor:byte; dir:shortint; autostop:word);
begin
  SendData(MOUNTER_ROLL, char(motor)+char(dir)+char(hi(autostop))+char(lo(autostop)));
end;

Procedure TMounter.Stop(motor:byte);
begin
  SendData(MOUNTER_STOP, motor);
end;

Procedure TMounter.Release;
begin
  SendData(MOUNTER_RELEASE, MOTOR_RA);
  SendData(MOUNTER_RELEASE, MOTOR_DE);
end;

procedure TMounter.GotoPosition(ra, de:integer);
begin
  SendData(FOCUSER_GO_TO_POSITION, IntToStr(ra) + ';'+IntToStr(de));
end;

procedure TMounter.GotoPositionEQ(ra: Double; de: Double);
var ra_l, de_l:integer;
begin

   if (fIsTime) then
      ra := ra - (SecondOf(Now-fOffset) + MinuteOf(Now-fOffset)*60 + HourOf(Now-fOffset)*3600 + MilliSecondOf(Now-fOffset)/1000)*EARTH_TURN_SEC;

   if (fPosRA>=0)and(ra>12) then
      ra_l := round((24-ra)*fGearRatioRA)
   else if (fPosRA>=0)and(ra<=12) then
      ra_l := -round(ra*fGearRatioRA)
   else if (fPosRA<0)and(ra>12) then
      ra_l := round((24-ra)*fGearRatioRA)
   else
      ra_l := -round(ra*fGearRatioRA);

   if (fPosDE>=0) then
     de_l := round((90-de)*fGearRatioDE)
   else
     de_l := -round((90-de)*fGearRatioDE);

   GotoPosition(ra_l, de_l);
end;

Procedure TMounter.ResetPosition;
begin
  SendData(FOCUSER_SET_POSITION, byte(MOTOR_RA), long(0));
  SendData(FOCUSER_SET_POSITION, byte(MOTOR_DE), long(0));
end;

procedure TMounter.fSetRangeCheck(check:boolean);
begin
  if (check) then
     SendData(FOCUSER_RANGE_CHECK, 1)
    else
     SendData(FOCUSER_RANGE_CHECK, 0);
end;

procedure TMounter.fSetMaxPosition(pos:integer);
begin
  SendData(FOCUSER_SET_MAX_POSITION, IntToStr(pos));
end;

procedure TMounter.fSetPosRA(pos:integer);
begin
//  SendData(FOCUSER_SET_POSITION, char(MOTOR_RA) + IntToStr(pos));
  SendData(FOCUSER_SET_POSITION, byte(MOTOR_RA), pos);
end;

procedure TMounter.fSetPosDE(pos:integer);
begin
  SendData(FOCUSER_SET_POSITION, byte(MOTOR_DE), pos);
end;

procedure TMounter.fSetPosRA_EQ(pos:double);
var ra_l:integer;
begin
   if abs(pos)>24 then
      exit;

   if (pos>12) then
     pos := pos-24;
   ra_l := -round(pos*fGearRatioRA);

   fOffset := Now;

   PosRA := ra_l;
end;

procedure TMounter.fSetPosDE_EQ(pos:double);
var de_l:integer;
begin
  if abs(pos)>90 then
   exit;

  if (fPosDE>=0) then
     de_l := round((90-pos)*fGearRatioDE)
  else
     de_l := -round((90-pos)*fGearRatioDE);

  PosDE := de_l;

end;

procedure TMounter.ParseData(data:TCommandData; size:integer);
var s:string;
    a:double;
    data_b:TInteger;
begin
     try
      case data[1] of
        FBC_HANDSHAKE:
          begin
            if (data[2] = fDeviceType) then
              begin
                PostMessage(ParentHandle, WM_DEVICE_HANDSHAKE, 1, data[2]);
                OnHandshake;
              end
            else
               PostMessage(ParentHandle, WM_DEVICE_HANDSHAKE, 0, data[2]);
            exit;
          end;
        MOUNTER_GET_NAV_SPEED:
          begin
            fNavSpeed := StrToInt(ByteArrayToStr(data, 2, size-1));
            PostMessage(ParentHandle, WM_MOUNTER_NAV_SPEED, fNavSpeed, 0);
            exit;
          end;
        MOUNTER_GET_DAILY_SPEED:
          begin
            if data[2]=MOTOR_RA then
              fDailySpeedRA := StrToInt(ByteArrayToStr(data, 3, size-1))
            else if data[2]=MOTOR_DE then
              fDailySpeedDE := StrToInt(ByteArrayToStr(data, 3, size-1));
            PostMessage(ParentHandle, WM_MOUNTER_DAILY_SPEED, data[2], 0);
            exit;
          end;
        MOUNTER_STOP:
          begin
            PostMessage(ParentHandle, WM_MOUNTER_STOP, data[2], 0);
            exit;
          end;
        MOUNTER_STEP:
          begin
            PostMessage(ParentHandle, WM_MOUNTER_STEP, data[2], data[3]);
            exit;
          end;
       MOUNTER_ROLL:
          begin
            PostMessage(ParentHandle, WM_MOUNTER_ROLL, data[2], data[3]);
            exit;
          end;
       MOUNTER_RELEASE:
          begin
            PostMessage(ParentHandle, WM_MOUNTER_RELEASE, data[2], 0);
            exit;
          end;
       FOCUSER_GET_MICROSTEP, FOCUSER_SET_MICROSTEP:
          begin
            if data[2]=DAILY then
              fMicroStepDaily := data[3]
            else if data[2]=NAVIGATION then
              fMicroStepNav := data[3];
            PostMessage(ParentHandle, WM_FOCUSER_MICROSTEP, byte(data[2]), 0);
            exit;
          end;
       MOUNTER_GET_NAV_MAX_SPEED:
          begin
            fNavMaxSpeed := StrToInt(ByteArrayToStr(data, 2, size-1));
            PostMessage(ParentHandle, WM_MOUNTER_GET_NAV_MAX_SPEED, 0, 0);
            exit;
          end;
       MOUNTER_GET_DAILY_MAX_SPEED:
          begin
            if data[2]=MOTOR_RA then
               fDailyMaxSpeedRA := StrToInt(ByteArrayToStr(data, 3, size-1))
            else if data[2]=MOTOR_DE then
               fDailyMaxSpeedDE := StrToInt(ByteArrayToStr(data, 3, size-1));
            PostMessage(ParentHandle, WM_MOUNTER_GET_DAILY_MAX_SPEED, data[2], 0);
            exit;
          end;
       MOUNTER_GET_NAV_MIN_SPEED:
          begin
            fNavMinSpeed := StrToInt(ByteArrayToStr(data, 2, size-1));
            PostMessage(ParentHandle, WM_MOUNTER_GET_NAV_MIN_SPEED, 0, 0);
            exit;
          end;
       MOUNTER_GET_DAILY_MIN_SPEED:
          begin
            if data[2]=MOTOR_RA then
               fDailyMinSpeedRA := StrToInt(ByteArrayToStr(data, 3, size-1))
            else if data[2]=MOTOR_DE then
               fDailyMinSpeedDE := StrToInt(ByteArrayToStr(data, 3, size-1));
            PostMessage(ParentHandle, WM_MOUNTER_GET_DAILY_MIN_SPEED, data[2], 0);
            exit;
          end;
       FOCUSER_GET_POSITION:
          begin
//            s := ByteArrayToStr(data, 3, size-1);
//            if length(s)=0 then
//              exit;
            data_b.b1 := data[3];
            data_b.b2 := data[4];
            data_b.b3 := data[5];
            data_b.b4 := data[6];
            if data[2]=MOTOR_RA then
              begin
               fPosRA := data_b.int;  //StrToInt(s);
               fPosRA_EQ := -fPosRA/fGearRatioRA;
               if (fIsTime) then
                 begin
                   fPosTime := Now;
                   a := (SecondOf(Now-fOffset) + MinuteOf(Now-fOffset)*60 + HourOf(Now-fOffset)*3600 + MilliSecondOf(Now-fOffset)/1000)*EARTH_TURN_SEC;
                   fPosRA_EQ := fPosRA_EQ + a;
                 end;
               if abs(fPosRA_EQ)>24 then
                   fPosRA_EQ := fPosRA_EQ - trunc(fPosRA_EQ/24)*24;
               if fPosRA_EQ<0 then
                 fPosRA_EQ := fPosRA_EQ + 24;
              end
            else if data[2]=MOTOR_DE then
              begin
               fPosDE := data_b.int; //StrToInt(s);
               fPosDE_EQ := 90 - abs(fPosDE)/fGearRatioDE;
              end;
            PostMessage(ParentHandle, WM_FOCUSER_GET_POSITION, data[2], 0);
            exit;
          end;
       FOCUSER_SET_MAX_POSITION:
          begin
            fMaxPosition := StrToInt(ByteArrayToStr(data, 3, size-1));   //   возможно ошибка в индексе, не 3 а 2
            PostMessage(ParentHandle, WM_FOCUSER_SET_MAX_POSITION, 0, 0);
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
       MOUNTER_SET_DROTATION, MOUNTER_GET_DROTATION:
          begin
            if (data[2]=MOTOR_RA) then
             begin
              if (data[3]<>0) then
                 fDuirnalRotationRA := true
                else
                 fDuirnalRotationRA := false;
              PostMessage(ParentHandle, WM_MOUNTER_DIURNAL_ROTATION, MOTOR_RA, 0);
              exit;
             end
            else if (data[2]=MOTOR_DE) then
             begin
              if (data[3]<>0) then
                 fDECompensastion := true
                else
                 fDECompensastion := false;
              PostMessage(ParentHandle, WM_MOUNTER_DIURNAL_ROTATION, MOTOR_DE, 0);
              exit;
             end
          end;
       FOCUSER_GO_TO_POSITION:
          begin
           s := ByteArrayToStr(data, 3, size-1);
           PostMessage(ParentHandle, WM_FOCUSER_GO_TO_POSITION, 0, 0);
           exit;
          end;
       FOCUSER_GET_RELAESE_TIME, FOCUSER_SET_RELAESE_TIME:
          begin
            fReleaseTime := StrToInt(ByteArrayToStr(data, 2, size-1));
            PostMessage(ParentHandle, WM_FOCUSER_RELEASE_TIME, fReleaseTime, 0);
            exit;
          end;
      end;
     except
       On EConvertError do
         begin
           fAddLastDeviceDbgMsg('Convert error!  Cmd:' + IntToStr(data[1]) + ' ' + s);
           PostMessage(ParentHandle, WM_DEVICE_DEBUG_MSG, 1, 0);
           exit;
         end;
     end;

     Inherited;
end;


end.
