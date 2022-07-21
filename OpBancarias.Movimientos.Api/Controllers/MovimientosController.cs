using Microsoft.AspNetCore.Mvc;
using OpBancarias.Api.Core.Controllers;
using OpBancarias.Movimientos.Api.Models;

namespace OpBancarias.Movimientos.Api.Controllers
{
    [Route("[controller]")]
    public class MovimientosController: OpBancariasControllerBase<MovimientosModelFactory>
    {
        public MovimientosController(MovimientosModelFactory modelFactory): base(modelFactory)
        {

        }

        [HttpPost("credito")]
        public async Task<ActionResult<MovimientoModel>> Deposito([FromBody] MovimientoQueryModel payload)
        {
            return Ok(await ModelFactory.Deposito(payload));
        }


        [HttpPost("debito")]
        public async Task<ActionResult<MovimientoModel>> Extraccion([FromBody] MovimientoQueryModel payload)
        {
            return Ok(await ModelFactory.Extraccion(payload));
        }

    }
}