using Gamlo.ValidationApi.Core.Interfaces;
using Gamlo.ValidationApi.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Gamlo.ValidationApi.Service
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

        public bool HasValidator(string name)
        {
            return validators.ContainsKey(name);
        }

        public int LoadAllValidators(string path)
        {
            throw new NotImplementedException();
        }

        public bool LoadValidator(string name, string path)
        {
            if (validators.ContainsKey(name))
            {
                return true;
            }

            var files = Directory.GetFiles(path, $"{name}*.dll");
            if (files.Length <= 0)
            {
                throw new FileNotFoundException($"Can't find a plugin for Validator {name}");
            }

            var file = files[0];
            var assembly = Assembly.LoadFrom(file);
            var types = assembly.GetTypes().Where(type => type.GetInterface("IValidator") != null);
            foreach (var type in types)
            {
                if (Activator.CreateInstance(type) is IValidator instance)
                {
                    if (instance.Name.Equals(name))
                    {
                        validators.Add(instance.Name, instance);
                        return true;
                    }
                }
            }

            return false;
        }

        public IValidator ResolveValidator(string name)
        {
            return validators[name];
        }
    }
}