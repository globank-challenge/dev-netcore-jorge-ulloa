using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OpBancarias.Api.Core.Factories;
using OpBancarias.Clientes.Biz;
using OpBancarias.Core.Biz;
using OpBancarias.Data.Repositories.Cliente;

namespace OpBancarias.Clientes.Api.Models
{
    public class ClientesModelFactory: OpBancariasModelFactoryBase<IClienteRepository>
    {
        private IClienteRepository _clienteRepository;

        public ClientesModelFactory(
            IClienteRepository repo,
            Application app,
            IMapper mapper,
            IActionContextAccessor actionContextAccessor)
                : base(
                      repo,
                      app,
                      mapper,
                      actionContextAccessor)
        {
            _clienteRepository = repo;
        }

        public async Task<ClienteModel> SaveCliente(ClienteQueryModel payload)
        {
         
            Cliente cliente = new Cliente(
                _clienteRepository,
                CurrentPrincipal,
                Application,
                Mapper
                );

            Mapper.Map(payload, cliente);

            await cliente.Save();

            return Mapper.Map<ClienteModel>(cliente);
        }

        public async Task<List<MovimientosByClienteModel>> GetMovimientosByCliente(string clienteId, MovimientosByClienteQueryModel payload)
        {

            Cliente cliente = new Cliente(
                clienteId,
                _clienteRepository,
                CurrentPrincipal,
                Application,
                Mapper
                );


            var movimientos = await cliente.GetMovimientos(payload.FechaInicio, payload.FechaFin);

            return Mapper.Map<List<MovimientosByClienteModel>>(movimientos);
        }

        public async Task<ClienteModel> GetCliente(string idCliente)
        {
            Cliente cliente = new Cliente(
                _clienteRepository,
                CurrentPrincipal,
                Application,
                Mapper
                );

            await cliente.Load(idCliente);

            return Mapper.Map<ClienteModel>(cliente);
        }

        public async Task<bool> RemoveCliente(string idCliente)
        {
            Cliente cliente = new Cliente(
                idCliente,
                _clienteRepository,
                CurrentPrincipal,
                Application,
                Mapper
                );

            return await cliente.Remove();
        }

        public async Task<ClienteModel> UpdateCliente(ClienteQueryModel payload)
        {
            Cliente cliente = new Cliente(
                payload.Identificacion,
                _clienteRepository,
                CurrentPrincipal,
                Application,
                Mapper
                );

            Mapper.Map(payload, cliente);

            await cliente.Update();

            return Mapper.Map<ClienteModel>(cliente);
        }

    }
}
