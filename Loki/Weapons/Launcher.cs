using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Harmony;
using Loki.Configuration;
using Loki.Configuration.Plugins;

namespace Loki.Weapons {
    static class Launcher {
        static Launcher() {
            PluginManager.DiscoverPlugins();
            HarmonyInstance.Create("loki").PatchAll(typeof(Launcher).Assembly);
        }

        internal static Assembly RealAssembly;
        
        internal static void Go() {
            var asm = TryLoadAssembly();
            if (asm == null)
                return;

            RealAssembly = asm;
            Console.Clear();
            Console.Title = string.Empty;
            
            new Thread(() => {
                Thread.Sleep(1000); //Make sure ^ can be read
                var entry = asm.EntryPoint;
                asm.EntryPoint.Invoke(null, entry.GetParameters().Length > 0 ? new object[] { ConfigManager.Settings.Parameters } : null);
            }) { IsBackground = true }.Start();

            Server.Go();
        }
        
        static Assembly TryLoadAssembly() {
            try {
                return Assembly.LoadFile(Path.GetFullPath(ConfigManager.Settings.ExecutablePath));
            }
            catch {
                return null;
            }
        }
    }
}