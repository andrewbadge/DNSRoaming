# What is RuleSetURL?

RuleSetURL is a paramter when running the installer (MSI) to specify a set of DNS rules to download and auto-update.

## How to use the RuleSetURL

When running the MSI from the command line, you can specify the RULESETURL parameter.
```
msiexec /i "[MSIPath]\DNSRoaming-ServiceAndClient.msi" /QN RULESETURL="http://mydomain.com/ruleset.txt"
```

## Format of the RuleSet File

The RuleSet file can be any name but is expected to a text file containing one or more lines of:
- Rule File Name
- Rule Download URL

Delimited by a comma

e.g.
```
Rule-8cc5f9f4-cea8-43b3-a215-dc846d614ad2.xml,https://http://mydomain.com/ruleA.xml
Rule-b7d757f1-a824-4ec5-82b6-64b9176953a0.xml,https://http://mydomain.com/ruleB.xml
```

## When are the rules downloaded

- When the service is started (including after installation)
- Every 3 days

# Some important points!

- The Rule File Name should be in the format Rule-[GUID].xml where the GUID matches the contents GUID in the XML file
- If the Filename and the file's GUID don't match; saving the rule in the UI will create a new file
- The ruleset file can't be more than 10KB
- Its expected to be a text file. Other file contents will not parse.

# How to Stop DNSRoaming downloading rules

The RULESETURL url is saved by the installer into the registry into HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\DNSRoaming.
Clear or Delete the RULESETURL value and the service will no longer check for downloads.
