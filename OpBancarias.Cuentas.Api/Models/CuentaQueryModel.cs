using System.ComponentModel.DataAnnotations;
using static OpBancarias.Core.Biz.Enumerations;

namespace OpBancarias.Cuentas.Api.Models
{
    public class CuentaQueryModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe indicarse número de cuenta")]
        [StringLength(12, ErrorMessage = "Número de cuenta no debe ser mayor a 12 caracteres")]
        public string? Numero { get; set; }

        [Required(ErrorMessage = "Debe indicarse tipo de cuenta")]
        public TipoCuenta Tipo { get; set; }

        [Required(ErrorMessage = "Debe indicarse saldo inicial de la cuenta")]
        [Range(0, double.MaxValue, ErrorMessage = "Saldo inicial debe ser un número positivo")]
        public decimal SaldoInicial { get; set; }

        [Required(ErrorMessage = "Debe indicarse estado de la cuenta")]
        public bool EstadoActivo { get; set; }
    }
}
