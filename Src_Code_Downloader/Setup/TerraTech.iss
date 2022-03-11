;      Скрипт создан с помощью
; IS GameScript Generator by South
;   специально для www.csmania.ru

[Setup]
SourceDir=.
OutputDir=Setup
AppName=TerraTech
AppVerName=TerraTech
AppVersion=TerraTech
AppPublisher=Repack from GlebYoutuber
AppCopyright=Repack from GlebYoutuber
DefaultDirName={pf}\TerraTech
DefaultGroupName=TerraTech
AllowNoIcons=yes
OutputBaseFilename=setup
WizardImageFile=C:\Users\GlebYT\Documents\terratech_repack1.bmp
WizardSmallImageFile=C:\Users\GlebYT\Downloads\TerraTech_Repack2.bmp
SetupIconFile=C:\Users\GlebYT\Documents\TerraTech.ico
WindowVisible=no
WindowShowCaption=no
WindowResizable=no
Compression=lzma/fast
DiskSpanning=yes
DiskSliceSize=157286400
SlicesPerDisk=1

[Languages]
Name: "english"; MessagesFile: "compiler:Languages\English.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[Files]
Source: ISSkin.dll; DestDir: {tmp}; Flags: ignoreversion dontcopy nocompression
Source: steam.cjstyles; DestDir: {tmp}; Flags: ignoreversion dontcopy nocompression
Source: "bass.dll"; DestDir: {tmp}; Flags: ignoreversion dontcopy nocompression
Source: "innocallback.dll"; DestDir: {tmp}; Flags: ignoreversion dontcopy nocompression
Source: "C:\Users\GlebYT\Downloads\y2mate.com - RiveR  Solo_320kbps.mp3"; DestDir: {tmp}; Flags: ignoreversion dontcopy nocompression

Source: "C:\Games\TerraTech\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs sortfilesbyextension

[Icons]
Name: "{group}\TerraTech"; Filename: "{app}\TerraTechWin64.exe"; WorkingDir: "{app}";
Name: "{userdesktop}\TerraTech"; Filename: "{app}\TerraTechWin64.exe"; WorkingDir: "{app}"; Tasks: desktopicon;
Name: "{group}\{cm:UninstallProgram,TerraTech}"; Filename: "{uninstallexe}"

[Run]
Description: "{cm:LaunchProgram, TerraTech}"; Filename: "{app}\TerraTechWin64.exe"; WorkingDir: "{app}"; Flags: nowait postinstall skipifsilent unchecked

[UninstallDelete]
Type: filesandordirs; Name: "{app}"

[Code]
type
  HSTREAM=DWORD;
  TTimerProc=procedure(uTimerID,uMessage:UINT;dwUser,dw1,dw2:DWORD);
var
  MP3List:TStringList;
  CurrentMP3:integer;
  hMP3:HWND;
  TimerID:LongWord;

function SetTimer(hWnd:HWND;nIDEvent,uElapse:UINT;lpTimerFunc:LongWord{TFNTimerProc}):UINT;  external 'SetTimer@user32.dll stdcall delayload';
function KillTimer(hWnd:HWND;uIDEvent:UINT):BOOL; external 'KillTimer@user32.dll stdcall delayload';
function BASS_ChannelIsActive(Handle:HWND):DWORD; external 'BASS_ChannelIsActive@files:bass.dll stdcall';
function BASS_SetConfig(Option,Value:DWORD):DWORD; external 'BASS_SetConfig@files:bass.dll stdcall';
function BASS_Init(Device:integer;Freq,Flags:DWORD;Win:HWND;CLSID:integer):boolean; external 'BASS_Init@files:bass.dll stdcall delayload';
function BASS_StreamCreateFile(Mem:BOOL;f:PAnsiChar;Offset:DWORD;Length:DWORD;Flags:DWORD):HSTREAM; external 'BASS_StreamCreateFile@files:bass.dll stdcall';
function BASS_StreamFree(Handle:HWND):boolean; external 'BASS_StreamFree@files:bass.dll stdcall';
function BASS_ChannelPlay(Handle:HWND;Restart:boolean):boolean; external 'BASS_ChannelPlay@files:bass.dll stdcall';
function BASS_Start: Boolean; external 'BASS_Start@files:bass.dll stdcall';
function BASS_Stop: Boolean; external 'BASS_Stop@files:bass.dll stdcall';
function BASS_Free: Boolean; external 'BASS_Free@files:bass.dll stdcall delayload';
function WrapTimerProc(CallBack:TTimerProc;ParamCount:integer):LongWord; external 'wrapcallback@files:innocallback.dll stdcall';
procedure LoadSkin(lpszPath: String; lpszIniFileName: String); external 'LoadSkin@files:isskin.dll stdcall';
procedure UnloadSkin(); external 'UnloadSkin@files:isskin.dll stdcall';
function ShowWindow(hWnd: Integer; uType: Integer): Integer; external 'ShowWindow@user32.dll stdcall';

procedure TimerTick(uTimerID,uMessage:UINT;dwUser,dw1,dw2:DWORD);
begin
  if BASS_ChannelIsActive(hMP3)=0 then begin
    BASS_Stop;
    BASS_StreamFree(hMP3);
    hMP3:=BASS_StreamCreateFile(False,PAnsiChar(MP3List.Strings[CurrentMP3]),0,0,0);
    BASS_Start;
    if hMP3<>0 then
      if BASS_ChannelPlay(hMP3,True) then begin
        CurrentMP3:=CurrentMP3+1;
        if CurrentMP3>MP3List.Count-1 then CurrentMP3:=0;
      end;
  end;
end;

function InitializeSetup:boolean;
begin
  ExtractTemporaryFile('y2mate.com - RiveR  Solo_320kbps.mp3');
  MP3List:=TStringList.Create;
  MP3List.Add(ExpandConstant('{tmp}')+'\y2mate.com - RiveR  Solo_320kbps.mp3');
  CurrentMP3:=0;
  ExtractTemporaryFile('steam.cjstyles');
  LoadSkin(ExpandConstant('{tmp}')+'\steam.cjstyles', '');
  Result:=True;
end;

procedure InitializeWizard;
begin
  TimerID:=SetTimer(0,0,500,WrapTimerProc(@TimerTick,5));
  BASS_Init(-1,44100,0,0,0);
  BASS_SetConfig(5,100);
  BASS_SetConfig(6,100);
  WizardForm.BeveledLabel.Enabled:=True;
end;

procedure DeinitializeSetup;
begin
  KillTimer(0,TimerID);
  BASS_Stop;
  BASS_Free;
  MP3List.Free;
  ShowWindow(WizardForm.Handle,0);
  UnloadSkin();
end;
