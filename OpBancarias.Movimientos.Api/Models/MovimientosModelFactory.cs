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
        private IMovimientoRepository _movimientoRepository;

        public MovimientosModelFactory(
            IMovimientoRepository repo,
            Application app,
            IMapper mapper,
            IActionContextAccessor actionContextAccessor)
                : base(
                      repo,
                      app,
                      mapper,
                      actionContextAccessor)
        {
            _movimientoRepository = repo;
        }

        public async Task<MovimientoModel> Deposito(MovimientoQueryModel payload)
        {
            Movimiento movimiento = new Movimiento (
                _movimientoRepository,
                CurrentPrincipal,
                Application,
                Mapper
                );

            Mapper.Map(payload, movimiento);

            await movimiento.Deposito();

            return Mapper.Map<MovimientoModel>(movimiento);
        }

        public async Task<MovimientoModel> Extraccion(MovimientoQueryModel payload)
        {
            Movimiento movimiento = new Movimiento(
                _movimientoRepository,
                CurrentPrincipal,
                Application,
                Mapper
                );

            Mapper.Map(payload, movimiento);

            await movimiento.Extraccion();

            return Mapper.Map<MovimientoModel>(movimiento);
        }
    }
}