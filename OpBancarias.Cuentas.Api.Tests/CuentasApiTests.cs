using NSubstitute;
using OpBancarias.Api.Core.Tests;
using OpBancarias.Data.Repositories.Cuenta;
using OpBancarias.Cuentas.Api.Controllers;
using OpBancarias.Cuentas.Api.Models;
using static OpBancarias.Core.Biz.Enumerations;


namespace OpBancarias.Cuentas.Api.Tests
{
    [TestClass]
    public class CuentasApiTests : ApiTestBase
    {
        private CuentasModelFactory _factory;
        private CuentasController _controller;
        private ICuentaRepository _repo;

        public CuentasApiTests() : base()
        {

        }

        [TestInitialize]
        public void Init()
        {
            _repo = Substitute.For<ICuentaRepository>();
            _factory = new CuentasModelFactory(_repo,
                                                    Application,
                                                    Mapper,
                                                    ActionContextAccessor);

            _controller = new CuentasController(_factory);
        }

        [TestMethod, TestCategory("Cuentas API")]
        public async Task AddCuentaAhorro()
        {
            CuentaQueryModel payload = new()
            {
                Numero = "123456",
                EstadoActivo = true,
                SaldoInicial = 100,
                Tipo = TipoCuenta.Ahorros,
            };

            Data.Models.Cuenta cuentaSaved = new()
            {
                Id = 1,
                Numero = payload.Numero,
                EstadoActivo = payload.EstadoActivo,
                SaldoInicial = payload.SaldoInicial,
                Tipo = (int)payload.Tipo
            };

            _repo.SaveCuenta(Arg.Any<Data.Models.Cuenta>()).Returns(cuentaSaved);

            var result = GetResult(await _controller.AddCuenta(payload));

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, cuentaSaved.Id);
            Assert.AreEqual(result.Numero, payload.Numero);
            Assert.AreEqual(result.Tipo, TipoCuenta.Ahorros);
        }


        [TestMethod, TestCategory("Cuentas API")]
        public async Task AddCuentaCorriente()
        {
            CuentaQueryModel payload = new()
            {
                Numero = "123456",
                EstadoActivo = true,
                SaldoInicial = 100,
                Tipo = TipoCuenta.Corriente,
            };

            Data.Models.Cuenta cuentaSaved = new()
            {
                Id = 1,
                Numero = payload.Numero,
                EstadoActivo = payload.EstadoActivo,
                SaldoInicial = payload.SaldoInicial,
                Tipo = (int)payload.Tipo
            };

            _repo.SaveCuenta(Arg.Any<Data.Models.Cuenta>()).Returns(cuentaSaved);

            var result = GetResult(await _controller.AddCuenta(payload));

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, cuentaSaved.Id);
            Assert.AreEqual(result.Numero, payload.Numero);
            Assert.AreEqual(result.Tipo, TipoCuenta.Corriente);
        }


        [TestMethod, TestCategory("Cuentas API")]
        public async Task RemoveCuenta()
        {
            string numeroCuenta = "123456";

            Data.Models.Cuenta cuenta = new()
            {
                Id = 1,
                Numero = numeroCuenta,
                ClienteId = 1,
                EstadoActivo = true,
                SaldoInicial = 100,
                Tipo = (int)TipoCuenta.Ahorros
            };

            _repo.GetCuentaByNumero(Arg.Any<string>()).Returns(cuenta);
            _repo.RemoveCuenta(Arg.Any<Data.Models.Cuenta>()).Returns(1);

            var result = GetResult(await _controller.RemoveCuenta(numeroCuenta));

            Assert.IsTrue(result);
        }

        [TestMethod, TestCategory("Cuentas API")]
        public async Task RemoveCuentaErrorCuentaConMovimientos()
        {
            string numeroCuenta = "123456";

            Data.Models.Cuenta cuenta = new()
            {
                Id = 1,
                Numero = numeroCuenta,
                ClienteId = 1,
                EstadoActivo = true,
                SaldoInicial = 100,
                Tipo = (int)TipoCuenta.Ahorros,
                Movimientos = new List<Data.Models.Movimiento>()
                {
                    new Data.Models.Movimiento()
                    {
                        Id = 1,
                        CuentaId = 1,
                        Fecha = DateTime.Now,
                        Saldo = 100,
                        Tipo = (int)TipoMovimiento.Credito,
                        Valor =  10
                    }
                }
            };

            _repo.GetCuentaByNumero(Arg.Any<string>()).Returns(cuenta);

            try
            {
                GetResult(await _controller.RemoveCuenta(numeroCuenta));
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "No es posible eliminar cuenta con movimientos.");
            }


        }

        [TestMethod, TestCategory("Cuentas API")]
        public async Task GetCuenta()
        {
            string numeroCuenta = "123456";

            Data.Models.Cuenta cuenta = new()
            {
                Id = 1,
                Numero = numeroCuenta,
                ClienteId = 1,
                EstadoActivo = true,
                SaldoInicial = 100,
                Tipo = (int)TipoCuenta.Ahorros
            };

            _repo.GetCuentaByNumero(numeroCuenta).Returns(cuenta);

            var result = GetResult(await _controller.GetCuenta(numeroCuenta));

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, cuenta.Id);
        }

        [TestMethod, TestCategory("Cuentas API")]
        public async Task GetCuentaErrorCuentaNoExiste()
        {
            string numeroCuenta = "123456";

            _repo.GetCuentaByNumero(numeroCuenta).Returns(null as Data.Models.Cuenta);

            try
            {
                GetResult(await _controller.GetCuenta(numeroCuenta));
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Cuenta no existente.");
            }
        }

        [TestMethod, TestCategory("Cuentas API")]
        public async Task UpdateCuentaAhorro()
        {
            CuentaQueryModel payload = new()
            {
                Id = 1,
                Numero = "478758",
                EstadoActivo = true,
                SaldoInicial = 100,
                Tipo = TipoCuenta.Ahorros,
            };

            Data.Models.Cuenta cuentaInDB = new()
            {
                Id = 1,
                Numero = payload.Numero,
                EstadoActivo = payload.EstadoActivo,
                SaldoInicial = 1000,
                Tipo = (int)payload.Tipo
            };

            Data.Models.Cuenta cuentaUpdated = new()
            {
                Id = 1,
                Numero = payload.Numero,
                EstadoActivo = payload.EstadoActivo,
                SaldoInicial = payload.SaldoInicial,
                Tipo = (int)payload.Tipo
            };

            _repo.GetCuentaByNumero(payload.Numero).Returns(cuentaInDB);
            _repo.UpdateCuenta(Arg.Any<Data.Models.Cuenta>()).Returns(cuentaUpdated);

            var result = GetResult(await _controller.UpdateCuenta(payload));

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, payload.Id);
            Assert.AreEqual(result.SaldoInicial, payload.SaldoInicial);
        }

        [TestMethod, TestCategory("Cuentas API")]
        public async Task UpdateCuentaAhorroErrorCuentaNoExistente()
        {
            CuentaQueryModel payload = new()
            {
                Id = 1,
                Numero = "123456",
                EstadoActivo = true,
                SaldoInicial = 100,
                Tipo = TipoCuenta.Ahorros,
            };

            _repo.GetCuentaByNumero(payload.Numero).Returns(null as Data.Models.Cuenta);

            try
            {
                GetResult(await _controller.UpdateCuenta(payload));
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Cuenta no existente.");
            }
        }

    }
}