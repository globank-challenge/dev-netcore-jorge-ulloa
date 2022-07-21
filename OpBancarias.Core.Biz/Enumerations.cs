using System.ComponentModel;

namespace OpBancarias.Core.Biz
{
    public class Enumerations
    {
        public enum TipoMovimiento
        {
            [Description("Debito")]
            Debito,
            [Description("Credito")]
            Credito
        }
        
        public enum TipoCuenta
        {
            [Description("Ahorros")]
            Ahorros,
            [Description("Corriente")]
            Corriente
        }
    }
}