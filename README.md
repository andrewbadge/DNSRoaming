# DNS Roaming Client and Service

## Quick Start

Go to [Releases](https://github.com/andrewbadge/DNSRoaming/releases) and download the most recent release (and MSI file).
Install the MSI manually, via a policy or script.

NB: you will need Run as **Administrator** to install this MSI as a Windows Service is installed.

System Requirements:
- Windows 10/11 and a compatible PC
- .Net Framework 4.7 (In general will already be installed)

NB: it will probably work fine on older OS but its untested. Ensure [.NET Framework 4.7 is installed](https://www.microsoft.com/en-us/download/details.aspx?id=55167)

## Why is it Beta?

I haven't tested the installation in enough scenarios yet nor have I got feedback from anyone else. I believe the functionality and quality is solid but expected a randon stuff-up until the first feedback is in.

## What is DNS Roaming

DNS Roaming is a Client and Service to ensure the DNS Servers are set via a rule / policy rather than the network a PC is connected to. 
This allows an adminstrator to force that Quad9 or CloudFlare's DNS server is set rather than an ISPs DNS.

This is particularly useful if you want to block malware links for PCs you manage.

e.g.
- If the connected Network is Wireless then set the DNS to Quad9.
- If the connected Network is Ethernet and in the 10.0.0.1/24 Subnet then set the DNS to 10.0.0.10 and 10.0.0.11.

If you find this useful and would like me to continue working on it please [![Buy Me a Coffee](https://github.com/andrewbadge/DNSRoaming/blob/main/Images/BuyMeACoffee.png)](https://www.buymeacoffee.com/AndrewBadge)

## Why DNS Roaming?

DNS can play a strong part in the security of a PC. With the right DNS service; malicous links, sites and servers can be blocked. 
If you have the money a service like CISCO Umbrella or DNS Filter are effective.

If you don't have the money then you rely on the configuration of the destination network (and a knowledable user).

Of course if this is your managed corporate or home network then you have the control you need. 
But do you have the control over your friends home network? Do you have control over each of your co-workers home networks? No.

## Help and Screenshots

[Screenshots and Help](https://github.com/andrewbadge/DNSRoaming/blob/main/Images/Readme.md)

[DNS Set Definitions](https://github.com/andrewbadge/DNSRoaming/blob/main/DNSSets.md)

[Deployment Examples](https://github.com/andrewbadge/DNSRoaming/blob/main/Deployment.md)

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

# FAQ

## Can I install the files manually?

Sure. DNS Roaming won't care where the service and client files are located. If you copy the [Program Files] files (exes, dlls and config files) to any folder you can run the client and install the service manually (using "SC CREATE").

NB: the log and settings files will always be stored under %ProgramData%\DNSRoaming 

## Found a issue or bug?

In case you've found a bug, please open an issue on our GitHub.
Or you just think what I've done is stupid; then keep it to yourself. ;-)

## Why is Quad9 and CloudFlare default?

My original objective was to get fast and effective blocking of malware links and URLs for free. Both of these services are well known and high quality. You can always set your own customer servers!

Read about the [DNS Set Definitions](https://github.com/andrewbadge/DNSRoaming/blob/main/DNSSets.md)

## Got another DNS server suggestion?

Let me know via the issues.

# Licensing

See the [Apache 2.0](https://github.com/andrewbadge/DNSRoaming/blob/main/LICENSE) License

# Thanks

To every random person answering questions on StackExchange

To [Zky Icon](https://iconscout.com/contributors/zkyicon) for the [Server Dualtone Icon] used in this App. 
