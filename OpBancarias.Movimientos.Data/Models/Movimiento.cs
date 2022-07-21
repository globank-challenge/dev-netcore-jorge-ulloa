using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpBancarias.Data.Models
{
    public class Movimiento
    {
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public int Tipo { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Valor { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal Saldo { get; set; }

        [Required]
        public int CuentaId { get; set; }
    }
}