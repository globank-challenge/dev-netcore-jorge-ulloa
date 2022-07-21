using NSubstitute;
using OpBancarias.Api.Core.Tests;
using OpBancarias.Data.Repositories.Cuenta;
using OpBancarias.Cuentas.Api.Controllers;
using OpBancarias.Cuentas.Api.Models;
using static OpBancarias.Core.Biz.Enumerations;
using OpBancarias.Clientes.Api.Models;
using OpBancarias.Clientes.Api.Controllers;
using OpBancarias.Data.Repositories.Cliente;

namespace OpBancarias.Clientes.Api.Tests
{
    [TestClass]
    public class ClientesApiTests : ApiTestBase
    {
        private ClientesModelFactory _factory;
        private ClientesController _controller;
        private IClienteRepository _repo;

        public ClientesApiTests() : base()
        {

        }

        [TestInitialize]
        public void Init()
        {
            _repo = Substitute.For<IClienteRepository>();
            _factory = new ClientesModelFactory(_repo,
                                                    Application,
                                                    Mapper,
                                                    ActionContextAccessor);

            _controller = new ClientesController(_factory);
        }

        [TestMethod, TestCategory("Clientes API")]
        public async Task AddCliente()
        {
            ClienteQueryModel payload = new()
            {
                Nombre = "John",
                Apellido = "Doe",
                Edad = 30,
                Identificacion = "1234567890",
                UserName = "john.doe",
                Password = "S0meP@ssw0rd",
                EstadoActivo = true
            };

            Data.Models.Cliente clienteSaved = new()
            {
                Id = 1,
                Nombre = "John",
                Apellido = "Doe",
                Edad = 30,
                Identificacion = "1234567890",
                UserName = "john.doe",
                Password = "S0meP@ssw0rd",
                EstadoActivo = true
            };

            _repo.SaveCliente(Arg.Any<Data.Models.Cliente>()).Returns(clienteSaved);

            var result = GetResult(await _controller.AddCliente(payload));

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, clienteSaved.Id);
            Assert.AreEqual(result.Identificacion, payload.Identificacion);
        }

        [TestMethod, TestCategory("Clientes API")]
        public async Task RemoveCliente()
        {
            string idCliente = "123456";

            Data.Models.Cliente cliente = new()
            {
                Id = 1,
                Nombre = "John",
                Apellido = "Doe",
                Edad = 30,
                Identificacion = idCliente,
                UserName = "john.doe",
                Password = "S0meP@ssw0rd",
                EstadoActivo = true
            };

            _repo.GetClienteByIdentificador(idCliente).Returns(cliente);
            _repo.RemoveCliente(Arg.Any<Data.Models.Cliente>()).Returns(1);

            var result = GetResult(await _controller.RemoveCliente(idCliente));

            Assert.IsTrue(result);
        }

        [TestMethod, TestCategory("Clientes API")]
        public async Task RemoveClienteErrorClienteConCuentas()
        {
            string idCliente = "123456";

            Data.Models.Cliente cliente = new()
            {
                Id = 1,
                Nombre = "John",
                Apellido = "Doe",
                Edad = 30,
                Identificacion = idCliente,
                UserName = "john.doe",
                Password = "S0meP@ssw0rd",
                EstadoActivo = true,
                Cuentas = new()
                {
                    new Data.Models.Cuenta 
                    {
                        Id = 1,
                        Numero = "123",
                        EstadoActivo = true,
                        SaldoInicial = 100,
                        Tipo = (int)TipoCuenta.Corriente
                    }
                }
            };

            _repo.GetClienteByIdentificador(idCliente).Returns(cliente);

            try
            {
                GetResult(await _controller.RemoveCliente(idCliente));
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "No es posible eliminar un cliente con cuentas.");
            }

        }

        [TestMethod, TestCategory("Clientes API")]
        public async Task GetCliente()
        {
            string idCliente = "123456";

            Data.Models.Cliente cliente = new()
            {
                Id = 1,
                Nombre = "John",
                Apellido = "Doe",
                Edad = 30,
                Identificacion = idCliente,
                UserName = "john.doe",
                Password = "S0meP@ssw0rd",
                EstadoActivo = true
            };

            _repo.GetClienteByIdentificador(idCliente).Returns(cliente);

            var result = GetResult(await _controller.GetCliente(idCliente));

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, cliente.Id);
        }

        [TestMethod, TestCategory("Clientes API")]
        public async Task GetClienteErrorClienteNoExiste()
        {
            string idCliente = "123456";

            _repo.GetClienteByIdentificador(idCliente).Returns(null as Data.Models.Cliente);

            try
            {
                GetResult(await _controller.GetCliente(idCliente));
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Cliente no existente.");
            }
        }

        [TestMethod, TestCategory("Clientes API")]
        public async Task UpdateCliente()
        {
            ClienteQueryModel payload = new()
            {
                Nombre = "John",
                Apellido = "Doe",
                Edad = 30,
                Identificacion = "1234567890",
                UserName = "john.doe",
                Password = "S0meP@ssw0rd",
                EstadoActivo = true
            };

            Data.Models.Cliente clienteInDB = new()
            {
                Id = 1,
                Nombre = "John",
                Apellido = "Doe",
                Edad = 30,
                Identificacion = "1234567890",
                UserName = "john.doe",
                Password = "S0meP@ssw0rd",
                EstadoActivo = false
            };


            Data.Models.Cliente clienteUpdated = new()
            {
                Id = 1,
                Nombre = payload.Nombre,
                Apellido = payload.Apellido,
                Edad = payload.Edad,
                Identificacion = payload.Identificacion,
                UserName = payload.UserName,
                Password = payload.Password,
                EstadoActivo = payload.EstadoActivo
            };

            _repo.GetClienteByIdentificador(payload.Identificacion).Returns(clienteInDB);
            _repo.SaveCliente(Arg.Any<Data.Models.Cliente>()).Returns(clienteUpdated);

            var result = GetResult(await _controller.UpdateCliente(payload));

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, clienteUpdated.Id);
            Assert.AreEqual(result.Identificacion, payload.Identificacion);
        }

        [TestMethod, TestCategory("Clientes API")]
        public async Task UpdateClienteErrorClienteNoExistente()
        {
            ClienteQueryModel payload = new()
            {
                Nombre = "John",
                Apellido = "Doe",
                Edad = 30,
                Identificacion = "1234567890",
                UserName = "john.doe",
                Password = "S0meP@ssw0rd",
                EstadoActivo = true
            };

            _repo.GetClienteByIdentificador(payload.Identificacion).Returns(null as Data.Models.Cliente);

            try
            {
                GetResult(await _controller.UpdateCliente(payload));
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Cliente no existente.");
            }
        }

        [TestMethod, TestCategory("Clientes API")]
        public async Task GetReporteMovimientosByCuenta()
        {
            string idCliente = "123456";

            MovimientosByClienteQueryModel payload = new()
            {
                FechaInicio = DateTime.Now.AddDays(-30),
                FechaFin = DateTime.Now
            };

            List<Data.Models.MovimientosCuentaDto> movimientosCuenta = new()
            {
                new Data.Models.MovimientosCuentaDto
                {
                    NombreCliente = "John Doe",
                    EstadoActivo = true,
                    Numero = "123456",
                    SaldoInicial = 100,
                    Tipo = 1,
                    Movimientos = new()
                    {
                        new Data.Models.Movimiento
                        {
                            Id = 1,
                            CuentaId = 1,
                            Fecha = Convert.ToDateTime( "10/2/2022"),
                            Saldo = 300,
                            Valor = 200,
                            Tipo = 1
                        },
                        new Data.Models.Movimiento
                        {
                            Id = 2,
                            CuentaId = 1,
                            Fecha = Convert.ToDateTime( "10/3/2022"),
                            Saldo = 200,
                            Valor = -100,
                            Tipo = 1
                        }
                    }
                },
                new Data.Models.MovimientosCuentaDto
                {
                    NombreCliente = "John Doe",
                    EstadoActivo = true,
                    Numero = "987654",
                    SaldoInicial = 50,
                    Tipo = 1,
                    Movimientos = new()
                    {
                        new Data.Models.Movimiento
                        {
                            Id = 3,
                            CuentaId = 2,
                            Fecha = Convert.ToDateTime( "10/3/2022"),
                            Saldo = 550,
                            Valor = 500,
                            Tipo = 1
                        },
                    }
                }
            };

            List<MovimientosByClienteModel> response = new()
            {
                new MovimientosByClienteModel
                {
                    Cliente = "John Doe",
                    Estado = true,
                    Fecha = "10/2/2022",
                    NumeroCuenta = "123456",
                    SaldoInicial = 100,
                    Movimiento = 200,
                    SaldoDisponible = 300,
                    Tipo = "Corriente"
                },
                new MovimientosByClienteModel
                {
                    Cliente = "John Doe",
                    Estado = true,
                    Fecha = "10/3/2022",
                    NumeroCuenta = "123456",
                    SaldoInicial = 300,
                    Movimiento = -100,
                    SaldoDisponible = 200,
                    Tipo = "Corriente"
                },
                new MovimientosByClienteModel
                {
                    Cliente = "John Doe",
                    Estado = true,
                    Fecha = "10/3/2022",
                    NumeroCuenta = "987654",
                    SaldoInicial = 50,
                    Movimiento = 500,
                    SaldoDisponible = 550,
                    Tipo = "Ahorros"
                }
            };

            Data.Models.Cliente cliente = new()
            {
                Id = 1,
                Nombre = "John",
                Apellido = "Doe",
                Edad = 30,
                Identificacion = idCliente,
                UserName = "john.doe",
                Password = "S0meP@ssw0rd",
                EstadoActivo = true
            };

            _repo.GetClienteByIdentificador(idCliente).Returns(cliente);

            _repo.GetMovimientosByCliente(idCliente, payload.FechaInicio, payload.FechaFin).Returns(movimientosCuenta);

            var result = GetResult(await _controller.GetMovimientosByCliente(idCliente, payload));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == response.Count);
        }
    }
}
