namespace OpBancarias.Data.Repositories.Cuenta
{
    public interface ICuentaRepository
    {
        /// <summary>
        /// Get account´s info querying by Id
        /// </summary>
        /// <param name="idCuenta">Account´s Id</param>
        /// <returns></returns>
        Task<Models.Cuenta?> GetCuentaById(int idCuenta);
        /// <summary>
        /// Save account´s info, used to create a new account
        /// </summary>
        /// <param name="cuentaModel">model containing account´s info</param>
        /// <returns></returns>
        Task<Models.Cuenta> SaveCuenta(Models.Cuenta cuentaModel);
        /// <summary>
        /// Remove account from register of accounts
        /// </summary>
        /// <param name="cuenta">model representing the account to be removed</param>
        /// <returns></returns>
        Task<int> RemoveCuenta(Models.Cuenta cuenta);
        /// <summary>
        /// Get account´s info querying by number of account
        /// </summary>
        /// <param name="numeroCuenta">number of account</param>
        /// <returns></returns>
        Task<Models.Cuenta?> GetCuentaByNumero(string numeroCuenta);
        /// <summary>
        /// Update account´s info 
        /// </summary>
        /// <param name="cuentaModel">model with info to be updated</param>
        /// <returns></returns>
        Task<Models.Cuenta> UpdateCuenta(Models.Cuenta cuentaModel);
    }
}
