using System;
using Loki.Configuration.Plugins;
using Loki.Configuration.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Loki.Configuration.Skeleton {
    //https://stackoverflow.com/a/30579193
    public class ConcreteClassConverter : DefaultContractResolver {
        protected override JsonConverter ResolveContractConverter(Type objectType) {
            if (typeof(ResponseBase).IsAssignableFrom(objectType) && !objectType.IsAbstract)
                return null;

            return base.ResolveContractConverter(objectType);
        }
    }

    public class BaseConverter : JsonConverter {
        static readonly JsonSerializerSettings _specifiedSubclassConversion = new JsonSerializerSettings { ContractResolver = new ConcreteClassConverter() };

        public override bool CanConvert(Type objectType) => objectType == typeof(ResponseBase);
        public override bool CanWrite => false;
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) { }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var jo = JObject.Load(reader);
            switch (jo["Type"].Value<string>()) {
                case "Text":
                    return JsonConvert.DeserializeObject<TextResponse>(jo.ToString(), _specifiedSubclassConversion);
                case "File":
                    return JsonConvert.DeserializeObject<FileResponse>(jo.ToString(), _specifiedSubclassConversion);
                case "Custom":
                    return new PlaceHolderResponse((string)jo["Id"]); //This will be resolved later
                default: throw new NotSupportedException();
            }
        }
    }
}