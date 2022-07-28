using System.ComponentModel.DataAnnotations;

namespace OpBancarias.Data.Models
{
    public class Usuario 
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? UserName { get; set; }

        [Required]
        [MaxLength(20), MinLength(16)]
        public string? Password { get; set; }
    }
}
