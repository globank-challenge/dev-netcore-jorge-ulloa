namespace OpBancarias.Data.Repositories.Cuenta
{
    public interface ICuentaRepository
    {
        Task<Models.Cuenta?> GetCuentaById(int idCuenta);
        Task<Models.Cuenta> SaveCuenta(Models.Cuenta cuentaModel);
        Task<int> RemoveCuenta(Models.Cuenta cuenta);
        Task<Models.Cuenta?> GetCuentaByNumero(string numeroCuenta);
        Task<Models.Cuenta> UpdateCuenta(Models.Cuenta cuentaModel);
    }
}
