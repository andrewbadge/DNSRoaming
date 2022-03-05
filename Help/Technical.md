# Technical Stuff
DNS Roaming is two components:

- a Client: to show the status of the service and configure settings
- a Service: to parse the rules and set DNS servers

All Code is written in C# with a WIX Toolkit based installer and compiled with [Visual Studio Community Edition 2019](https://visualstudio.microsoft.com/downloads/).

## How it works

The Client creates and updates Rule (XML) files in the settings folder.
The Service monitors this folder for changes. 

Periodically or when a change is detected; the service will check active network interfaces against the rules. If matched; the network interface's DNS settings are updated.

## Configuration

All Logs are saved to %ProgramData%\DNSRoaming

All Settings file are in XML and are saved to %ProgramData%\DNSRoaming\Settings

## Default Rule

If no rules exist; a default rule will be created that will:

- when connected to any Wireless Network
- on any network address
- set the DNS server to Quad (Preferred) and CloudFlare - No Malware (Alternative)

## Does DNS Roaming connect to any servers or services?

### Updates

Periodically the DNS Roaming Updater will connect to this Repository in GitHub to check for new releases. Releases are downloaded from this repo only.

### Rules with a WAN Condition

The DNS Roaming Service will connect to http://checkip.dyndns.org to get the WAN IP address.
This is the only outbound connection the Service or client makes.

DNS Roaming only updates the network's DNS settings. It does not see or intercept the traffic between your PC, Apps and the DNS server.

### Rule Sets

If a RuleSet is defined (URL to automatically download a set of rules from); then the DNS Service will periodically download from the URL defined.

## Compiling DNS Roaming for yourself

Get yourself a copy of the Source folder. Open the DNS Roaming.sln file using Visual Studio 2019.

![image](https://user-images.githubusercontent.com/15990355/132493159-8c9c6e01-7da9-47b5-b580-46e664c1b95c.png)


