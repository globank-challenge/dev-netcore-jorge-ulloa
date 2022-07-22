namespace OpBancarias.Data.Repositories.Movimiento
{
    public interface IMovimientoRepository
    {
        /// <summary>
        /// Save movement (credit or debit) in database
        /// </summary>
        /// <param name="movimientoModel">model with credit/debit info to store</param>
        /// <returns></returns>
        Task<Models.Movimiento> SaveMovimiento(Models.Movimiento movimientoModel);
        /// <summary>
        /// Get updated account balance based on last entry in account´s history
        /// </summary>
        /// <param name="NumeroCuenta"></param>
        /// <returns></returns>
        Task<Models.Cuenta?> GetSaldoFromMovimientosCuenta(string? NumeroCuenta);
    }
}
