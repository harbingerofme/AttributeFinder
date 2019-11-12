using Mono.Cecil;
using BepInEx;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttributeFinder
{
    class Program
    {
        static int Main(string[] args)
        {
            if(args.Length == 0)
            {
                return 1;
            }
            AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(args[0]);
            ModuleDefinition a = assembly.MainModule;
            var jsonDict = new JsonObject();
            foreach(TypeDefinition typ in a.Types)
            {
                foreach(CustomAttribute attr in typ.CustomAttributes)
                {
                    if( attr.AttributeType.FullName == "BepInEx.BepInPlugin")
                    {
                        jsonDict.Add("PluginInfo",PluginJson(attr.ConstructorArguments));
                    }
                    if (attr.AttributeType.FullName == "BepInEx.BepInDependency")
                    {
                        JsonArray array = (JsonArray) jsonDict.Add("Dependencies", new JsonArray());
                        array.Add(DependencyJson(attr.ConstructorArguments));
                    }
                    if (attr.AttributeType.FullName == "BepInEx.BepInIncompatibility")
                    {
                        JsonArray array = (JsonArray)jsonDict.Add("Incompatibilities", new JsonArray());
                        array.Add(attr.ConstructorArguments[0].Value.ToString());
                    }
                }
            }
            Console.WriteLine(jsonDict);
            return 0;
        }

        static JsonObject PluginJson(Mono.Collections.Generic.Collection<CustomAttributeArgument> arguments)
        {
            JsonObject obj = new JsonObject();
            obj.Add("GUID", arguments[0].Value.ToString());
            obj.Add("Name", arguments[1].Value.ToString());
            obj.Add("Version", arguments[2].Value.ToString());
            return obj;
        }

        static JsonThing DependencyJson(Mono.Collections.Generic.Collection<CustomAttributeArgument> arguments)
        {
            JsonObject dep = new JsonObject();
            dep.Add("Name", arguments[0].Value.ToString());
            string type = "";
            if (arguments.Count <= 1 || arguments[1].Value.ToString() == "1")
                type = "hard";
            else
                type = "soft";
            dep.Add("Type",type);
            return dep;

        }

        private abstract class JsonThing
        {
        }
        private class JsonArray : JsonThing
        {
            private List<string> Objects;
            public JsonArray()
            {
                Objects = new List<string>();
            }

            public void Add(string thing)
            {
                Objects.Add($"\"{thing}\"");
            }
            public void Add(JsonThing thing)
            {
                Objects.Add(thing.ToString());
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                sb.Append(string.Join(", ", Objects.ToArray()));
                sb.Append("]");
                return sb.ToString();
            }
        }

        private class JsonObject : JsonThing
        {
            private Dictionary<string,JsonThing> Objects;
            public JsonObject()
            {
                Objects = new Dictionary<string, JsonThing>();
            }

            public JsonThing Add(string key, JsonThing thing)
            {
                if (Objects.ContainsKey(key))
                {
                    return Objects[key];
                }
                Objects.Add(key, thing);
                return thing;
            }
            public JsonThing Add(string key, string thing)
            {
                return Add(key, new JsonString(thing));
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("{");
                List<string> list = new List<string>();
                foreach (var pair in Objects)
                {
                    list.Add($"\"{pair.Key}\" : {pair.Value}");
                }
                sb.Append(string.Join(", ",list.ToArray()));
                sb.Append("}");
                return sb.ToString();
            }
        }

        private class JsonString : JsonThing
        {
            string Value;
            public JsonString(string value)
            {
                Value = value;
            }

            public override string ToString()
            {
                return $"\"{Value}\"";
            }
        }
    }
}
