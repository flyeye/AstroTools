program FocuserControlPanel;

uses
  Forms,
  main in 'main.pas' {Form1},
  Focuser in 'Focuser.pas',
  FConnection in 'D:\FConnections\FConnection.pas';

{$R *.RES}

begin
  Application.Initialize;
  Application.CreateForm(TForm1, Form1);
  Application.Run;
end.
