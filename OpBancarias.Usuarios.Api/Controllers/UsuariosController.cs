using Microsoft.AspNetCore.Mvc;
using OpBancarias.Api.Core.Controllers;
using OpBancarias.Api.Core.Filters;
using OpBancarias.Usuarios.Api.Models;

namespace OpBancarias.Usuarios.Api.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    [Route("[controller]")]
    public class UsuariosController : OpBancariasControllerBase<UsuariosModelFactory>
    {
        public UsuariosController(UsuariosModelFactory modelFactory) : base(modelFactory)
        {

        }

        [HttpPost("add")]
        public async Task<ActionResult<UsuarioModel>> AddUsuario([FromBody] UsuarioQueryModel payload)
        {
            return Ok(await ModelFactory.SaveUsuario(payload));
        }


        [HttpGet("{userName}")]
        public async Task<ActionResult<UsuarioModel>> GetUsuario(string userName)
        {
            return Ok(await ModelFactory.GetUsuario(userName));
        }


        [HttpPost("{userName}/token")]
        public async Task<ActionResult<UsuarioModel>> GetTokenUsuario(UsuarioQueryModel payload)
        {
            return Ok(await ModelFactory.GetTokenUsuario(payload));
        }
    }
}