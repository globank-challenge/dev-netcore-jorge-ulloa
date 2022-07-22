using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OpBancarias.Data.Models
{
    public class Cliente : Persona
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(20), MinLength(8)]
        public string Password { get; set; }

        [Required]
        public bool EstadoActivo { get; set; }

        public List<Cuenta>? Cuentas { get; set; }
    }
}