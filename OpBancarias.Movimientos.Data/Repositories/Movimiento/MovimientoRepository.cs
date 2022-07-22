using Microsoft.EntityFrameworkCore;
using OpBancarias.Data.Exceptions;
using System.Net;

namespace OpBancarias.Data.Repositories.Movimiento
{
    public class MovimientoRepository: IMovimientoRepository
    {
        private readonly OpBancariasContext _context;

        public MovimientoRepository(OpBancariasContext context)
        {
            _context = context;
        }

        public async Task<Models.Movimiento> SaveMovimiento(Models.Movimiento movimientoModel)
        {
            try
            {
                var savedMovimiento = await _context.Movimientos.AddAsync(movimientoModel);
                await _context.SaveChangesAsync();
                return savedMovimiento.Entity;
            }
            catch 
            {
                throw new CustomException(
                                "Error actualizando datos: Error insertando movimiento de cuenta",
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }

        }

        public async Task<Models.Cuenta?> GetSaldoFromMovimientosCuenta(string? NumeroCuenta)
        {
            try
            {
                var cuenta = await _context.Cuentas.AsNoTracking().FirstOrDefaultAsync(cta => cta.Numero == NumeroCuenta);

                if (cuenta != null)
                {
                    decimal saldo = cuenta.SaldoInicial;

                    var lastMovimiento = _context.Movimientos.AsNoTracking().Where(mov => mov.CuentaId == cuenta.Id)?
                        .OrderByDescending(mov => mov.Id)
                        .FirstOrDefault();

                    if (lastMovimiento != null)
                        saldo = lastMovimiento.Saldo;

                    cuenta.SaldoInicial = saldo;
                }

                return cuenta;
            }
            catch 
            {
                throw new CustomException(
                                "Error obteniendo datos: Error obteniendo saldo de cuenta",
                                HttpStatusCode.InternalServerError,
                                CustomException.ErrorCodes.InternalServerError);
            }
        }
       
    }
}
