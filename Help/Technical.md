# Technical Stuff
DNS Roaming is two components:

- a Client: to show the status of the service and configure settings
- a Service: to parse the rules and set DNS servers

All Code is written in C# with a WIX Toolkit based installer and compiled with [Visual Studio Community Edition 2019](https://visualstudio.microsoft.com/downloads/).

### Configuration

All Logs are saved to %ProgramData%\DNSRoaming

All Settings file are in XML and are saved to %ProgramData%\DNSRoaming\Settings

### Default Rule

If no rules exist; a default rule will be created that will:

- when connected to any Wireless Network
- on any network address
- set the DNS server to Quad (Preferred) and CloudFlare - No Malware (Alternative)
