<#
.SYNOPSIS
   Downloads DNS Roaming and installs (if not already installed)
.DESCRIPTION
    NB: Update the variables with the Paths that suit you.
    Set the download URL to a release you choose from https://github.com/andrewbadge/DNSRoaming/releases

    Script has to be run as admin
.INPUTS
    None
.OUTPUTS
    Debug Console information only
.NOTES
    File Name      : Deploy-Windows-DNSRoaming.ps1
    Author         : Andrew Badge
    Prerequisite   : Powershell 5.1    
#>

function Download-File {
    param (
        [Parameter(Mandatory = $true)]
        [string] $GitHubFileURL,
        [Parameter(Mandatory = $true)]
        [string] $LocalFileName
    )    

    try {
        
        $wc = New-Object System.Net.WebClient
        $wc.DownloadFile($GitHubFileURL,"$LocalFileName")
        
    } catch {
        Write-Host "Fatal Exception:[$_.Exception.Message]"
    }
}

function Is-Application-Installed {
    param (
        [string] $AppName
    )    

    try {

        $installed = (Get-ItemProperty HKLM:\Software\Microsoft\Windows\CurrentVersion\Uninstall\* | Where { $_.DisplayName -like $AppName }) -ne $null
        If(-Not $installed) {
            $installed = (Get-ItemProperty HKLM:\Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\* | Where { $_.DisplayName -like $AppName }) -ne $null
        }

        If(-Not $installed) {
            Write-Host "'$AppName' NOT is installed."
            return $false
        } else {
            Write-Host "'$AppName' is installed."
            return $true
        }

    } catch {
        Write-Host "Fatal Exception:[$_.Exception.Message]"
    }
}

function Install-MSI {
    param (
        [Parameter(Mandatory = $true)]
        [string] $AppName,
        [Parameter(Mandatory = $true)]
        [string] $DownloadURL,
        [Parameter(Mandatory = $true)]
        [string] $LocalFolder,
        [Parameter(Mandatory = $true)]
        [string] $LocalFileName
    )    

        try {

            $File = "$LocalFolder\$LocalFileName"

            # Check if already installed
            $IsInstalled = Is-Application-Installed $AppName
            if (!$IsInstalled) {

                # Create the Base Folder if missing
                New-Item -ItemType Directory -Force -Path $LocalFolder

                # Check if a 64-Bit OS. Who runs a 32-Bit OS anymore? 
                if ((Get-WmiObject win32_operatingsystem | select-object osarchitecture).osarchitecture -eq "64-bit")
                {
                    if(!(Test-Path $File)) {
                        Write-Host "Downloading $AppName"
                        Download-File $DownloadURL $File
                    }

                    Write-Host "Installing $AppName"
                    msiexec /q /i "$File"
                }
                else
                {
                    Write-Host "This script does not run on a 32-bit OS. Nothing installed."
                }
                Write-Host "Complete."
            }
        } catch {
            Write-Host "Fatal Exception:[$_.Exception.Message]"
        }
    }

function Download-File-If-Missing {
    param (
        [Parameter(Mandatory = $true)]
        [string] $DownloadURL,
        [Parameter(Mandatory = $true)]
        [string] $LocalFolder,
        [Parameter(Mandatory = $true)]
        [string] $LocalFileName
    )    

        try {

            $File = "$LocalFolder\$LocalFileName"

                # Create the Base Folder if missing
                New-Item -ItemType Directory -Force -Path $LocalFolder

                if(!(Test-Path $File)) {
                    Write-Host "Downloading $DownloadURL"
                    Download-File $DownloadURL $File
                }
                Write-Host "Complete."
        } catch {
            Write-Host "Fatal Exception:[$_.Exception.Message]"
        }
    }


# Download a precreated Default Rule
$URL = "https://raw.githubusercontent.com/YOUROWNREPO/YOUROWNPATH/Rule-4444588d-4a33-25b8-817e-0fb02341e242.xml"
$BaseFolder = "$env:ALLUSERSPROFILE\DNSRoaming\Settings"
# Important! The Rule Filename GUID should match the Rule ID inside the file
$Filename = "Rule-4444588d-4a33-25b8-817e-0fb02341e242.xml"
Download-File-If-Missing $URL $BaseFolder $Filename

# Download a precreated Options File
$URL = "https://raw.githubusercontent.com/YOUROWNREPO/YOUROWNPATH/Options-13Sept2021.xml"
$BaseFolder = "$env:ALLUSERSPROFILE\DNSRoaming\Options"
$Filename = "Options.xml"
Download-File-If-Missing $URL $BaseFolder $Filename

# App Name
$App = "DNS Roaming"
# Base path where the download file will be stored
$BaseFolder = "$env:ALLUSERSPROFILE\DNSRoaming\Downloads"
# Local File name of the downloaded file
$Filename = "DNSRoaming-ServiceOnly-Beta11.msi"
# URL to download from
$URL = "https://github.com/andrewbadge/DNSRoaming/releases/download/Beta11/DNSRoaming-ServiceOnly.msi"
Install-MSI $App $URL $BaseFolder $Filename
