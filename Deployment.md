# Deployment

The Installation is a standard MSI package.

You can

- Double click to run
- Install via a script or command 
- Install via a policy (e.g. Group Policy)

## Installation via a Command

From an Administrative Command Prompt run

```
msiexec /i "[MSIPath]\DNS_Roaming_Installer.msi" /QN /L*V "[LogPath]\msilog.log"
```

e.g.

```
msiexec /i "C:\temp\DNS_Roaming_Installer.msi" /QN /L*V "C:\tmp\msilog.log"
```

## Distributing Rules

To send a standard rule to all PCs in a network; create the rule on your machine and then copy Rule XML Files to the  %ProgramData%\DNSRoaming\Settings Folder on each PC.
You could do this via Powershell with a GitHub Repo for the locations of your settings files.

Hints: Rules based on a network name are unlikely to work on multiple PCs. Use Network Types.
