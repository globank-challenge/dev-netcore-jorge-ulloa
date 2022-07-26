using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpBancarias.Data.Models
{
    public class Cuenta
    {
        public int Id { get; set; }

        [Required]
        [StringLength(12)]
        public string? Numero { get; set; }

        [Required]
        public int Tipo { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(12,2)")]
        public decimal SaldoInicial { get; set; }

        [Required]
        public bool EstadoActivo { get; set; }

        [Required]
        public int ClienteId { get; set; }

        public List<Movimiento>? Movimientos { get; set; }
    }
}

