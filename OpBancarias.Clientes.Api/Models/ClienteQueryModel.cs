namespace OpBancarias.Clientes.Api.Models
{
    public class ClienteQueryModel: PersonaModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool EstadoActivo { get; set; }
    }
}
