using OpBancarias.Cuentas.Api.Models;

namespace OpBancarias.Clientes.Api.Models
{
    public class ClienteModel: PersonaModel
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }

        public bool EstadoActivo { get; set; }

        public List<CuentaModel>? Cuentas { get; set; }
    }
}
