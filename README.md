# DNS Roaming Client and Service

## Quick Start

Go to Releases (TBA) and download the most recent release (and MSI file).
Install the MSI manually, via a policy or script.

NB: you will need Run as **Administrator** to install this MSI as a Windows Service is installed.

System Requirements:
- Windows 10/11 and a compatible PC
- .Net Framework 4.7 (In general will already be installed)

## Why DNS Roaming?

DNS can play a strong part in the security of a PC. With the right DNS service; malicous links, sites and servers can be blocked. 
If you have the money a service like CISCO Umbrella or DNS Filter are effective.

If you don't have the money then you rely on the configuration of the destination network (and a knowledable user).

Of course if this is your managed corporate or home network then you have the control you need. 
But do you have the control over your friends home network? Do you have control over each of your co-workers home networks? No.

DNS Roaming is a Client and Service to ensure the DNS Servers are set via a rule / policy rather than the network a PC is connected to. 
This allows an adminstrator to force that Quad9 or CloudFlare's DNS server is set rather than an ISPs.

e.g.
- If the connected Network is Wireless then set the DNS to Quad9.
- If the connected Network is Ethernet and in the 10.0.0.1/24 Subnet then set the DNS to 10.0.0.10 and 10.0.0.11.

If you find this useful and would like me to continue working on it please [![Buy Me a Coffee](https://github.com/andrewbadge/DNSRoaming/blob/main/Images/BuyMeACoffee.png)](https://www.buymeacoffee.com/AndrewBadge)

# Technical Stuff
DNS Roaming is two components:

- a Client: to show the status of the service and configure settings
- a Service: to parse the rules and set DNS servers

All Code is written in C# with a WIX Toolkit based installer.

### Configuration

All Logs are saved to %ProgramData%\DNSRoaming

All Settings file are in XML and are saved to %ProgramData%\DNSRoadming\Settings

### Default Rule

If no rules exist; a default rule will be created that will:

- when connected to any Wireless Network
- on any network address
- set the DNS server to Quad (Preferred) and CloudFlare - No Malware (Alternative)

# FAQ

## Can I install the files manually?

Sure. DNS Roaming won't care where the service and client files are located. If you copy the [Program Files] files (exes, dlls and config files) to any folder you can run the client and install the service manually (using "SC CREATE").

NB: the log and settings files will always be stored under %ProgramData%\DNSRoaming 

## Can I sell DNS Roaming for profit?

This product is licensed using the Apache License 2.0. You can use it as a tool as part of a commercial service (e.g. as a MSP).
But you shouldn't be a dick and try and profit directly from by work by cloning my repo and selling your own renamed version. That would just be a shitty thing to do.

## Found a issue or bug?

In case you've found a bug, please open an issue on our GitHub.
Or you just think what I've done is stupid; then keep it to yourself. ;-)

# Licensing

See the [Apache 2.0](https://github.com/andrewbadge/DNSRoaming/blob/main/LICENSE) License

# Thanks

To every random person answering questions on StackExchange

To [Zky Icon](https://iconscout.com/contributors/zkyicon) for the [Server Dualtone Icon] used in this App. 
