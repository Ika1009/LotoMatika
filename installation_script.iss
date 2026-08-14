[Setup]
AppName=Lotomatika
AppVersion=1.0
DefaultDirName={autopf}\Lotomatika
DefaultGroupName=Lotomatika
; Ikona aplikacije na deinstalaciji
UninstallDisplayIcon={app}\Lotomatika.exe
Compression=lzma2
SolidCompression=yes
; Gdje želiš da se spremi gotov Setup.exe
OutputDir="C:\Users\ilija\Downloads\InstallationScript"
OutputBaseFilename=LotomatikaSetup_x64
; Traži administratorske privilegije za instalaciju u Program Files
PrivilegesRequired=admin

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Dirs]
; Daje puna prava korisnicima nad instalacijskim direktorijem
Name: "{app}"; Permissions: users-full

[Files]
; 1. Ovdje stavljaš putanju do tvog self-contained publish foldera (zvijezdica * na kraju kopira sve datoteke)
Source: "C:\Users\ilija\OneDrive\Documents\Programming\LotoMatika\bin\Release\net8.0-windows\win-x64\*"; DestDir: "{app}"; Flags: recursesubdirs createallsubdirs

; 2. Ovdje stavljaš putanje do DirectX i .NET instalera koje si spremio u Koraku 2
Source: "C:\Users\ilija\Downloads\dotnet-sdk-3.1.426-win-x64.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall
Source: "C:\Users\ilija\Downloads\dxwebsetup.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall

[Icons]
Name: "{group}\Lotomatika"; Filename: "{app}\Lotomatika.exe"
Name: "{autodesktop}\Lotomatika"; Filename: "{app}\Lotomatika.exe"; Tasks: desktopicon

[Run]
; Pokretanje .NET SDK / Runtime instalacije u "tihom" načinu rada (/passive znači da korisnik vidi samo progress bar bez klikanja)
Filename: "{tmp}\dotnet-sdk-3.1.426-win-x64.exe"; Parameters: "/passive /norestart"; StatusMsg: "Instaliranje .NET komponenti (ovo može potrajati)..."

; Pokretanje DirectX instalacije u tihom načinu rada (/q znači silent)
Filename: "{tmp}\dxwebsetup.exe"; Parameters: "/q"; StatusMsg: "Instaliranje DirectX Runtime-a..."

; Pokretanje same aplikacije nakon što se instalacija završi
Filename: "{app}\Lotomatika.exe"; Description: "{cm:LaunchProgram,Lotomatika}"; Flags: nowait postinstall skipifsilent