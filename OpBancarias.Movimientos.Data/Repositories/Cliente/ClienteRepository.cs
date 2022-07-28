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

        public async Task<Models.Cliente> SaveCliente(Models.Cliente? clienteModel)
        {
            if (clienteModel == null) 
                throw new CustomException(
                                "Error de datos: Datos a actualizar no válidos.",
                                HttpStatusCode.BadRequest,
                                CustomException.ErrorCodes.BadRequest);

            try {
                var savedCliente = await _context.Clientes.AddAsync(clienteModel);
                await _context.SaveChangesAsync();
                return savedCliente.Entity;
            }
            catch 
            {
                throw new CustomException(
                                "Error actualizando datos: Error creando nuevo cliente",
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }

        public async Task<Models.Cliente> UpdateCliente(Models.Cliente? clienteModel)
        {
            if (clienteModel == null)
                throw new CustomException(
                                "Error de datos: Datos a actualizar no válidos.",
                                HttpStatusCode.BadRequest,
                                CustomException.ErrorCodes.BadRequest);

            try
            {
                var updatedCliente = _context.Update(clienteModel);

                await _context.SaveChangesAsync();
                return updatedCliente.Entity;
            }
            catch 
            {
                throw new CustomException(
                                "Error actualizando datos del cliente",
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }

        /// <summary>
        /// Get all cuentas and movimientos for a specific customer
        /// </summary>
        /// <param name="idCliente">record Id of customer</param>
        /// <returns></returns>
        private async Task<List<Models.Cuenta>?> GetAllCuentasCliente(int? idCliente)
        {
            if (idCliente == null)
                throw new CustomException(
                                "Error obteniendo datos: Datos de búsqueda no válidos.",
                                HttpStatusCode.BadRequest,
                                CustomException.ErrorCodes.BadRequest);

            var cuentas = await _context.Cuentas.AsNoTracking().Where(cta => cta.ClienteId.Equals(idCliente)).ToListAsync();
            foreach (var cuenta in cuentas)
            {
                cuenta.Movimientos = await _context.Movimientos.AsNoTracking().Where(mov => mov.CuentaId.Equals(cuenta.Id)).ToListAsync();
            }
            return cuentas;
        }
       
        public async Task<Models.Cliente?> GetClienteById(int? idCliente)
        {
            if (idCliente == null)
                throw new CustomException(
                                "Error obteniendo datos: Datos de búsqueda no válidos.",
                                HttpStatusCode.BadRequest,
                                CustomException.ErrorCodes.BadRequest);

            try
            {
                var cliente = await _context.Clientes.FindAsync(idCliente);
                if (cliente != null)
                {
                    cliente.Cuentas = await GetAllCuentasCliente(idCliente);
                }
                return cliente;
            }
            catch 
            {
                throw new CustomException(
                                "Error obteniendo datos del cliente",
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }

        public async Task<Models.Cliente?> GetClienteByIdentificador(string? identificador)
        {
            if (identificador == null)
                throw new CustomException(
                                "Error obteniendo datos: Datos de búsqueda no válidos.",
                                HttpStatusCode.BadRequest,
                                CustomException.ErrorCodes.BadRequest);
            try
            {
                var cliente =  await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(cli => cli.Identificacion == identificador);
                if (cliente != null)
                {
                    cliente.Cuentas = await GetAllCuentasCliente(cliente.Id);
                }
                return cliente;
            }
            catch 
            {
                throw new CustomException(
                                "Error obteniendo datos del cliente",
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }

        public async Task<int> RemoveCliente(Models.Cliente? cliente)
        {
            if (cliente == null)
                throw new CustomException(
                                "Error de datos: Datos a actualizar no válidos.",
                                HttpStatusCode.BadRequest,
                                CustomException.ErrorCodes.BadRequest);

            try
            {
                _context.Clientes.Remove(cliente);
                return await _context.SaveChangesAsync();
            }
            catch 
            {
                throw new CustomException(
                                "Error actualizando datos: Error eliminando registro del cliente",
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }

        public async Task<List<Models.MovimientosCuentaDto>> GetMovimientosByCliente(string? identificador, DateTime fechaInicio, DateTime fechaFin)
        {
            if (identificador == null)
                throw new CustomException(
                                "Error de datos: Datos a actualizar no válidos.",
                                HttpStatusCode.BadRequest,
                                CustomException.ErrorCodes.BadRequest);
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
            catch 
            {
                throw new CustomException(
                                "Error obteniendo datos: Error obteniendo reporte de movimientos del cliente",
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }
    }
}
