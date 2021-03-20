using Gamlo.StoreBackend.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Gamlo.StoreBackend.Service
{
    public class ValidatorResolver : IValidatorResolver
    {
        private readonly Dictionary<string, IValidator> validators;

        public ValidatorResolver(IEnumerable<IValidator> validators)
        {
            this.validators = new Dictionary<string, IValidator>();
            foreach (var validator in validators)
            {
                this.validators.Add(validator.Name, validator);
            }
        }

        public int LoadAllValidators(string path)
        {
            throw new NotImplementedException();
        }

        public bool LoadValidator(string name, string path)
        {
            var files = Directory.GetFiles(path, $"{name}*.dll");

            return false;
        }

        public IValidator ResolveValidator(string name)
        {
            return validators[name];
        }
    }
}
