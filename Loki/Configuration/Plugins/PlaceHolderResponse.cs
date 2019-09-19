using System.Net;
using Loki.Configuration.Skeleton;

namespace Loki.Configuration.Plugins {
    sealed class PlaceHolderResponse : ResponseBase {
        internal PlaceHolderResponse(string id) => Id = id;
        
        internal string Id { get; }
        
        public override void ProcessResponse(HttpListenerRequest request, HttpListenerResponse response) {
            throw new System.NotImplementedException();
        }
    }
}