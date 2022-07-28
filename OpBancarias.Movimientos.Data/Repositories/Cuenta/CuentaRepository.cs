using Microsoft.EntityFrameworkCore;
using OpBancarias.Data.Exceptions;
using System.Net;

namespace OpBancarias.Data.Repositories.Cuenta
{
    public class CuentaRepository: ICuentaRepository
    {
        private readonly OpBancariasContext _context;

        public CuentaRepository(OpBancariasContext context)
        {
            _context = context;
        }

        public async Task<Models.Cuenta> SaveCuenta(Models.Cuenta? cuentaModel)
        {
            if (cuentaModel == null)
                throw new CustomException(
                                "Error de datos: Datos a actualizar no válidos.",
                                HttpStatusCode.BadRequest,
                                CustomException.ErrorCodes.BadRequest);
            try
            {
                var savedCuenta = await _context.Cuentas.AddAsync(cuentaModel);
                await _context.SaveChangesAsync();
                return savedCuenta.Entity;
            }
            catch 
            {
                throw new CustomException(
                                "Error actualizando datos: Error creando cuenta.",
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }

        public async Task<Models.Cuenta> UpdateCuenta(Models.Cuenta? cuentaModel)
        {
            if (cuentaModel == null)
                throw new CustomException(
                                "Error de datos: Datos a actualizar no válidos.",
                                HttpStatusCode.BadRequest,
                                CustomException.ErrorCodes.BadRequest);
            try
            {
                var updatedCuenta = _context.Update(cuentaModel);

                await _context.SaveChangesAsync();
                return updatedCuenta.Entity;
            }
            catch 
            {
                throw new CustomException(
                                "Error actualizando datos: Error actualizando datos de cuenta.",
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }

        public async Task<Models.Cuenta?> GetCuentaById(int? idCuenta)
        {
            if (idCuenta == null)
                throw new CustomException(
                                "Error de datos: Datos de búsqueda no válidos.",
                                HttpStatusCode.BadRequest,
                                CustomException.ErrorCodes.BadRequest);

            try
            {
                var cuenta = await _context.Cuentas.FindAsync(idCuenta);
                if (cuenta != null)
                {
                    cuenta.Movimientos = await _context.Movimientos.AsNoTracking().Where(mov => mov.CuentaId.Equals(cuenta.Id)).ToListAsync();
                }
                return cuenta;
            }
            catch (DbUpdateException ex)
            {
                throw new CustomException(
                                "Error obteniendo datos: " + ex.Message,
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }

        public async Task<Models.Cuenta?> GetCuentaByNumero(string? numeroCuenta)
        {
            if (numeroCuenta == null)
                throw new CustomException(
                                "Error de datos: Datos de búsqueda no válidos.",
                                HttpStatusCode.BadRequest,
                                CustomException.ErrorCodes.BadRequest);

            try
            {
                var cuenta = await _context.Cuentas.AsNoTracking().FirstOrDefaultAsync(cta => cta.Numero == numeroCuenta);
                if (cuenta != null)
                {
                    cuenta.Movimientos = await _context.Movimientos.AsNoTracking().Where(mov => mov.CuentaId.Equals(cuenta.Id)).ToListAsync();
                }

                return cuenta;
            }
            catch 
            {
                throw new CustomException(
                                "Error obteniendo datos: Error obteniendo datos de cuenta",
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }

        public async Task<int> RemoveCuenta(Models.Cuenta? cuenta)
        {
            if (cuenta == null)
                throw new CustomException(
                                "Error de datos: Datos a actualizar no válidos.",
                                HttpStatusCode.BadRequest,
                                CustomException.ErrorCodes.BadRequest);

            int successfullyRemoved = 0;
            try
            {
                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    if (cuenta.Movimientos?.Count > 0)
                        foreach (var movimiento in cuenta.Movimientos)
                        {
                            _context.Remove(movimiento);
                        }

                    _context.Cuentas.Remove(cuenta);

                    successfullyRemoved = await _context.SaveChangesAsync();

                    dbContextTransaction.Commit();
                }

                return successfullyRemoved;
            }
            catch 
            {
                throw new CustomException(
                                "Error actualizando datos: Error eliminando cuenta del registro ",
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }
    }
}
