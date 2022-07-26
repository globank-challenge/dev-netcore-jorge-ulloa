using Microsoft.AspNetCore.Mvc;
using OpBancarias.Api.Core.Controllers;
using OpBancarias.Api.Core.Filters;
using OpBancarias.Cuentas.Api.Models;

namespace OpBancarias.Cuentas.Api.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    [Route("[controller]")]
    public class CuentasController: OpBancariasControllerBase<CuentasModelFactory>
    {
        public CuentasController(CuentasModelFactory modelFactory): base(modelFactory)
        {

        }

        [HttpPost("add")]
        public async Task<ActionResult<CuentaModel>> AddCuenta([FromBody] CuentaQueryModel payload)
        {
            return Ok(await ModelFactory.SaveCuenta(payload));
        }

        [HttpDelete("{id}/remove")]
        public async Task<ActionResult<bool>> RemoveCuenta(string id)
        {
            return Ok(await ModelFactory.RemoveCuenta(id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CuentaModel>> GetCuenta(string id)
        {
            return Ok(await ModelFactory.GetCuenta(id));
        }

        [HttpPut("update")]
        public async Task<ActionResult<CuentaModel>> UpdateCuenta([FromBody] CuentaQueryModel payload)
        {
            return Ok(await ModelFactory.UpdateCuenta(payload));
        }

    }
}