using OpBancarias.Data.Repositories.Cliente;

namespace OpBancarias.Data.Test
{  
    [TestClass]
    public class OpBancariasClienteRepositoryTests : OpBancariasRepositoryTestBase
    {
        private IClienteRepository _repo;

        public OpBancariasClienteRepositoryTests()
        {
            _repo = new ClienteRepository(_context);
        }

        [TestMethod]
        public async Task SaveCliente()
        {
            Models.Cliente model = OpBancariasModelsMockery.GetMockedCliente();

            var result = await _repo.SaveCliente(model);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Identificacion, model.Identificacion);
        }
    }
}
