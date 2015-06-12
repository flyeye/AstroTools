unit main;

// Файл "main.pas" проекта Focuser
// Управление фокусировочным устройством
//
// Автор - Попов Алексей Владирович, 9141866@gmail.com
// 27.04.2014, Санкт-Петербург.


interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  StdCtrls, ExtCtrls, IniFiles, Vcl.ComCtrls, Focuser, MMSystem, fConnection;

  type TFocuserItem = class (TObject)
     Name:string;
     ComPort:String;
     MaxPos:integer;
     CurrentPos:integer;
     Settings:TIniFile;
     RangeCheck:bool;
     Positions:TStringList;

     Constructor Create(s:string); overload;
     Constructor Create(s, p:string; f:TFocuser); overload;
     procedure Init(s:string);
     procedure Load;
     procedure Save;
  end;

type
  TForm1 = class(TForm)
    Panel2: TPanel;
    btnConnect: TButton;
    btnStop: TButton;
    cbPort: TComboBox;
    PageControl1: TPageControl;
    TabSheet1: TTabSheet;
    Debug: TTabSheet;
    btnClearMem: TButton;
    btnSend: TButton;
    eDataLine: TEdit;
    memOut: TMemo;
    btnPing: TButton;
    btnRelease: TButton;
    btnRight: TButton;
    btnStepRight: TButton;
    btnStepLeft: TButton;
    btnLeft: TButton;
    lMinSpeed: TLabel;
    lMaxSpeed: TLabel;
    About: TTabSheet;
    Memo1: TMemo;
    lePosition: TLabeledEdit;
    btnSet0: TButton;
    lMinPos: TLabel;
    lMaxPos: TLabel;
    btnSetMax: TButton;
    cbShowDebugMsg: TCheckBox;
    pgSpeed: TProgressBar;
    pgPosition: TProgressBar;
    cbRangeCheck: TCheckBox;
    cbGoto: TComboBox;
    btnGoto: TButton;
    btnSave: TButton;
    btnDelete: TButton;
    cbFocuser: TComboBox;
    btnFocuserSave: TButton;
    btnFocuserDelete: TButton;
    Label2: TLabel;
    cbMicroStep: TComboBox;
    leSpeed: TLabeledEdit;
    btnSetSpeed: TButton;
    cbRCP: TCheckBox;
    Button1: TButton;
    Label1: TLabel;
    eReleaseTime: TEdit;
    cbPower: TCheckBox;
    ConnectionTimer: TTimer;
    procedure btnConnectClick(Sender: TObject);
    procedure btnStopClick(Sender: TObject);
    procedure btnSendClick(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure FormCreate(Sender: TObject);
    procedure btnClearMemClick(Sender: TObject);
    procedure btnPingClick(Sender: TObject);
    procedure btnStepLeftClick(Sender: TObject);
    procedure btnStepRightClick(Sender: TObject);
    procedure btnReleaseClick(Sender: TObject);
    procedure eDataLineKeyDown(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure cbMicroStepChange(Sender: TObject);
    procedure btnRightMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure btnRightMouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure btnLeftMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure btnLeftMouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure btnSet0Click(Sender: TObject);
    procedure btnSetMaxClick(Sender: TObject);
    procedure PageControl1Exit(Sender: TObject);
    procedure pgSpeedMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure pgSpeedMouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure pgSpeedMouseMove(Sender: TObject; Shift: TShiftState; X,
      Y: Integer);
    procedure cbRangeCheckClick(Sender: TObject);
    procedure btnGotoClick(Sender: TObject);
    procedure cbGotoChange(Sender: TObject);
    procedure btnSaveClick(Sender: TObject);
    procedure btnDeleteClick(Sender: TObject);
    procedure cbFocuserChange(Sender: TObject);
    procedure btnFocuserDeleteClick(Sender: TObject);
    procedure btnFocuserSaveClick(Sender: TObject);
    procedure btnSetSpeedClick(Sender: TObject);
    procedure cbShowDebugMsgClick(Sender: TObject);
    procedure cbRCPClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure cbPowerClick(Sender: TObject);
    procedure ConnectionTimerTimer(Sender: TObject);
  private
    { Private declarations }

  protected

    procedure WMFocuserRCP(var Message:TMessage); message WM_DEVICE_RCP;

    procedure WMFocuserSpeed(var Message: TMessage); message WM_FOCUSER_SPEED;
    procedure WMFocuserMaxSpeed(var Message: TMessage); message WM_FOCUSER_GET_MAX_SPEED;
    procedure WMFocuserMinSpeed(var Message: TMessage); message WM_FOCUSER_GET_MIN_SPEED;
    procedure WMFocuserStop(var Message: TMessage); message WM_FOCUSER_STOP;
    procedure WMFocuserStepRight(var Message: TMessage); message WM_FOCUSER_STEP_RIGHT;
    procedure WMFocuserRollRight(var Message: TMessage); message WM_FOCUSER_ROLL_RIGHT;
    procedure WMFocuserStepLeft(var Message: TMessage); message  WM_FOCUSER_STEP_LEFT;
    procedure WMFocuserRollLeft(var Message: TMessage); message WM_FOCUSER_ROLL_LEFT;
    procedure WMFocuserRolling(var Message: TMessage); message WM_FOCUSER_ROLLING;
    procedure WMFocuserPing(var Message: TMessage); message WM_DEVICE_PING;
    procedure WMFocuserHandShake(var Message: TMessage); message WM_DEVICE_HANDSHAKE;
    procedure WMFocuserRelease(var Message: TMessage); message WM_FOCUSER_RELEASE;
    procedure WMFocuserUnknownMessage(var Message: TMessage); message WM_DEVICE_UNKNOWN_MESSAGE;
    procedure WMFocuserMicroStep(var Message: TMessage); message WM_FOCUSER_MICROSTEP;
    procedure WMFocuserPosition(var Message: TMessage); message WM_FOCUSER_GET_POSITION;
    procedure WMFocuserGotoPosition(var Message: TMessage); message WM_FOCUSER_GO_TO_POSITION;
    procedure WMFocuserMsgDebug(var Message:TMessage); message WM_DEVICE_DEBUG_MSG;
    procedure WMFocuserMaxPosition(var Message:TMessage); message WM_FOCUSER_SET_MAX_POSITION;
    procedure WMFocuserRangeCheck(var Message:TMessage); message WM_FOCUSER_RANGE_CHECK;
    procedure WMFocuserDebug(var Message:TMessage); message WM_DEVICE_DEBUG;
    procedure WMFocuserReleaseTime(var Message:TMessage); message WM_FOCUSER_RELEASE_TIME;
    procedure WMFocuserFirmwareVersion(var Message:TMessage); message WM_FIRMWARE_VERSION;
    procedure StopRolling;

  public
    { Public declarations }
    Settings: TInifile;

    IsChangingSpeed :boolean;
    IsChangingMicroStep: boolean;

    Focuser: TFocuser;
//    FocuserList: array of TFocuserItem;
//    FocuserCount:integer;
  end;

const AppVer = '1.3';

var
  Form1: TForm1;

implementation

{$R *.DFM}

procedure TFocuserItem.Init(s:string);
var fname:string;
    ifile:THandle;
begin
   fname := ExtractFilePath(Application.ExeName) + '\' + s + '.ini';
   if (FileExists(fname)=false) then
     begin
       ifile:=FileCreate(fName);
       if (ifile=INVALID_HANDLE_VALUE) then
           exit
         else
             FileClose(ifile);
     end;
   Settings := TIniFile.Create(fname);
end;

Constructor TFocuserItem.Create(s:string);
begin
   Name := s;
   MaxPos :=0;
   CurrentPos :=0;
   ComPort := 'COM1';
   RangeCheck := false;
   Positions := TStringList.Create;
   Init(s);
   Load;
end;


Constructor TFocuserItem.Create(s, p:string; f:TFocuser);
var i:integer;
    fname:string;
    ifile:THandle;
begin
 Name := s;
 MaxPos := f.MaxPosition;
 CurrentPos := f.Position;
 RangeCheck := f.RangeCheck;
 ComPort := p;
 Positions := TStringList.Create;
 Init(s);
end;


procedure TFocuserItem.Load;
var i,c:integer;
    s:string;
begin
  ComPort := Settings.ReadString('General', 'COM_PORT', 'COM1');
  MaxPos := Settings.ReadInteger('General', 'MAX_POS', 1000);
  CurrentPos := Settings.ReadInteger('General', 'CUR_POS', 0);
  RangeCheck := Settings.ReadBool('General', 'RANGE_CHECK', false);
  c := Settings.ReadInteger('Positions', 'COUNT', 0);
  for i := 1 to c do
    begin
     s:= Settings.ReadString('Positions', IntToStr(i), '');
     if length(s)>0 then Positions.Add(s);
   end;
end;

procedure TFocuserItem.Save;
var i:integer;
begin
  Settings.WriteString('General', 'COM_PORT', ComPort);
  Settings.WriteInteger('General', 'MAX_POS', MaxPos);
  Settings.WriteInteger('General', 'CUR_POS', CurrentPos);
  Settings.WriteBool('General', 'RANGE_CHECK', RangeCheck);
  Settings.WriteInteger('Positions', 'COUNT', Positions.Count);
  for i := 1 to Positions.Count do
    Settings.WriteString('Positions', IntToStr(i), Positions[i-1]);
end;


procedure TForm1.WMFocuserUnknownMessage(var Message: TMessage);
begin
   Form1.memOut.Lines.Add( FOCUSER_MESSAGES[12] + Focuser.UnknownMessage);
end;

procedure TForm1.WMFocuserMsgDebug(var Message: TMessage);
begin
  if cbShowDebugMsg.Checked then
    Form1.memOut.Lines.Add( FOCUSER_MESSAGES[18] + Focuser.LastDebugMessage);
end;

procedure TForm1.WMFocuserDebug(var Message: TMessage);
begin
   if Focuser.IsDebug then
   Form1.memOut.Lines.Add('Debugging ON')
  else
   Form1.memOut.Lines.Add( 'Debugging OFF');
  cbRCP.Checked := Focuser.IsRCP;
end;

procedure TForm1.WMFocuserRCP(var Message: TMessage);
begin
 if Focuser.IsRCP then
   Form1.memOut.Lines.Add('Remote control is ON')
  else
   Form1.memOut.Lines.Add('Remote control is OFF');
end;

procedure TForm1.WMFocuserMaxSpeed(var Message: TMessage);
begin
   Form1.lMinSpeed.Caption := IntToStr(1000000 div Focuser.MinSpeed);
   Form1.pgSpeed.Min := 1000000 div Focuser.MinSpeed;
   Form1.memOut.Lines.Add(FOCUSER_MESSAGES[13] + IntToStr(Focuser.MinSpeed));
end;

procedure TForm1.WMFocuserMinSpeed(var Message: TMessage);
begin
   Form1.lMaxSpeed.Caption := IntToStr(1000000 div Focuser.MaxSpeed);
   Form1.pgSpeed.Max := 1000000 div Focuser.MaxSpeed;
   Form1.memOut.Lines.Add(FOCUSER_MESSAGES[13] + IntToStr(Focuser.MaxSpeed));
end;


procedure TForm1.WMFocuserSpeed(var Message: TMessage);
begin
   if (Form1.IsChangingSpeed=false)and((1000000 div Focuser.CurrentSpeed)<>Form1.pgSpeed.Position) then
     begin
       Form1.pgSpeed.Position := 1000000 div Focuser.CurrentSpeed;
     end;
   Form1.leSpeed.Text := IntToStr(1000000 div Focuser.CurrentSpeed);
   Form1.memOut.Lines.Add(FOCUSER_MESSAGES[2] + IntToStr(Message.WParam));
   inherited;
end;

procedure TForm1.WMFocuserMicroStep(var Message: TMessage);
begin
    Form1.memOut.Lines.Add('Microstep mode: ' + IntToStr(Focuser.MicroStep));
    if(Focuser.MicroStep<>Form1.cbMicroStep.ItemIndex) then
      begin
        IsChangingMicroStep := true;
        Form1.cbMicroStep.ItemIndex := Focuser.MicroStep;
        IsChangingMicroStep := false;
      end;
end;


procedure TForm1.WMFocuserStop(var Message: TMessage);
begin
   Form1.memOut.Lines.Add(FOCUSER_MESSAGES[3]);
end;

procedure TForm1.WMFocuserStepRight(var Message: TMessage);
begin
   Form1.memOut.Lines.Add(FOCUSER_MESSAGES[5]);
   cbPower.Checked := true;
end;

procedure TForm1.WMFocuserRollRight(var Message: TMessage);
begin
   Form1.memOut.Lines.Add(FOCUSER_MESSAGES[8]);
   cbPower.Checked := true;
end;

procedure TForm1.WMFocuserStepLeft(var Message: TMessage);
begin
   Form1.memOut.Lines.Add(FOCUSER_MESSAGES[6]);
   cbPower.Checked := true;
end;

procedure TForm1.WMFocuserRollLeft(var Message: TMessage);
begin
   Form1.memOut.Lines.Add(FOCUSER_MESSAGES[7]);
   cbPower.Checked := true;
end;

procedure TForm1.WMFocuserRolling(var Message: TMessage);
begin
   Form1.memOut.Lines.Add(FOCUSER_MESSAGES[9]);
   cbPower.Checked := true;
end;

procedure TForm1.WMFocuserPing(var Message: TMessage);
begin
   Form1.memOut.Lines.Add(FOCUSER_MESSAGES[11]);
end;

procedure TForm1.WMFocuserFirmwareVersion(var Message:TMessage);
begin
    Form1.memOut.Lines.Add(FOCUSER_MESSAGES[1] + Focuser.Version);
    if Focuser.Version <> AppVer then
       Form1.memOut.Lines.Add('Application and firmware has different version!');
end;

procedure TForm1.WMFocuserHandShake(var Message: TMessage);
begin
  if (Message.WParam>0) then
   begin
    ConnectionTimer.Enabled := false;
    Form1.memOut.Lines.Add('Focuser connected successfully!');
    Focuser.MaxPosition := TFocuserItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).MaxPos;
    Focuser.Position := TFocuserItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).CurrentPos;
    Focuser.RangeCheck := TFocuserItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).RangeCheck;
    Focuser.IsDebug := Settings.ReadBool('General', 'SHOW_DEBUG', false);
   end
  else
    Form1.memOut.Lines.Add('Wrong device is connected! DevType:' + IntToStr(Message.LParam))
end;

procedure TForm1.WMFocuserRelease(var Message: TMessage);
begin
   Form1.memOut.Lines.Add(FOCUSER_MESSAGES[10]);
   cbPower.Checked := false;
end;

procedure TForm1.WMFocuserPosition(var Message: TMessage);
begin
  lePosition.Text := IntToStr(Focuser.Position);
  Form1.memOut.Lines.Add(FOCUSER_MESSAGES[15] + IntToStr(Focuser.Position));
  pgPosition.Position := Focuser.Position;
end;

procedure TForm1.WMFocuserMaxPosition(var Message: TMessage);
begin
  lMaxPos.Caption := IntToStr(Focuser.MaxPosition);
  pgPosition.Max := Focuser.MaxPosition;

  lMinPos.Caption := IntToStr(Focuser.MinPosition);
  pgPosition.Min := Focuser.MinPosition;

  pgPosition.Position := Focuser.Position;

  Form1.memOut.Lines.Add('Max or Min position has been changed');
end;

procedure TForm1.WMFocuserRangeCheck(var Message:TMessage);
begin
  Form1.memOut.Lines.Add(FOCUSER_MESSAGES[20]+BoolToStr(Focuser.RangeCheck));
  cbRangeCheck.Checked := Focuser.RangeCheck;
end;


procedure TForm1.WMFocuserReleaseTime(var Message:TMessage);
begin
  Form1.memOut.Lines.Add('New release timeout: ' + IntToStr(Focuser.ReleaseTime));
  eReleaseTime.Text := IntToStr(Focuser.ReleaseTime);
end;

procedure TForm1.WMFocuserGotoPosition(var Message: TMessage);
begin
  Form1.memOut.Lines.Add(FOCUSER_MESSAGES[17] + IntToStr(integer(Message.WParam)));
  if Message.WParam=0 then
  begin
    btnStepLeft.Enabled := true;
    btnStepRight.Enabled := true;
    btnLeft.Enabled := true;
    btnRight.Enabled := true;
    pgSpeed.Enabled := true;
    cbMicroStep.Enabled := true;
    pgPosition.Enabled := true;
    btnSet0.Enabled := true;
    btnSetMax.Enabled := true;
  end;
end;

procedure TForm1.btnClearMemClick(Sender: TObject);
begin
  memOut.Clear;
end;


procedure TForm1.btnConnectClick(Sender: TObject);
var i, c:integer;
    s:string;
begin

 memOut.Clear;
 if (Focuser.Connect(cbPort.Items.Strings[cbPort.ItemIndex], Form1.Handle)<>0) then
    begin
      memOut.Lines.Add('Can`t open port ' + cbPort.Items.Strings[cbPort.ItemIndex] + '!');
      memOut.Lines.Add('Error: ' + IntToStr(Focuser.LastError));
      exit;
    end
  else
    memOut.Lines.Add('Port ' + cbPort.Items.Strings[cbPort.ItemIndex] + ' has been opened!');

  ConnectionTimer.Enabled := true;

  cbGoto.Items.Clear;
  cbGoto.Items.Add('Rightmost (0)');
  cbGoto.Items.Add('Leftmost (Max)');
  cbGoto.Items.AddStrings(TFocuserItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).Positions);
  cbGoto.ItemIndex := 0;

{ Focuser.MaxPosition := Settings.ReadInteger('General', 'MAX_POS', 1000);
 Focuser.Position := Settings.ReadInteger('General', 'POS', 0);
 Focuser.RangeCheck := Settings.ReadBool('General', 'RANGE_CHECK', false);

 c := Settings.ReadInteger('General', 'POSITION_COUNT', 30);
 for i := 1 to c do
   begin
    s:= Settings.ReadString('Positions', IntToStr(i), '');
    if length(s)>0 then cbGoto.Items.Add(s);
   end;}

 btnStop.Enabled := true;
 btnConnect.Enabled := false;
 cbPort.Enabled := false;
 btnSend.Enabled := true;
 btnPing.Enabled := true;
 eDataLine.Enabled := true;
 btnStepLeft.Enabled := true;
 btnStepRight.Enabled := true;
 btnLeft.Enabled := true;
 btnRight.Enabled := true;
 pgSpeed.Enabled := true;
 btnRelease.Enabled := true;
 cbMicroStep.Enabled := true;
// tbPosition.Enabled := true;
 btnSet0.Enabled := true;
 btnSetMax.Enabled := true;
 lePosition.Enabled := true;
 cbRangeCheck.Enabled := true;
 cbGoto.Enabled := true;
 btnGoto.Enabled := true;
 cbPower.Enabled := true;

 cbGotoChange(Sender);

 Settings.WriteInteger('General','PORT', cbPort.ItemIndex);
end;


procedure TForm1.btnLeftMouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
var  str:string;
begin
  Focuser.RollLeft;

  btnStepRight.Enabled := false;
  btnStepLeft.Enabled := false;
  btnRelease.Enabled := false;
  cbMicroStep.Enabled := false;

  str :=ExtractFilePath(Application.ExeName) + 'ding_new.wav';
  sndPlaySound(@str[1], SND_NODEFAULT Or SND_ASYNC Or SND_LOOP);
end;

procedure TForm1.btnLeftMouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
  StopRolling;
  sndPlaySound(nil, 0); // Stops the sound
end;

procedure TForm1.btnPingClick(Sender: TObject);
begin
 Focuser.Ping;
end;

procedure TForm1.btnReleaseClick(Sender: TObject);
begin
   Focuser.Release;
end;


procedure TForm1.btnSet0Click(Sender: TObject);
begin
   Focuser.Position := 0;
end;


procedure TForm1.btnSetMaxClick(Sender: TObject);
begin
    pgPosition.Position := Focuser.Position;
    Focuser.MaxPosition := Focuser.Position;
end;

procedure TForm1.btnSetSpeedClick(Sender: TObject);
begin
  Focuser.CurrentSpeed := 1000000 div StrToInt(leSpeed.Text );
end;

procedure TForm1.btnRightMouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
var  str:string;
begin
   Focuser.RollRight;

   btnStepRight.Enabled := false;
   btnStepLeft.Enabled := false;
   btnRelease.Enabled := false;
   cbMicroStep.Enabled := false;
   str :=ExtractFilePath(Application.ExeName) + 'ding_new.wav';
   sndPlaySound(@str[1], SND_NODEFAULT Or SND_ASYNC Or SND_LOOP);
end;

procedure TForm1.btnRightMouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
  StopRolling;
  sndPlaySound(nil, 0); // Stops the sound
end;

procedure TForm1.btnStepLeftClick(Sender: TObject);
begin
   Focuser.StepLeft;
end;

procedure TForm1.btnStepRightClick(Sender: TObject);
begin
  Focuser.StepRight;
end;

procedure TForm1.btnStopClick(Sender: TObject);
var i:integer;
begin
  Focuser.Disconnect;

{  Settings.WriteInteger('General', 'MAX_POS', Focuser.MaxPosition);
  Settings.WriteInteger('General', 'POS', Focuser.Position);
  Settings.WriteBool('General','RANGE_CHECK', Focuser.RangeCheck);

  Settings.EraseSection('Positions');
  Settings.WriteInteger('General', 'POSITION_COUNT', cbGoto.Items.Count-2);
  for i := 2 to cbGoto.Items.Count-1 do
    Settings.WriteString('Positions', IntToStr(i-1), cbGoto.Items[i]);}

  TFocuserItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).MaxPos := Focuser.MaxPosition;
  TFocuserItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).CurrentPos := Focuser.Position;
  TFocuserItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).RangeCheck := Focuser.RangeCheck;
  TFocuserItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).Positions.Clear;
  for i := 2 to cbGoto.Items.Count-1 do
    TFocuserItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).Positions.Add(cbGoto.Items[i]);
  TFocuserItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).Save;

  btnStop.Enabled := false;
  btnConnect.Enabled := true;
  cbPort.Enabled := true;
  btnSend.Enabled := false;
  btnPing.Enabled := false;
  eDataLine.Enabled := false;
  btnStepLeft.Enabled := false;
  btnStepRight.Enabled := false;
  btnLeft.Enabled := false;
  btnRight.Enabled := false;
  pgSpeed.Enabled := false;
  btnRelease.Enabled := false;
  cbMicroStep.Enabled := false;
//  tbPosition.Enabled := false;
  btnSet0.Enabled := false;
  btnSetMax.Enabled := false;
  lePosition.Enabled := false;
  cbRangeCheck.Enabled := false;
  btnGoto.Enabled := false;
  cbGoto.Enabled := false;
  cbPower.Enabled := false;
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
   Focuser.ReleaseTime := StrToInt(eReleaseTime.Text);
end;

procedure TForm1.btnGotoClick(Sender: TObject);
var i, j:integer;
    s:string;
begin
  case cbGoto.ItemIndex of
    -1: begin
          try
            i := StrToInt(cbGoto.Text);
          except
           On EConvertError do
             begin
               exit;
             end;
          end;
          Focuser.GotoPosition(i);
        end;
     0: Focuser.GotoPosition(0);
     1: Focuser.GotoPosition(Focuser.MaxPosition);
     else begin
       i := pos('(', cbGoto.Items[cbGoto.ItemIndex]);
       j := pos(')', cbGoto.Items[cbGoto.ItemIndex]);
       s := Copy(cbGoto.Items[cbGoto.ItemIndex], i+1, j-i-1);
       try
         i := StrToInt(s);
       except
         On EConvertError do
           begin
             exit;
           end;
       end;
       Focuser.GotoPosition(i);
     end;
  end;

  btnStepLeft.Enabled := false;
  btnStepRight.Enabled := false;
  btnLeft.Enabled := false;
  btnRight.Enabled := false;
  pgSpeed.Enabled := false;
  cbMicroStep.Enabled := false;
  pgPosition.Enabled := false;
  btnSet0.Enabled := false;
  btnSetMax.Enabled := false;

end;

procedure TForm1.StopRolling;
begin
  Focuser.Stop;

  btnStepRight.Enabled := true;
  btnStepLeft.Enabled := true;
  btnRelease.Enabled := true;
  cbMicroStep.Enabled := true;
end;

procedure TForm1.ConnectionTimerTimer(Sender: TObject);
begin
  Focuser.ReConnect;
end;

procedure TForm1.cbFocuserChange(Sender: TObject);
begin
  if (cbFocuser.ItemIndex=-1) then
    begin
      btnFocuserDelete.Enabled := false;
      if length(cbGoto.Text)=0 then
        btnFocuserSave.Enabled := false
       else
        btnFocuserSave.Enabled := true;
    end
   else
    begin
      btnFocuserSave.Enabled := true;
      btnFocuserDelete.Enabled := true;
      cbPort.ItemIndex := cbPort.Items.IndexOf(TFocuserItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).ComPort)
    end;

end;

procedure TForm1.cbGotoChange(Sender: TObject);
begin
  case cbGoto.ItemIndex of
    -1: begin
      btnDelete.Enabled := false;
      if length(cbGoto.Text)=0 then
        btnSave.Enabled := false
       else
        btnSave.Enabled := true;
    end;
    0..1: begin
      btnSave.Enabled := false;
      btnDelete.Enabled := false;
    end;
    else begin
      btnSave.Enabled := true;
      btnDelete.Enabled := true;
    end;
  end;
end;

procedure TForm1.btnDeleteClick(Sender: TObject);
begin
  case cbGoto.ItemIndex of
    -1,0,1: exit;
    else begin
      cbGoto.Items.Delete(cbGoto.ItemIndex);
    end;
  end;
  cbGotoChange(Sender);
end;

procedure TForm1.btnFocuserDeleteClick(Sender: TObject);
var i:integer;
begin
   if cbFocuser.ItemIndex>=0 then
        cbFocuser.Items.Delete(cbFocuser.ItemIndex);
   cbFocuser.OnChange(Sender);
end;

procedure TForm1.btnFocuserSaveClick(Sender: TObject);
var p:TFocuserItem;
    i:integer;
begin
  if cbFocuser.ItemIndex<0 then
   begin
       p:= TFocuserItem.Create(cbFocuser.Text, cbPort.Text, Focuser);
       for i := 2 to cbGoto.Items.Count-1 do
          p.Positions.Add(cbGoto.Items[i]);
       p.Save;
       cbFocuser.Items.AddObject(p.Name, p);
       cbFocuser.ItemIndex := cbFocuser.Items.IndexOf(p.Name);
   end
    else
      begin
        TFocuserItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).ComPort := cbPort.Text;
        TFocuserItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).MaxPos := Focuser.MaxPosition;
        TFocuserItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).CurrentPos := Focuser.Position;
        TFocuserItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).RangeCheck := Focuser.RangeCheck;
        TFocuserItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).Positions.Clear;
        for i := 2 to cbGoto.Items.Count-1 do
          TFocuserItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).Positions.Add(cbGoto.Items[i]);
        TFocuserItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).Save;
      end;
  cbFocuser.OnChange(Sender);
end;

procedure TForm1.btnSaveClick(Sender: TObject);
var i, j:integer;
     s:string;
begin
  case cbGoto.ItemIndex of
    -1: begin
     //  add new position with a new label
       cbGoto.ItemIndex := cbGoto.Items.Add(cbGoto.Text + ' (' + IntToStr(Focuser.Position)+ ')' );
    end;
    0..1: begin
      exit;
    end;
    else begin
    //  save new position to the old label
       s:=cbGoto.Items[cbGoto.ItemIndex];
       i:= pos(' (', s);
       j:= length(s)-i+1;
       Delete(s, i, j);
       s := s +  ' (' + IntToStr(Focuser.Position)+ ')';
       cbGoto.Items[cbGoto.ItemIndex] := s;
       cbGoto.ItemIndex := cbGoto.Items.IndexOf(s);
    end;
  end;
  cbGotoChange(Sender);
end;

procedure TForm1.cbMicroStepChange(Sender: TObject);
begin
  if (IsChangingMicroStep = false) then
     Focuser.MicroStep := cbMicroStep.ItemIndex;
end;

procedure TForm1.cbPowerClick(Sender: TObject);
begin
   if (cbPower.Checked=false) then
     Focuser.Release
   else
     Focuser.PowerOn;
end;

procedure TForm1.cbRangeCheckClick(Sender: TObject);
begin
   Focuser.RangeCheck := cbRangeCheck.Checked;
end;

procedure TForm1.cbRCPClick(Sender: TObject);
begin
   Focuser.IsRCP := cbRCP.Checked;
end;

procedure TForm1.cbShowDebugMsgClick(Sender: TObject);
begin
   Focuser.IsDebug := Form1.cbShowDebugMsg.Checked;
end;

procedure TForm1.eDataLineKeyDown(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
  if Key=13 then
    btnSendClick(Sender);
end;

procedure TForm1.FormClose(Sender: TObject; var Action: TCloseAction);
var i:integer;
begin

  if (btnStop.Enabled) then
      Focuser.Disconnect;

  for i := 1 to cbFocuser.Items.Count do
    begin
      TFocuserItem(cbFocuser.Items.Objects[i-1]).Save;
      Settings.WriteString('Focusers', IntToStr(i), TFocuserItem(cbFocuser.Items.Objects[i-1]).Name);
    end;
  Settings.WriteInteger('General', 'Focuser Count', cbFocuser.Items.Count);

  Settings.UpdateFile;
  Settings.Destroy;
end;

procedure TForm1.FormCreate(Sender: TObject);
var name:string;
    ifile:THandle;
    i,c:integer;
    P:TFocuserItem;
begin

   name := ExtractFilePath(Application.ExeName) + '\' + 'Focuser.ini';
   if (FileExists(name)=false) then
     begin
       ifile:=FileCreate(name);
       if (ifile=INVALID_HANDLE_VALUE) then
           memOut.Lines.Add('Error creating ini-file!')
         else
           begin
             memOut.Lines.Add('New ini-file created');
             FileClose(ifile);
           end;
     end;

   Settings := TIniFile.Create(name);
   c := Settings.ReadInteger('General', 'Focuser Count', 0);
//   SetLength(FocuserList, FocuserCount);
   if c = 0 then
      begin
        p:= TFocuserItem.Create('Focuser1');
        cbFocuser.Items.AddObject(p.Name, p);
      end
   else
    for i := 1 to c do
     begin
       p:= TFocuserItem.Create(Settings.ReadString('Focusers', IntToStr(i), ''));
       cbFocuser.Items.AddObject(p.Name, p);
     end;
   cbFocuser.ItemIndex := 0;
   cbFocuser.OnChange(Sender);

//   cbPort.ItemIndex := Settings.ReadInteger('General', 'PORT', 0);

   IsChangingSpeed := false;
   IsChangingMicroStep := false;
   Focuser := TFocuser.Init;
end;


procedure TForm1.PageControl1Exit(Sender: TObject);
begin
   Settings.WriteBool('General', 'SHOW_DEBUG', cbShowDebugMsg.Checked);
end;

procedure TForm1.pgSpeedMouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
   if (pgSpeed.Enabled=false)then
      exit;
    Form1.IsChangingSpeed := true;
    pgSpeedMouseMove(Sender, Shift, X, Y);
end;

procedure TForm1.pgSpeedMouseMove(Sender: TObject; Shift: TShiftState; X,
  Y: Integer);
var l:integer;
begin
   if (IsChangingSpeed)and(pgSpeed.Enabled)then
      begin
        l:=round(x*(pgSpeed.Max-pgSpeed.Min)/(pgSpeed.Width));
        pgSpeed.Position := l;
      end;
end;

procedure TForm1.pgSpeedMouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
   if (pgSpeed.Enabled=false)then
      exit;
   Form1.IsChangingSpeed := false;
   Focuser.CurrentSpeed := 1000000 div pgSpeed.Position;
end;


procedure TForm1.btnSendClick(Sender: TObject);
begin
  Focuser.SendDebugCommand(StrToInt(eDataline.Text));
end;

end.
