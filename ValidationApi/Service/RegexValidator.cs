using Gamlo.StoreBackend.Model;
using System;
using System.Text.RegularExpressions;

namespace Gamlo.StoreBackend.Service
{
    public class RegexValidator : IValidator
    {
        virtual public bool IsValueValid(SchemeModel scheme, ValueModel value)
        {
            return Regex.IsMatch(value.Value, scheme.Constraint);
        }
    }
}
