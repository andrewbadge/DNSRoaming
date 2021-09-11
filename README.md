# DNS Roaming Client and Service

## Quick Start

Go to [Releases](https://github.com/andrewbadge/DNSRoaming/releases) and download the most recent **DNSRoaming-ServiceAndClient.msi** file. 
Install the MSI manually, via a policy or script.

Once installed run the DNS Roaming Client to Create or Edit Rules

![image](https://user-images.githubusercontent.com/15990355/132930618-01ad00db-d038-4674-a3e7-610707ab8252.png)

NB: you will need **Run as Administrator** to install this MSI as a Windows Service is installed.

System Requirements:
- Windows 10/11 and a compatible PC
- .Net Framework 4.7 (In general will already be installed)

NB: it will probably work fine on older OS but its untested. Ensure [.NET Framework 4.7 is installed](https://www.microsoft.com/en-us/download/details.aspx?id=55167)

## Why is it Beta?

I haven't tested the installation in enough scenarios yet nor have I got feedback from anyone else. I believe the functionality and quality is solid but expected a random stuff-up until the first feedback is in.

## What is DNS Roaming

DNS Roaming is a Client and Service to ensure the DNS Servers are set via a rule / policy rather than the network a PC is connected to. 
This allows an adminstrator to force that Quad9 or CloudFlare's DNS server is set rather than an ISPs DNS.

This is particularly useful if you want to block malware links for PCs you manage. In general this would apply to MSPs, Corporate or school laptops (devices that travel into different networks frequently..work then home and back to work again).

e.g.
- If the connected Network is Wireless then set the DNS to Quad9.
- If the connected Network is Ethernet and in the 10.0.0.1/24 Subnet then set the DNS to 10.0.0.10 and 10.0.0.11.

If you find this useful and would like me to continue working on it please [![Buy Me a Coffee](https://github.com/andrewbadge/DNSRoaming/blob/main/Images/BuyMeACoffee.png)](https://github.com/sponsors/andrewbadge) or Sponsor this project.

## Why DNS Roaming?

![image](https://user-images.githubusercontent.com/15990355/132497136-99aca035-9c05-4e2b-8f9a-3a6e39592118.png)

DNS can play a strong part in the security of a PC. With the right DNS service; malicous links, sites and servers can be blocked. 
If you have the money a service like CISCO Umbrella or DNS Filter are effective.

If you don't have the money then you rely on the configuration of the destination network (and a knowledable user).

Of course if this is your managed corporate or home network then you have the control you need. 
But do you have the control over your friends home network? Do you have control over each of your co-workers home networks? No.

## Help, Troubleshoot, Screenshots and more

[Help and Troubleshooting](https://github.com/andrewbadge/DNSRoaming/blob/main/Help/Readme.md)

Or Jump to [DNS Set Definitions](https://github.com/andrewbadge/DNSRoaming/blob/main/Help/DNSSets.md)

[Deployment](https://github.com/andrewbadge/DNSRoaming/blob/main/Deployment/Readme.md)

[Technical Details](https://github.com/andrewbadge/DNSRoaming/blob/main/Help/Technical.md)

[FAQs](https://github.com/andrewbadge/DNSRoaming/blob/main/Help/FAQ.md)

## Found a issue or bug?

See the [Troubleshooting](https://github.com/andrewbadge/DNSRoaming/blob/main/Help/Readme.md) section in Help

In case you've found a bug, please open an issue on our GitHub.
Or you just think what I've done is stupid; then keep it to yourself. ;-)

## Got a favourite DNS server you'd like added to DNS Sets?

Let me know via the issues. If you can include a link to DNS values and a about page please.

# Licensing

See the [Apache 2.0](https://github.com/andrewbadge/DNSRoaming/blob/main/LICENSE) License

# Thanks

@jsakamoto for the [ipaddressrange](https://github.com/jsakamoto/ipaddressrange) library
 
To [Zky Icon](https://iconscout.com/contributors/zkyicon) for the [Server Dualtone Icon] used in this App. 

To every random person answering questions on StackExchange
