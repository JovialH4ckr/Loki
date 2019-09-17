using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Loki.Configuration.Skeleton;

namespace Loki.Configuration.Plugins {
    static class PluginManager {
        internal static IList<IPlugin> Plugins = new List<IPlugin>();
        
        internal static void DiscoverPlugins() {
            foreach (var dll in ConfigManager.Settings.Plugins) {
                var asm = Assembly.LoadFrom(Path.GetFullPath(dll));
                foreach (var type in asm.GetExportedTypes()) {
                    if (!typeof(IPlugin).IsAssignableFrom(type))
                        continue;

                    Plugins.Add((IPlugin)Activator.CreateInstance(type));
                }
            }
        }

        internal static ResponseBase ResolveCustomResponse(string name) {
            foreach (var p in Plugins) {
                foreach (var c in p.GetResponses()) {
                    if (c.Name == name)
                        return c;
                }
            }
            
            throw new ArgumentOutOfRangeException(name);
        }
    }
}