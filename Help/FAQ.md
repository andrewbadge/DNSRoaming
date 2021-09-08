# Frequently Asked Questions

## Can I install the files manually?

Sure. DNS Roaming won't care where the service and client files are located. If you copy the [Program Files] files (exes, dlls and config files) to any folder you can run the client and install the service manually (using "SC CREATE").

NB: the log and settings files will always be stored under %ProgramData%\DNSRoaming 

## Why is Quad9 and CloudFlare default?

My original objective was to get fast and effective blocking of malware links and URLs for free. Both of these services are well known and high quality. You can always set your own customer servers!

Read about the [DNS Set Definitions](https://github.com/andrewbadge/DNSRoaming/blob/main/DNSSets.md)

## Limitations

### DNS Roaming only supports IPV4

Currently DNS Roaming only supports IPV4 settings and configuration. In the future I'll make it compatible with IPV6 as well.
