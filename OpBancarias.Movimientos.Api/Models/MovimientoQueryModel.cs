using System.ComponentModel.DataAnnotations;

namespace OpBancarias.Movimientos.Api.Models
{
    public class MovimientoQueryModel
    {
        [Required(ErrorMessage = "Debe indicarse valor para el depósito")]
        [RegularExpression(@"^-?[0-9]\d*.\d{0,2}$", ErrorMessage = "Valor debe tener 2 lugares decimales")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Debe indicarse número de cuenta para el depósito")]
        [StringLength(12, ErrorMessage = "Número de cuenta no debe ser mayor a 12 caracteres")]
        public string? NumeroCuenta { get; set; }
    }
}
