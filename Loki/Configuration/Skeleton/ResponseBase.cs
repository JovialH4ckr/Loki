using System.Net;
using Newtonsoft.Json;

namespace Loki.Configuration.Skeleton {
    [JsonConverter(typeof(BaseConverter))]
    public abstract class ResponseBase {
        [JsonRequired]
        public virtual string Type { get; }

        [JsonRequired]
        public string Name { get; set; } = "LokiResponse";
        
        [JsonRequired]
        public string Url { get; set; }

        public abstract void ProcessResponse(HttpListenerRequest request, HttpListenerResponse response);
    }
}
