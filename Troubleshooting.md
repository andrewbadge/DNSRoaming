# Troubleshooting

## Read the logs

![image](https://user-images.githubusercontent.com/15990355/131957665-10c4be59-9c59-4538-bdb7-65a0248305cd.png)

Logs are also in %ProgramData%\DNSRoaming folder.

## DNS Roaming and a VPN

I've tested with FortiNet's VPN client and the VPN connection will force its own DNS settings. DNS Roaming will still change the NIC's settings but the VPN connection wins here.
In this case its probably not terrible as you're connecting to a (hopefully) secure Enterprise network.

What you'll see in the logs is the VPN's DNS become Preferred and Alternate DNS. Your original NIC's DNS server become 3rd and 4th preference.

![image](https://user-images.githubusercontent.com/15990355/131958247-6bf1b7bf-810d-429e-8334-e9df4e806975.png)



