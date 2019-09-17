using System.Collections.Generic;
using Newtonsoft.Json;
using Loki.Configuration.Skeleton;

namespace Loki.Configuration {
    class Config {
        [JsonRequired]
        public string Name { get; set; } = "LokiConfig";

        [JsonRequired]
        public string Author { get; set; } = "User";

        [JsonRequired]
        public string ExecutablePath { get; set; }
        
        public IList<string> Plugins { get; } = new List<string>();
        
        public IList<string> Parameters { get; } = new List<string>();

        [JsonRequired]
        public IList<ResponseBase> Responses { get; } = new List<ResponseBase>();
    }
}