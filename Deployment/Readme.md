# Deployment

The Installation is a standard MSI package.

You can

- Double click to run
- Install via a script or command 
- Install via a policy (e.g. Group Policy)

## Difference between the MSIs

From v1.0.0.11 the MSI comes in two flavours:

- DNSRoaming-ServiceAndClient.msi : Install the Service, Client files and creates a startup and start menu shortcut
- DNSRoaming-ServiceOnly.msi : Install the Service only. The Client files are not installed


## Installation via a Command

From an Administrative Command Prompt run

```
msiexec /i "[MSIPath]\DNSRoaming-ServiceAndClient.msi" /QN /L*V "[LogPath]\msilog.log"
```

e.g.

```
msiexec /i "C:\temp\DNSRoaming-ServiceAndClient.msi" /QN /L*V "C:\tmp\msilog.log"
```

## Installation via a PowerShell Script

This could be used from Intune or AD Group Policy. the intention of this script is to 

1. Download a redefined Rule File (from your own distrubtion Repo)
2. Download a redefined Options (from your own distrubtion Repo)
3. Download and isnatll the DNS roaming MSI (from this Repo)

See the Script [Deploy-Windows-DNSRoaming.ps1](https://github.com/andrewbadge/DNSRoaming/blob/main/Deployment/Deploy-Windows-DNSRoaming.ps1)

## Distributing Rules

To send a standard rule to all PCs in a network; create the rule on your machine and then copy Rule XML Files to the  %ProgramData%\DNSRoaming\Settings Folder on each PC.
You could do this via Powershell with a GitHub Repo for the locations of your settings files.

Hints: Rules based on a network name are unlikely to work on multiple PCs. Use Network Types.

## Hardening DNS Roaming

If the objective is to install DNS Roaming so the user is not aware of it (and cannot modify the rules); try the following for end-users.

- Install DNSRoaming-ServiceOnly.msi (don't install the client)
- Deploy your own Settings XML files to the %ProgramData%\DNSRoaming\Settings folder

NB: Currently the installer will always Allow the BUILTIN\Users group to edit settings. You may want to update these permissions to restrict modify access to Administrators only.
