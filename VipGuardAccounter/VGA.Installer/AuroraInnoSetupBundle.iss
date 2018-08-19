[Setup]
AppName=Aurora
AppVersion=1
DefaultDirName={pf}\Macphun\Aurora
DisableWelcomePage=yes
DisableDirPage=yes
DisableProgramGroupPage=yes
DisableReadyPage=yes
DisableFinishedPage=yes
DisableStartupPrompt=yes
ArchitecturesAllowed=x64
Uninstallable=no
SetupIconFile=aurora_icon.ico
OutputDir=bin\Release
OutputBaseFilename=AuroraSetupSimpleBundle

[Files]
Source: "packages\Aurora.Install.Client.msi"; DestDir: "{tmp}";
Source: "packages\netFx46.exe"; DestDir: "{tmp}"; AfterInstall: RunNetFX46 
Source: "packages\vc_redist_x64.exe"; DestDir: "{tmp}"; 
Source: "packages\vc_redist_120_x64.exe"; DestDir: "{tmp}";        
                                                            
[Run]          
Filename: "msiexec.exe"; Parameters: "/i ""{tmp}\Aurora.Install.Client.msi"""
Filename: "{tmp}\vc_redist_x64.exe"; Parameters:/q; StatusMsg: "Validating VC Redists..." 
Filename: "{tmp}\vc_redist_120_x64.exe"; Parameters:/q; StatusMsg: "Validating VC Redists..." 

[Code]                                                                                        
function IsDotNetDetected(version: string; service: cardinal): boolean;
var                                                                                                                                                        
    key, versionKey: string;
    install, release, serviceCount, versionRelease: cardinal;
    success: boolean;
begin
    versionKey := version;
    versionRelease := 0;

    // .NET 1.1 and 2.0 embed release number in version key
    if version = 'v1.1' then begin
        versionKey := 'v1.1.4322';
    end else if version = 'v2.0' then begin
        versionKey := 'v2.0.50727';
    end

    // .NET 4.5 and newer install as update to .NET 4.0 Full
    else if Pos('v4.', version) = 1 then begin
        versionKey := 'v4\Full';
        case version of
          'v4.5':   versionRelease := 378389;
          'v4.5.1': versionRelease := 378675; // 378758 on Windows 8 and older
          'v4.5.2': versionRelease := 379893;
          'v4.6':   versionRelease := 393295; // 393297 on Windows 8.1 and older
          'v4.6.1': versionRelease := 394254; // 394271 before Win10 November Update
          'v4.6.2': versionRelease := 394802; // 394806 before Win10 Anniversary Update
          'v4.7':   versionRelease := 460798; // 460805 before Win10 Creators Update
        end;
    end;

    // installation key group for all .NET versions
    key := 'SOFTWARE\Microsoft\NET Framework Setup\NDP\' + versionKey;

    // .NET 3.0 uses value InstallSuccess in subkey Setup
    if Pos('v3.0', version) = 1 then begin
        success := RegQueryDWordValue(HKLM, key + '\Setup', 'InstallSuccess', install);
    end else begin
        success := RegQueryDWordValue(HKLM, key, 'Install', install);
    end;

    // .NET 4.0 and newer use value Servicing instead of SP
    if Pos('v4', version) = 1 then begin
        success := success and RegQueryDWordValue(HKLM, key, 'Servicing', serviceCount);
    end else begin
        success := success and RegQueryDWordValue(HKLM, key, 'SP', serviceCount);
    end;

    // .NET 4.5 and newer use additional value Release
    if versionRelease > 0 then begin
        success := success and RegQueryDWordValue(HKLM, key, 'Release', release);
        success := success and (release >= versionRelease);
    end; 
    result := success and (install = 1) and (serviceCount >= service);
end;

procedure RunNetFX46;
var
  ResultCode: Integer;
begin
WizardForm.StatusLabel.Caption := 'Intalling .NET Framework'
  if not IsDotNetDetected('v4.6.1', 0) 
  then begin
    if not Exec(ExpandConstant('{tmp}\netFx46.exe'), '/q', '', SW_SHOWNORMAL,
      ewWaitUntilTerminated, ResultCode)
    then
      MsgBox('Framework installer failed to run!' + #13#10 +
        SysErrorMessage(ResultCode), mbError, MB_OK);
  end;
end;






 
