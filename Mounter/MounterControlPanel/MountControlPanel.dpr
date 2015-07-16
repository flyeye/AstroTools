program MountControlPanel;



uses
  Forms,
  main in 'main.pas' {Form1},
  Mounter in 'Mounter.pas',
  FConnection in 'D:\FConnections\FConnection.pas';

{$R *.RES}

begin
  Application.Initialize;
  Application.CreateForm(TForm1, Form1);
  Application.Run;
end.
