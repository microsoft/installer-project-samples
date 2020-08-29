Simple example of creating an MSI installer that creates and starts a .NET Core Windows service using a Visual Studio installer project. The service was created using the "Worker Service" template in Visual Studio along with the Microsoft.Extensions.Hosting.WindowsServices package.  The service just logs to the Event Viewer.

Known issue: Force closing the running service on uninstall will sometimes be required.

Requires Visual Studio 2019 update 5 with the Installer projects extension (https://marketplace.visualstudio.com/items?itemName=visualstudioclient.MicrosoftVisualStudio2017InstallerProjects).
