using System.ComponentModel.DataAnnotations;

namespace OpBancarias.Clientes.Api.Models
{
    public class ClienteQueryModel: PersonaQueryModel
    {
        [Required(ErrorMessage = "Debe indicarse usuario del cliente")]
        [MaxLength(50, ErrorMessage = "Identificador de usuario no debe ser mayor a 50 caracteres")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Debe indicarse password del cliente")]
        [MaxLength(20, ErrorMessage = "Password debe tener un máximo de 20 caracteres"), 
            MinLength(8, ErrorMessage = "Password debe tener 8 o más caracteres")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Debe indicarse estado del cliente")]
        public bool EstadoActivo { get; set; }
    }
}
