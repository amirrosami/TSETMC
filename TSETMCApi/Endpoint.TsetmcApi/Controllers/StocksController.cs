using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.TsetmcApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        [HttpGet("GetStocks")]
        public IActionResult GetStocks()
        {

            return Ok();
        }
    }
}

