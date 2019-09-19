using System.Collections.Generic;
using System.Net;
using System.Text;
using Loki.Configuration.Plugins;

namespace PluginExample {
    public class ExamplePlugin : IPlugin {
        public string Name => "ExamplePlugin";
        public string Author => "xsilent007";
        public IEnumerable<CustomResponse> GetResponses() {
            yield return new Example();
        }
        
        class Example : CustomResponse {
            public override string Id => "COOKIES!!!";
        
            public override void ProcessResponse(HttpListenerRequest request, HttpListenerResponse response) {
                var cookies = request.Cookies;
                if (cookies.Count > 1) {
                    var txt = (request.ContentEncoding ?? Encoding.UTF8).GetBytes("COOKIES ARE PRESENT!!! YUM");
                    response.OutputStream.Write(txt, 0, txt.Length);
                }
            }
        }
    }
}