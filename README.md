# About R7.DotNetNuke.Extensions

[![R7.DotNetNuke.Extensions on NuGet.org](https://img.shields.io/nuget/v/R7.DotNetNuke.Extensions.svg)](https://www.nuget.org/packages/R7.DotNetNuke.Extensions)

*R7.DotNetNuke.Extensions* is a library for DNN Platform web CMS extensions development (primarily, modules). 
It includes DAL2-based generic data provider, a set of base classes for modules and settings, 
useful extension methods for webcontrols and strings, various utilities and some version-specific hacks.

The pragmatic goal of *R7.DotNetNuke.Extensions* project is to provide a library, which could be shared
by all existing *R7* extensions for DNN Platform, and also new ones via 
[R7.DnnTemplates](https://github.com/roman-yagodin/R7.DnnTemplates) integration.

## License

[![LGPLv3](http://www.gnu.org/graphics/lgplv3-147x51.png)](http://www.gnu.org/licenses/lgpl.txt)

The *R7.DotNetNuke.Extensions* library is free software: you can redistribute it and/or modify it under the terms of 
the GNU Lesser General Public License as published by the Free Software Foundation, either version 3 of the License, 
or (at your option) any later version.

## Add to project

The most convenient way to add *R7.DotNetNuke.Extensions* library in your project is to use *NuGet* package manager.

For *Visual Studio*, use package manager console:

```Shell
PM> Install-Package R7.DotNetNuke.Extensions
```
For pre-release versions:

```Shell
PM> Install-Package R7.DotNetNuke.Extensions -Pre
```

For *MonoDevelop* / *Xamarin Studio*, use *Add &gt; Add NuGet Packages* command in the context menu of the project,
then search for *"R7.DotNetNuke.Extensions"* and install it. Check *Show pre-release packages* to install pre-release versions.

After that you can add *R7.DotNetNuke.Extensions* namespaces to your usings:

```C#
using R7.DotNetNuke.Extensions;
...
```

## Ship your extension

To ship your extension which use *R7.DotNetNuke.Extensions* library, declare the dependency in the DNN extension package 
manifest file (see example below) and provide link to the corresponding release in the project
[releases](https://github.com/roman-yagodin/R7.DotNetNuke.Extensions/releases).

```XML
<dependencies>
    <-- replace 0.x.y with actual used library version -->
    <dependency type="managedPackage" version="0.x.y">R7.DotNetNuke.Extensions</dependency>
<dependencies>
```

If you want to ship object files (DLL's) of unmodified *R7.DotNetNuke.Extensions* library in your package, 
make sure that you also include copy of library license and give prominent notice with each copy of your package 
that the library is used in it and that the library and its use are covered by its license.

## Links

- [NuGet repository page](https://www.nuget.org/packages/R7.DotNetNuke.Extensions)
- [R7.Documents](https://github.com/roman-yagodin/R7.Documents) module
- [R7.University](https://github.com/roman-yagodin/R7.University) educational organization subsystem
