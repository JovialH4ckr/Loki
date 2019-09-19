using System.IO;
using System.Net;
using Loki.Configuration.Skeleton;
using Newtonsoft.Json;

namespace Loki.Configuration.Responses {
    class FileResponse : ResponseBase {
        public override string Type => "File";

        [JsonRequired]
        public string Path { get; set; }

        public override string ToString() => $"FileResponse [ Url: '{Url}' | Path: '{Path}' ]";

        byte[] _cache;
        public override void ProcessResponse(HttpListenerRequest request, HttpListenerResponse response) {
            var stream = response.OutputStream;
            if (_cache == null)
                _cache = File.ReadAllBytes(Path);
            
            stream.Write(_cache, 0, _cache.Length);
        }
    }
}
