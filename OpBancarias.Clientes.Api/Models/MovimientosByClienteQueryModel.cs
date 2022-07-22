using System.ComponentModel.DataAnnotations;

namespace OpBancarias.Clientes.Api.Models
{
    public class MovimientosByClienteQueryModel
    {
        [Required(ErrorMessage = "Debe indicarse fecha de inicio")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "Debe indicarse fecha de fin")]
        public DateTime FechaFin { get; set; }
    }
}
