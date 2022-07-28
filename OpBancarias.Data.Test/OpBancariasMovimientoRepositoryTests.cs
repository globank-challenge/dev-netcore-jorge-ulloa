using OpBancarias.Data.Repositories.Movimiento;

namespace OpBancarias.Data.Test
{
    [TestClass]
    public class OpBancariasMovimientoRepositoryTests: OpBancariasRepositoryTestBase
    {
        private readonly IMovimientoRepository _repo;

        public OpBancariasMovimientoRepositoryTests()
        {
            _repo = new MovimientoRepository(_context);
        }

        [TestMethod, Ignore]
        public async Task SaveMovimiento()
        {
            //todo: get valid cuentaid
            Models.Movimiento model = OpBancariasModelsMockery.GetMockedMovimiento();

            var result = await _repo.SaveMovimiento(model);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Valor, model.Valor);
        }

        
        [TestMethod]
        public async Task GetSaldoFromMovimientosCuenta()
        {
            string numeroCuenta = "478758";

            var result = await _repo.GetSaldoFromMovimientosCuenta(numeroCuenta);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.SaldoInicial >= 0);
        }
    }
}
