using Microsoft.AspNetCore.Mvc;

namespace Pagamento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("PagamentoMicrosservice is healthy.");
        }
    }
}