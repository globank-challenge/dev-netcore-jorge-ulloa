using Microsoft.AspNetCore.Mvc;
using OpBancarias.Api.Core.Controllers;
using OpBancarias.Clientes.Api.Models;

namespace OpBancarias.Clientes.Api.Controllers
{
    [Route("[controller]")]
    public class ClientesController: OpBancariasControllerBase<ClientesModelFactory>
    {
        public ClientesController(ClientesModelFactory modelFactory): base(modelFactory)
        {

        }

        [HttpPost("add")]
        public async Task<ActionResult<ClienteModel>> AddCliente([FromBody] ClienteQueryModel payload)
        {
            return Ok(await ModelFactory.SaveCliente(payload));
        }

        [HttpDelete("{id}/remove")]
        public async Task<ActionResult<bool>> RemoveCliente(string id)
        {
            return Ok(await ModelFactory.RemoveCliente(id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteModel>> GetCliente(string id)
        {
            return Ok(await ModelFactory.GetCliente(id));
        }


        [HttpPut("update")]
        public async Task<ActionResult<ClienteModel>> UpdateCliente([FromBody] ClienteQueryModel payload)
        {
            return Ok(await ModelFactory.UpdateCliente(payload));
        }


        [HttpGet("{clienteId}/movimientos")]
        public async Task<ActionResult<List<MovimientosByClienteModel>>> GetMovimientosByCliente(string clienteId, [FromQuery] MovimientosByClienteQueryModel payload)
        {
            return Ok(await ModelFactory.GetMovimientosByCliente(clienteId, payload));
        }

    }
}