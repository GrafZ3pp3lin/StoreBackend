using System;
using System.Text.Json.Serialization;

namespace Gamlo.ValidationApi.Core.Model
{
    /// <summary>
    /// Defines Value Scheme
    /// </summary>
    public class SchemeModel
    {
        [JsonConstructor]
        public SchemeModel(string constraint, string name, string validationType)
        {
            Constraint = constraint ?? throw new ArgumentNullException(nameof(constraint));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ValidationName = validationType ?? throw new ArgumentNullException(nameof(validationType));
        }

        /// <summary>
        /// Value Constraint
        /// </summary>
        public string Constraint { get; init; }

        /// <summary>
        /// Initial Value for all Values of this Type
        /// </summary>
        public string? InitialValue { get; init; }

        /// <summary>
        /// Name of Scheme
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Validation Type
        /// </summary>
        public string ValidationName { get; init; }
    }
}