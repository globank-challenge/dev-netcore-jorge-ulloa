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
        public ClientesModelFactory(
            IClienteRepository repo,
            Application app,
            IMapper mapper,
            ILogger<ClientesModelFactory> logger,
            IActionContextAccessor actionContextAccessor)
                : base(
                      repo,
                      app,
                      mapper,
                      logger,
                      actionContextAccessor)
        {

        }

        public async Task<ClienteModel> SaveCliente(ClienteQueryModel payload)
        {
         
            Cliente cliente = new Cliente(
                OpBancariasRepo,
                CurrentPrincipal,
                Application,
                Mapper,
                OpBancariasLogger
                );

            Mapper.Map(payload, cliente);

            await cliente.Save();

            return Mapper.Map<ClienteModel>(cliente);
        }

        public async Task<List<MovimientosByClienteModel>> GetMovimientosByCliente(string clienteId, MovimientosByClienteQueryModel payload)
        {

            Cliente cliente = new Cliente(
                clienteId,
                OpBancariasRepo,
                CurrentPrincipal,
                Application,
                Mapper,
                OpBancariasLogger
                );


            var movimientos = await cliente.GetMovimientos(payload.FechaInicio, payload.FechaFin);

            return Mapper.Map<List<MovimientosByClienteModel>>(movimientos);
        }

        public async Task<ClienteModel> GetCliente(string idCliente)
        {
            Cliente cliente = new Cliente(
                OpBancariasRepo,
                CurrentPrincipal,
                Application,
                Mapper,
                OpBancariasLogger
                );

            await cliente.Load(idCliente);

            return Mapper.Map<ClienteModel>(cliente);
        }

        public async Task<bool> RemoveCliente(string idCliente)
        {
            Cliente cliente = new Cliente(
                idCliente,
                OpBancariasRepo,
                CurrentPrincipal,
                Application,
                Mapper,
                OpBancariasLogger
                );

            return await cliente.Remove();
        }

        public async Task<ClienteModel> UpdateCliente(ClienteQueryModel payload)
        {
            Cliente cliente = new Cliente(
                payload.Identificacion,
                OpBancariasRepo,
                CurrentPrincipal,
                Application,
                Mapper,
                OpBancariasLogger
                );

            Mapper.Map(payload, cliente);

            await cliente.Update();

            return Mapper.Map<ClienteModel>(cliente);
        }

    }
}
