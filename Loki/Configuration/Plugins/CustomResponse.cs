using Loki.Configuration.Skeleton;

namespace Loki.Configuration.Plugins {
    public abstract class CustomResponse : ResponseBase {
        public abstract string CustomName { get; }
    }
}