using OpBancarias.Data.Repositories.Movimiento;
using OpBancarias.Movimientos.Api.Models;

namespace OpBancarias.Movimientos.Api
{
    public class Startup : OpBancarias.Api.Core.Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
            : base(configuration)
        {

        }
        public override void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            base.ConfigureServices(services);

            services.AddAutoMapper(typeof(Biz.MovimientosMappingProfile),
                                            typeof(MovimientosMappingProfile));

            services.AddScoped<MovimientosModelFactory, MovimientosModelFactory>();
            services.AddScoped<IMovimientoRepository, MovimientoRepository>();

        }
    }
}
