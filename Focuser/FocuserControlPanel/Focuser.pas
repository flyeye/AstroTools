
// Файл "Focuser.pas"
// Соержит описание реализацию класса TFocuser,
// предназначенного для управления электрофокусером через COM-порт.
//
// Автор - Попов Алексей Владирович, 9141866@gmail.com
// 27.04.2014, Санкт-Петербург.


unit Focuser;

interface
uses windows, sysutils, messages;

 type TFocuserData = array [0..256] of byte;
      TBuffer = array [0..1024] of byte;

 const FOCUSER_STOP = 210;             // остановиться
 const FOCUSER_STEP_RIGHT = 211;       // шаг вправо
 const FOCUSER_ROLL_RIGHT = 212;       // вращение вправо с заданной скоростью
 const FOCUSER_STEP_LEFT = 209;        // шаг влево
 const FOCUSER_ROLL_LEFT = 208;        // вращение влево с заданной скоростью
 const FOCUSER_STEPPED = 253;          // нотификация о том, что мотор шагнул
 const FOCUSER_ROLLING = 252;          // нотификация о вращении с заданной скоростью
 const FOCUSER_PING = 255;             // проверка связи
 const FOCUSER_HANDSHAKE = 254;        // установка связи
 const FOCUSER_GET_SPEED = 240;        // получить текущую скорость
 const FOCUSER_SET_SPEED = 241;        // установить текущую скорость
 const FOCUSER_GET_MAX_SPEED = 239;    // получить максимальную скорость
 const FOCUSER_GET_MIN_SPEED = 238;    // получить минимальную скорость
 const FOCUSER_RELEASE = 220;          // снять напряжение с двигателя
 const FOCUSER_SET_MICROSTEP = 231;    // установить режим микрошага
 const FOCUSER_GET_MICROSTEP = 230;    // получить режим микрошага
 const FOCUSER_GET_POSITION = 225;     // получить текущее положение
 const FOCUSER_GO_TO_POSITION = 226;   // вращаться до заданной позиции
 const FOCUSER_RESET_POSITION = 227;   // сбросить текущее положение в 0
 const FOCUSER_SET_MAX_POSITION = 228; // установить максимальное положение
 const FOCUSER_SET_POSITION = 229;     // установить текущее положение
 const FOCUSER_CMD_DEBUG = 251;        // отладочное сообщение
 const FOCUSER_DEBUG = 250;            // вкл/выкл отладочный режим
 const FOCUSER_RANGE_CHECK=215;        // вкл/выкл проверку выхода за границы по положению

// 168 - превый байт команды, #13#10 - конец комнды


 const WM_FOCUSER_SPEED = WM_APP + FOCUSER_GET_SPEED;
 const WM_FOCUSER_SET_SPEED = WM_APP + FOCUSER_SET_SPEED;
 const WM_FOCUSER_GET_MAX_SPEED = WM_APP + FOCUSER_GET_MAX_SPEED;
 const WM_FOCUSER_GET_MIN_SPEED = WM_APP + FOCUSER_GET_MIN_SPEED;
 const WM_FOCUSER_STOP = WM_APP + FOCUSER_STOP;
 const WM_FOCUSER_STEP_RIGHT = WM_APP + FOCUSER_STEP_RIGHT;
 const WM_FOCUSER_ROLL_RIGHT = WM_APP + FOCUSER_ROLL_RIGHT;
 const WM_FOCUSER_STEP_LEFT = WM_APP + FOCUSER_STEP_LEFT;
 const WM_FOCUSER_ROLL_LEFT = WM_APP + FOCUSER_ROLL_LEFT;
 const WM_FOCUSER_STEPPED = WM_APP + FOCUSER_STEPPED;
 const WM_FOCUSER_ROLLING = WM_APP + FOCUSER_ROLLING;
 const WM_FOCUSER_PING = WM_APP + FOCUSER_PING;
 const WM_FOCUSER_HANDSHAKE = WM_APP + FOCUSER_HANDSHAKE;
 const WM_FOCUSER_RELEASE = WM_APP + FOCUSER_RELEASE;
 const WM_FOCUSER_UNKNOWN_MESSAGE = WM_APP + 256;
 const WM_FOCUSER_MICROSTEP = WM_APP + FOCUSER_GET_MICROSTEP;
 const WM_FOCUSER_GET_POSITION = WM_APP + FOCUSER_GET_POSITION;
 const WM_FOCUSER_GO_TO_POSITION = WM_APP + FOCUSER_GO_TO_POSITION;
 const WM_FOCUSER_RESET_POSITION = WM_APP + FOCUSER_RESET_POSITION;
 const WM_FOCUSER_CMD_DEBUG = WM_APP + FOCUSER_CMD_DEBUG;
 const WM_FOCUSER_SET_MAX_POSITION = WM_APP + FOCUSER_SET_MAX_POSITION;
 const WM_FOCUSER_RANGE_CHECK=WM_APP+FOCUSER_RANGE_CHECK;
 const WM_FOCUSER_DEBUG = WM_APP + FOCUSER_DEBUG;


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


 type TFocuser = class(TObject)
    private
    protected
      CommHandle : integer;
      DCB : TDCB;
      Stat : TComStat;
      CommThread : THandle;
      Ovr : TOverlapped;
      ParentHandle:HWND;
      ThreadID, fError:Dword;
      fSpeed, fMaxSpeed, fMinSpeed:integer;
      fMicroStep:integer;
      fPosition, fMaxPosition:integer;
      fFocuserDbgMsg:array of string;
      fRangeCheck:boolean;
      buff:TBuffer;
      prevsize:DWORD;

      fIsDebug:boolean;

      fFocuserVersion, fUnknownMessage:string;

      class function ThreadProc(Param: Pointer): DWord; stdcall; static;

      function GetFocuserData:integer;
      procedure ParseData(data:TFocuserData; size:integer);

      procedure fSetSpeed(speed:integer);
      procedure fSetMicroStep(step:integer);

      procedure fSetMaxPosition(pos:integer);
      procedure fSetPosition(pos:integer);

      procedure fSetRangeCheck(check:boolean);

      procedure SendData(cmd:byte; data:byte); overload;
      procedure SendData(cmd:byte; data:string); overload;

      function fGetLastFocuserDbgMsg:string;
      procedure fAddLastFocuserDbgMsg(str:string);

      procedure fSetDebug(debug:boolean);

    public

      Constructor Init;

      function Connect(port:string; handle:HWND):integer;
      procedure Disconnect;

      property MaxSpeed:integer read fMaxSpeed;
      property MinSpeed:integer read fMinSpeed;
      property CurrentSpeed:integer read fSpeed write fSetSpeed;
      property UnknownMessage:string read fUnknownMessage;
      property LastDebugMessage:string read fGetLastFocuserDbgMsg;
      property Version:string read fFocuserVersion;
      property LastError:DWORD read fError;
      property MicroStep:integer read fMicroStep write fSetMicroStep;
      property Position:integer read fPosition write fSetPosition;
      property MaxPosition:integer read fMaxPosition write fSetMaxPosition;
      property RangeCheck:boolean read fRangeCheck write fSetRangeCheck;
      property IsDebugging:boolean read fIsDebug write fSetDebug;

      procedure StepLeft;
      procedure StepRight;
      Procedure RollLeft;
      Procedure RollRight;
      Procedure Stop;
      Procedure Release;
      Procedure Ping;
      Procedure ResetPosition;
      procedure GotoPosition(pos:integer);

      procedure SendData(cmd:byte); overload;
 end;

 type TProcedure = procedure of object;


implementation

Constructor TFocuser.Init;
begin
  fFocuserVersion := '';
  ParentHandle := 0;
  fUnknownMessage := '';
  fMicroStep := 0;
  CommHandle := 0;
end;


function TFocuser.fGetLastFocuserDbgMsg:string;
var i:integer;
begin
   result := fFocuserDbgMsg[0];
   for i := 0 to Length(fFocuserDbgMsg)-2 do
      fFocuserDbgMsg[i]:= fFocuserDbgMsg[i+1];
   Setlength(fFocuserDbgMsg, Length(fFocuserDbgMsg)-1);
end;

procedure TFocuser.fAddLastFocuserDbgMsg(str:string);
var i:integer;
begin
   Setlength(fFocuserDbgMsg, Length(fFocuserDbgMsg)+1);
   fFocuserDbgMsg[Length(fFocuserDbgMsg)-1] := str;
end;

class function TFocuser.ThreadProc(Param: Pointer): DWord;
begin
  result := TFocuser(Param).GetFocuserData;
end;

procedure TFocuser.fSetSpeed(speed:integer);
begin
   SendData(FOCUSER_SET_SPEED, IntToStr(speed));
end;

procedure TFocuser.fSetMicroStep(step:integer);
begin
    SendData(FOCUSER_SET_MICROSTEP, IntToStr(step));
end;

procedure TFocuser.fSetDebug(debug:boolean);
begin
  if (debug) then
     SendData(FOCUSER_DEBUG, 1)
    else
     SendData(FOCUSER_DEBUG, 0);
end;

function TFocuser.Connect(port:string; handle:HWND):integer;
var
   str:string;
   i: Integer;
  begin

   for i := 0 to sizeof(Buff)+1 do
      Buff[i] := 0;
   PrevSize:=0;


   ParentHandle := handle;
   result := 0;
   str := '//./' + port;
   CommHandle := CreateFile(PChar(str),GENERIC_READ or GENERIC_WRITE,0,nil,
         OPEN_EXISTING, FILE_FLAG_OVERLAPPED,0);

   if (CommHandle=-1) then
    begin
      result := -1;
      fError := GetLastError;
      exit;
    end;

   SetCommMask(CommHandle,EV_RXFLAG);

   GetCommState(CommHandle,DCB);
   DCB.BaudRate:=CBR_9600;
   DCB.Parity:=NOPARITY;
   DCB.ByteSize:=8;
   DCB.StopBits:=OneStopBit;
   DCB.EvtChar:=chr(10);
   SetCommState(CommHandle,DCB);

   CommThread := CreateThread(nil,0,@ThreadProc, Self,0,ThreadID);

   SendData(FOCUSER_HANDSHAKE);   // FirmwareVersion request
   SendData(FOCUSER_GET_MICROSTEP);  //  Get microstep mode
   SendData(FOCUSER_GET_MAX_SPEED);   // Get motor max speed
   SendData(FOCUSER_GET_MIN_SPEED);   // Get motor min speed
   SendData(FOCUSER_GET_SPEED);   // Get motor current speed
   SendData(FOCUSER_GET_POSITION);   // Get motor current position
end;

procedure TFocuser.Disconnect;
begin
    SendData(FOCUSER_STOP);
    SendData(FOCUSER_RELEASE);
    TerminateThread(CommThread,0);
    CloseHandle(CommHandle);
end;

procedure TFocuser.SendData(cmd:byte);
var
  Transmit:array [0..255] of byte;
  sendsize:DWORD;
begin
   if (CommHandle=0) then exit;
   sendsize:=4;
   Transmit[0]:=168;
   Transmit[1]:=cmd;
   Transmit[2]:=13;
   Transmit[3]:=10;
   WriteFile(CommHandle,Transmit,sendsize,sendsize,@Ovr);
end;

procedure TFocuser.SendData(cmd:byte; data:byte);
var
  Transmit:array [0..255] of byte;
  sendsize:DWORD;
begin
   if (CommHandle=0) then exit;
   sendsize:=5;
   Transmit[0]:=168;
   Transmit[1]:=cmd;
   Transmit[2]:=data;
   Transmit[3]:=13;
   Transmit[4]:=10;
   WriteFile(CommHandle,Transmit,sendsize,sendsize,@Ovr);
end;


procedure TFocuser.SendData(cmd:byte; data:string);
var
  Transmit:array [0..255] of byte;
  sendsize:DWORD;
  i:integer;
begin
   if (CommHandle=0) then exit;
   for i:=0 to length(Transmit) do
       Transmit[i] := 0;
   Transmit[0]:=168;
   Transmit[1]:=cmd;

   for i:=1 to length(data) do
     Transmit[i+1]:=byte(data[i]);
   Transmit[i+1]:=13;
   Transmit[i+2]:=10;
   sendsize:= length(data) + 4;
   WriteFile(CommHandle,Transmit,sendsize,sendsize,@Ovr);
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

procedure TFocuser.GotoPosition(pos:integer);
begin
  SendData(FOCUSER_GO_TO_POSITION, IntToStr(pos));
end;

Procedure TFocuser.ResetPosition;
begin
  SendData(FOCUSER_RESET_POSITION);
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

Procedure TFocuser.Ping;
begin
   SendData(FOCUSER_PING);
end;

procedure TFocuser.ParseData(data:TFocuserData; size:integer);
var i:integer;
    s:string;
begin
      case data[1] of
        FOCUSER_HANDSHAKE:
          begin
            fFocuserVersion := '';
            for i := 2 to 4 do
              fFocuserVersion := fFocuserVersion + char(data[i]);
            PostMessage(ParentHandle, WM_FOCUSER_HANDSHAKE, 0, 0);
          end;
        FOCUSER_CMD_DEBUG:
          begin
            s := '';
            for i := 2 to size-1 do
              s := S + char(data[i]);
            fAddLastFocuserDbgMsg(s);
            PostMessage(ParentHandle, WM_FOCUSER_CMD_DEBUG, 0, 0);
          end;
        FOCUSER_GET_SPEED:
          begin
            s := '';
            for i := 3 to size do
              s := s + char(data[i-1]);
            fSpeed := StrToInt(s);
            PostMessage(ParentHandle, WM_FOCUSER_SPEED, fSpeed, 0);
          end;
        FOCUSER_STOP:
          PostMessage(ParentHandle, WM_FOCUSER_STOP, 0, 0);
        FOCUSER_STEP_RIGHT:
          PostMessage(ParentHandle, WM_FOCUSER_STEP_RIGHT, 0, 0);
        FOCUSER_STEP_LEFT:
          PostMessage(ParentHandle, WM_FOCUSER_STEP_LEFT, 0, 0);
        FOCUSER_STEPPED:
          PostMessage(ParentHandle, WM_FOCUSER_STEPPED, 0, 0);
       FOCUSER_ROLL_RIGHT:
          PostMessage(ParentHandle, WM_FOCUSER_ROLL_RIGHT, 0, 0);
       FOCUSER_ROLL_LEFT:
          PostMessage(ParentHandle, WM_FOCUSER_ROLL_LEFT, 0, 0);
       FOCUSER_ROLLING:
          PostMessage(ParentHandle, WM_FOCUSER_ROLLING, 0, 0);
       FOCUSER_RELEASE:
          PostMessage(ParentHandle, WM_FOCUSER_RELEASE, 0, 0);
       FOCUSER_PING:
          PostMessage(ParentHandle, WM_FOCUSER_PING, 0, 0);
       FOCUSER_SET_SPEED: ;
//          PostMessage(ParentHandle, WM_FOCUSER_SPEED, data[2], 0);
       FOCUSER_GET_MICROSTEP:
          begin
            fMicroStep := data[2];
            PostMessage(ParentHandle, WM_FOCUSER_MICROSTEP, fMicroStep, 0);
          end;
       FOCUSER_GET_MAX_SPEED:
          begin
            s := '';
            for i := 3 to size do
              s := s + char(data[i-1]);
            fMaxSpeed := StrToInt(s);
            PostMessage(ParentHandle, WM_FOCUSER_GET_MAX_SPEED, fMaxSpeed, 0);
          end;
       FOCUSER_GET_MIN_SPEED:
          begin
            s := '';
            for i := 3 to size do
              s := s + char(data[i-1]);
            fMinSpeed := StrToInt(s);
            PostMessage(ParentHandle, WM_FOCUSER_GET_MIN_SPEED, 0, 0);
          end;
       FOCUSER_GET_POSITION:
          begin
            s := '';
            for i := 3 to size do
              s := s + char(data[i-1]);
            fPosition := StrToInt(s);
            PostMessage(ParentHandle, WM_FOCUSER_GET_POSITION, fPosition, 0);
          end;
       FOCUSER_SET_MAX_POSITION:
          begin
            s := '';
            for i := 3 to size do
              s := s + char(data[i-1]);
            fMaxPosition := StrToInt(s);
            PostMessage(ParentHandle, WM_FOCUSER_SET_MAX_POSITION, 0, 0);
          end;
       FOCUSER_RESET_POSITION:
          begin
            PostMessage(ParentHandle, WM_FOCUSER_RESET_POSITION, 0, 0);
          end;
       FOCUSER_RANGE_CHECK:
          begin
            if (data[2]<>0) then
               fRangeCheck := true
              else
               fRangeCheck := false;
            PostMessage(ParentHandle, WM_FOCUSER_RANGE_CHECK, 0, 0);
          end;
       FOCUSER_GO_TO_POSITION:
          begin
            s := '';
            for i := 3 to size do
              s := s + char(data[i-1]);
           PostMessage(ParentHandle, WM_FOCUSER_GO_TO_POSITION, StrToInt(s), 0);
          end;
       FOCUSER_DEBUG:
          begin
            if data[2]<>0 then
                 fIsDebug := true
               else
                 fIsDebug := false;
            PostMessage(ParentHandle, WM_FOCUSER_DEBUG, data[2], 0);
          end
        else
         begin
           fUnknownMessage := '';
           for i := 1 to size do
             fUnknownMessage := fUnknownMessage + char(data[i-1]);
           PostMessage(ParentHandle, WM_FOCUSER_UNKNOWN_MESSAGE, 0, 0);
         end;
      end;
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

function TFocuser.GetFocuserData:integer;
var
   data:TBuffer;
   i, pos: Integer;
   readsize:DWORD;
   TransMask: DWord;
   Errs : DWord;
   cmd:TFocuserData;
 begin
   readsize := PrevSize;

   while true do
   begin
    TransMask:=0;
    WaitCommEvent(CommHandle,TransMask,@Ovr);
    if ((TransMask and EV_RXFLAG)=EV_RXFLAG) or
       ((TransMask and EV_RXCHAR)=EV_RXCHAR) then
     begin
      ClearCommError(CommHandle,Errs,@Stat);
      prevsize := readsize;
      readsize := Stat.cbInQue;
      for i := 0 to sizeof(buff)+1 do
         data[i]:=0;
      ReadFile(CommHandle,data,readsize,readsize,@Ovr);
      for i := 0 to readsize do
        buff[prevsize+i] := data[i];
      readsize := readsize+prevsize;

      while readsize<>0 do
      begin
        pos := FindCmdStart(Buff, readsize);
        if pos=-1 then
          begin
            readsize:=0;
            for i := 0 to sizeof(buff)+1 do
              buff[i]:=0;
            break;
          end;
        if pos>0 then
          begin
            ShiftBuffer(Buff, readsize, pos);
            readsize:=readsize-pos;
          end;

        pos := FindCmdEnd(Buff, readsize);
        if pos=-1 then
           break;
        for i := 0 to length(cmd) do
          cmd[i] := 0;
        for i := 0 to pos-1 do
          cmd[i] := Buff[i];
        ParseData(cmd, pos);
        ShiftBuffer(Buff, readsize, pos+2);
        readsize := readsize-(pos+2);
      end;

     end;
    end;
end;


end.
