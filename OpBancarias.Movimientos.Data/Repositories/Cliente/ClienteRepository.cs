using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OpBancarias.Data.Exceptions;
using System.Net;

namespace OpBancarias.Data.Repositories.Cliente
{
    public class ClienteRepository: IClienteRepository
    {
        private readonly OpBancariasContext _context;

        public ClienteRepository(OpBancariasContext context)
        {
            _context = context;
        }

        public async Task<Models.Cliente> SaveCliente(Models.Cliente clienteModel)
        {
            try {
                var savedCliente = await _context.Clientes.AddAsync(clienteModel);
                await _context.SaveChangesAsync();
                return savedCliente.Entity;
            }
            catch (DbUpdateException ex)
            {
                throw new CustomException(
                                "Error actualizando datos: " + ex.Message,
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }

        public async Task<Models.Cliente> UpdateCliente(Models.Cliente clienteModel)
        {
            try
            {
                var updatedCliente = _context.Update<Models.Cliente>(clienteModel);

                await _context.SaveChangesAsync();
                return updatedCliente.Entity;
            }
            catch (DbUpdateException ex)
            {
                throw new CustomException(
                                "Error actualizando datos: " + ex.Message,
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }

        public async Task<Models.Cliente?> GetClienteById(int? idCliente)
        {
            try
            {
                return await _context.Clientes.FindAsync(idCliente);
            }
            catch (DbUpdateException ex)
            {
                throw new CustomException(
                                "Error obteniendo datos: " + ex.Message,
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }

        public async Task<Models.Cliente?> GetClienteByIdentificador(string identificador)
        {
            try
            {
                return await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(cli => cli.Identificacion == identificador);
            }
            catch (DbUpdateException ex)
            {
                throw new CustomException(
                                "Error obteniendo datos: " + ex.Message,
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }

        public async Task<int> RemoveCliente(Models.Cliente cliente)
        {
            try
            {
                _context.Clientes.Remove(cliente);
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new CustomException(
                                "Error actualizando datos: " + ex.Message,
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }

        public async Task<List<Models.MovimientosCuentaDto>> GetMovimientosByCliente(string identificador, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<Models.MovimientosCuentaDto>? movimientos = new();

                var cliente = await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(cli => cli.Identificacion == identificador);

                if (cliente != null)
                {
                    var cuentas = _context.Cuentas.AsNoTracking().Where(cta => cta.ClienteId == cliente.Id).ToList();

                    if (cuentas!= null)
                    {
                        foreach(var cuenta in cuentas)
                        {
                            var movimientosByCuenta = _context.Movimientos.AsNoTracking().Where(mov => mov.CuentaId == cuenta.Id
                                            && mov.Fecha >= fechaInicio && mov.Fecha <= fechaFin).ToList();

                            if (movimientosByCuenta != null)
                            {
                                var movsCuenta = new Models.MovimientosCuentaDto()
                                {
                                    NombreCliente = cliente.NombreCliente,
                                    Numero = cuenta.Numero,
                                    Tipo = cuenta.Tipo,
                                    SaldoInicial = cuenta.SaldoInicial,
                                    EstadoActivo = cuenta.EstadoActivo,
                                    Movimientos = new List<Models.Movimiento>()
                                };

                                movsCuenta.Movimientos.AddRange(movimientosByCuenta);

                                movimientos.Add(movsCuenta);
                            }
                        }
                    }
                }

                return movimientos;
            }
            catch (DbUpdateException ex)
            {
                throw new CustomException(
                                "Error obteniendo datos: " + ex.Message,
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }
    }
}
