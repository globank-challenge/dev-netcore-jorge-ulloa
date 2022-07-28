using System.ComponentModel.DataAnnotations;

namespace OpBancarias.Data.Models
{
    public class Persona
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string? Identificacion { get; set; }

        [Required]
        [StringLength(50)]
        public string? Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string? Apellido { get; set; }

        [Range(0, int.MaxValue)]
        public int Edad { get; set; }

        [StringLength(100)]
        public string? Direccion { get; set; }

        [Phone]
        public string? Telefono { get; set; }
    }
}