using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OpBancarias.Api.Core.Factories;
using OpBancarias.Core.Biz;
using OpBancarias.Data.Repositories.Cuenta;


namespace OpBancarias.Cuentas.Api.Models
{
    public class CuentasModelFactory: OpBancariasModelFactoryBase<ICuentaRepository>
    {
        private ICuentaRepository _cuentaRepository;

        public CuentasModelFactory(
            ICuentaRepository repo,
            Application app,
            IMapper mapper,
            IActionContextAccessor actionContextAccessor)
                : base(
                      repo,
                      app,
                      mapper,
                      actionContextAccessor)
        {
            _cuentaRepository = repo;
        }

        public async Task<CuentaModel> SaveCuenta(CuentaQueryModel payload)
        {
            Cuenta.Biz.Cuenta cuenta = new Cuenta.Biz.Cuenta(
                _cuentaRepository,
                CurrentPrincipal,
                Application,
                Mapper
                );

            Mapper.Map(payload, cuenta);

            await cuenta.Save();

            return Mapper.Map<CuentaModel>(cuenta);
        }

        public async Task<CuentaModel> GetCuenta(string idCuenta)
        {
            Cuenta.Biz.Cuenta cuenta = new Cuenta.Biz.Cuenta(
                _cuentaRepository,
                CurrentPrincipal,
                Application,
                Mapper
                );

            await cuenta.Load(idCuenta);

            return Mapper.Map<CuentaModel>(cuenta);
        }

        public async Task<bool> RemoveCuenta(string idCuenta)
        {
            Cuenta.Biz.Cuenta cuenta = new Cuenta.Biz.Cuenta(
                idCuenta,
                _cuentaRepository,
                CurrentPrincipal,
                Application,
                Mapper
                );

            return await cuenta.Remove();
        }

        public async Task<CuentaModel> UpdateCuenta(CuentaQueryModel payload)
        {
            Cuenta.Biz.Cuenta cuenta = new Cuenta.Biz.Cuenta(
                payload.Numero,
                _cuentaRepository,
                CurrentPrincipal,
                Application,
                Mapper
                );

            Mapper.Map(payload, cuenta);

            await cuenta.Update();

            return Mapper.Map<CuentaModel>(cuenta);
        }
    }
}