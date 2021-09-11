# Frequently Asked Questions

## Can I install the files manually?

Sure. DNS Roaming won't care where the service and client files are located. If you copy the [Program Files] files (exes, dlls and config files) to any folder you can run the client and install the service manually (using "SC CREATE").

NB: the log and settings files will always be stored under %ProgramData%\DNSRoaming 

## Why is Quad9 and CloudFlare default?

My original objective was to get fast and effective blocking of malware links and URLs for free. Both of these services are well known and high quality. You can always set your own customer servers!

Read about the [DNS Set Definitions](https://github.com/andrewbadge/DNSRoaming/blob/main/DNSSets.md)

## Can DNS Roaming report on DNS traffic?

No. DNS Roaming only sets the DNS server address. It does not see any traffic or queries the PC or Apps make to the DNS Servers.

## Limitations

### DNS Roaming only supports IPV4

Currently DNS Roaming only supports IPV4 settings and configuration. A future update may make it compatible with IPV6 as well.

As a workaround; DNS Roaming supports disabling IPV6 for Network Interface that match a rule. See the Options tab.

### DNS Roaming Client installation

<del>The current install will install the Service and Client everytime. A future update may allow for the client to be optional.
This would be particuarly useful for MSP distribution where the rules are preset and you don't want users changing them.</del>

Resolved in v1.0.0.11. See [Deployment](https://github.com/andrewbadge/DNSRoaming/blob/main/Deployment/Readme.md) for more details.
