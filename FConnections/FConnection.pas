unit FConnection;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes;

type TCommandData = array [0..256] of byte;
     TBuffer = array [0..1024] of byte;

//  FBC Protocol
// 168 - превый байт команды, #13#10 - конец комнды

 const FBC_CMD_DEBUG = 205;

 const FBC_DEBUG = 251;
 const FBC_RCP = 252;  // Remote Control Panel
 const FBC_GET_FIRMWARE_VERSION = 253;
 const FBC_HANDSHAKE = 254;
 const FBC_PING = 255;

//  Windows mrssages
 const WM_DEVICE_PING = WM_APP + FBC_PING;
 const WM_DEVICE_HANDSHAKE = WM_APP + FBC_HANDSHAKE;
 const WM_DEVICE_UNKNOWN_MESSAGE = WM_APP + 256;
 const WM_DEVICE_CMD_DEBUG = WM_APP + FBC_CMD_DEBUG;
 const WM_DEVICE_DEBUG = WM_APP + FBC_DEBUG;
 const WM_DEVICE_RCP = WM_APP + FBC_RCP;
 const WM_FIRMWARE_VERSION = WM_APP + FBC_GET_FIRMWARE_VERSION;

//  Devices

 const FD_NONE = 100;
 const FD_FOCUSER = 101;
 const FD_MOUNTER = 102;
 const FD_GROWER = 103;


//  Basic communication class
 type TFBasicCom = class(TObject)
   private
   protected
      CommHandle : integer;
      DCB : TDCB;
      Stat : TComStat;
      CommThread : THandle;
      Ovr : TOverlapped;
      ParentHandle:HWND;
      ThreadID, fError:Dword;

      fBuff:TBuffer;
      fPrevSize:DWORD;

      fIsDebug:boolean;
      fIsRCP_ON:boolean;  //  блокировка пульта ДУ, false - заблокировано

      fDeviceVersion, fUnknownMessage:string;
      fDeviceDbgMsg:array of string;
      fDeviceType:integer;

      procedure SendData(cmd:byte; data:byte); overload;
      procedure SendData(cmd:byte; par:byte; data:byte); overload;
      procedure SendData(cmd:byte; data:string); overload;
      procedure SendData(cmd:byte); overload;

      class function ThreadProc(Param: Pointer): DWord; stdcall; static;
      function GeTCommandData:integer;
      procedure ParseData(data:TCommandData; size:integer); virtual;

      function fGetLastDeviceDbgMsg:string;
      procedure fAddLastDeviceDbgMsg(str:string);

      procedure fSetIsDebug(debug:boolean);
      procedure fSetRCP(status:boolean);

   public
      Constructor Init; virtual;
      function Connect(port:string; handle:HWND):integer; virtual;
      procedure Disconnect; virtual;

      property UnknownMessage:string read fUnknownMessage;
      property LastDebugMessage:string read fGetLastDeviceDbgMsg;
      property Version:string read fDeviceVersion;
      property LastError:DWORD read fError;
      property IsDebug:boolean read fIsDebug write fSetIsDebug;
      property IsRCP:boolean read fIsRCP_ON write fSetRCP;

      Procedure Ping;

      procedure SendDebugCommand(cmd:byte);

 end;

  function  ByteArrayToStr(data:TCommandData; index:integer; size:integer):string;


implementation

function  ByteArrayToStr(data:TCommandData; index:integer; size:integer):string;
var i:integer;
begin
  result := '';
  for i := index to size do
    result := result + char(data[i]);
end;


// -------------    TFBasicCom   -------------------

Constructor TFBasicCom.Init;
begin
  fDeviceVersion := '';
  ParentHandle := 0;
  fUnknownMessage := '';

  fIsDebug := false;
  CommHandle := 0;
  ThreadID := 0;
  fError := 0;

  fDeviceType := FD_NONE;

  fIsRCP_ON := true;
end;

function TFBasicCom.fGetLastDeviceDbgMsg:string;
var i:integer;
begin
   result := fDeviceDbgMsg[0];
   for i := 0 to Length(fDeviceDbgMsg)-2 do
      fDeviceDbgMsg[i]:= fDeviceDbgMsg[i+1];
   Setlength(fDeviceDbgMsg, Length(fDeviceDbgMsg)-1);
end;

procedure TFBasicCom.fAddLastDeviceDbgMsg(str:string);
begin
   Setlength(fDeviceDbgMsg, Length(fDeviceDbgMsg)+1);
   fDeviceDbgMsg[Length(fDeviceDbgMsg)-1] := str;
end;

function TFBasicCom.Connect(port:string; handle:HWND):integer;
var
   str:string;
   i: Integer;
begin

   for i := 0 to sizeof(fBuff)+1 do
      fBuff[i] := 0;
   fPrevSize:=0;


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

   SendData(FBC_HANDSHAKE);   // FirmwareVersion request
   SendData(FBC_GET_FIRMWARE_VERSION);
end;


procedure TFBasicCom.Disconnect;
begin
    TerminateThread(CommThread,0);
    CloseHandle(CommHandle);
end;

class function TFBasicCom.ThreadProc(Param: Pointer): DWord;
begin
  result := TFBasicCom(Param).GetCommandData;
end;


procedure TFBasicCom.SendData(cmd:byte);
var
  Transmit:array [0..255] of byte;
  sendsize:DWORD;
begin
   sendsize:=4;
   Transmit[0]:=168;
   Transmit[1]:=cmd;
   Transmit[2]:=13;
   Transmit[3]:=10;
   WriteFile(CommHandle,Transmit,sendsize,sendsize,@Ovr);
end;

procedure TFBasicCom.SendData(cmd:byte; data:byte);
var
  Transmit:array [0..255] of byte;
  sendsize:DWORD;
begin
   sendsize:=5;
   Transmit[0]:=168;
   Transmit[1]:=cmd;
   Transmit[2]:=data;
   Transmit[3]:=13;
   Transmit[4]:=10;
   WriteFile(CommHandle,Transmit,sendsize,sendsize,@Ovr);
end;


procedure TFBasicCom.SendData(cmd:byte; par:byte; data:byte);
var
  Transmit:array [0..255] of byte;
  sendsize:DWORD;
begin
   sendsize:=6;
   Transmit[0]:=168;
   Transmit[1]:=cmd;
   Transmit[2]:=par;
   Transmit[3]:=data;
   Transmit[4]:=13;
   Transmit[5]:=10;
   WriteFile(CommHandle,Transmit,sendsize,sendsize,@Ovr);
end;

procedure TFBasicCom.SendData(cmd:byte; data:string);
var
  Transmit:array [0..255] of byte;
  sendsize:DWORD;
  i:integer;
begin
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

procedure TFBasicCom.SendDebugCommand(cmd:byte);
begin
  SendData(cmd);
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

function TFBasicCom.GeTCommandData:integer;
var
   data:TBuffer;
   i, pos: Integer;
   readsize:DWORD;
   TransMask: DWord;
   Errs : DWord;
   cmd:TCommandData;
begin
   readsize := fPrevSize;  //?????

   while true do
   begin
    TransMask:=EV_RXFLAG;
    WaitCommEvent(CommHandle,TransMask,NIL);
    if ((TransMask and EV_RXFLAG)=EV_RXFLAG) then
     begin
      ClearCommError(CommHandle,Errs,@Stat);
      fPrevSize := readsize;
      readsize := Stat.cbInQue;
      for i := 0 to sizeof(fBuff)+1 do
         data[i]:=0;
      ReadFile(CommHandle,data,readsize,readsize,@Ovr);
      for i := 0 to readsize do
        fBuff[fPrevSize+i] := data[i];
      readsize := readsize+fPrevSize;

      while readsize<>0 do
      begin
        pos := FindCmdStart(fBuff, readsize);
        if pos=-1 then
          begin
            readsize:=0;
            for i := 0 to sizeof(fBuff)+1 do
              fBuff[i]:=0;
            break;
          end;
        if pos>0 then
          begin
            ShiftBuffer(fBuff, readsize, pos);
            readsize:=readsize-pos;
          end;

        pos := FindCmdEnd(fBuff, readsize);
        if pos=-1 then
           break;
        for i := 0 to length(cmd) do
          cmd[i] := 0;
        for i := 0 to pos-1 do
          cmd[i] := fBuff[i];
        ParseData(cmd, pos);
        ShiftBuffer(fBuff, readsize, pos+2);
        readsize := readsize-(pos+2);
      end;

     end;
    end;
end;

procedure TFBasicCom.ParseData(data:TCommandData; size:integer);
var s:string;
begin
     try
      case data[1] of
        FBC_HANDSHAKE:
          begin
            if (data[2] = fDeviceType) then
               PostMessage(ParentHandle, WM_DEVICE_HANDSHAKE, 1, data[2])
            else
               PostMessage(ParentHandle, WM_DEVICE_HANDSHAKE, 0, data[2]);
          end;
        FBC_CMD_DEBUG:
          begin
            fAddLastDeviceDbgMsg(ByteArrayToStr(data, 2, size-1));
            PostMessage(ParentHandle, WM_DEVICE_CMD_DEBUG, 0, 0);
          end;
       FBC_PING:
          PostMessage(ParentHandle, WM_DEVICE_PING, 0, 0);
       FBC_DEBUG:
          begin
            if (data[2]<>0) then
               fIsDebug := true
              else
               fIsDebug := false;
            PostMessage(ParentHandle, WM_DEVICE_DEBUG, 0, 0);
          end;
       FBC_RCP:
         begin
           if (data[2]<>0) then
               fIsRCP_ON := true
              else
               fIsRCP_ON := false;
           PostMessage(ParentHandle, WM_DEVICE_RCP, 0, 0);
         end;
       FBC_GET_FIRMWARE_VERSION:
         begin
           fDeviceVersion := ByteArrayToStr(data, 2, 4);
           PostMessage(ParentHandle, WM_FIRMWARE_VERSION, 0, 0);
         end
        else
         begin
           fUnknownMessage := ByteArrayToStr(data, 2, size-1);
           PostMessage(ParentHandle, WM_DEVICE_UNKNOWN_MESSAGE, 0, 0);
         end;
      end;
     except
       On EConvertError do
         begin
           fAddLastDeviceDbgMsg('Convert error!  Cmd:' + IntToStr(data[1]) + ' ' + s);
           PostMessage(ParentHandle, WM_DEVICE_CMD_DEBUG, 1, 0);
         end;
     end;
end;



procedure TFBasicCom.fSetIsDebug(debug:boolean);
begin
    if (debug) then
      SendData(FBC_DEBUG, 1)
    else
      SendData(FBC_DEBUG, 0);
end;

Procedure TFBasicCom.Ping;
begin
   SendData(FBC_PING);
end;

procedure TFBasicCom.fSetRCP(status:boolean);
begin
    if (status) then
      SendData(FBC_RCP, 1)
    else
      SendData(FBC_RCP, 0);
end;


end.
