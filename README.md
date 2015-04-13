# About DotNetNuke.R7

*DotNetNuke.R7* is a library speedup development of various extensions (primarily, modules)
for DNN Platform web CMS. It includes DAL2-based generic controller, a set of base classes for modules and settings,
useful extension methods for webcontrol and strings, various utilities and some version-specific hacks.

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

## Use

To use library features, add *DotNetNuke.R7* to your usings:

```C#
using DotNetNuke.R7;
...
```

# Links

- [NuGet repository page](https://www.nuget.org/packages/DotNetNuke.R7/)
- Examples
