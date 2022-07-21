using OpBancarias.Data.Repositories.Movimiento;

namespace OpBancarias.Data.Test
{
    [TestClass]
    public class OpBancariasMovimientoRepositoryTests: OpBancariasRepositoryTestBase
    {
        private IMovimientoRepository _repo;

        public OpBancariasMovimientoRepositoryTests()
        {
            _repo = new MovimientoRepository(_context);
        }

        [TestMethod]
        public async Task SaveMovimiento()
        {
            //todo: get valid cuentaid
            Models.Movimiento model = OpBancariasModelsMockery.GetMockedMovimiento();

            var result = await _repo.SaveMovimiento(model);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.CuentaId, model.CuentaId);
        }
    }
}
