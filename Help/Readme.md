
# Help and Troubleshooting

## Screenshots

### Tray Menu

Right client the Tray icon for:

- Settings: Add, Edit or Remove Rules
- About: Opens this GitHub Page
- Service Status: Is the service Installed, Running or Stopped
- Logs: View a log or open the folder

![Tray Menu](https://github.com/andrewbadge/DNSRoaming/raw/main/Images/DNSRoamingClient-TrayMenu.PNG)

### Settings

List of Rules to Add, Edit or Remove

![image](https://user-images.githubusercontent.com/15990355/145540011-e363f18e-a905-4f99-bce5-01cb699e34fc.png)

Options

![image](https://user-images.githubusercontent.com/15990355/145540035-c49a184b-4f53-48e9-8d10-2073c0b20c04.png)

### Edit Rule

Editing a Rule

- Network Type: When the type is one of the select network Types. Common Networks are Ethernet or Wireless80211 (Wi-Fi). Types will work across different PCs.
- Network Name: when the Network Name is XXX. NB: This is unlikely to be consistent across different PCs.
- Address Filter
  - Any Address
  - By LAN Address
  - By WAN Address
- Address: Can be any address or a specific address range (Defined by Address and subnet). Use the icon to fill your current network's address.
- Set to: a preefined list or use a specific preferred or alernative network. Use the icon to fill copy down a DNS set to the feilds below.

![Edit Rule](![image](https://user-images.githubusercontent.com/15990355/155834962-6971c4b1-a53c-426c-8861-2bfbcf1c53c2.png))

### DNS Sets

DNS Roaming has several preset DNS options you can choose from including Quad9 and CloudFlare. Use the copy button to move the values in the Preferred and Alternate DNS fields.

Alternatively you can set your own DNS servers.

Read more about the [DNS Set Definitions](https://github.com/andrewbadge/DNSRoaming/blob/main/DNSSets.md)

## Troubleshooting

### Read the logs

![image](https://user-images.githubusercontent.com/15990355/131957665-10c4be59-9c59-4538-bdb7-65a0248305cd.png)

Logs are also in the %ProgramData%\DNSRoaming folder.

### DNS Roaming and a VPN

I've tested with FortiNet's VPN client and the VPN connection will force its own DNS settings. DNS Roaming will still change the NIC's settings but the VPN connection wins here.
In this case its probably not terrible as you're connecting to a (hopefully) secure Enterprise network.

What you'll see in the logs is the VPN's DNS become Preferred and Alternate DNS. Your original NIC's DNS server become 3rd and 4th preference.

![image](https://user-images.githubusercontent.com/15990355/131958247-6bf1b7bf-810d-429e-8334-e9df4e806975.png)

