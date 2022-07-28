using Microsoft.EntityFrameworkCore;
using OpBancarias.Data.Exceptions;
using System.Net;

namespace OpBancarias.Data.Repositories.Usuario
{
    public class UsuarioRepository: IUsuarioRepository
    {
        private readonly OpBancariasContext _context;

        public UsuarioRepository(OpBancariasContext context)
        {
            _context = context;
        }

        public async Task<Models.Usuario> SaveUsuario(Models.Usuario? usuarioModel)
        {
            if (usuarioModel == null)
                throw new CustomException(
                                "Error de datos: Datos a actualizar no válidos.",
                                HttpStatusCode.BadRequest,
                                CustomException.ErrorCodes.BadRequest);

            try {
                var savedUsuario = await _context.Usuarios.AddAsync(usuarioModel);
                await _context.SaveChangesAsync();
                return savedUsuario.Entity;
            }
            catch 
            {
                throw new CustomException(
                                "Error actualizando datos: Error creando nuevo usuario",
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }

        public async Task<Models.Usuario?> GetUsuario(string? userName)
        {
            if (userName == null)
                throw new CustomException(
                                "Error de datos: Datos de búqueda no válidos.",
                                HttpStatusCode.BadRequest,
                                CustomException.ErrorCodes.BadRequest);

            try
            {
                var usuario =  await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(user => user.UserName == userName);

                return usuario;
            }
            catch 
            {
                throw new CustomException(
                                "Error obteniendo datos del usuario",
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }
    }
}
