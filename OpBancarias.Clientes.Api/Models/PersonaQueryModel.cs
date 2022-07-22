using System.ComponentModel.DataAnnotations;

namespace OpBancarias.Clientes.Api.Models
{
    public class PersonaQueryModel
    {
        [Required(ErrorMessage = "Debe indicarse identificador del cliente")]
        [StringLength(10, ErrorMessage = "Identificador de usuario no debe ser mayor a 10 caracteres")]
        public string? Identificacion { get; set; }

        [Required(ErrorMessage = "Debe indicarse nombre del cliente")]
        [StringLength(50, ErrorMessage = "Nombre de usuario no debe ser mayor a 50 caracteres")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Debe indicarse apellido del cliente")]
        [StringLength(50, ErrorMessage = "Apellido de usuario no debe ser mayor a 50 caracteres")]
        public string? Apellido { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Edad debe ser un entero positivo")]
        public int Edad { get; set; }

        [StringLength(100, ErrorMessage = "Direccion no debe tener más de 100 caracteres")]
        public string? Direccion { get; set; }

        [Phone]
        public string? Telefono { get; set; }
    }
}