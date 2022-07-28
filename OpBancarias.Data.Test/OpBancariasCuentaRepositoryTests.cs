using OpBancarias.Data.Repositories.Cliente;
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

        [TestMethod, Ignore]
        public async Task SaveCuenta()
        { 
            Models.Cuenta model = OpBancariasModelsMockery.GetMockedCuenta();

            var result = await _repo.SaveCuenta(model);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.ClienteId, model.ClienteId);
        }

        [TestMethod, Ignore]
        public async Task UpdateCuenta()
        {
            int cuentaId = 7;

            var cuenta = await _repo.GetCuentaById(cuentaId);
            decimal nuevoSaldoInicial = 0;

            if (cuenta != null)
            {
                nuevoSaldoInicial = cuenta.SaldoInicial += 10;
                cuenta.SaldoInicial = nuevoSaldoInicial;

                cuenta = await _repo.UpdateCuenta(cuenta);
            }

            Assert.IsNotNull(cuenta);
            Assert.AreEqual(cuenta.SaldoInicial, nuevoSaldoInicial);
        }

        [TestMethod]
        public async Task GetCuentaById()
        {
            int cuentaId = 1;

            var result = await _repo.GetCuentaById(cuentaId);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, cuentaId);
        }

        [TestMethod]
        public async Task GetCuentaByNumero()
        {
            string numeroCuenta = "478758";

            var result = await _repo.GetCuentaByNumero(numeroCuenta);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Numero, numeroCuenta);
        }

        [TestMethod, Ignore]
        public async Task RemoveCuenta()
        {
            Models.Cuenta model = OpBancariasModelsMockery.GetMockedCuenta();

            await _repo.SaveCuenta(model);

            var result = await _repo.RemoveCuenta(model);

            Assert.IsNotNull(result);
            Assert.IsTrue(result > 0);
        }
    }
}