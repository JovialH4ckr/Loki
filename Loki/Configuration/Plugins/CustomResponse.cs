using Loki.Configuration.Skeleton;

namespace Loki.Configuration.Plugins {
    public abstract class CustomResponse : ResponseBase {
        public abstract string Id { get; }
        public override string Type => "Custom";
    }
}