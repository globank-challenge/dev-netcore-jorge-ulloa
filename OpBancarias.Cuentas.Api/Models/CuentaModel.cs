using OpBancarias.Movimientos.Api.Models;
using static OpBancarias.Core.Biz.Enumerations;

namespace OpBancarias.Cuentas.Api.Models
{
    public class CuentaModel
    {
        public int Id { get; set; }

        public string? Numero { get; set; }

        public TipoCuenta Tipo { get; set; }

        public decimal SaldoInicial { get; set; }

        public bool EstadoActivo { get; set; }

        public List<MovimientoModel>? Movimientos { get; set; }
        public int ClienteId { get; set; }
    }
}