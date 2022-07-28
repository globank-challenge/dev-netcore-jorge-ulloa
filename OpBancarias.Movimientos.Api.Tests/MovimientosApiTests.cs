using Microsoft.Extensions.Logging;
using NSubstitute;
using OpBancarias.Api.Core.Tests;
using OpBancarias.Data.Repositories.Movimiento;
using OpBancarias.Movimientos.Api.Controllers;
using OpBancarias.Movimientos.Api.Models;
using static OpBancarias.Core.Biz.Enumerations;

namespace OpBancarias.Movimientos.Api.Tests
{
    [TestClass]
    public class MovimientosApiTests: ApiTestBase
    {
        private MovimientosModelFactory _factory;
        private MovimientosController _controller;
        private IMovimientoRepository _movimientosRepo;
        private ILogger<MovimientosModelFactory> _logger;

        public MovimientosApiTests() : base()
        {

        }

        [TestInitialize]
        public void Init()
        {
            _movimientosRepo = Substitute.For<IMovimientoRepository>();
            _logger = Substitute.For<ILogger<MovimientosModelFactory>>();

            _factory = new MovimientosModelFactory(_movimientosRepo,
                                                    Application,
                                                    Mapper,
                                                    _logger,
                                                    ActionContextAccessor);

            _controller = new MovimientosController(_factory);
        }

        [TestMethod, TestCategory("Movimientos API")]
        public async Task Deposito()
        {
            MovimientoQueryModel payload = new ()
            {
                Valor = 100,
                NumeroCuenta = "1234567890"
            };

            Data.Models.Cuenta cuentaWithSaldoUpdated = new ()
            {
                Id = 1,
                Numero = payload.NumeroCuenta,
                ClienteId = 1,
                EstadoActivo = true,
                SaldoInicial = 100,
                Tipo = (int)TipoCuenta.Ahorros
            };

            Data.Models.Movimiento movimiento = new ()
            {
                Id = 1,
                CuentaId = 1,
                Fecha = DateTime.Now,
                Valor = payload.Valor,
                Tipo = (int)TipoMovimiento.Credito,
                Saldo = cuentaWithSaldoUpdated.SaldoInicial + payload.Valor
            };

            _movimientosRepo.SaveMovimiento(Arg.Any<Data.Models.Movimiento>()).Returns(movimiento);

            _movimientosRepo.GetSaldoFromMovimientosCuenta(payload.NumeroCuenta).Returns(cuentaWithSaldoUpdated);


            var result = GetResult(await _controller.Deposito(payload));

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, movimiento.Id);
            Assert.AreEqual(result.Saldo, cuentaWithSaldoUpdated.SaldoInicial + payload.Valor);
        }

        [TestMethod, TestCategory("Movimientos API")]
        public async Task DepositoErrorCuentaNoActiva()
        {
            MovimientoQueryModel payload = new()
            {
                Valor = 100,
                NumeroCuenta = "1234567890"
            };

            Data.Models.Cuenta cuentaWithSaldoUpdated = new()
            {
                Id = 1,
                Numero = payload.NumeroCuenta,
                ClienteId = 1,
                EstadoActivo = false,
                SaldoInicial = 100,
                Tipo = (int)TipoCuenta.Ahorros
            };

            Data.Models.Movimiento movimiento = new()
            {
                Id = 1,
                CuentaId = 1,
                Fecha = DateTime.Now,
                Valor = payload.Valor,
                Tipo = (int)TipoMovimiento.Credito,
                Saldo = cuentaWithSaldoUpdated.SaldoInicial + payload.Valor
            };

            _movimientosRepo.SaveMovimiento(Arg.Any<Data.Models.Movimiento>()).Returns(movimiento);

            _movimientosRepo.GetSaldoFromMovimientosCuenta(payload.NumeroCuenta).Returns(cuentaWithSaldoUpdated);

            try
            {
                var result = GetResult(await _controller.Deposito(payload));

            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Cuenta inactiva.");
            }
        }


        [TestMethod, TestCategory("Movimientos API")]
        public async Task Extraccion()
        {
            MovimientoQueryModel payload = new ()
            {
                Valor = -100,
                NumeroCuenta = "1234567890"
            };

            Data.Models.Cuenta cuentaWithSaldoUpdated = new ()
            {
                Id = 1,
                Numero = payload.NumeroCuenta,
                ClienteId = 1,
                EstadoActivo = true,
                SaldoInicial = 100,
                Tipo = (int)TipoCuenta.Ahorros
            };

            Data.Models.Movimiento movimiento = new ()
            {
                Id = 1,
                CuentaId = 1,
                Fecha = DateTime.Now,
                Valor = payload.Valor,
                Tipo = (int)TipoMovimiento.Debito,
                Saldo = cuentaWithSaldoUpdated.SaldoInicial + payload.Valor
            };

            _movimientosRepo.SaveMovimiento(Arg.Any<Data.Models.Movimiento>()).Returns(movimiento);

            _movimientosRepo.GetSaldoFromMovimientosCuenta(payload.NumeroCuenta).Returns(cuentaWithSaldoUpdated);


            var result = GetResult(await _controller.Extraccion(payload));

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, movimiento.Id);
            Assert.AreEqual(result.Saldo, cuentaWithSaldoUpdated.SaldoInicial + payload.Valor);
        }

        [TestMethod, TestCategory("Movimientos API")]
        public async Task ExtraccionErrorSaldoNoDisponible()
        {
            MovimientoQueryModel payload = new ()
            {
                Valor = -500,
                NumeroCuenta = "1234567890"
            };

            Data.Models.Cuenta cuentaWithSaldoUpdated = new Data.Models.Cuenta()
            {
                Id = 1,
                Numero = payload.NumeroCuenta,
                ClienteId = 1,
                EstadoActivo = true,
                SaldoInicial = 100,
                Tipo = (int)TipoCuenta.Ahorros
            };

            Data.Models.Movimiento movimiento = new Data.Models.Movimiento()
            {
                CuentaId = 1,
                Fecha = DateTime.Now,
                Valor = payload.Valor,
                Tipo = (int)TipoMovimiento.Debito,
            };

            _movimientosRepo.SaveMovimiento(Arg.Any<Data.Models.Movimiento>()).Returns(movimiento);

            _movimientosRepo.GetSaldoFromMovimientosCuenta(payload.NumeroCuenta).Returns(cuentaWithSaldoUpdated);

            try
            {
                var result = GetResult(await _controller.Extraccion(payload));

            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Saldo no disponible.");
            }
        }
    }
}