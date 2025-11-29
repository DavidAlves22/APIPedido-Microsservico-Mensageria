using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pedido.Application.UseCases;

namespace Pedido.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly CriarPedidoUseCase _criarPedidoUseCase;

        public PedidoController(CriarPedidoUseCase criarPedidoUseCase)
        {
            _criarPedidoUseCase = criarPedidoUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CriarPedido([FromBody] CriarPedidoInput input)
        {
            if (input == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Garante que o PedidoId seja fornecido ou gerado, se necessário
            if (input.PedidoId == Guid.Empty)
            {
                input.PedidoId = Guid.NewGuid();
            }

            await _criarPedidoUseCase.ExecuteAsync(input);
            return CreatedAtAction(nameof(CriarPedido), new { id = input.PedidoId }, input);
        }
    }
}