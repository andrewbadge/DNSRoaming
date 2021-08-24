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
Of course if this is your corporate or home network then you have the control. 

But do you have the control over your friends home network? Do you have control over each of your co-workers home networks? No.

DNS Roaming is a Client and Service to ensure the DNS Servers are set via a rule / policy rather than the network a PC is connected to. 
This allows an adminstrator to force that Quad9 or CloudFlare's DNS server is set rather than an ISPs.

e.g.
- If the connected Network is Wireless then set the DNS to Quad9.
- If the connected Network is Ethernet and in the 10.0.0.1/24 Subnet then set the DNS to 10.0.0.10 and 10.0.0.11.


If you find this useful and would like me to continue working on it please Buy Me a Coffee 

# Technical Stuff
DNS Roaming is two components:
- a Client: to show the status of the service and configure settings
- a Service: to parse the rules and set DNS servers

All Code is written in C# with a WIX Toolkit based installer.

### Configuration

All Logs are saved to %ProgramData%\DNSRoaming
All Settings file are in XML and are saved to %ProgramData%\DNSRoadming\Settings

# FAQ

## Can I install the files manually?

Sure. DNS Roaming won't care where the service and client files are located. If you copy the Program Files files (exes and dlls) to any folder you can run the client and install the service manually (using "SC CREATE").

NB: the log and settings files will always be stored under %ProgramData%\DNSRoaming 

## Can I sell DNS Roaming for profit?

No. You can't sell it as your product. See the Licensing section. You can use it as a tool as part of a commercial service (e.g. as a MSP).
