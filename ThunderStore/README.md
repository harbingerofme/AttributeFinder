# Attribute Finder

**Attribute Finder** is an open source utility tool to help discover mod dependencies, incompatabilities and general plugininfo.
You may use this tool in whatever project you want without attribution. You may freely redistribute this tool and make modifications to the source code.

You may **not** freely redistribute the icon. It's graciously provided for free by [Vecteezy](https://www.vecteezy.com/free-vector/magnifying)


---

## How to use

Jsonifies bepinex plugins to their attributes.

Usage `AttributeFinder.exe {pathToAssembly}`

---

Output is of the form:
```
{
	PluginInfo: {"GUID" : "<PLUGINGUID>", "Name" : "<PLUGINNAME>", "Version": "<PLUGINVERSION>"}
	Dependencies: [ {"Name" : "<MODGUID1>", "Type" : <"hard"|"soft">},{"Name" : "<MODGUID2>", "Type" : <"hard"|"soft">}]
	Incompatabilities: [ {"<MODGUID1>", "<MODGUID2>"]
}
```
There's no guaranteed ordering within a specific output.  If a key is not present in the plugin, it won't be present in the output.

---

Example:
```
./AttributeFinder.exe HarbTweaks.dll
{"Dependencies" : [{"Name" : "community.mmbait", "Type" : "soft"}, {"Name" : "community.mmhook", "Type" : "soft"}, {"Nam
e" : "com.bepis.r2api", "Type" : "soft"}], "PluginInfo" : {"GUID" : "com.harbingerofme.harbtweaks", "Name" : "HarbTweaks
", "Version" : "1.0.0"}}
```
