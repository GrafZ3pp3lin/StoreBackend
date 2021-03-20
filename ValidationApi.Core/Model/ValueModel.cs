using System;
using System.Text.Json.Serialization;

namespace Gamlo.ValidationApi.Core.Model
{
    public class ValueModel
    {
        [JsonConstructor]
        public ValueModel(string scheme, string value)
        {
            Scheme = scheme ?? throw new ArgumentNullException(nameof(scheme));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Scheme { get; init; }
        public string Value { get; init; }
    }
}