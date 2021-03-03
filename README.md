# fried-fame-client-windows
This is a sample project for Fried-Fame offering a complete VPN functioanlity in the form of a C# desktop application.

Video installation-guide coming sooon.

### Feature List:
- Secure web API to your Fried-Fame web front.
- OpenVPN Tunneling with realtime data reports.
- Secure session storage system.
- Update checks.
- Task-Tray icon



# Build Guide
The build-guide for this project is fairly straight forward. That is the primary objective for the Fried-Fame project, to allow you to easily create your own VPN service wih relative ease.


## Prerequisite
- Visual Studio with .NET 4.7 support.
- Inno Setup (Optional)

## Updating images (Optional)
To update the branding of the VPN, you will need to update the following images.

`./BuildFiles/assets/images/evpn.png` (300x145)

`./BuildFiles/assets/images/icon.ico` (255x255)

`./fried-fame-client-windows/Assets/icon.ico` (255x255)


## Updating branding (Optional)
Most branding shown to the user within the VPN client, will be fetched from your deployment of fried-fame. There is also branding properties within Project Properties that cannot be fetched from your deployment of Fried-Fame, so they will need to be manually configured. Follow the following steps:

- Open project in Visual Studio.

- Navigate to Project -> fried-fame-client-windows Properties

- Click "Assembly Information"

- (Optional) Take note of the version for later - Default is likely 1.0.0.0

- Fill the fields displayed

- Click "OK"

- Close tab


## Updating Server API Endpoint

There is a C# Script entitled "Constants.cs" which contains a critcal variable that points to your deployment of Fried-Fame. All information communicates are built off of this web address.

- Open project in Visual Studio

- On the right of your screen, find "Solution Explorer"

- Exapand "fried-fame-client-windows". You will be able to see several files.

- Click and open "Constants.cs"

- By default, the variable "REMOTE_CONTEXT" is configured to point to a test deployment of Fried-Fame (gfee.net). We will configure this to our API.

- You will want to replace REMOTE_CONTEXT content with "https://domain.com/website_prefix/containers/winnative/api/context". Replate domain.com with YOUR domain. You may need to remove "website_prefix" DEPENDING if you installed fried-fame in the ROOT of your website, or otherwise adjust it to your installation directory.

- Server API has been updated.

## Building project
In debug mode, it skips administrative checks just to ensure the front-end is wokring. VPN Connectivity may not be functioanl in debug mode.

- Open porject in Visual Studio

- Seelct whether you want a release or debug build. For publishing this to Fried-Fame website, we heavily suggest RELEASE.

- To build, click Build -> Build Solution OR ctrl+shift+b

- Project output will be located ./fried-fame-client-windows/bin/Release/ or ../Debug

## Building Installation Executable. (Optional)
To build the installation file, you will need Inno Setup.

- Open "setup generator.iss" located in project root.

- Adjust macros up top appropriately. Namely MyAppName, MyAppVersion, MyAppPublisher, MyAppURL etc.

- Important to note, that changing MyAppExeName WILL result in errors UNLESS you modify the executable name located in `./fried-fame-client-windows/bin/Release/` to match. You will need to modify the file name every time you compile the project as release.

- Ensure MyAppVersion matches the version of the C# prject settings.

- Navigate to Build -> Compile OR press ctrl+f9 to build project.

- Your setup file can be found `./Output/`

# Deployment
Deployment process is entierly manual at this projects current state. In the future, scripts making use of InternalAPI may be implemented.

- Please see "Building Project" for information on building the installer.

- Navigate to your deployment of Fried-Fame as an Administrator account.

- Go to your control panel

- On the side-bar, select "New Package"

- Select platform "Windows"

- Enter the version mentioned above. ENSURE THE VERSION IS CONSISTENT ACROSS PROJECT-PROPERTIES, INSTALLATION BUILDING, AND HERE. If it is not consistent, it WILL result in a bad user experience.

- Select the package you wish to upload. The package SHOULD be the installation file built above, it can be found at `./Output`.

- Submit and your deployment has been created.

# FAQ

## 413 error, or file too large
If you get this error while deploying a version, check the following:

https://stackoverflow.com/questions/2184513/change-the-maximum-upload-file-size

https://stackoverflow.com/questions/24306335/413-request-entity-too-large-file-upload-issue
