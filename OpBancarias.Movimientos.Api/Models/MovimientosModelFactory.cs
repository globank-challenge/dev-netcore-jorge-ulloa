using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OpBancarias.Api.Core.Factories;
using OpBancarias.Core.Biz;
using OpBancarias.Data.Repositories.Movimiento;
using OpBancarias.Movimientos.Biz;

namespace OpBancarias.Movimientos.Api.Models
{
    public class MovimientosModelFactory : OpBancariasModelFactoryBase<IMovimientoRepository>
    {
        public MovimientosModelFactory(
            IMovimientoRepository repo,
            Application app,
            IMapper mapper,
            ILogger<MovimientosModelFactory> logger,
            IActionContextAccessor actionContextAccessor)
                : base(
                      repo,
                      app,
                      mapper,
                      logger,
                      actionContextAccessor)
        {

        }

        /// <summary>
        /// Executes a deposit of a given amount into a given account
        /// </summary>
        /// <param name="payload">contains account number and withdrawal amount</param>
        /// <returns></returns>
        public async Task<MovimientoModel> Deposito(MovimientoQueryModel payload)
        {
            Movimiento movimiento = new Movimiento (
                OpBancariasRepo,
                CurrentPrincipal,
                Application,
                Mapper,
                OpBancariasLogger
                );

            Mapper.Map(payload, movimiento);

            await movimiento.Deposito();

            return Mapper.Map<MovimientoModel>(movimiento);
        }

        /// <summary>
        /// Executes a withdrawal of a given amount into a given account
        /// </summary>
        /// <param name="payload">contains account number and withdrawal amount</param>
        /// <returns></returns>
        public async Task<MovimientoModel> Extraccion(MovimientoQueryModel payload)
        {
            Movimiento movimiento = new Movimiento(
                OpBancariasRepo,
                CurrentPrincipal,
                Application,
                Mapper,
                OpBancariasLogger
                );

            Mapper.Map(payload, movimiento);

            await movimiento.Extraccion();

            return Mapper.Map<MovimientoModel>(movimiento);
        }
    }
}