using Microsoft.Extensions.Configuration;

namespace OpBancarias.Core.Biz
{
    public class Application
    {
        public IConfigurationRoot Configuration { get; set; }

        public IConfigurationBuilder Builder { get; set; }

        public Application()
        {
            // common configuration for all apis and unit tests
            Builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            Builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
            Configuration = Builder.Build();
        }
    }
}
