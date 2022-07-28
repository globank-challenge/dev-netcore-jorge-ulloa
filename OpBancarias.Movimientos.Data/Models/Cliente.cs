using System.ComponentModel.DataAnnotations;

namespace OpBancarias.Data.Models
{
    public class Cliente : Persona
    {
        [Required]
        [MaxLength(50)]
        public string? UserName { get; set; }

        [Required]
        [MaxLength(20), MinLength(8)]
        public string? Password { get; set; }

        [Required]
        public bool EstadoActivo { get; set; }

        public string NombreCliente
        {
            get { return Nombre + " " + Apellido; }
        }

        public List<Cuenta>? Cuentas { get; set; }
    }
}