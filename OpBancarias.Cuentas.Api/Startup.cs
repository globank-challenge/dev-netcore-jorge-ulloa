using OpBancarias.Cuentas.Api.Models;
using OpBancarias.Data.Repositories.Cuenta;

namespace OpBancarias.Cuentas.Api
{
    public class Startup: OpBancarias.Api.Core.Startup
    {
        public Startup(IConfiguration configuration)
            :base (configuration)
        {

        }
        public override void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            base.ConfigureServices(services);

            services.AddAutoMapper(typeof(Biz.CuentaMappingProfile),
                                            typeof(CuentaMappingProfile));

            services.AddScoped<CuentasModelFactory, CuentasModelFactory>();
            services.AddScoped<ICuentaRepository, CuentaRepository>();

        }
    }

}