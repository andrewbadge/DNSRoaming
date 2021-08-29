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
