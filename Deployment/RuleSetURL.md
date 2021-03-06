# What is RuleSetURL?

From v1.1.0.1 and onwards; RuleSetURL is a paramter when running the installer (MSI) to specify a set of DNS rules to download and auto-update.

## How to use the RuleSetURL

When running the MSI from the command line, you can specify the RULESETURL parameter.
```
msiexec /i "[MSIPath]\DNSRoaming-ServiceAndClient.msi" /QN RULESETURL="http://mydomain.com/ruleset.txt"
```

NB: Ensure the parameter is RULESETURL (exactly and uppercase). Wix seems particular about this.

## How to remove (clear) the RuleSetURL

When running the MSI from the command line, you specify the RULESETURL parameter as "clear".
```
msiexec /i "[MSIPath]\DNSRoaming-ServiceAndClient.msi" /QN RULESETURL="clear"
```

## Format of the RuleSet File

The RuleSet file can be any name but is expected to a text file containing one or more lines of:
- MinVersion (optional)
- DeleteAllRules (Optional)
- Rule Download URL (http://domain.com/filename.txt or https://domain.com/filename.txt)
- Comments starting with //

Delimited by a comma

e.g.
```
//Only process this file with version 1.3.0.5 and above
MinVersion:1.3.0.5
// Delete all Rules first
DeleteAllRules
// My Rule A
https://http://mydomain.com/ruleA.xml
// My Rule B
https://http://mydomain.com/ruleB.xml
```

NB: the old format of Rule Filename, Rule URL will still work. The Filename is just ignored.

## When are the rules downloaded

- When the service is started after installation
- Every 3 days (by Default). You can change the frequency in Client / Settings

# Some important points!

- The ruleset file can't be more than 10KB
- Its expected to be a text file. Other file contents will not parse.
- The RuleSet file must be public without authentication; although querystrings will work
- The MinVersion instruction will only work for version 1.3.0.5 and later

# How to Stop DNSRoaming downloading rules

The RULESETURL url is saved by the installer into the registry into HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\DNSRoaming if set via the MSI parameter.
Once the service loads this value is moved into the RuleSetData.xml file in the Options folder.

There are 2 ways to stop DNS Roaming download more rules:
1. Open the Client and Settings. Open the Options Tab and clear the URL
![image](https://user-images.githubusercontent.com/15990355/159150792-98b95fc5-349d-404e-b1f0-97b9504d6438.png)

2. Open the RuleSetData.xml file and Clear or Delete the RULESETURL value.
