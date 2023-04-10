# About DNS Roaming

## What is DNS Roaming?

DNS Roaming is a free and Open Source Client and Service to ensure the DNS Servers are set via a rule / policy rather than the network a PC is connected to. 
This allows an adminstrator to force that Quad9 or CloudFlare's DNS server is set rather than an ISPs DNS.

This is particularly useful if you want to block malware links for PCs you manage. In general this would apply to MSPs, Corporate or school laptops (devices that travel into different networks frequently; work then home and back to work again).

e.g.
- If the connected Network is Wireless then set the DNS to Quad9.
- If the connected Network is Ethernet and in the 10.0.0.1/24 Subnet then set the DNS to 10.0.0.10 and 10.0.0.11.

## Why DNS Roaming?

![image](https://user-images.githubusercontent.com/15990355/132497136-99aca035-9c05-4e2b-8f9a-3a6e39592118.png)

DNS can play a strong part in the security of a PC. With the right DNS service; malicous links, sites and servers can be blocked. 
If you have the money, a service like CISCO Umbrella or DNS Filter are effective.

If you don't have the money then you rely on the configuration of the destination network (and a knowledable user).

Of course if this is your managed corporate or home network then you have the control you need. 
But do you have the control over your friends home network? Do you have control over each of your co-workers home networks? No.
