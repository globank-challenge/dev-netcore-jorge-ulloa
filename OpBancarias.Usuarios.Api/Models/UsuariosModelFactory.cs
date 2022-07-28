using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OpBancarias.Core.Biz;
using OpBancarias.Data.Repositories.Usuario;
using OpBancarias.Api.Core.Factories;
using OpBancarias.Usuarios.Biz;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OpBancarias.Data.Exceptions;
using System.Net;

namespace OpBancarias.Usuarios.Api.Models
{
    public class UsuariosModelFactory: OpBancariasModelFactoryBase<IUsuarioRepository>
    {
        public UsuariosModelFactory(
            IUsuarioRepository repo,
            Application app,
            IMapper mapper,
            ILogger<UsuariosModelFactory> logger,
            IActionContextAccessor actionContextAccessor)
                : base(
                      repo,
                      app,
                      mapper,
                      logger,
                      actionContextAccessor)
        {

        }

        public async Task<UsuarioModel> SaveUsuario(UsuarioQueryModel payload)
        {

            Usuario usuario = new Usuario(
                OpBancariasRepo,
                CurrentPrincipal,
                Application,
                Mapper,
                OpBancariasLogger
                );

            Mapper.Map(payload, usuario);

            await usuario.Save();

            return Mapper.Map<UsuarioModel>(usuario);
        }

        public async Task<UsuarioModel> GetUsuario(string userName)
        {
            Usuario usuario = new Usuario(
                OpBancariasRepo,
                CurrentPrincipal,
                Application,
                Mapper,
                OpBancariasLogger
                );

            await usuario.Load(userName);

            return Mapper.Map<UsuarioModel>(usuario);
        }

        public async Task<string?> GetTokenUsuario(UsuarioQueryModel model)
        {
            if (model == null || model.UserName == null)
                throw new CustomException(
                                    "Datos de usuario incorrectos.",
                                    HttpStatusCode.BadRequest,
                                    CustomException.ErrorCodes.BadRequest);

            Usuario usuario = new Usuario(
                OpBancariasRepo,
                CurrentPrincipal,
                Application,
                Mapper,
                OpBancariasLogger
                );

            await usuario.Load(model.UserName);
            
            if (usuario.UserName == model.UserName)
            {
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, Application.Configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserName", usuario.UserName)
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Application.Configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    Application.Configuration["Jwt:Issuer"],
                    Application.Configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);

                var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);
                return tokenHandler;
            }
            else
            {
                throw new CustomException(
                                "Usuario no existente.",
                                HttpStatusCode.BadRequest,
                                CustomException.ErrorCodes.BadRequest);
            }
        }
    }
}
