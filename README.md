# DNS Roaming Client and Service

## Quick Start

Got to Releases and download the most recent release (and MSI file).
Install the MSI manually, via a policy or script.

NB: you will need Run as **Administrator** to install this MSI as a Windows Service is installed.

System Requirements:
- Windows 10/11 and a compatible PC
- .Net Framework 4.7 (In general will already be installed)

## Why DNS Roaming?

DNS can play a strong part in the security of a PC. With the right DNS service; malicous links, sites and servers can be blocked. 
If you have the money a service like CISCO Umbrella or DNS Filter are effective.

If you don't have the money then you rely on the configuration of the destination network (and a knowledable user).

DNS Roaming is a Client and Service to ensure the DNS Servers are set via a rule / policy rather than the network a PC is connected to. 
This allows an adminsitrator to force that Quad9 or CloudFlare's DNS server is set rather than an ISPs.

If you find this useful and would like me to continie working on it please Buy Me a Coffee 

# Technical Stuff
DNS Roadming is two components:
- a Client: to show the status of the service and configure settings
- a Service: to parse the rules and set DNS servers

All Code is written in C# with a WIX Toolkit based installer.

### Configuration

All Logs are saved to %ProgramData%\DNSRoadming
All Settings file are in XML and are saved to %ProgramData%\DNSRoadming\Settings


