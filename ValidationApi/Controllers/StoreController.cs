using Gamlo.ValidationApi.Core.Interfaces;
using Gamlo.ValidationApi.Core.Model;
using Gamlo.ValidationApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Gamlo.ValidationApi.Controllers
{
    [ApiController]
    [Route("api/store")]
    internal class StoreController : ControllerBase
    {
        private readonly ILogger<StoreController> _logger;
        private readonly IStore store;
        private readonly IValidatorResolver resolver;

        public StoreController(ILogger<StoreController> logger, IStore store, IValidatorResolver resolver)
        {
            _logger = logger;
            this.store = store;
            this.resolver = resolver;
        }

        [HttpPost]
        public async Task<IActionResult> PostScheme([FromBody] SchemeModel scheme)
        {
            await store.StoreScheme(scheme);
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateValue([FromRoute] string id, [FromBody] ValueModel data)
        {
            IValidator validator;
            SchemeModel scheme;
            try
            {
                scheme = await store.GetScheme(data.Scheme);
                if (!resolver.HasValidator(scheme.ValidationName) && !resolver.LoadValidator(scheme.ValidationName, "TODO")) // TODO
                {
                    throw new InvalidOperationException($"No Validator for Type {scheme.ValidationName} found");
                }
                validator = resolver.ResolveValidator(scheme.ValidationName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Can't resolve validator");
                throw;
            }
            try
            {
                if (!validator.IsValueValid(scheme, data))
                {
                    throw new InvalidOperationException("Value is not valid");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Can't validate value");
                throw;
            }
            await store.StoreValue(id, data.Value);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetValue([FromRoute] string id)
        {
            try
            {
                string value = await store.GetValue(id);
                return Ok(value);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "error while get value {id}", id);
                return NotFound();
            }
        }
    }
}
