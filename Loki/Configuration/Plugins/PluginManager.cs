using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Loki.Configuration.Skeleton;

namespace Loki.Configuration.Plugins {
    static class PluginManager {
        static readonly IList<IPlugin> _plugins = new List<IPlugin>();
        
        static PluginManager() {
            foreach (var dll in ConfigManager.Settings.Plugins) {
                var asm = Assembly.LoadFrom(Path.GetFullPath(dll));
                foreach (var type in asm.GetExportedTypes()) {
                    if (!typeof(IPlugin).IsAssignableFrom(type))
                        continue;

                    _plugins.Add((IPlugin)Activator.CreateInstance(type));
                }
            }
        }

        internal static void ResolveCustomResponses() {
            for (var i = 0; i < ConfigManager.Settings.Responses.Count; i++) {
                var curr = ConfigManager.Settings.Responses[i];
                if (curr.GetType() != typeof(PlaceHolderResponse))
                    continue;

                var id = (curr as PlaceHolderResponse).Id;
                ConfigManager.Settings.Responses[i] = ResolveId(id);
            }
        }
        
        static ResponseBase ResolveId(string id) {
            foreach (var p in _plugins) {
                foreach (var c in p.GetResponses()) {
                    if (c.Id == id)
                        return c;
                }
            }
            
            throw new ArgumentOutOfRangeException(id);
        }
    }
}