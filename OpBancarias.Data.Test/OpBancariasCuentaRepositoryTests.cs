using OpBancarias.Data.Repositories.Cuenta;

namespace OpBancarias.Data.Test
{
    [TestClass]
    public class OpBancariasCuentaRepositoryTests: OpBancariasRepositoryTestBase
    {
        private ICuentaRepository _repo;

        public OpBancariasCuentaRepositoryTests()
        {
            _repo = new CuentaRepository(_context);
        }

        [TestMethod]
        public async Task SaveCuenta()
        { 
            //todo: Get valid clienteId from cliente context
            Models.Cuenta model = OpBancariasModelsMockery.GetMockedCuenta();

            var result = await _repo.SaveCuenta(model);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.ClienteId, model.ClienteId);
        }
    }
}