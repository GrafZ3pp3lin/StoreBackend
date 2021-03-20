using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Gamlo.StoreBackend.Model;
using Gamlo.StoreBackend.Service;
using System.Threading.Tasks;

namespace Gamlo.StoreBackend.Controllers
{
    [ApiController]
    [Route("api/store")]
    internal class StoreController : ControllerBase
    {
        private readonly ILogger<StoreController> _logger;
        private readonly IStore store;

        public StoreController(ILogger<StoreController> logger, IStore store)
        {
            _logger = logger;
            this.store = store;
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
