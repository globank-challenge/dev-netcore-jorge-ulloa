﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OpBancarias.Api.Core.Factories;
using OpBancarias.Core.Biz;
using OpBancarias.Data.Repositories.Cuenta;


namespace OpBancarias.Cuentas.Api.Models
{
    public class CuentasModelFactory: OpBancariasModelFactoryBase<ICuentaRepository>
    {
        public CuentasModelFactory(
            ICuentaRepository repo,
            Application app,
            IMapper mapper,
            ILogger<CuentasModelFactory> logger,
            IActionContextAccessor actionContextAccessor)
                : base(
                      repo,
                      app,
                      mapper,
                      logger,
                      actionContextAccessor)
        {

        }

        public async Task<CuentaModel> SaveCuenta(CuentaModel payload)
        {
            Cuenta.Biz.Cuenta cuenta = new Cuenta.Biz.Cuenta(
                OpBancariasRepo,
                CurrentPrincipal,
                Application,
                Mapper,
                OpBancariasLogger
                );

            Mapper.Map(payload, cuenta);

            await cuenta.Save();

            return Mapper.Map<CuentaModel>(cuenta);
        }

        public async Task<CuentaModel> GetCuenta(string idCuenta)
        {
            Cuenta.Biz.Cuenta cuenta = new Cuenta.Biz.Cuenta(
                OpBancariasRepo,
                CurrentPrincipal,
                Application,
                Mapper,
                OpBancariasLogger
                );

            await cuenta.Load(idCuenta);

            return Mapper.Map<CuentaModel>(cuenta);
        }

        public async Task<bool> RemoveCuenta(string idCuenta)
        {
            Cuenta.Biz.Cuenta cuenta = new Cuenta.Biz.Cuenta(
                idCuenta,
                OpBancariasRepo,
                CurrentPrincipal,
                Application,
                Mapper,
                OpBancariasLogger
                );

            return await cuenta.Remove();
        }

        public async Task<CuentaModel> UpdateCuenta(CuentaQueryModel payload)
        {
            Cuenta.Biz.Cuenta cuenta = new Cuenta.Biz.Cuenta(
                payload.Numero,
                OpBancariasRepo,
                CurrentPrincipal,
                Application,
                Mapper,
                OpBancariasLogger
                );

            Mapper.Map(payload, cuenta);

            await cuenta.Update();

            return Mapper.Map<CuentaModel>(cuenta);
        }
    }
}