object Form1: TForm1
  Left = 209
  Top = 196
  Caption = 'Focuser Control Panel'
  ClientHeight = 420
  ClientWidth = 224
  Color = clBtnFace
  Constraints.MinHeight = 378
  Constraints.MinWidth = 230
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnClose = FormClose
  OnCreate = FormCreate
  DesignSize = (
    224
    420)
  PixelsPerInch = 96
  TextHeight = 13
  object Panel2: TPanel
    Left = 0
    Top = 0
    Width = 224
    Height = 68
    Align = alTop
    TabOrder = 0
    DesignSize = (
      224
      68)
    object btnConnect: TButton
      Left = 159
      Top = 9
      Width = 58
      Height = 25
      Anchors = [akTop, akRight]
      Caption = 'Connect'
      TabOrder = 1
      OnClick = btnConnectClick
    end
    object btnStop: TButton
      Left = 159
      Top = 36
      Width = 58
      Height = 25
      Anchors = [akTop, akRight]
      Caption = 'Close'
      Enabled = False
      TabOrder = 2
      OnClick = btnStopClick
    end
    object cbPort: TComboBox
      Left = 100
      Top = 11
      Width = 56
      Height = 21
      ItemIndex = 0
      TabOrder = 0
      Text = 'COM1'
      Items.Strings = (
        'COM1'
        'COM2'
        'COM3'
        'COM4'
        'COM5'
        'COM6'
        'COM7'
        'COM8'
        'COM9'
        'COM10'
        'COM11'
        'COM12'
        'COM13'
        'COM14'
        'COM15'
        'COM16')
    end
    object cbFocuser: TComboBox
      Left = 4
      Top = 11
      Width = 93
      Height = 21
      TabOrder = 3
      Text = 'focuser1'
      OnChange = cbFocuserChange
    end
    object btnFocuserSave: TButton
      Left = 40
      Top = 36
      Width = 57
      Height = 25
      Caption = 'Save'
      TabOrder = 4
      OnClick = btnFocuserSaveClick
    end
    object btnFocuserDelete: TButton
      Left = 100
      Top = 36
      Width = 56
      Height = 25
      Caption = 'Delete'
      TabOrder = 5
      OnClick = btnFocuserDeleteClick
    end
  end
  object PageControl1: TPageControl
    Left = 0
    Top = 74
    Width = 224
    Height = 343
    ActivePage = TabSheet1
    Anchors = [akLeft, akTop, akRight, akBottom]
    TabOrder = 1
    OnExit = PageControl1Exit
    object TabSheet1: TTabSheet
      Caption = 'Focuser'
      object lMinSpeed: TLabel
        Left = 24
        Top = 62
        Width = 6
        Height = 13
        Alignment = taRightJustify
        Caption = '0'
      end
      object lMaxSpeed: TLabel
        Left = 182
        Top = 62
        Width = 12
        Height = 13
        Caption = '30'
      end
      object lMinPos: TLabel
        Left = 24
        Top = 88
        Width = 6
        Height = 13
        Caption = '0'
      end
      object lMaxPos: TLabel
        Left = 182
        Top = 88
        Width = 12
        Height = 13
        Caption = '30'
      end
      object btnRelease: TButton
        Left = 119
        Top = 276
        Width = 75
        Height = 25
        Caption = 'Release'
        Enabled = False
        TabOrder = 4
        OnClick = btnReleaseClick
      end
      object btnRight: TButton
        Left = 105
        Top = 16
        Width = 47
        Height = 25
        Caption = '>>'
        Enabled = False
        TabOrder = 3
        OnMouseDown = btnRightMouseDown
        OnMouseUp = btnRightMouseUp
      end
      object btnStepRight: TButton
        Left = 154
        Top = 16
        Width = 41
        Height = 25
        Caption = '>'
        Enabled = False
        TabOrder = 1
        OnClick = btnStepRightClick
      end
      object btnStepLeft: TButton
        Left = 8
        Top = 16
        Width = 42
        Height = 25
        Caption = '<'
        Enabled = False
        TabOrder = 0
        OnClick = btnStepLeftClick
      end
      object btnLeft: TButton
        Left = 52
        Top = 16
        Width = 47
        Height = 25
        Caption = '<<'
        Enabled = False
        TabOrder = 2
        OnMouseDown = btnLeftMouseDown
        OnMouseUp = btnLeftMouseUp
      end
      object lePosition: TLabeledEdit
        Left = 55
        Top = 123
        Width = 58
        Height = 21
        EditLabel.Width = 37
        EditLabel.Height = 13
        EditLabel.Caption = 'Position'
        EditLabel.Color = clBtnText
        EditLabel.ParentColor = False
        Enabled = False
        LabelPosition = lpLeft
        ReadOnly = True
        TabOrder = 5
      end
      object btnSet0: TButton
        Left = 119
        Top = 122
        Width = 75
        Height = 25
        Caption = 'Set 0'
        Enabled = False
        TabOrder = 6
        OnClick = btnSet0Click
      end
      object btnSetMax: TButton
        Left = 119
        Top = 151
        Width = 75
        Height = 25
        Caption = 'Set Max'
        Enabled = False
        TabOrder = 7
        OnClick = btnSetMaxClick
      end
      object pgSpeed: TProgressBar
        Left = 33
        Top = 60
        Width = 147
        Height = 17
        Enabled = False
        Min = 10
        Position = 10
        TabOrder = 8
        OnMouseDown = pgSpeedMouseDown
        OnMouseMove = pgSpeedMouseMove
        OnMouseUp = pgSpeedMouseUp
      end
      object pgPosition: TProgressBar
        Left = 33
        Top = 86
        Width = 147
        Height = 17
        Enabled = False
        Max = 30
        TabOrder = 9
      end
      object cbRangeCheck: TCheckBox
        Left = 16
        Top = 153
        Width = 97
        Height = 17
        Caption = 'Range Check'
        Enabled = False
        TabOrder = 10
        OnClick = cbRangeCheckClick
      end
      object cbGoto: TComboBox
        Left = 16
        Top = 182
        Width = 178
        Height = 21
        TabOrder = 11
        OnChange = cbGotoChange
        Items.Strings = (
          'Leftmost (0)'
          'Rightmost (Max)')
      end
      object btnGoto: TButton
        Left = 119
        Top = 209
        Width = 75
        Height = 25
        Caption = 'Goto'
        Enabled = False
        TabOrder = 12
        OnClick = btnGotoClick
      end
      object btnSave: TButton
        Left = 16
        Top = 209
        Width = 75
        Height = 25
        Caption = 'Save'
        Enabled = False
        TabOrder = 13
        OnClick = btnSaveClick
      end
      object btnDelete: TButton
        Left = 16
        Top = 240
        Width = 75
        Height = 25
        Caption = 'Delete'
        Enabled = False
        TabOrder = 14
        OnClick = btnDeleteClick
      end
      object cbPower: TCheckBox
        Left = 16
        Top = 282
        Width = 77
        Height = 17
        Caption = 'Power'
        Enabled = False
        TabOrder = 15
        OnClick = cbPowerClick
      end
    end
    object Debug: TTabSheet
      Caption = 'Debug'
      ImageIndex = 1
      DesignSize = (
        216
        315)
      object Label2: TLabel
        Left = 106
        Top = 262
        Width = 49
        Height = 13
        Anchors = [akLeft, akBottom]
        Caption = 'Microstep:'
      end
      object Label1: TLabel
        Left = 24
        Top = 236
        Width = 83
        Height = 13
        Alignment = taRightJustify
        Anchors = [akLeft, akBottom]
        Caption = 'Release Timeout:'
      end
      object btnClearMem: TButton
        Left = 3
        Top = 287
        Width = 48
        Height = 24
        Anchors = [akRight, akBottom]
        Caption = 'Clear'
        TabOrder = 2
        OnClick = btnClearMemClick
      end
      object btnSend: TButton
        Left = 57
        Top = 258
        Width = 43
        Height = 24
        Anchors = [akLeft, akBottom]
        Caption = 'Send'
        Enabled = False
        TabOrder = 1
        OnClick = btnSendClick
      end
      object eDataLine: TEdit
        Left = 3
        Top = 258
        Width = 48
        Height = 21
        Hint = #1092#1086#1088#1084#1072#1090' '#1076#1083#1103' '#1093#1077#1082#1089#1072' $3F'
        Anchors = [akLeft, akBottom]
        Enabled = False
        TabOrder = 0
        Text = '$'
        OnKeyDown = eDataLineKeyDown
      end
      object memOut: TMemo
        Left = 3
        Top = 0
        Width = 210
        Height = 185
        Anchors = [akLeft, akTop, akRight, akBottom]
        ScrollBars = ssVertical
        TabOrder = 4
      end
      object btnPing: TButton
        Left = 57
        Top = 287
        Width = 43
        Height = 25
        Anchors = [akLeft, akBottom]
        Caption = 'Ping'
        Enabled = False
        TabOrder = 3
        OnClick = btnPingClick
      end
      object cbShowDebugMsg: TCheckBox
        Left = 3
        Top = 191
        Width = 148
        Height = 17
        Anchors = [akLeft, akBottom]
        Caption = 'Show debug message'
        TabOrder = 5
        OnClick = cbShowDebugMsgClick
      end
      object cbMicroStep: TComboBox
        Left = 161
        Top = 259
        Width = 42
        Height = 21
        Style = csDropDownList
        Anchors = [akLeft, akBottom]
        Enabled = False
        TabOrder = 6
        OnChange = cbMicroStepChange
        Items.Strings = (
          '1'
          '2'
          '4'
          '8'
          '16')
      end
      object leSpeed: TLabeledEdit
        Left = 116
        Top = 290
        Width = 50
        Height = 21
        Anchors = [akLeft, akBottom]
        EditLabel.Width = 7
        EditLabel.Height = 13
        EditLabel.Caption = 'V'
        LabelPosition = lpLeft
        TabOrder = 7
      end
      object btnSetSpeed: TButton
        Left = 168
        Top = 287
        Width = 35
        Height = 25
        Anchors = [akLeft, akBottom]
        Caption = 'Set'
        TabOrder = 8
        OnClick = btnSetSpeedClick
      end
      object cbRCP: TCheckBox
        Left = 3
        Top = 211
        Width = 97
        Height = 17
        Anchors = [akLeft, akBottom]
        Caption = 'Remote Control'
        Checked = True
        State = cbChecked
        TabOrder = 9
        OnClick = cbRCPClick
      end
      object Button1: TButton
        Left = 168
        Top = 231
        Width = 35
        Height = 25
        Anchors = [akLeft, akBottom]
        Caption = 'Set'
        TabOrder = 10
        OnClick = Button1Click
      end
      object eReleaseTime: TEdit
        Left = 116
        Top = 233
        Width = 46
        Height = 21
        Anchors = [akLeft, akBottom]
        TabOrder = 11
        Text = '3'
      end
    end
    object About: TTabSheet
      Caption = 'About'
      ImageIndex = 2
      DesignSize = (
        216
        315)
      object Memo1: TMemo
        Left = -2
        Top = -2
        Width = 215
        Height = 315
        Alignment = taCenter
        Anchors = [akLeft, akTop, akRight, akBottom]
        BevelEdges = []
        BevelInner = bvNone
        BorderStyle = bsNone
        Color = clBtnFace
        Lines.Strings = (
          ''
          ''
          ''
          ''
          'Focuser Control Panel'
          'ver 1.3'
          ''
          'Created by Alexey V. Popov'
          '9141866@gmail.com'
          'St.-Petersburg'
          'Russia'
          ''
          '2014')
        TabOrder = 0
      end
    end
  end
  object ConnectionTimer: TTimer
    Enabled = False
    Interval = 100
    OnTimer = ConnectionTimerTimer
    Left = 24
  end
end
