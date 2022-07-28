using OpBancarias.Usuarios.Api.Models;
using OpBancarias.Data.Repositories.Usuario;

namespace OpBancarias.Usuarios.Api
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

            services.AddAutoMapper(typeof(Biz.UsuariosMappingProfile),
                                            typeof(UsuariosMappingProfile));

            services.AddScoped<UsuariosModelFactory, UsuariosModelFactory>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        }
    }
}
