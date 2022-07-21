using Microsoft.Extensions.Configuration;

namespace OpBancarias.Core.Biz
{
    public class Application
    {
        public IConfigurationRoot Configuration { get; set; }

        public IConfigurationBuilder Builder { get; set; }

        public Application()
        {
            Builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddJsonFile("shared-appsettings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
            Configuration = Builder.Build();
        }
    }
}
