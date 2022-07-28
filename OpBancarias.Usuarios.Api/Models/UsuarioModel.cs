using System.ComponentModel.DataAnnotations;

namespace OpBancarias.Usuarios.Api.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }
    }
}
