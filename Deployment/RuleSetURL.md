# What is RuleSetURL?

From v1.1.0.1 and onwards; RuleSetURL is a paramter when running the installer (MSI) to specify a set of DNS rules to download and auto-update.

## How to use the RuleSetURL

When running the MSI from the command line, you can specify the RULESETURL parameter.
```
msiexec /i "[MSIPath]\DNSRoaming-ServiceAndClient.msi" /QN RULESETURL="http://mydomain.com/ruleset.txt"
```

NB: Ensure the parameter is RULESETURL (exactly and uppercase). Wix seems particular about this.

## Format of the RuleSet File

The RuleSet file can be any name but is expected to a text file containing one or more lines of:
- Rule Download URL

Delimited by a comma

e.g.
```
https://http://mydomain.com/ruleA.xml
https://http://mydomain.com/ruleB.xml
```

NB: the old format of Rule Filename, Rule URL will still work. The Filename is just ignored.

## When are the rules downloaded

- When the service is started after installation
- Every 3 days (by Default). You can change the frequency in Client / Settings

# Some important points!

- The ruleset file can't be more than 10KB
- Its expected to be a text file. Other file contents will not parse.

# How to Stop DNSRoaming downloading rules

The RULESETURL url is saved by the installer into the registry into HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\DNSRoaming if set via the MSI parameter.
Once the service loads this value is moved into the RuleSetData.xml file in the Options folder.

There are 2 ways to stop DNS Roaming download more rules:
1. Open the Client and Settings. Open the Options Tab and clear the URL
![image](https://user-images.githubusercontent.com/15990355/145539656-a999966e-f4cb-4992-b2da-d48470e53744.png)

2. Open the RuleSetData.xml file and Clear or Delete the RULESETURL value.
