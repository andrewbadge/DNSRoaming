
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

![image](https://user-images.githubusercontent.com/15990355/159150915-dd94e855-dc7f-4cd3-99e6-0b47c12aa711.png)

Network Options

![image](https://user-images.githubusercontent.com/15990355/159150925-c23820cb-9b89-4a67-8bc0-c1ad2858b2fa.png)

Log Options

![image](https://user-images.githubusercontent.com/15990355/159150932-99603579-b3d1-4bcb-9359-3b7310b11b2f.png)

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

![image](https://user-images.githubusercontent.com/15990355/159150961-b943ba4c-25c7-4962-8688-aa3af6ebd162.png)

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

