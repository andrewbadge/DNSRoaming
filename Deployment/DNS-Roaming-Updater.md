# Automatic updates via the updater

DNS Roaming now includes an automatic updater. The service will check for updates every 3 days by default. This can be changned to between 1 and 365 days in Settings.

## What the Updater does

- Checks the version installed (Version number saved by the MSI)
- Check the Type of installation (Service and Client or Service Only)
- Checks releases in GitHub andf compares the versions
- If GitHub is newer; the MSI is downloaded and run silently

## How often does it check?

Every 3 days by default. The frequency can be changed in Options. 

NB: the frequency is saved in "AutoUpdateHours" value in Options.xml. This value is in hours (not days to allow for quicker testing). 
Any hour value saved in the Settings UI will be convered into Days

## Where are files saved?

New MSIs are downloaded to %ProgramData%\DNSRoaming\Downloads. When the MSI is run the MSIExec log is saved into MSIInstallation.log in the same folder.

Last time an update check was run is saved into UpdateData.xml in %ProgramData%\DNSRoaming\Options

## Running the Updater manually

You can run the **DNS Roaming Updater.exe** from %ProgramFiles(x86)\DNSRoaming\Updater.
This is a console app soy u can view debug information if you run via the Terminal/Command Prompt.

NB: Either run the exe as Admin or the MSI MSI will prompt to run as admin.
By default running the Updater will obey the frequency set out in the Options.

There are two commandline arguments to force a behaviour:

- forcecheck: Ignores the schedule and checks for a new update
- forcedownloadandinstall: Ignores the schedule and current version check. Downloads a new version and installs

e.g. 

```
.\"DNS Roaming Updater.exe" forcedownloadandinstall
```

![image](https://user-images.githubusercontent.com/15990355/140630647-5e5cfed1-e2db-4545-b19c-a8c68697340e.png)
