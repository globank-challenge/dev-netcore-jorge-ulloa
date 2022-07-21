namespace OpBancarias.Data.Repositories.Movimiento
{
    public interface IMovimientoRepository
    {
        Task<Models.Movimiento> SaveMovimiento(Models.Movimiento movimientoModel);
        Task<Models.Cuenta?> GetSaldoFromMovimientosCuenta(string NumeroCuenta);
    }
}
