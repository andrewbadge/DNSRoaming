# DNS Roaming Client and Service

## Quick Start for Individuals

Go to [Releases](https://github.com/andrewbadge/DNSRoaming/releases) and download the most recent **DNSRoaming-ServiceAndClient.msi** file. 

Run the MSI manually. Once installed run the DNS Roaming Client from the Windows Start menu to Create or Edit Rules

![image](https://user-images.githubusercontent.com/15990355/132930618-01ad00db-d038-4674-a3e7-610707ab8252.png)

NB: you will need **Run as Administrator** to install this MSI as a Windows Service is installed.

## Quick Start for Admins and Automated Deployement 

Go to [Releases](https://github.com/andrewbadge/DNSRoaming/releases) and download the most recent **DNSRoaming-ServiceOnly.msi** file. You'll want to deploy this via you favourite management tool,  via a policy or [script](https://github.com/andrewbadge/DNSRoaming/blob/main/Deployment/Deploy-Windows-DNSRoaming.ps1).

I'd recomment to use the [RULESETURL parameter](https://github.com/andrewbadge/DNSRoaming/blob/main/Deployment/RuleSetURL.md) when installing to make management of rules easier across many PCs.

## Topics

- [About DNS Roaming](https://github.com/andrewbadge/DNSRoaming/blob/main/Help/AboutDNSRoaming.md)

- [Help and Troubleshooting](https://github.com/andrewbadge/DNSRoaming/blob/main/Help/Readme.md)

- [DNS Set Definitions](https://github.com/andrewbadge/DNSRoaming/blob/main/Help/DNSSets.md)

- [Deployment](https://github.com/andrewbadge/DNSRoaming/blob/main/Deployment/Readme.md)

- [Automatic Updates](https://github.com/andrewbadge/DNSRoaming/blob/main/Deployment/DNS-Roaming-Updater.md) 

- [RULESETURL parameter](https://github.com/andrewbadge/DNSRoaming/blob/main/Deployment/RuleSetURL.md)

- [Technical Details](https://github.com/andrewbadge/DNSRoaming/blob/main/Help/Technical.md)

- [FAQs](https://github.com/andrewbadge/DNSRoaming/blob/main/Help/FAQ.md)

## Found a issue or bug?

See the [Troubleshooting](https://github.com/andrewbadge/DNSRoaming/blob/main/Help/Readme.md) section in Help

In case you've found a bug, please open an issue on our GitHub.

## Supporting the Project

If you find this useful and would like me to continue working on it please [![Buy Me a Coffee](https://github.com/andrewbadge/DNSRoaming/blob/main/Images/BuyMeACoffee.png)](https://buymeacoffee.com/AndrewBadge) or [Sponsor](https://github.com/sponsors/andrewbadge) this project via GitHub.

## Got a question or a  favourite DNS server you'd like added to DNS Sets?

Let me know via the issues. If suggesting a new DNS provider please include a link to DNS values and an about page for the service.

# Licensing

See the [Apache 2.0](https://github.com/andrewbadge/DNSRoaming/blob/main/LICENSE) License

# Thanks

@jsakamoto for the [ipaddressrange](https://github.com/jsakamoto/ipaddressrange) library

[Octokit](https://github.com/octokit) for the GitHub API library
 
[MichaCo / DnsClient.NET](https://github.com/MichaCo/DnsClient.NET) for the DNSClient library
 
To [Zky Icon](https://iconscout.com/contributors/zkyicon) for the [Server Dualtone Icon] used in this App. 

To every random person answering questions on StackExchange
