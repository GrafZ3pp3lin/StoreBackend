using Gamlo.ValidationApi.Core.Interfaces;
using Gamlo.ValidationApi.Core.Model;
using Gamlo.ValidationApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Gamlo.ValidationApi.Controllers
{
    /// <summary>
    /// Controller for Store Operations
    /// </summary>
    [ApiController]
    [Route("api/store")]
    internal class StoreController : ControllerBase
    {
        private readonly ILogger<StoreController> _logger;
        private readonly IStore store;
        private readonly IValidatorResolver resolver;
        private readonly IConfiguration configuration;

        public StoreController(ILogger<StoreController> logger, IStore store, IValidatorResolver resolver, IConfiguration configuration)
        {
            _logger = logger;
            this.store = store;
            this.resolver = resolver;
            this.configuration = configuration;
        }

        /// <summary>
        /// Add a ValueScheme to the Store
        /// </summary>
        /// <param name="scheme"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostScheme([FromBody] SchemeModel scheme)
        {
            await store.StoreScheme(scheme);
            return Ok();
        }

        /// <summary>
        /// Set Or Update a Value with a defined Scheme
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get the Value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
