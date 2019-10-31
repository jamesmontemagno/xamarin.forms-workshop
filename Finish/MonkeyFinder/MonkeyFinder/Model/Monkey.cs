// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using MonkeyFinder.Model;
//
//    var monkeys = Monkeys.FromJson(jsonString);

using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MonkeyFinder.Model
{
    public partial class Monkey
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Location")]
        public string Location { get; set; }

        [JsonProperty("Details")]
        public string Details { get; set; }

        [JsonProperty("Image")]
        public Uri Image { get; set; }

        [JsonProperty("Population")]
        public long Population { get; set; }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class Monkey
    {
        public static Monkey[] FromJson(string json) => JsonConvert.DeserializeObject<Monkey[]>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Monkey[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
