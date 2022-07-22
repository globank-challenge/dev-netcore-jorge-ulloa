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


        [TestMethod]
        public async Task GetClienteByRecordId()
        {
            int clienteId = 14;

            var result = await _repo.GetClienteById(clienteId);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, clienteId);
        }

        [TestMethod]
        public async Task GetClienteByIdentificador()
        {
            var identificacion = "1756880301";

            var result = await _repo.GetClienteByIdentificador(identificacion);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Identificacion, identificacion);
        }

        [TestMethod]
        public async Task GetMovimientosCliente()
        {
            var identificacion = "1756880301";

            var result = await _repo.GetMovimientosByCliente(identificacion, DateTime.Now.AddDays(-100), DateTime.Now);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }
    }
}
