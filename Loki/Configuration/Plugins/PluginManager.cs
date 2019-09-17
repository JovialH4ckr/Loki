using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Loki.Configuration.Plugins {
    static class PluginManager {
        internal static IList<IPlugin> Plugins = new List<IPlugin>();
        
        internal static void DiscoverPlugin() {
            var dlls = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            dlls = dlls.Where(f => string.Equals(Path.GetExtension(f), ".dll", StringComparison.OrdinalIgnoreCase)).ToArray();

            foreach (var dll in dlls) {
                var asm = Assembly.LoadFrom(Path.GetFullPath(dll));
                foreach (var type in asm.GetExportedTypes()) {
                    if (!typeof(IPlugin).IsAssignableFrom(type))
                        continue;

                    Plugins.Add((IPlugin)Activator.CreateInstance(type));
                }
            }
        }
    }
}