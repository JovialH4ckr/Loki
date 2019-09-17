using System.Collections.Generic;
using Loki.Configuration.Skeleton;

namespace Loki.Configuration.Plugins {
    public interface IPlugin {
        string Name { get; }
        string Author { get; }
        IEnumerable<CustomResponse> GetResponses();
    }
}