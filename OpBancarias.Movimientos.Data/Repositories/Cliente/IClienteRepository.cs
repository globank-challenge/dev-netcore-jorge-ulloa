namespace OpBancarias.Data.Repositories.Cliente
{
    public interface IClienteRepository
    {
        Task<Models.Cliente> SaveCliente(Models.Cliente clienteModel);
        Task<Models.Cliente?> GetClienteById(int? idCliente);
        Task<Models.Cliente> UpdateCliente(Models.Cliente clienteModel);
        Task<List<Models.MovimientosCuentaDto>> GetMovimientosByCliente(string identificador, DateTime fechaInicio, DateTime fechaFin);
        Task<Models.Cliente?> GetClienteByIdentificador(string identificador);
        Task<int> RemoveCliente(Models.Cliente cliente);
    }
}
