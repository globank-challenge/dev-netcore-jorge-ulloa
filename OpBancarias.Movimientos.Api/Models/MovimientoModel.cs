using static OpBancarias.Core.Biz.Enumerations;

namespace OpBancarias.Movimientos.Api.Models
{
    public class MovimientoModel
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public TipoMovimiento Tipo { get; set; }

        public decimal Valor { get; set; }

        public decimal Saldo { get; set; }

        public string? NumeroCuenta { get; set; }
    }
}
