using Gamlo.ValidationApi.Core.Interfaces;
using Gamlo.ValidationApi.Core.Model;
using System.Text.RegularExpressions;

namespace Gamlo.ValidationApi.Service
{
    public class RegexValidator : IValidator
    {
        public string Name => "Regex";

        virtual public bool IsValueValid(SchemeModel scheme, ValueModel value)
        {
            return Regex.IsMatch(value.Value, scheme.Constraint);
        }
    }
}
