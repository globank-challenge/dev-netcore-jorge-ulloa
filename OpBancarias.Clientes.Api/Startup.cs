using OpBancarias.Clientes.Api.Models;
using OpBancarias.Data.Repositories.Cliente;

namespace OpBancarias.Clientes.Api
{
    public class Startup : OpBancarias.Api.Core.Startup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {

        }
        public override void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            base.ConfigureServices(services);

            services.AddAutoMapper(typeof(Biz.ClienteMappingProfile),
                                            typeof(ClienteMappingProfile));

            services.AddScoped<ClientesModelFactory, ClientesModelFactory>();
            services.AddScoped<IClienteRepository, ClienteRepository>();

        }
    }
}
