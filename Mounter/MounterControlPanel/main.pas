unit main;


interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  StdCtrls, ExtCtrls, IniFiles, Vcl.ComCtrls, Mounter, MMSystem, System.StrUtils, FConnection, UITypes, DateUtils;

  type TMounterItem = class (TObject)
     Name:string;
     ComPort:String;
     Settings:TIniFile;
     IsDebug:boolean;
     Positions:TStringList;
     fGearRatioRA, fGearRatioDE : integer;
     fNavSpeed, fNavMinSpeed, fNavMaxSpeed:double;
     fDailySpeedRA, fDailyMinSpeedRA, fDailyMaxSpeedRA:double;
     fDailySpeedDE, fDailyMinSpeedDE, fDailyMaxSpeedDE:double;

     Constructor Create(s:string); overload;
     Constructor Create(s, p:string; f:TMounter); overload;
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
    pgMounter: TPageControl;
    tsMounter: TTabSheet;
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
    lMinNavSpeed: TLabel;
    lMaxNavSpeed: TLabel;
    About: TTabSheet;
    Memo1: TMemo;
    lePositionRA: TLabeledEdit;
    btnSetPos: TButton;
    cbShowDebugMsg: TCheckBox;
    pgNavSpeed: TProgressBar;
    cbGoto: TComboBox;
    btnGoto: TButton;
    btnSave: TButton;
    btnDelete: TButton;
    cbFocuser: TComboBox;
    btnFocuserSave: TButton;
    btnFocuserDelete: TButton;
    leNavSpeed: TLabeledEdit;
    btnSetNavSpeed: TButton;
    cbDRotRA: TCheckBox;
    btnDERollForward: TButton;
    btnDEStepForward: TButton;
    btnDERollBackward: TButton;
    btnDEStepBackward: TButton;
    lMinDailySpeedRA: TLabel;
    pgDailySpeedRA: TProgressBar;
    lMaxDailySpeedRA: TLabel;
    btnSetDailySpeedRA: TButton;
    leDailySpeedRA: TLabeledEdit;
    lePositionDE: TLabeledEdit;
    eNewPosRA: TEdit;
    eNewPosDE: TEdit;
    btnSetDE: TButton;
    tEarthRotation: TTimer;
    pgDailySpeedDE: TProgressBar;
    lMinDailySpeedDE: TLabel;
    lMaxDailySpeedDE: TLabel;
    btnSetDailySpeedDE: TButton;
    leDailySpeedDE: TLabeledEdit;
    cbDRotDE: TCheckBox;
    tsParameters: TTabSheet;
    tDateTimeTemp: TTimer;
    GroupBox1: TGroupBox;
    leGearRatioRA: TLabeledEdit;
    leGearRatioDE: TLabeledEdit;
    Button1: TButton;
    eTemp: TLabeledEdit;
    eDateTime: TLabeledEdit;
    btnSetTime: TButton;
    Button2: TButton;
    btneMaxNavSpeed: TButtonedEdit;
    btneMaxDailyRASpeed: TButtonedEdit;
    btneMinDailyRASpeed: TButtonedEdit;
    btneMaxDailyDESpeed: TButtonedEdit;
    btneMinDailyDESpeed: TButtonedEdit;
    btneMinNavSpeed: TButtonedEdit;
    GroupBox2: TGroupBox;
    eStepDuration: TLabeledEdit;
    cbRCP: TCheckBox;
    GroupBox3: TGroupBox;
    cbTime: TCheckBox;
    cbSound: TCheckBox;
    eReleaseTime: TLabeledEdit;
    Button3: TButton;
    lePosRA_EQ: TEdit;
    lePosDE_EQ: TEdit;
    Label1: TLabel;
    cbAutoscroll: TCheckBox;
    Label2: TLabel;
    cbMicroStep: TComboBox;
    Label3: TLabel;
    Label4: TLabel;
    cbDailyMicroStep: TComboBox;
    rbRAHours: TRadioButton;
    rbRADegrees: TRadioButton;
    procedure btnConnectClick(Sender: TObject);
    procedure btnStopClick(Sender: TObject);
    procedure btnSendClick(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure FormCreate(Sender: TObject);
    procedure btnClearMemClick(Sender: TObject);
    procedure btnPingClick(Sender: TObject);
    procedure btnRAStepBack(Sender: TObject);
    procedure btnRAStepForward(Sender: TObject);
    procedure btnReleaseClick(Sender: TObject);
    procedure eDataLineKeyDown(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure cbMicroStepChange(Sender: TObject);
    procedure btnRAForwardRoll(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure btnRAForwardStop(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure btnRABackwardRoll(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure btnRABackwardStop(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure btnSetPosClick(Sender: TObject);
    procedure pgMounterExit(Sender: TObject);
    procedure pgNavSpeedMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure pgNavSpeedMouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure pgNavSpeedMouseMove(Sender: TObject; Shift: TShiftState; X,
      Y: Integer);
    procedure btnGotoClick(Sender: TObject);
    procedure cbGotoChange(Sender: TObject);
    procedure btnSaveClick(Sender: TObject);
    procedure btnDeleteClick(Sender: TObject);
    procedure cbFocuserChange(Sender: TObject);
    procedure btnFocuserDeleteClick(Sender: TObject);
    procedure btnFocuserSaveClick(Sender: TObject);
    procedure btnSetNavSpeedClick(Sender: TObject);
    procedure cbDRotRAClick(Sender: TObject);
    procedure cbShowDebugMsgClick(Sender: TObject);
    procedure btnDEStepForwardClick(Sender: TObject);
    procedure btnDEStepBackwardClick(Sender: TObject);
    procedure btnDERollForwardMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure btnDERollBackwardMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure btnDERollForwardMouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure btnDERollBackwardMouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure btnSetDailySpeedRAClick(Sender: TObject);
    procedure pgDailySpeedRAMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure pgDailySpeedRAMouseMove(Sender: TObject; Shift: TShiftState; X,
      Y: Integer);
    procedure pgDailySpeedRAMouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure eObjectNameChange(Sender: TObject);
    procedure btnSetDEClick(Sender: TObject);
    procedure tEarthRotationTimer(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure cbTimeClick(Sender: TObject);
    procedure btnSetDailySpeedDEClick(Sender: TObject);
    procedure cbDRotDEClick(Sender: TObject);
    procedure pgDailySpeedDEMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure pgDailySpeedDEMouseMove(Sender: TObject; Shift: TShiftState; X,
      Y: Integer);
    procedure pgDailySpeedDEMouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure hkLeftEnter(Sender: TObject);
    procedure cbRCPClick(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure btnSetTimeClick(Sender: TObject);
    procedure tDateTimeTempTimer(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure cbDailyMicroStepChange(Sender: TObject);

  private
    { Private declarations }

  protected

    HotKeyUp1, HotKeyDown1, HotKeyLeft1, HotKeyRight1,
    HotKeyUp2, HotKeyDown2, HotKeyLeft2, HotKeyRight2,
    HotKeyUp3, HotKeyDown3, HotKeyLeft3, HotKeyRight3,
    HotKeyStop: Word;

    procedure WMMounterNavSpeed(var Message: TMessage); message WM_MOUNTER_NAV_SPEED;
    procedure WMMounterDailySpeed(var Message: TMessage); message WM_MOUNTER_DAILY_SPEED;
    procedure WMMounterDailyMaxSpeed(var Message: TMessage); message WM_MOUNTER_GET_DAILY_MAX_SPEED;
    procedure WMMounterDailyMinSpeed(var Message: TMessage); message WM_MOUNTER_GET_DAILY_MIN_SPEED;
    procedure WMMounterNavMaxSpeed(var Message: TMessage); message WM_MOUNTER_GET_NAV_MAX_SPEED;
    procedure WMMounterNavMinSpeed(var Message: TMessage); message WM_MOUNTER_GET_NAV_MIN_SPEED;
    procedure WMMounterStop(var Message: TMessage); message WM_MOUNTER_STOP;
    procedure WMMounterStep(var Message: TMessage); message WM_MOUNTER_STEP;
    procedure WMMounterRoll(var Message: TMessage); message WM_MOUNTER_ROLL;
    procedure WMFocuserPing(var Message: TMessage); message WM_DEVICE_PING;
    procedure WMFocuserHandShake(var Message: TMessage); message WM_DEVICE_HANDSHAKE;
    procedure WMMounterRelease(var Message: TMessage); message WM_MOUNTER_RELEASE;
    procedure WMFocuserReleaseTime(var Message:TMessage); message WM_FOCUSER_RELEASE_TIME;
    procedure WMFocuserUnknownMessage(var Message: TMessage); message WM_DEVICE_UNKNOWN_MESSAGE;
    procedure WMFocuserMicroStep(var Message: TMessage); message WM_FOCUSER_MICROSTEP;
    procedure WMFocuserPosition(var Message: TMessage); message WM_FOCUSER_GET_POSITION;
    procedure WMFocuserGotoPosition(var Message: TMessage); message WM_FOCUSER_GO_TO_POSITION;
    procedure WMFocuserDebug(var Message:TMessage); message WM_DEVICE_DEBUG_MSG;
    procedure WMMounterDuirnalRotation(var Message:TMessage); message WM_MOUNTER_DIURNAL_ROTATION;
    procedure WMMounterIsDebug(var Message:TMessage); message WM_DEVICE_DEBUG;
    procedure WMMounterIsRPC(var MEssage:TMessage); message WM_DEVICE_RCP;
    procedure WMFocuserFirmwareVersion(var Message:TMessage); message WM_FIRMWARE_VERSION;

    procedure WMFocuserRTCTemp(var Message:TMessage); message WM_RTC_TEMP;
    procedure WMFocuserRTCGet(var Message:TMessage); message WM_RTC_GET;

    procedure WMHotKey(var HTK: TWMHotKey); message WM_HOTKEY;

    procedure StopRolling;

    procedure UpdatePosBtn;

  public
    { Public declarations }
    Settings: TInifile;

    IsChangingSpeed :boolean;
    IsChangingMicroStep: boolean;

    Mounter: TMounter;

    procedure AddLog(str:string);

  end;

const AppVer = '0.4';

var
  Form1: TForm1;

implementation

{$R *.DFM}

procedure TMounterItem.Init(s:string);
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

Constructor TMounterItem.Create(s:string);
begin
   Name := s;
   ComPort := 'COM1';
   IsDebug := false;
   Positions := TStringList.Create;
   fGearRatioRA := GEAR_RATIO_RA_DEF;
   fGearRatioDE := GEAR_RATIO_DE_DEF;
   Init(s);
   Load;
end;


Constructor TMounterItem.Create(s, p:string; f:TMounter);
begin
 Name := s;
 IsDebug := f.IsDebug;
 ComPort := p;
 Positions := TStringList.Create;
 fGearRatioRA := GEAR_RATIO_RA_DEF;
 fGearRatioDE := GEAR_RATIO_DE_DEF;
 Init(s);
end;


procedure TMounterItem.Load;
var i,c:integer;
    s:string;
begin
  ComPort := Settings.ReadString('General', 'COM_PORT', 'COM1');
  IsDebug := Settings.ReadBool('General', 'IS_DEBUG', false);
  fGearRatioRA := Settings.ReadInteger('General', 'GEAR_RATIO_RA', GEAR_RATIO_RA_DEF);
  fGearRatioDE := Settings.ReadInteger('General', 'GEAR_RATIO_DE', GEAR_RATIO_DE_DEF);

  fNavSpeed := Settings.ReadFloat('General', 'NAV_SPEED', 3000.0);
  fNavMinSpeed := Settings.ReadFloat('General', 'NAV_MIN_SPEED', 20);
  fNavMaxSpeed := Settings.ReadFloat('General', 'NAV_MAX_SPEED', 4800);
  fDailySpeedRA := Settings.ReadFloat('General', 'DAILY_SPEED_RA', 15);
  fDailyMinSpeedRA := Settings.ReadFloat('General', 'DAILY_MIN_SPEED_RA', 12);
  fDailyMaxSpeedRA := Settings.ReadFloat('General', 'DAILY_MAX_SPEED_RA', 18);
  fDailySpeedDE := Settings.ReadFloat('General', 'DAILY_SPEED_DE', 0);
  fDailyMinSpeedDE := Settings.ReadFloat('General', 'DAILY_MIN_SPEED_DE', -10);
  fDailyMaxSpeedDE := Settings.ReadFloat('General', 'DAILY_MAX_SPEED_DE', 10);


  c := Settings.ReadInteger('Positions', 'COUNT', 0);
  for i := 1 to c do
    begin
     s:= Settings.ReadString('Positions', IntToStr(i), '');
     if length(s)>0 then Positions.Add(s);
   end;
end;

procedure TMounterItem.Save;
var i:integer;
begin
  Settings.WriteString('General', 'COM_PORT', ComPort);

  Settings.WriteBool('General', 'IS_DEBUG', IsDebug);
  Settings.WriteInteger('General', 'GEAR_RATIO_RA', fGearRatioRA);
  Settings.WriteInteger('General', 'GEAR_RATIO_DE', fGearRatioDE);
  Settings.WriteInteger('Positions', 'COUNT', Positions.Count);
  for i := 1 to Positions.Count do
    Settings.WriteString('Positions', IntToStr(i), Positions[i-1]);

  Settings.WriteFloat('General', 'NAV_SPEED', fNavSpeed);
  Settings.ReadFloat('General', 'NAV_MIN_SPEED', fNavMinSpeed);
  Settings.ReadFloat('General', 'NAV_MAX_SPEED', fNavMaxSpeed);
  Settings.ReadFloat('General', 'DAILY_SPEED_RA', fDailySpeedRA);
  Settings.ReadFloat('General', 'DAILY_MIN_SPEED_RA', fDailyMinSpeedRA);
  Settings.ReadFloat('General', 'DAILY_MAX_SPEED_RA', fDailyMaxSpeedRA);
  Settings.ReadFloat('General', 'DAILY_SPEED_DE', fDailySpeedDE);
  Settings.ReadFloat('General', 'DAILY_MIN_SPEED_DE', fDailyMinSpeedDE);
  Settings.ReadFloat('General', 'DAILY_MAX_SPEED_DE', fDailyMaxSpeedDE);

end;

//------------------------------------

procedure TForm1.AddLog(str:string);
begin

  if (cbAutoscroll.Checked) then
    memOut.Lines.Add(str)
  else
    begin
      memOut.Lines.BeginUpdate;
      memOut.Lines.Add(str);
      memOut.Lines.EndUpdate;
    end;
end;


procedure TForm1.WMFocuserUnknownMessage(var Message: TMessage);
begin
   AddLog( MOUNTER_MESSAGES[12] + Mounter.UnknownMessage);
end;

procedure TForm1.WMFocuserDebug(var Message: TMessage);
begin
  AddLog(MOUNTER_MESSAGES[18] + Mounter.LastDebugMessage);
  if Message.WParam=1 then
    begin
      AddLog( MOUNTER_MESSAGES[18] + Mounter.LastDebugMessage);
      ShowMessage('Error! Check logs!');
    end;

end;

procedure TForm1.WMMounterDailyMaxSpeed(var Message: TMessage);
begin
   if Message.WParam=MOTOR_RA then
     begin
      Form1.lMaxDailySpeedRA.Caption := FloatToStrF(Mounter.DailyMaxSpeedRA, ffFixed, 15, 5);
      btneMaxDailyRASpeed.Text := FloatToStrF(Mounter.DailyMaxSpeedRA, ffFixed, 15, 6);

      AddLog(MOUNTER_MESSAGES[13] + GetMotorName(Message.WParam) + ' ' + Form1.lMaxDailySpeedRA.Caption);
     end
   else if Message.WParam = MOTOR_DE then
     begin
       Form1.lMaxDailySpeedDE.Caption := FloatToStrF(Mounter.DailyMaxSpeedDE, ffFixed, 15, 6);
       btneMaxDailyDESpeed.Text := FloatToStrF(Mounter.DailyMaxSpeedDE, ffFixed, 15, 5);

       AddLog(MOUNTER_MESSAGES[13] + GetMotorName(Message.WParam) + ' ' + Form1.lMaxDailySpeedDE.Caption);
     end;
end;

procedure TForm1.WMMounterDailyMinSpeed(var Message: TMessage);
begin
   if Message.WParam=MOTOR_RA then
     begin
      Form1.lMinDailySpeedRA.Caption := FloatToStrF(Mounter.DailyMinSpeedRA, ffFixed, 15, 5);
      btneMinDailyRASpeed.Text := FloatToStrF(Mounter.DailyMinSpeedRA, ffFixed, 15, 6);
      AddLog(MOUNTER_MESSAGES[13] + GetMotorName(Message.WParam) + ' ' + Form1.lMinDailySpeedRA.Caption);
     end
   else if Message.WParam = MOTOR_DE then
     begin
       Form1.lMinDailySpeedDE.Caption := FloatToStrF(Mounter.DailyMinSpeedDE, ffFixed, 15, 6);
       btneMinDailyDESpeed.Text := FloatToStrF(Mounter.DailyMinSpeedDE, ffFixed, 15, 5);
       AddLog(MOUNTER_MESSAGES[13] + GetMotorName(Message.WParam) + ' ' + Form1.lMinDailySpeedDE.Caption);
     end;
end;

procedure TForm1.WMMounterNavMaxSpeed(var Message: TMessage);
begin
     Form1.lMaxNavSpeed.Caption := FloatToStrF(Mounter.NavMaxSpeed, ffFixed, 15, 2);
     btneMaxNavSpeed.Text := FloatToStrF(Mounter.NavMaxSpeed, ffFixed, 15, 0);
     AddLog(MOUNTER_MESSAGES[13] + GetMotorName(Message.WParam) + ' ' + Form1.lMaxNavSpeed.Caption);
end;

procedure TForm1.WMMounterNavMinSpeed(var Message: TMessage);
begin
     Form1.lMinNavSpeed.Caption := FloatToStrF(Mounter.NavMinSpeed, ffFixed, 15, 2);
     btneMinNavSpeed.Text := FloatToStrF(Mounter.NavMinSpeed, ffFixed, 15, 0);
     AddLog(MOUNTER_MESSAGES[13] + GetMotorName(Message.WParam) + ' ' + Form1.lMinNavSpeed.Caption);
end;



procedure TForm1.WMHotKey(var HTK: TWMHotKey);
begin

  if HTK.HotKey = HotKeyUp1 then
      Mounter.Roll(MOTOR_DE, STEP_FORWARD, StrToInt(eStepDuration.Text))
   else if HTK.HotKey = HotKeyDown1 then
      Mounter.Roll(MOTOR_DE, STEP_BACKWARD, StrToInt(eStepDuration.Text))
   else if HTK.HotKey = HotKeyLeft1 then
      Mounter.Roll(MOTOR_RA, STEP_BACKWARD, StrToInt(eStepDuration.Text))
   else if HTK.HotKey = HotKeyRight1 then
      Mounter.Roll(MOTOR_RA, STEP_FORWARD, StrToInt(eStepDuration.Text))
   else if (HTK.HotKey = HotKeyUp2) or (HTK.HotKey = HotKeyUp3) then
      Mounter.Roll(MOTOR_DE, STEP_FORWARD, 0)
   else if (HTK.HotKey = HotKeyDown2) or (HTK.HotKey = HotKeyDown3) then
      Mounter.Roll(MOTOR_DE, STEP_BACKWARD, 0)
   else if (HTK.HotKey = HotKeyLeft2) or (HTK.HotKey = HotKeyLeft3) then
      Mounter.Roll(MOTOR_RA, STEP_BACKWARD, 0)
   else if (HTK.HotKey = HotKeyRight2) or (HTK.HotKey = HotKeyRight3) then
      Mounter.Roll(MOTOR_RA, STEP_FORWARD, 0)
   else if HTK.HotKey = HotKeyStop then
     begin
      Mounter.Stop(MOTOR_DE);
      Mounter.Stop(MOTOR_RA);
     end;
end;


procedure TForm1.WMMounterDailySpeed(var Message: TMessage);
var speed, sp:double;
begin
 if Message.WParam=MOTOR_RA then
   begin
     speed := Mounter.DailyCurrentSpeedRA;
     sp :=  ((speed-Mounter.DailyMinSpeedRA)/(Mounter.DailyMaxSpeedRA-Mounter.DailyMinSpeedRA))*(Form1.pgDailySpeedRA.Max-Form1.pgDailySpeedRA.Min);
     if (Form1.IsChangingSpeed=false)and(abs(speed-Form1.pgDailySpeedRA.Position)>0.00001) then
       begin
         Form1.pgDailySpeedRA.Position := round(sp);
       end;
     Form1.leDailySpeedRA.Text := FloatToStrF(speed, ffFixed, 15, 4);
     AddLog(MOUNTER_MESSAGES[2] + 'Daily RA ' + FloatToStrF(speed, ffFixed, 15, 4) + '  ' + IntToStr(Mounter.DailySpeedRA));
   end
 else if Message.WParam = MOTOR_DE then
   begin
     speed := Mounter.DailyCurrentSpeedDE;
     sp :=  ((speed-Mounter.DailyMinSpeedDE)/(Mounter.DailyMaxSpeedDE-Mounter.DailyMinSpeedDE))*(Form1.pgDailySpeedDE.Max-Form1.pgDailySpeedDE.Min);
     if (Form1.IsChangingSpeed=false)and(abs(speed-Form1.pgDailySpeedDE.Position)>0.00001) then
       begin
         Form1.pgDailySpeedDE.Position := round(sp);
       end;
     Form1.leDailySpeedDE.Text := FloatToStrF(speed, ffFixed, 15, 4);
     AddLog(MOUNTER_MESSAGES[2] + 'Daily DE' + FloatToStrF(speed, ffFixed, 15, 4) + '  ' + IntToStr(Mounter.DailySpeedDE));
   end;

   inherited;
end;

procedure TForm1.WMMounterNavSpeed(var Message: TMessage);
var speed, sp:double;
begin
   speed := Mounter.NavCurrentSpeed;
   sp := speed / ((Mounter.NavMaxSpeed-Mounter.NavMinSpeed)/(Form1.pgNavSpeed.Max-Form1.pgNavSpeed.Min));
   if (Form1.IsChangingSpeed=false)and(Message.WParam<>Form1.pgNavSpeed.Position) then
     begin
       Form1.pgNavSpeed.Position := round (sp);
     end;
   Form1.leNavSpeed.Text := FloatToStrF(speed, ffFixed, 15, 0);
   AddLog(MOUNTER_MESSAGES[2] + ' Nav ' + IntToStr(Message.WParam));
   inherited;
end;

procedure TForm1.WMFocuserMicroStep(var Message: TMessage);
begin
  case Message.WParam of
    DAILY: if(Mounter.DailyMicroStep<>Form1.cbMicroStep.ItemIndex) then
      begin
        IsChangingMicroStep := true;
        Form1.cbDailyMicroStep.ItemIndex := Mounter.DailyMicroStep;
        IsChangingMicroStep := false;
      end;
     NAVIGATION: if(Mounter.NavMicroStep<>Form1.cbMicroStep.ItemIndex) then
      begin
        IsChangingMicroStep := true;
        Form1.cbMicroStep.ItemIndex := Mounter.NavMicroStep;
        IsChangingMicroStep := false;
      end;
  end;
end;


procedure TForm1.WMMounterStop(var Message: TMessage);
var s:string;
begin
   s:= ' ';
   if Message.WParam=MOTOR_RA then s := s + ' RA'
   else if Message.WParam=MOTOR_DE then s := s + ' DE';
   AddLog(MOUNTER_MESSAGES[3] + s);
end;

procedure TForm1.WMMounterStep(var Message: TMessage);
var s:string;
begin
   s:= ' ';
   if Message.WParam=MOTOR_RA then s := s + 'RA '
   else if Message.WParam=MOTOR_DE then s := s + 'DE ';
   if Message.LParam=STEP_FORWARD then s := s + 'forward'
   else if (shortint(Message.LParam))=STEP_BACKWARD then s := s + 'backward';
   AddLog(MOUNTER_MESSAGES[5] + ' ' + s);
end;

procedure TForm1.WMMounterRoll(var Message: TMessage);
var s:string;
begin
   s:= ' ';
   if Message.WParam=MOTOR_RA then s := s + 'RA '
   else if Message.WParam=MOTOR_DE then s := s + 'DE ';
   if Message.LParam=STEP_FORWARD then s := s + 'forward'
   else if (shortint(Message.LParam))=STEP_BACKWARD then s := s + 'backward';
   AddLog(MOUNTER_MESSAGES[8] + ' ' + s);
end;


procedure TForm1.WMFocuserPing(var Message: TMessage);
begin
   AddLog(MOUNTER_MESSAGES[11]);
end;

procedure TForm1.WMFocuserRTCTemp(var Message:TMessage);
begin
   AddLog('Mounter RTC Temp: ' + FloatToStrF(Mounter.RTCTemperature, ffFixed, 15, 1));
   eTemp.Text := FloatToStrF(Mounter.RTCTemperature, ffFixed, 15, 1);
end;

procedure TForm1.WMFocuserRTCGet(var Message:TMessage);
begin
   AddLog('Mounter RTC Date Time: ' + Mounter.RTCDateTimeStr);
   eDateTime.Text := Mounter.RTCDateTimeStr;
end;

procedure TForm1.WMFocuserFirmwareVersion(var Message:TMessage);
begin
    AddLog(MOUNTER_MESSAGES[1] + Mounter.Version);
    if Mounter.Version <> AppVer then
       AddLog('Application and firmware has different version!');
end;


procedure TForm1.WMFocuserHandShake(var Message: TMessage);
begin
  if (Message.WParam>0) then
    AddLog('Focuser connected successfully!')
  else
    AddLog('Wrong device is connected! DevType:' + IntToStr(Message.LParam))
end;

procedure TForm1.WMFocuserReleaseTime(var Message:TMessage);
begin
  AddLog('New release timeout: ' + IntToStr(Mounter.ReleaseTime));
  eReleaseTime.Text := IntToStr(Mounter.ReleaseTime);
end;

procedure TForm1.WMMounterRelease(var Message: TMessage);
var s:string;
begin
   if Message.WParam=MOTOR_RA then s := s + 'RA '
   else if Message.WParam=MOTOR_DE then s := s + 'DE ';
   AddLog(MOUNTER_MESSAGES[10] + s);
end;

procedure TForm1.WMFocuserPosition(var Message: TMessage);
var s:string;
begin
   if Message.WParam=MOTOR_RA then
     begin
      lePositionRA.Text := IntToStr(Mounter.PosRA);
      if rbRAHours.Checked then
        lePosRA_EQ.Text := FloatToStrF(Mounter.PosRA_EQ, ffFixed, 15, 5)
      else
        lePosRA_EQ.Text := FloatToStrF(Mounter.PosRA_EQ*15, ffFixed, 15, 5);
      s := s + 'RA ' + lePositionRA.Text;
     end
   else if Message.WParam=MOTOR_DE then
     begin
       lePositionDE.Text := IntToStr(Mounter.PosDE);
       lePosDE_EQ.Text := FloatToStrF(Mounter.PosDE_EQ, ffFixed, 15, 5);
       s := s + 'DE ' + lePositionDE.Text;
     end;

  AddLog(MOUNTER_MESSAGES[15] + s);
end;


procedure TForm1.WMMounterIsDebug(var Message:TMessage);
begin
  AddLog('Debugging ' + BoolToStr(Mounter.IsDebug));
  cbShowDebugMsg.Checked := Mounter.IsDebug;
end;


procedure TForm1.WMMounterIsRPC(var Message:TMessage);
begin
  AddLog('Remote control panel is ' + BoolToStr(Mounter.IsRCP));
  cbRCP.Checked := Mounter.IsRCP;
end;

procedure TForm1.WMMounterDuirnalRotation(var Message:TMessage);
begin
   if Message.WParam=MOTOR_RA then
     begin
       if Mounter.DuirnalRotation then
          AddLog('Duirnal rotations is switched on!')
       else
          AddLog('Duirnal rotations is switched off!');
       cbDRotRA.Checked := Mounter.DuirnalRotation;
     end
   else if Message.WParam=MOTOR_DE then
     begin
       if Mounter.DECompensation then
          AddLog('DE compensation rotations is switched on!')
       else
          AddLog('DE compensation rotations is switched off!');
       cbDRotDE.Checked := Mounter.DECompensation;
     end;
end;

procedure TForm1.WMFocuserGotoPosition(var Message: TMessage);
begin
  AddLog(MOUNTER_MESSAGES[17] + IntToStr(integer(Message.WParam)));
  if Message.WParam=0 then
  begin
    btnStepLeft.Enabled := true;
    btnStepRight.Enabled := true;
    btnLeft.Enabled := true;
    btnRight.Enabled := true;
    pgNavSpeed.Enabled := true;
    pgDailySpeedRA.Enabled := true;
    pgDailySpeedDE.Enabled := true;
    cbMicroStep.Enabled := true;
    cbDailyMicroStep.Enabled := true;
    btnSetPos.Enabled := true;
    btnSetDE.Enabled := true;
  end;
end;

procedure TForm1.btnClearMemClick(Sender: TObject);
begin
  memOut.Clear;
end;


procedure TForm1.btnConnectClick(Sender: TObject);
begin

 memOut.Clear;

 if (Mounter.Connect(cbPort.Items.Strings[cbPort.ItemIndex], Form1.Handle)<>0) then
    begin
      memOut.Lines.Add('Can`t open port ' + cbPort.Items.Strings[cbPort.ItemIndex] + '!');
      memOut.Lines.Add('Error: ' + IntToStr(Mounter.LastError));
      exit;
    end
  else
    memOut.Lines.Add('Port ' + cbPort.Items.Strings[cbPort.ItemIndex] + ' has been opened!');
  Mounter.IsDebug :=TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).IsDebug;

  Mounter.fGearRatioRA := TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).fGearRatioRA;
  Mounter.fGearRatioDE := TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).fGearRatioDE;

  Mounter.NavMaxSpeed := TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).fNavMaxSpeed;
  Mounter.NavMinSpeed := TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).fNavMinSpeed;
  Mounter.DailyMaxSpeedRA := TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).fDailyMaxSpeedRA;
  Mounter.DailyMinSpeedRA := TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).fDailyMinSpeedRA;
  Mounter.DailyMaxSpeedDE := TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).fDailyMaxSpeedDE;
  Mounter.DailyMinSpeedDE := TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).fDailyMinSpeedDE;
  Mounter.DailyCurrentSpeedDE := 0;

  Form1.lMaxDailySpeedRA.Caption := FloatToStrF(Mounter.DailyMaxSpeedRA, ffFixed, 15, 0);
  Form1.lMaxDailySpeedDE.Caption := FloatToStrF(Mounter.DailyMaxSpeedDE, ffFixed, 15, 0);
  Form1.lMinDailySpeedRA.Caption := FloatToStrF(Mounter.DailyMinSpeedRA, ffFixed, 15, 0);
  Form1.lMinDailySpeedDE.Caption := FloatToStrF(Mounter.DailyMinSpeedDE, ffFixed, 15, 0);
  Form1.lMaxNavSpeed.Caption := FloatToStrF(Mounter.NavMaxSpeed, ffFixed, 15, 0);
  Form1.lMinNavSpeed.Caption := FloatToStrF(Mounter.NavMinSpeed, ffFixed, 15, 0);

  cbGoto.Items.Clear;
  cbGoto.Items.AddStrings(TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).Positions);
  cbGoto.ItemIndex := 0;

  tEarthRotation.Enabled := true;


  leGearRatioRA.Text := IntToStr(Mounter.fGearRatioRA);
  leGearRatioDE.Text := IntToStr(Mounter.fGearRatioDE);

{ Mounter.MaxPosition := Settings.ReadInteger('General', 'MAX_POS', 1000);
 Mounter.Position := Settings.ReadInteger('General', 'POS', 0);
 Mounter.RangeCheck := Settings.ReadBool('General', 'RANGE_CHECK', false);

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
 pgNavSpeed.Enabled := true;
 pgDailySpeedRA.Enabled := true;
 pgDailySpeedDE.Enabled := true;
 btnRelease.Enabled := true;
 cbMicroStep.Enabled := true;
 cbDailyMicroStep.Enabled := true;
// tbPosition.Enabled := true;
 btnSetPos.Enabled := true;
 btnSetDE.Enabled := true;
 lePositionRA.Enabled := true;
 lePositionDE.Enabled := true;
 lePosRA_EQ.Enabled := true;
 lePosDE_EQ.Enabled := true;
 cbGoto.Enabled := true;
 btnGoto.Enabled := true;
 btnSetNavSpeed.Enabled := true;
 btnSetDailySpeedRA.Enabled := true;
 btnSetDailySpeedDE.Enabled := true;
 cbDRotRA.Enabled := true;
 cbDRotDE.Enabled := true;
 btnDERollForward.Enabled := true;
 btnDEStepForward.Enabled := true;
 btnDERollBackward.Enabled := true;
 btnDEStepBackward.Enabled := true;
 btnSave.Enabled := true;
 eNewPosRA.Enabled := true;
 eNewPosDE.Enabled := true;
 btnDelete.Enabled := true;

 cbGotoChange(Sender);

 Settings.WriteInteger('General','PORT', cbPort.ItemIndex);
end;



procedure TForm1.btnRABackwardRoll(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
var  str:string;
begin
  Mounter.Roll(MOTOR_RA, STEP_BACKWARD);

  btnStepRight.Enabled := false;
  btnStepLeft.Enabled := false;
  btnRelease.Enabled := false;
  cbMicroStep.Enabled := false;
  cbDailyMicroStep.Enabled := false;

  str :=ExtractFilePath(Application.ExeName) + 'ding_new.wav';
  if cbSound.Checked then sndPlaySound(@str[1], SND_NODEFAULT Or SND_ASYNC Or SND_LOOP);
end;

procedure TForm1.btnRABackwardStop(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
  Mounter.Stop(MOTOR_RA);
  StopRolling;
  if cbSound.Checked then sndPlaySound(nil, 0); // Stops the sound
end;

procedure TForm1.btnPingClick(Sender: TObject);
begin
 Mounter.Ping;
end;

procedure TForm1.btnReleaseClick(Sender: TObject);
begin
   Mounter.Stop(MOTOR_DE);
   Mounter.Stop(MOTOR_RA);
   Mounter.Release;
end;


procedure TForm1.btnSetPosClick(Sender: TObject);
begin
   if rbRAHours.Checked then
      Mounter.PosRA_EQ := StrToFloat(eNewPosRA.Text)
   else
      Mounter.PosRA_EQ := StrToFloat(eNewPosRA.Text)/15;
end;

procedure TForm1.btnSetTimeClick(Sender: TObject);
var t:TDateTime;
begin
  t := Now;
  Mounter.RTCDateTimeStr := IntToStr(YearOf(t)) + '/' + IntToStr(MonthOf(t)) + '/' + IntToStr(DayOf(t)) +  '  ' + IntToStr(HourOf(t)) + ':' + IntToStr(MinuteOf(t)) + ':' + IntToStr(SecondOf(t));
end;

procedure TForm1.btnSetDailySpeedDEClick(Sender: TObject);
begin
  Mounter.DailyCurrentSpeedDE := StrToFloat(leDailySpeedDE.Text );
end;

procedure TForm1.btnSetDailySpeedRAClick(Sender: TObject);
begin
  Mounter.DailyCurrentSpeedRA := StrToFloat(leDailySpeedRA.Text );
end;

procedure TForm1.btnSetDEClick(Sender: TObject);
begin
   Mounter.PosDE_EQ := StrToFloat(eNewPosDE.Text);
end;

procedure TForm1.btnSetNavSpeedClick(Sender: TObject);
begin
  Mounter.NavCurrentSpeed := StrToFloat(leNavSpeed.Text);
end;

procedure TForm1.btnDERollBackwardMouseDown(Sender: TObject;
  Button: TMouseButton; Shift: TShiftState; X, Y: Integer);
var  str:string;
begin
   Mounter.Roll(MOTOR_DE, STEP_BACKWARD);
   btnStepRight.Enabled := false;
   btnStepLeft.Enabled := false;
   btnRelease.Enabled := false;
   cbMicroStep.Enabled := false;
   cbDailyMicroStep.Enabled := false;
   str :=ExtractFilePath(Application.ExeName) + 'ding_new.wav';
   if cbSound.Checked then sndPlaySound(@str[1], SND_NODEFAULT Or SND_ASYNC Or SND_LOOP);
end;

procedure TForm1.btnDERollBackwardMouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
  Mounter.Stop(MOTOR_DE);
  StopRolling;
  if cbSound.Checked then sndPlaySound(nil, 0); // Stops the sound
end;

procedure TForm1.btnDERollForwardMouseDown(Sender: TObject;
  Button: TMouseButton; Shift: TShiftState; X, Y: Integer);
var  str:string;
begin
   Mounter.Roll(MOTOR_DE, STEP_FORWARD);
   cbMicroStep.Enabled := false;
   cbDailyMicroStep.Enabled := false;
   str :=ExtractFilePath(Application.ExeName) + 'ding_new.wav';
   if cbSound.Checked then sndPlaySound(@str[1], SND_NODEFAULT Or SND_ASYNC Or SND_LOOP);
end;


procedure TForm1.btnDERollForwardMouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
  Mounter.Stop(MOTOR_DE);
  StopRolling;
  if cbSound.Checked then sndPlaySound(nil, 0); // Stops the sound
end;

procedure TForm1.btnRAForwardRoll(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
var  str:string;
begin
   Mounter.Roll(MOTOR_RA, STEP_FORWARD);
   cbMicroStep.Enabled := false;
   cbDailyMicroStep.Enabled := false;
   str :=ExtractFilePath(Application.ExeName) + 'ding_new.wav';
   if cbSound.Checked then sndPlaySound(@str[1], SND_NODEFAULT Or SND_ASYNC Or SND_LOOP);
end;

procedure TForm1.btnRAForwardStop(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
  Mounter.Stop(MOTOR_RA);
  StopRolling;
  if cbSound.Checked then sndPlaySound(nil, 0); // Stops the sound
end;

procedure TForm1.btnRAStepBack(Sender: TObject);
begin
//   Mounter.Step(MOTOR_RA, STEP_BACKWARD);
   Mounter.Roll(MOTOR_RA, STEP_BACKWARD, StrToInt(eStepDuration.Text))
end;

procedure TForm1.btnRAStepForward(Sender: TObject);
begin
//   Mounter.Step(MOTOR_RA, STEP_FORWARD);
   Mounter.Roll(MOTOR_RA, STEP_FORWARD, StrToInt(eStepDuration.Text))
end;

procedure TForm1.btnStopClick(Sender: TObject);
var i:integer;
begin

  tEarthRotation.Enabled := false;

  Mounter.Disconnect;

{  Settings.WriteInteger('General', 'MAX_POS', Mounter.MaxPosition);
  Settings.WriteInteger('General', 'POS', Mounter.Position);
  Settings.WriteBool('General','RANGE_CHECK', Mounter.RangeCheck);

  Settings.EraseSection('Positions');
  Settings.WriteInteger('General', 'POSITION_COUNT', cbGoto.Items.Count-2);
  for i := 2 to cbGoto.Items.Count-1 do
    Settings.WriteString('Positions', IntToStr(i-1), cbGoto.Items[i]);}

  TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).Positions.Clear;
  for i := 0 to cbGoto.Items.Count-1 do
    TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).Positions.Add(cbGoto.Items[i]);
  TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).fGearRatioRA := Mounter.fGearRatioRA;
  TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).fGearRatioDE := Mounter.fGearRatioDE;
  TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).ComPort := cbPort.Text;
  TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).IsDebug := Mounter.IsDebug;

  TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).Save;

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
  pgNavSpeed.Enabled := false;
  pgDailySpeedRA.Enabled := false;
  pgDailySpeedDE.Enabled := false;
  btnRelease.Enabled := false;
  cbMicroStep.Enabled := false;
  cbDailyMicroStep.Enabled := false;
//  tbPosition.Enabled := false;
  btnSetPos.Enabled := false;
  btnSetDE.Enabled := false;
  lePositionRA.Enabled := false;
  lePositionDE.Enabled := false;
  lePosRA_EQ.Enabled := false;
  lePosDE_EQ.Enabled := false;
  btnGoto.Enabled := false;
  cbGoto.Enabled := false;
  btnSetNavSpeed.Enabled := false;
  btnSetDailySpeedRA.Enabled := false;
  btnSetDailySpeedDE.Enabled := false;
  cbDRotRA.Enabled := false;
  cbDRotDE.Enabled := false;
  btnDERollForward.Enabled := false;
  btnDEStepForward.Enabled := false;
  btnDERollBackward.Enabled := false;
  btnDEStepBackward.Enabled := false;
  btnSave.Enabled := false;
  eNewPosRA.Enabled := false;
  eNewPosDE.Enabled := false;
  btnDelete.Enabled := false;


end;

procedure TForm1.Button1Click(Sender: TObject);
begin

  Mounter.fGearRatioRA := StrToInt(leGearRatioRA.Text);
  Mounter.fGearRatioDE := StrToInt(leGearRatioDE.Text);

end;

procedure TForm1.Button2Click(Sender: TObject);
begin
  Mounter.ReleaseTime := StrToInt(eReleaseTime.Text);
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
   try

    Mounter.NavMaxSpeed := StrToFloat(btneMaxNavSpeed.Text);
    Mounter.NavMinSpeed := StrToFloat(btneMinNavSpeed.Text);
    Mounter.DailyMinSpeedRA := StrToFloat(btneMinDailyRASpeed.Text);
    Mounter.DailyMaxSpeedRA := StrToFloat(btneMaxDailyRASpeed.Text);
    Mounter.DailyMinSpeedDE := StrToFloat(btneMinDailyDESpeed.Text);
    Mounter.DailyMaxSpeedDE := StrToFloat(btneMaxDailyDESpeed.Text);

   except
     On EConvertError do
     begin
       exit;
     end;
   end;
end;

procedure TForm1.btnDEStepForwardClick(Sender: TObject);
begin
//   Mounter.Step(MOTOR_DE, STEP_FORWARD);
   Mounter.Roll(MOTOR_DE, STEP_FORWARD, StrToInt(eStepDuration.Text));
end;



procedure TForm1.btnDEStepBackwardClick(Sender: TObject);
begin
//   Mounter.Step(MOTOR_DE, STEP_BACKWARD);
   Mounter.Roll(MOTOR_DE, STEP_BACKWARD, StrToInt(eStepDuration.Text))
end;

procedure TForm1.btnGotoClick(Sender: TObject);
var ra, de:double;
begin

  if rbRAHours.Checked then
    ra := StrToFloat(eNewPosRA.Text)
  else
    ra := StrToFloat(eNewPosRA.Text)/15;
  de := StrToFloat(eNewPosDE.Text);

  Mounter.GoToPositionEQ(ra, de);

  btnStepLeft.Enabled := false;
  btnStepRight.Enabled := false;
  btnLeft.Enabled := false;
  btnRight.Enabled := false;
  pgNavSpeed.Enabled := false;
  pgDailySpeedRA.Enabled := false;
  pgDailySpeedDE.Enabled := false;
  cbMicroStep.Enabled := false;
  cbDailyMicroStep.Enabled := false;
  btnSetPos.Enabled := false;
  btnSetDE.Enabled := true;

end;

procedure TForm1.StopRolling;
begin
  btnStepRight.Enabled := true;
  btnStepLeft.Enabled := true;
  btnRelease.Enabled := true;
  cbMicroStep.Enabled := true;
  cbDailyMicroStep.Enabled := true;
end;

procedure TForm1.tDateTimeTempTimer(Sender: TObject);
begin
  if ((btnConnect.Enabled=false)and(pgMounter.TabIndex = 1)) then
    begin
      Mounter.GetTemperature;
      Mounter.GetDateTime;
    end;
end;

procedure TForm1.tEarthRotationTimer(Sender: TObject);
begin
  if (btnConnect.Enabled=false) then
    begin
     Mounter.UpdatePosition;
    end;
end;

procedure TForm1.cbDailyMicroStepChange(Sender: TObject);
begin
  if (IsChangingMicroStep = false) then
     Mounter.DailyMicroStep := cbDailyMicroStep.ItemIndex;
end;

procedure TForm1.cbDRotDEClick(Sender: TObject);
begin
  Mounter.DECompensation := cbDRotDE.Checked;
end;

procedure TForm1.cbDRotRAClick(Sender: TObject);
begin
  Mounter.DuirnalRotation := cbDRotRA.Checked;
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
      cbPort.ItemIndex := cbPort.Items.IndexOf(TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).ComPort)
    end;

end;


procedure TForm1.UpdatePosBtn;
begin
  if cbGoto.ItemIndex=-1 then
     begin
      btnDelete.Enabled := false;
      if length(cbGoto.Text)=0 then
        btnSave.Enabled := false
       else
        btnSave.Enabled := true;
     end
    else
     begin
      btnSave.Enabled := true;
      btnDelete.Enabled := true;
     end;
end;


procedure TForm1.cbGotoChange(Sender: TObject);
begin
   if cbGoto.ItemIndex<>-1 then
     begin
       if rbRAHours.Checked then
         eNewPosRA.Text := Copy(cbGoto.Items[cbGoto.ItemIndex], pos('(',cbGoto.Items[cbGoto.ItemIndex])+1, pos(';',cbGoto.Items[cbGoto.ItemIndex])-pos('(',cbGoto.Items[cbGoto.ItemIndex])-1)
       else
         eNewPosRA.Text := FloatToStrF((StrToFloat(Copy(cbGoto.Items[cbGoto.ItemIndex], pos('(',cbGoto.Items[cbGoto.ItemIndex])+1, pos(';',cbGoto.Items[cbGoto.ItemIndex])-pos('(',cbGoto.Items[cbGoto.ItemIndex])-1))*15), ffFixed, 15, 6);
       eNewPosDE.Text := Copy(cbGoto.Items[cbGoto.ItemIndex], pos(';',cbGoto.Items[cbGoto.ItemIndex])+1, pos(')',cbGoto.Items[cbGoto.ItemIndex])-pos(';',cbGoto.Items[cbGoto.ItemIndex])-1);
     end;

   UpdatePosBtn;
end;


procedure TForm1.btnDeleteClick(Sender: TObject);
begin
  if (cbGoto.ItemIndex>=0)and(MessageDlg('Do you really want to delete object?', mtConfirmation, [mbOK, mbCancel], 0)=mrOk) then
      cbGoto.Items.Delete(cbGoto.ItemIndex);

  if cbGoto.Items.Count=0 then
    cbGoto.Text := ''
   else
   cbGoto.ItemIndex:=0;

  UpdatePosBtn;
end;

procedure TForm1.btnFocuserDeleteClick(Sender: TObject);
begin
   if cbFocuser.ItemIndex>=0 then
        cbFocuser.Items.Delete(cbFocuser.ItemIndex);
   cbFocuser.OnChange(Sender);
end;

procedure TForm1.btnFocuserSaveClick(Sender: TObject);
var p:TMounterItem;
    i:integer;
begin
  if cbFocuser.ItemIndex<0 then
   begin
       p:= TMounterItem.Create(cbFocuser.Text, cbPort.Text, Mounter);
       p.fGearRatioRA := Mounter.fGearRatioRA;
       p.fGearRatioDE := Mounter.fGearRatioDE;
       for i := 2 to cbGoto.Items.Count-1 do
          p.Positions.Add(cbGoto.Items[i]);
       p.Save;
       cbFocuser.Items.AddObject(p.Name, p);
       cbFocuser.ItemIndex := cbFocuser.Items.IndexOf(p.Name);
   end
    else
      begin
        TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).fGearRatioRA := Mounter.fGearRatioRA;
        TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).fGearRatioDE := Mounter.fGearRatioDE;
        TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).ComPort := cbPort.Text;
        TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).IsDebug := Mounter.IsDebug;
        TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).Positions.Clear;
        for i := 2 to cbGoto.Items.Count-1 do
          TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).Positions.Add(cbGoto.Items[i]);
        TMounterItem(cbFocuser.Items.Objects[cbFocuser.ItemIndex]).Save;
      end;
  cbFocuser.OnChange(Sender);
end;

procedure TForm1.btnSaveClick(Sender: TObject);
var  s:string;
     i:integer;
begin
  if cbGoto.ItemIndex = -1 then
    begin
     //  add new position with a new label
       cbGoto.ItemIndex := cbGoto.Items.Add(cbGoto.Text + ' (' + FloatToStrF(Mounter.PosRA_EQ, ffFixed, 15, 5)+ ';' + FloatToStrF(Mounter.PosDE_EQ, ffFixed, 15, 5) + ')' );
    end
   else
    if (MessageDlg('Are you sure you want to replace coordinates?', mtConfirmation, [mbOK, mbCancel], 0)=mrOk) then
     begin
    //  save new position to the old label
       if Length(cbGoto.Text)=0 then
         exit;
       i := Pos('(', cbGoto.Text);
       if i=0 then
         s := cbGoto.Text
       else
         s := LeftStr(cbGoto.Text, i-2);

       s := s +  ' (' + FloatToStrF(Mounter.PosRA_EQ, ffFixed, 15, 5)+ ';' + FloatToStrF(Mounter.PosDE_EQ, ffFixed, 15, 5) + ')';
       cbGoto.Items[cbGoto.ItemIndex] := s;
       cbGoto.ItemIndex := cbGoto.Items.IndexOf(s);
    end;
  cbGotoChange(Sender);
end;

procedure TForm1.cbMicroStepChange(Sender: TObject);
begin
  if (IsChangingMicroStep = false) then
     Mounter.NavMicroStep := cbMicroStep.ItemIndex;
end;


procedure TForm1.cbRCPClick(Sender: TObject);
begin
   Mounter.IsRCP := cbRCP.Checked;
end;

procedure TForm1.cbShowDebugMsgClick(Sender: TObject);
begin
  Mounter.IsDebug := cbShowDebugMsg.Checked;
end;

procedure TForm1.cbTimeClick(Sender: TObject);
begin
  Mounter.IsTime := cbTime.Checked;
end;

procedure TForm1.eDataLineKeyDown(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
  if Key=13 then
    btnSendClick(Sender);
end;

procedure TForm1.eObjectNameChange(Sender: TObject);
begin
   UpdatePosBtn;
end;

procedure TForm1.FormClose(Sender: TObject; var Action: TCloseAction);
var i:integer;
begin

  if (btnStop.Enabled) then
      Mounter.Disconnect;

  for i := 1 to cbFocuser.Items.Count do
    begin
      TMounterItem(cbFocuser.Items.Objects[i-1]).Save;
      Settings.WriteString('Focusers', IntToStr(i), TMounterItem(cbFocuser.Items.Objects[i-1]).Name);
    end;
  Settings.WriteInteger('General', 'Focuser Count', cbFocuser.Items.Count);
  Settings.WriteBool('General', 'Sound', cbSound.Checked);
  Settings.WriteBool('General', 'RA:Hours', rbRAHours.Checked);
  Settings.WriteBool('General', 'RA:Degrees', rbRADegrees.Checked);

  Settings.UpdateFile;
  Settings.Destroy;

// удаляем комбинацию
  UnRegisterHotKey(Handle, HotKeyUp1);
  UnRegisterHotKey(Handle, HotKeyDown1);
  UnRegisterHotKey(Handle, HotKeyLeft1);
  UnRegisterHotKey(Handle, HotKeyRight1);
  UnRegisterHotKey(Handle, HotKeyUp2);
  UnRegisterHotKey(Handle, HotKeyDown2);
  UnRegisterHotKey(Handle, HotKeyLeft2);
  UnRegisterHotKey(Handle, HotKeyRight2);
  UnRegisterHotKey(Handle, HotKeyUp3);
  UnRegisterHotKey(Handle, HotKeyDown3);
  UnRegisterHotKey(Handle, HotKeyLeft3);
  UnRegisterHotKey(Handle, HotKeyRight3);
  UnRegisterHotKey(Handle, HotKeyStop);
  // удаляем атом
  GlobalDeleteAtom(HotKeyUp1);
  GlobalDeleteAtom(HotKeyDown1);
  GlobalDeleteAtom(HotKeyLeft1);
  GlobalDeleteAtom(HotKeyRight1);
  GlobalDeleteAtom(HotKeyUp2);
  GlobalDeleteAtom(HotKeyDown2);
  GlobalDeleteAtom(HotKeyLeft2);
  GlobalDeleteAtom(HotKeyRight2);
  GlobalDeleteAtom(HotKeyUp3);
  GlobalDeleteAtom(HotKeyDown3);
  GlobalDeleteAtom(HotKeyLeft3);
  GlobalDeleteAtom(HotKeyRight3);
  GlobalDeleteAtom(HotKeyStop);

end;

procedure TForm1.FormCreate(Sender: TObject);
var name:string;
    ifile:THandle;
    i,c:integer;
    P:TMounterItem;

//    imList : TImageList;
//    bmap   : TBitmap;


begin

   HotKeyUp1 := GlobalAddAtom('MounterHotKeyUp1');
   HotKeyDown1 := GlobalAddAtom('MounterHotKeyDown1');
   HotKeyLeft1 := GlobalAddAtom('MounterHotKeyLeft1');
   HotKeyRight1 := GlobalAddAtom('MounterHotKeyRight1');
   HotKeyUp2 := GlobalAddAtom('MounterHotKeyUp2');
   HotKeyDown2 := GlobalAddAtom('MounterHotKeyDown2');
   HotKeyLeft2 := GlobalAddAtom('MounterHotKeyLeft2');
   HotKeyRight2 := GlobalAddAtom('MounterHotKeyRight2');
   HotKeyUp3 := GlobalAddAtom('MounterHotKeyUp3');
   HotKeyDown3 := GlobalAddAtom('MounterHotKeyDown3');
   HotKeyLeft3 := GlobalAddAtom('MounterHotKeyLeft3');
   HotKeyRight3 := GlobalAddAtom('MounterHotKeyRight3');
   HotKeyStop := GlobalAddAtom('MounterHotKeyStop');

   RegisterHotKey(Handle, HotKeyUp1, MOD_CONTROL or MOD_WIN, VK_UP);
   RegisterHotKey(Handle, HotKeyDown1, MOD_CONTROL or MOD_WIN, VK_DOWN);
   RegisterHotKey(Handle, HotKeyLeft1, MOD_CONTROL or MOD_WIN, VK_LEFT);
   RegisterHotKey(Handle, HotKeyRight1, MOD_CONTROL or MOD_WIN, VK_RIGHT);
   RegisterHotKey(Handle, HotKeyUp2, MOD_CONTROL or MOD_ALT, VK_UP);
   RegisterHotKey(Handle, HotKeyDown2, MOD_CONTROL or MOD_ALT, VK_DOWN);
   RegisterHotKey(Handle, HotKeyLeft2, MOD_CONTROL or MOD_ALT, VK_LEFT);
   RegisterHotKey(Handle, HotKeyRight2, MOD_CONTROL or MOD_ALT, VK_RIGHT);
   RegisterHotKey(Handle, HotKeyUp3, MOD_WIN or MOD_ALT, VK_UP);
   RegisterHotKey(Handle, HotKeyDown3, MOD_WIN or MOD_ALT, VK_DOWN);
   RegisterHotKey(Handle, HotKeyLeft3, MOD_WIN or MOD_ALT, VK_LEFT);
   RegisterHotKey(Handle, HotKeyRight3, MOD_WIN or MOD_ALT, VK_RIGHT);
   RegisterHotKey(Handle, HotKeyStop, MOD_WIN or MOD_ALT, VK_SPACE);


{   imList := TImageList.Create(Self);
   bmap := TBitmap.Create();
   bmap.LoadFromFile('OK.bmp');
   imList.Add(bmap,nil);
   btneMaxNavSpeed.Images := imList;
   btneMaxNavSpeed.RightButton.ImageIndex := 1;
   btneMaxNavSpeed.RightButton.DisabledImageIndex := 1;
   btneMaxNavSpeed.RightButton.PressedImageIndex := 1;
   btneMaxNavSpeed.RightButton.HotImageIndex := 1;
 }



   name := ExtractFilePath(Application.ExeName) + '\' + 'Mounter.ini';
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
        p:= TMounterItem.Create('Focuser1');
        cbFocuser.Items.AddObject(p.Name, p);
      end
   else
    for i := 1 to c do
     begin
       p:= TMounterItem.Create(Settings.ReadString('Focusers', IntToStr(i), ''));
       cbFocuser.Items.AddObject(p.Name, p);
     end;
   cbFocuser.ItemIndex := 0;
   cbFocuser.OnChange(Sender);

   cbSound.Checked := Settings.ReadBool('General', 'Sound', false);
   rbRAHours.Checked := Settings.ReadBool('General', 'RA:Hours', true);
   rbRADegrees.Checked := Settings.ReadBool('General', 'RA:Degrees', false);

//   cbPort.ItemIndex := Settings.ReadInteger('General', 'PORT', 0);

// cbShowDebugMsg.Checked := Settings.ReadBool('General', 'SHOW_DEBUG', false);
   IsChangingSpeed := false;
   IsChangingMicroStep := false;
   Mounter := TMounter.Init;
end;


procedure TForm1.hkLeftEnter(Sender: TObject);
begin
  Mounter.Roll(MOTOR_RA, STEP_BACKWARD);
end;

procedure TForm1.pgMounterExit(Sender: TObject);
begin
   Settings.WriteBool('General', 'SHOW_DEBUG', cbShowDebugMsg.Checked);
end;

procedure TForm1.pgDailySpeedDEMouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
   if (pgDailySpeedDE.Enabled=false)then
      exit;
    Form1.IsChangingSpeed := true;
    pgDailySpeedDEMouseMove(Sender, Shift, X, Y);
end;

procedure TForm1.pgDailySpeedDEMouseMove(Sender: TObject; Shift: TShiftState; X,
  Y: Integer);
begin
   if (IsChangingSpeed)and(pgDailySpeedDE.Enabled)then
           pgDailySpeedDE.Position :=round(pgDailySpeedDE.Min + x*(pgDailySpeedDE.Max-pgDailySpeedDE.Min)/(pgDailySpeedDE.Width));
end;

procedure TForm1.pgDailySpeedDEMouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
   if (pgDailySpeedDE.Enabled=false)then
      exit;
   Form1.IsChangingSpeed := false;
   Mounter.DailyCurrentSpeedDE := Mounter.DailyMinSpeedDE + (Mounter.DailyMaxSpeedDE-Mounter.DailyMinSpeedDE)*pgDailySpeedDE.Position/(Form1.pgDailySpeedDE.Max-Form1.pgDailySpeedDE.Min);
end;

procedure TForm1.pgDailySpeedRAMouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
   if (pgDailySpeedRA.Enabled=false)then
      exit;
    Form1.IsChangingSpeed := true;
    pgDailySpeedRAMouseMove(Sender, Shift, X, Y);
end;

procedure TForm1.pgDailySpeedRAMouseMove(Sender: TObject; Shift: TShiftState; X,
  Y: Integer);
begin
   if (IsChangingSpeed)and(pgDailySpeedRA.Enabled)then
           pgDailySpeedRA.Position :=round(pgDailySpeedRA.Min + x*(pgDailySpeedRA.Max-pgDailySpeedRA.Min)/(pgDailySpeedRA.Width));
end;

procedure TForm1.pgDailySpeedRAMouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
   if (pgDailySpeedRA.Enabled=false)then
      exit;
   Form1.IsChangingSpeed := false;

   Mounter.DailyCurrentSpeedRA := Mounter.DailyMinSpeedRA + (Mounter.DailyMaxSpeedRA-Mounter.DailyMinSpeedRA)*pgDailySpeedRA.Position/(Form1.pgDailySpeedRA.Max-Form1.pgDailySpeedRA.Min);
end;

procedure TForm1.pgNavSpeedMouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
   if (pgNavSpeed.Enabled=false)then
      exit;
    Form1.IsChangingSpeed := true;
    pgNavSpeedMouseMove(Sender, Shift, X, Y);
end;

procedure TForm1.pgNavSpeedMouseMove(Sender: TObject; Shift: TShiftState; X,
  Y: Integer);
begin
   if (IsChangingSpeed)and(pgNavSpeed.Enabled)then
        pgNavSpeed.Position := round(pgNavSpeed.Min + x*(pgNavSpeed.Max-pgNavSpeed.Min)/(pgNavSpeed.Width));
end;

procedure TForm1.pgNavSpeedMouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
   if (pgNavSpeed.Enabled=false)then
      exit;
   Form1.IsChangingSpeed := false;
   Mounter.NavCurrentSpeed := Mounter.NavMinSpeed + (Mounter.NavMaxSpeed-Mounter.NavMinSpeed)*pgNavSpeed.Position/(Form1.pgNavSpeed.Max-Form1.pgNavSpeed.Min);

end;


procedure TForm1.btnSendClick(Sender: TObject);
begin

  Mounter.SendDebugCommand(StrToInt(eDataline.Text));
end;

end.
