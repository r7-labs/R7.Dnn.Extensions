# About DotNetNuke.R7

[![DotNetNuke.R7 on NuGet.org](https://img.shields.io/nuget/v/DotNetNuke.R7.svg)](https://www.nuget.org/packages/DotNetNuke.R7)

[![DotNetNuke.R7 on NuGet.org](https://buildstats.info/nuget/DotNetNuke.R7)](https://www.nuget.org/packages/DotNetNuke.R7)

*DotNetNuke.R7* is a library for DNN Platform web CMS extensions development (primarily, modules). 
It includes DAL2-based generic controller, a set of base classes for modules and settings, 
useful extension methods for webcontrols and strings, various utilities and some version-specific hacks.

*DotNetNuke.R7* project's pragmatic goal is to make a library, which could be shared
by all existing *R7.Solutions* modules, and also new ones via [R7.DnnTemplates](https://github.com/roman-yagodin/R7.DnnTemplates) integration.

## Add to project

The most convenient way to add *DotNetNuke.R7* library in your project is to use *NuGet* package manager.

For *Visual Studio*, use package manager console:

```Shell
PM> Install-Package DotNetNuke.R7
```

For *MonoDevelop* / *Xamarin Studio*, use *Add &gt; Add NuGet Packages* command in the context menu of the project,
then search for *"DotNetNuke.R7"* and install it.

## Start using it

To use library features, add *DotNetNuke.R7* to your usings:

```C#
using DotNetNuke.R7;
...
```

## Ship extensions

To ship your extension which use *DotNetNuke.R7* library, declare the dependency in the DNN extension package 
manifest file (see example below) and provide link to corresponding [release](https://github.com/roman-yagodin/DotNetNuke.R7/releases).

```XML
<dependencies>
    <dependency type="managedPackage" version="0.3.0">DotNetNuke.R7</dependency>
<dependencies>
```

## Links

- [NuGet repository page](https://www.nuget.org/packages/DotNetNuke.R7/)
- [R7.University](https://github.com/roman-yagodin/R7.University/) subsystem for DNN Platform
