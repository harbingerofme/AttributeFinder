# AttributeFinder #

Jsonifies bepinex plugins to their attributes.
Requires a (stripped) bepinex.dll in the same folder as either the program or the target.

Usage `AttributeFinder.exe {pathToAssembly}`

---

Output is of the form:
```xml
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
