using Microsoft.Extensions.Configuration;

namespace OpBancarias.Data.Test
{
    public class OpBancariasRepositoryTestBase
    {
        protected OpBancariasContext _context;
        protected IConfiguration _configuration;

        public OpBancariasRepositoryTestBase()
        {
            var inMemorySettings = new Dictionary<string, string> {
                    {"ConnectionStrings:OpBancariasContext", "Server=localhost; Database=OpBancarias; User ID=SA; Password=MySQLP@ssw0rd;"},
                };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

           // _context = new OpBancariasContext(_configuration);
            _context = new OpBancariasContext();
        }
    }
}
