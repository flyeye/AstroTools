object Form1: TForm1
  Left = 209
  Top = 196
  Caption = 'Mounter Control Panel'
  ClientHeight = 451
  ClientWidth = 375
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
    375
    451)
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 240
    Top = 240
    Width = 32
    Height = 13
    Caption = 'Label1'
  end
  object Label3: TLabel
    Left = 252
    Top = 274
    Width = 49
    Height = 13
    Caption = 'Microstep:'
  end
  object Panel2: TPanel
    Left = 0
    Top = 0
    Width = 375
    Height = 68
    Align = alTop
    TabOrder = 0
    DesignSize = (
      375
      68)
    object btnConnect: TButton
      Left = 311
      Top = 9
      Width = 57
      Height = 25
      Anchors = [akTop, akRight]
      Caption = 'Connect'
      TabOrder = 1
      OnClick = btnConnectClick
    end
    object btnStop: TButton
      Left = 311
      Top = 35
      Width = 57
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
      Width = 68
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
      Text = 'Mount 1'
      OnChange = cbFocuserChange
    end
    object btnFocuserSave: TButton
      Left = 54
      Top = 35
      Width = 57
      Height = 25
      Caption = 'Save'
      TabOrder = 4
      OnClick = btnFocuserSaveClick
    end
    object btnFocuserDelete: TButton
      Left = 112
      Top = 35
      Width = 56
      Height = 25
      Caption = 'Delete'
      TabOrder = 5
      OnClick = btnFocuserDeleteClick
    end
  end
  object pgMounter: TPageControl
    Left = 0
    Top = 74
    Width = 375
    Height = 374
    ActivePage = tsMounter
    Anchors = [akLeft, akTop, akRight, akBottom]
    TabOrder = 1
    OnExit = pgMounterExit
    ExplicitHeight = 350
    object tsMounter: TTabSheet
      Caption = 'Mounter'
      ExplicitLeft = 0
      ExplicitTop = 0
      ExplicitWidth = 0
      ExplicitHeight = 322
      DesignSize = (
        367
        346)
      object lMinNavSpeed: TLabel
        Left = 206
        Top = 81
        Width = 6
        Height = 13
        Alignment = taRightJustify
        Caption = '0'
      end
      object lMaxNavSpeed: TLabel
        Left = 322
        Top = 81
        Width = 30
        Height = 13
        Caption = '20000'
      end
      object lMinDailySpeedRA: TLabel
        Left = 21
        Top = 19
        Width = 6
        Height = 13
        Alignment = taRightJustify
        Caption = '0'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
      end
      object lMaxDailySpeedRA: TLabel
        Left = 144
        Top = 19
        Width = 30
        Height = 13
        Caption = '20000'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
      end
      object lMinDailySpeedDE: TLabel
        Left = 179
        Top = 19
        Width = 33
        Height = 13
        Alignment = taRightJustify
        Caption = '-20000'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
      end
      object lMaxDailySpeedDE: TLabel
        Left = 321
        Top = 19
        Width = 30
        Height = 13
        Caption = '20000'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
      end
      object btnRelease: TButton
        Left = 74
        Top = 127
        Width = 47
        Height = 33
        Caption = 'Stop'
        Enabled = False
        TabOrder = 4
        OnClick = btnReleaseClick
      end
      object btnRight: TButton
        Left = 122
        Top = 127
        Width = 42
        Height = 33
        Caption = '>>'
        Enabled = False
        TabOrder = 3
        OnMouseDown = btnRAForwardRoll
        OnMouseUp = btnRAForwardStop
      end
      object btnStepRight: TButton
        Left = 166
        Top = 127
        Width = 24
        Height = 33
        Caption = '>'
        Enabled = False
        TabOrder = 1
        OnClick = btnRAStepForward
      end
      object btnStepLeft: TButton
        Left = 7
        Top = 127
        Width = 23
        Height = 33
        Caption = '<'
        Enabled = False
        TabOrder = 0
        OnClick = btnRAStepBack
      end
      object btnLeft: TButton
        Left = 31
        Top = 127
        Width = 42
        Height = 33
        Caption = '<<'
        Enabled = False
        TabOrder = 2
        OnMouseDown = btnRABackwardRoll
        OnMouseUp = btnRABackwardStop
      end
      object lePositionRA: TLabeledEdit
        Left = 136
        Top = 232
        Width = 58
        Height = 21
        Anchors = [akLeft, akBottom]
        EditLabel.Width = 15
        EditLabel.Height = 13
        EditLabel.Caption = 'RA'
        EditLabel.Color = clBtnText
        EditLabel.ParentColor = False
        Enabled = False
        NumbersOnly = True
        ReadOnly = True
        TabOrder = 5
        ExplicitTop = 208
      end
      object btnSetPos: TButton
        Left = 22
        Top = 283
        Width = 51
        Height = 25
        Anchors = [akLeft, akBottom]
        Caption = 'Set RA'
        Enabled = False
        TabOrder = 6
        OnClick = btnSetPosClick
        ExplicitTop = 259
      end
      object pgNavSpeed: TProgressBar
        Left = 218
        Top = 77
        Width = 98
        Height = 17
        Enabled = False
        TabOrder = 7
        OnMouseDown = pgNavSpeedMouseDown
        OnMouseMove = pgNavSpeedMouseMove
        OnMouseUp = pgNavSpeedMouseUp
      end
      object cbGoto: TComboBox
        Left = 24
        Top = 322
        Width = 293
        Height = 21
        AutoCompleteDelay = 1000
        Anchors = [akLeft, akBottom]
        Sorted = True
        TabOrder = 8
        OnChange = cbGotoChange
      end
      object btnGoto: TButton
        Left = 266
        Top = 283
        Width = 51
        Height = 25
        Anchors = [akLeft, akBottom]
        Caption = 'Goto'
        Enabled = False
        TabOrder = 9
        OnClick = btnGotoClick
        ExplicitTop = 259
      end
      object btnSave: TButton
        Left = 266
        Top = 230
        Width = 51
        Height = 25
        Anchors = [akLeft, akBottom]
        Caption = 'Save'
        Enabled = False
        TabOrder = 10
        OnClick = btnSaveClick
        ExplicitTop = 206
      end
      object btnDelete: TButton
        Left = 266
        Top = 256
        Width = 51
        Height = 25
        Anchors = [akLeft, akBottom]
        Caption = 'Delete'
        Enabled = False
        TabOrder = 11
        OnClick = btnDeleteClick
        ExplicitTop = 232
      end
      object leNavSpeed: TLabeledEdit
        Left = 218
        Top = 100
        Width = 61
        Height = 21
        EditLabel.Width = 7
        EditLabel.Height = 13
        EditLabel.Caption = 'V'
        LabelPosition = lpLeft
        TabOrder = 12
      end
      object btnSetNavSpeed: TButton
        Left = 285
        Top = 100
        Width = 31
        Height = 25
        Caption = 'Set'
        Enabled = False
        TabOrder = 13
        OnClick = btnSetNavSpeedClick
      end
      object cbDRotRA: TCheckBox
        Left = 144
        Top = 42
        Width = 43
        Height = 17
        Caption = 'RA'
        Enabled = False
        TabOrder = 14
        OnClick = cbDRotRAClick
      end
      object btnDERollForward: TButton
        Left = 74
        Top = 94
        Width = 47
        Height = 33
        Caption = 'UP'
        Enabled = False
        TabOrder = 15
        OnMouseDown = btnDERollForwardMouseDown
        OnMouseUp = btnDERollForwardMouseUp
      end
      object btnDEStepForward: TButton
        Left = 74
        Top = 72
        Width = 47
        Height = 22
        Caption = 'UP'
        Enabled = False
        TabOrder = 16
        OnClick = btnDEStepForwardClick
      end
      object btnDERollBackward: TButton
        Left = 74
        Top = 160
        Width = 47
        Height = 33
        Caption = 'D'
        Enabled = False
        TabOrder = 17
        OnMouseDown = btnDERollBackwardMouseDown
        OnMouseUp = btnDERollBackwardMouseUp
      end
      object btnDEStepBackward: TButton
        Left = 74
        Top = 193
        Width = 47
        Height = 19
        Caption = 'D'
        Enabled = False
        TabOrder = 18
        OnClick = btnDEStepBackwardClick
      end
      object pgDailySpeedRA: TProgressBar
        Left = 33
        Top = 15
        Width = 105
        Height = 17
        Enabled = False
        TabOrder = 19
        OnMouseDown = pgDailySpeedRAMouseDown
        OnMouseMove = pgDailySpeedRAMouseMove
        OnMouseUp = pgDailySpeedRAMouseUp
      end
      object btnSetDailySpeedRA: TButton
        Left = 107
        Top = 38
        Width = 31
        Height = 25
        Caption = 'Set'
        Enabled = False
        TabOrder = 20
        OnClick = btnSetDailySpeedRAClick
      end
      object leDailySpeedRA: TLabeledEdit
        Left = 33
        Top = 38
        Width = 68
        Height = 21
        EditLabel.Width = 7
        EditLabel.Height = 13
        EditLabel.Caption = 'V'
        LabelPosition = lpLeft
        TabOrder = 21
      end
      object lePositionDE: TLabeledEdit
        Left = 200
        Top = 232
        Width = 60
        Height = 21
        Anchors = [akLeft, akBottom]
        EditLabel.Width = 15
        EditLabel.Height = 13
        EditLabel.Caption = 'DE'
        Enabled = False
        NumbersOnly = True
        ReadOnly = True
        TabOrder = 22
        ExplicitTop = 208
      end
      object eNewPosRA: TEdit
        Left = 136
        Top = 285
        Width = 58
        Height = 21
        Anchors = [akLeft, akBottom]
        Enabled = False
        TabOrder = 23
        Text = '0'
        ExplicitTop = 261
      end
      object eNewPosDE: TEdit
        Left = 200
        Top = 285
        Width = 60
        Height = 21
        Anchors = [akLeft, akBottom]
        Enabled = False
        TabOrder = 24
        Text = '90'
        ExplicitTop = 261
      end
      object btnSetDE: TButton
        Left = 79
        Top = 283
        Width = 51
        Height = 25
        Anchors = [akLeft, akBottom]
        Caption = 'Set DE'
        Enabled = False
        TabOrder = 25
        OnClick = btnSetDEClick
        ExplicitTop = 259
      end
      object pgDailySpeedDE: TProgressBar
        Left = 218
        Top = 15
        Width = 99
        Height = 17
        Enabled = False
        TabOrder = 26
        OnMouseDown = pgDailySpeedDEMouseDown
        OnMouseMove = pgDailySpeedDEMouseMove
        OnMouseUp = pgDailySpeedDEMouseUp
      end
      object btnSetDailySpeedDE: TButton
        Left = 285
        Top = 38
        Width = 31
        Height = 25
        Caption = 'Set'
        Enabled = False
        TabOrder = 27
        OnClick = btnSetDailySpeedDEClick
      end
      object leDailySpeedDE: TLabeledEdit
        Left = 218
        Top = 38
        Width = 61
        Height = 21
        EditLabel.Width = 7
        EditLabel.Height = 13
        EditLabel.Caption = 'V'
        LabelPosition = lpLeft
        TabOrder = 28
      end
      object cbDRotDE: TCheckBox
        Left = 322
        Top = 42
        Width = 43
        Height = 17
        Caption = 'DE'
        Enabled = False
        TabOrder = 29
        OnClick = cbDRotDEClick
      end
      object lePosRA_EQ: TEdit
        Left = 136
        Top = 258
        Width = 58
        Height = 21
        Anchors = [akLeft, akBottom]
        Enabled = False
        TabOrder = 30
        ExplicitTop = 234
      end
      object lePosDE_EQ: TEdit
        Left = 200
        Top = 258
        Width = 60
        Height = 21
        Anchors = [akLeft, akBottom]
        Enabled = False
        TabOrder = 31
        ExplicitTop = 234
      end
    end
    object tsParameters: TTabSheet
      Caption = 'Parameters'
      ImageIndex = 3
      ExplicitLeft = 0
      ExplicitTop = 0
      ExplicitWidth = 0
      ExplicitHeight = 322
      object GroupBox1: TGroupBox
        Left = 5
        Top = 3
        Width = 356
        Height = 198
        Caption = 'Mechanics && Electonics'
        TabOrder = 0
        DesignSize = (
          356
          198)
        object Label2: TLabel
          Left = 222
          Top = 146
          Width = 72
          Height = 13
          Alignment = taRightJustify
          Caption = 'Nav Microstep:'
        end
        object Label4: TLabel
          Left = 217
          Top = 173
          Width = 75
          Height = 13
          Alignment = taRightJustify
          Caption = 'Daily Microstep:'
        end
        object leGearRatioRA: TLabeledEdit
          Left = 32
          Top = 25
          Width = 121
          Height = 21
          EditLabel.Width = 18
          EditLabel.Height = 13
          EditLabel.Caption = 'RA:'
          LabelPosition = lpLeft
          TabOrder = 0
        end
        object leGearRatioDE: TLabeledEdit
          Left = 32
          Top = 52
          Width = 121
          Height = 21
          EditLabel.Width = 18
          EditLabel.Height = 13
          EditLabel.Caption = 'DE:'
          LabelPosition = lpLeft
          TabOrder = 1
        end
        object Button1: TButton
          Left = 105
          Top = 75
          Width = 48
          Height = 24
          Caption = 'Set'
          TabOrder = 2
          OnClick = Button1Click
        end
        object eTemp: TLabeledEdit
          Left = 32
          Top = 132
          Width = 56
          Height = 21
          EditLabel.Width = 10
          EditLabel.Height = 13
          EditLabel.Caption = 'T:'
          LabelPosition = lpLeft
          NumbersOnly = True
          ReadOnly = True
          TabOrder = 3
        end
        object eDateTime: TLabeledEdit
          Left = 32
          Top = 105
          Width = 121
          Height = 21
          EditLabel.Width = 73
          EditLabel.Height = 13
          EditLabel.Caption = 'Date and Time:'
          ReadOnly = True
          TabOrder = 4
        end
        object btnSetTime: TButton
          Left = 105
          Top = 132
          Width = 48
          Height = 25
          Caption = 'Set'
          TabOrder = 5
          OnClick = btnSetTimeClick
        end
        object Button2: TButton
          Left = 159
          Top = 166
          Width = 48
          Height = 25
          Anchors = [akLeft, akBottom]
          Caption = 'Set'
          TabOrder = 6
          OnClick = Button2Click
        end
        object btneMaxNavSpeed: TButtonedEdit
          Left = 265
          Top = 25
          Width = 80
          Height = 21
          Alignment = taRightJustify
          LeftButton.Enabled = False
          LeftButton.Visible = True
          NumbersOnly = True
          RightButton.Visible = True
          TabOrder = 7
          Text = '0'
        end
        object btneMaxDailyRASpeed: TButtonedEdit
          Left = 265
          Top = 52
          Width = 80
          Height = 21
          Alignment = taRightJustify
          LeftButton.Enabled = False
          LeftButton.Visible = True
          NumbersOnly = True
          RightButton.Visible = True
          TabOrder = 8
          Text = '0'
        end
        object btneMinDailyRASpeed: TButtonedEdit
          Left = 179
          Top = 52
          Width = 80
          Height = 21
          Alignment = taRightJustify
          LeftButton.Enabled = False
          LeftButton.Visible = True
          NumbersOnly = True
          RightButton.Visible = True
          TabOrder = 9
          Text = '0'
        end
        object btneMaxDailyDESpeed: TButtonedEdit
          Left = 265
          Top = 79
          Width = 80
          Height = 21
          Alignment = taRightJustify
          LeftButton.Enabled = False
          LeftButton.Visible = True
          NumbersOnly = True
          RightButton.Visible = True
          TabOrder = 10
          Text = '0'
        end
        object btneMinDailyDESpeed: TButtonedEdit
          Left = 179
          Top = 79
          Width = 80
          Height = 21
          Alignment = taRightJustify
          LeftButton.Enabled = False
          LeftButton.Visible = True
          NumbersOnly = True
          RightButton.Visible = True
          TabOrder = 11
          Text = '0'
        end
        object btneMinNavSpeed: TButtonedEdit
          Left = 179
          Top = 25
          Width = 80
          Height = 21
          Alignment = taRightJustify
          LeftButton.Enabled = False
          LeftButton.Visible = True
          NumbersOnly = True
          RightButton.Visible = True
          TabOrder = 12
          Text = '0'
        end
        object eReleaseTime: TLabeledEdit
          Left = 110
          Top = 170
          Width = 43
          Height = 21
          Alignment = taRightJustify
          EditLabel.Width = 93
          EditLabel.Height = 13
          EditLabel.Caption = 'Release timeout (s):'
          LabelPosition = lpLeft
          TabOrder = 13
          Text = '3'
        end
        object Button3: TButton
          Left = 297
          Top = 106
          Width = 48
          Height = 25
          Anchors = [akLeft, akBottom]
          Caption = 'Set'
          TabOrder = 14
          OnClick = Button3Click
        end
        object cbMicroStep: TComboBox
          Left = 298
          Top = 143
          Width = 42
          Height = 21
          Style = csDropDownList
          Enabled = False
          TabOrder = 15
          OnChange = cbMicroStepChange
          Items.Strings = (
            '1'
            '2'
            '4'
            '8'
            '16'
            '32')
        end
        object cbDailyMicroStep: TComboBox
          Left = 298
          Top = 170
          Width = 42
          Height = 21
          Style = csDropDownList
          Enabled = False
          TabOrder = 16
          OnChange = cbDailyMicroStepChange
          Items.Strings = (
            '1'
            '2'
            '4'
            '8'
            '16'
            '32')
        end
      end
      object GroupBox2: TGroupBox
        Left = 5
        Top = 200
        Width = 356
        Height = 60
        Caption = 'Manned control'
        TabOrder = 1
        object eStepDuration: TLabeledEdit
          Left = 11
          Top = 29
          Width = 121
          Height = 21
          EditLabel.Width = 87
          EditLabel.Height = 13
          EditLabel.Caption = 'Step Duration (ms)'
          TabOrder = 0
          Text = '10'
        end
        object cbRCP: TCheckBox
          Left = 162
          Top = 33
          Width = 191
          Height = 17
          Caption = 'Remote panel ON/OFF'
          TabOrder = 1
          OnClick = cbRCPClick
        end
      end
      object GroupBox3: TGroupBox
        Left = 5
        Top = 262
        Width = 356
        Height = 81
        Caption = 'System'
        TabOrder = 2
        object cbTime: TCheckBox
          Left = 18
          Top = 24
          Width = 135
          Height = 17
          Caption = 'Time count ON/OFF'
          Checked = True
          State = cbChecked
          TabOrder = 0
          OnClick = cbTimeClick
        end
        object cbSound: TCheckBox
          Left = 18
          Top = 47
          Width = 97
          Height = 17
          Caption = 'Sound ON/OFF'
          TabOrder = 1
        end
        object rbRAHours: TRadioButton
          Left = 217
          Top = 24
          Width = 113
          Height = 17
          Caption = 'RA: Hours'
          Checked = True
          TabOrder = 2
          TabStop = True
        end
        object rbRADegrees: TRadioButton
          Left = 217
          Top = 47
          Width = 113
          Height = 17
          Caption = 'RA: Degrees'
          TabOrder = 3
        end
      end
    end
    object Debug: TTabSheet
      Caption = 'Debug'
      ImageIndex = 1
      ExplicitLeft = 0
      ExplicitTop = 0
      ExplicitWidth = 0
      ExplicitHeight = 322
      DesignSize = (
        367
        346)
      object btnSend: TButton
        Left = 58
        Top = 2
        Width = 43
        Height = 24
        Caption = 'Send'
        Enabled = False
        TabOrder = 1
        OnClick = btnSendClick
      end
      object eDataLine: TEdit
        Left = 4
        Top = 2
        Width = 48
        Height = 21
        Hint = #1092#1086#1088#1084#1072#1090' '#1076#1083#1103' '#1093#1077#1082#1089#1072' $3F'
        Enabled = False
        TabOrder = 0
        Text = '$'
        OnKeyDown = eDataLineKeyDown
      end
      object memOut: TMemo
        Left = 3
        Top = 32
        Width = 361
        Height = 281
        Anchors = [akLeft, akTop, akRight, akBottom]
        ReadOnly = True
        ScrollBars = ssVertical
        TabOrder = 4
        ExplicitHeight = 257
      end
      object btnPing: TButton
        Left = 105
        Top = 1
        Width = 43
        Height = 25
        Caption = 'Ping'
        Enabled = False
        TabOrder = 3
        OnClick = btnPingClick
      end
      object cbShowDebugMsg: TCheckBox
        Left = 3
        Top = 326
        Width = 146
        Height = 17
        Anchors = [akLeft, akBottom]
        Caption = 'Show debug message'
        TabOrder = 5
        OnClick = cbShowDebugMsgClick
        ExplicitTop = 302
      end
      object btnClearMem: TButton
        Left = 304
        Top = 319
        Width = 57
        Height = 24
        Anchors = [akLeft, akBottom]
        Caption = 'Clear'
        TabOrder = 2
        OnClick = btnClearMemClick
        ExplicitTop = 295
      end
      object cbAutoscroll: TCheckBox
        Left = 139
        Top = 326
        Width = 97
        Height = 17
        Anchors = [akLeft, akBottom]
        Caption = 'Autoscroll'
        TabOrder = 6
        ExplicitTop = 302
      end
    end
    object About: TTabSheet
      Caption = 'About'
      ImageIndex = 2
      ExplicitLeft = 0
      ExplicitTop = 0
      ExplicitWidth = 0
      ExplicitHeight = 0
      DesignSize = (
        367
        346)
      object Memo1: TMemo
        Left = -2
        Top = -2
        Width = 366
        Height = 345
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
          'Mounter Control Panel'
          'ver 0.4'
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
  object tEarthRotation: TTimer
    Enabled = False
    Interval = 10000
    OnTimer = tEarthRotationTimer
    Left = 208
    Top = 8
  end
  object tDateTimeTemp: TTimer
    OnTimer = tDateTimeTempTimer
    Left = 264
    Top = 8
  end
end
