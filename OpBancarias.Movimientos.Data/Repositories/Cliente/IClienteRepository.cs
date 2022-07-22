namespace OpBancarias.Data.Repositories.Cliente
{
    public interface IClienteRepository
    {
        /// <summary>
        /// Saves customer´s info on database, used to create a new customer
        /// </summary>
        /// <param name="clienteModel">model with customer´s info</param>
        /// <returns></returns>
        Task<Models.Cliente> SaveCliente(Models.Cliente clienteModel);
        /// <summary>
        /// Get customer´s info querying by customer´s record id
        /// </summary>
        /// <param name="idCliente">Customer´s record id</param>
        /// <returns></returns>
        Task<Models.Cliente?> GetClienteById(int? idCliente);
        /// <summary>
        /// Updates customer´s info
        /// </summary>
        /// <param name="clienteModel">mode with info to be updated</param>
        /// <returns></returns>
        Task<Models.Cliente> UpdateCliente(Models.Cliente clienteModel);
        /// <summary>
        /// Get report of customer´s account entries ordered by account then entry´s date
        /// </summary>
        /// <param name="identificador">customer´s identifier</param>
        /// <param name="fechaInicio">beginning date for report</param>
        /// <param name="fechaFin">ending date for report</param>
        /// <returns></returns>
        Task<List<Models.MovimientosCuentaDto>> GetMovimientosByCliente(string identificador, DateTime fechaInicio, DateTime fechaFin);
        /// <summary>
        /// Get customer´s info querying by customer´s identifier
        /// </summary>
        /// <param name="identificador">customer´s identifier</param>
        /// <returns></returns>
        Task<Models.Cliente?> GetClienteByIdentificador(string identificador);
        /// <summary>
        /// Removes customer´s record from database
        /// </summary>
        /// <param name="cliente">model with info of customer to be removed</param>
        /// <returns></returns>
        Task<int> RemoveCliente(Models.Cliente cliente);
    }
}
