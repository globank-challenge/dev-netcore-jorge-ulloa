using System.ComponentModel.DataAnnotations;

namespace OpBancarias.Usuarios.Api.Models
{
    public class UsuarioQueryModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "El nombre de usuario debe tener una longitud máxima de 50 caracteres."), 
         MinLength(8, ErrorMessage = "El nombre de usuario debe tener una longitud mayor a 8 caracteres.")]
        public string? UserName { get; set; }

        [Required]
        [MaxLength(20), MinLength(16)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{16,}$",
            ErrorMessage = "La contraseña no cumple los requerimientos de seguridad.")]
        public string? Password { get; set; }
    }
}
