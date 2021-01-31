using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using TitoBackend.Model;
using TitoBackend.Service;

namespace TitoBackend.Controllers
{
    [ApiController]
    [Route("api/store")]
    public class StoreController : ControllerBase
    {
        private readonly ILogger<StoreController> _logger;
        private readonly IStore store;

        public StoreController(ILogger<StoreController> logger, IStore store)
        {
            _logger = logger;
            this.store = store;
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateTime([FromRoute] string id, [FromBody] StoreModel data)
        {
            store.StoreValue(id, data.Value);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTime([FromRoute] string id)
        {
            try
            {
                var value = store.GetValue(id);
                return Ok(value);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "error while gte value {id}", id);
                return NotFound();
            }
        }
    }
}
