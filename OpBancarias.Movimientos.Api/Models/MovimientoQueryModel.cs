using static OpBancarias.Core.Biz.Enumerations;

namespace OpBancarias.Movimientos.Api.Models
{
    public class MovimientoQueryModel
    {
        public decimal Valor { get; set; }

        public string? NumeroCuenta { get; set; }
    }
}
