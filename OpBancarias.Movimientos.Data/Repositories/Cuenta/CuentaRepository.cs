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

        public async Task<Models.Cuenta> SaveCuenta(Models.Cuenta cuentaModel)
        {
            try
            {
                var savedCuenta = await _context.Cuentas.AddAsync(cuentaModel);
                await _context.SaveChangesAsync();
                return savedCuenta.Entity;
            }
            catch (DbUpdateException ex)
            {
                throw new CustomException(
                                "Error actualizando datos: " + ex.Message,
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }

        public async Task<Models.Cuenta> UpdateCuenta(Models.Cuenta cuentaModel)
        {
            try
            {
                var updatedCuenta = _context.Update<Models.Cuenta>(cuentaModel);

                await _context.SaveChangesAsync();
                return updatedCuenta.Entity;
            }
            catch (DbUpdateException ex)
            {
                throw new CustomException(
                                "Error actualizando datos: " + ex.Message,
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }

        public async Task<Models.Cuenta?> GetCuentaById(int idCuenta)
        {
            try
            {
                return await _context.Cuentas.FindAsync(idCuenta);
            }
            catch (DbUpdateException ex)
            {
                throw new CustomException(
                                "Error obteniendo datos: " + ex.Message,
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }

        public async Task<Models.Cuenta?> GetCuentaByNumero(string numeroCuenta)
        {
            try
            {
                return await _context.Cuentas.AsNoTracking().FirstOrDefaultAsync(cta => cta.Numero == numeroCuenta);
            }
            catch (DbUpdateException ex)
            {
                throw new CustomException(
                                "Error obteniendo datos: " + ex.Message,
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }

        public async Task<int> RemoveCuenta(Models.Cuenta cuenta)
        {
            try
            {
                _context.Cuentas.Remove(cuenta);
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
    }
}
