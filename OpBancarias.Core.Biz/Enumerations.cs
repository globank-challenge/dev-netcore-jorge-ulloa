using System.ComponentModel;

namespace OpBancarias.Core.Biz
{
    public class Enumerations
    {
        public enum TipoMovimiento
        {
            [Description("Debito")]
            Debito = 1,
            [Description("Credito")]
            Credito = 2
        }
        
        public enum TipoCuenta
        {
            [Description("Ahorros")]
            Ahorros = 1,
            [Description("Corriente")]
            Corriente = 2
        }
    }
}