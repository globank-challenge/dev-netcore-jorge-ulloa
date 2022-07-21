using AutoMapper;
using OpBancarias.Core.Biz;
using OpBancarias.Data.Exceptions;
using OpBancarias.Data.Repositories.Cliente;
using System.Net;
using System.Security.Principal;
using static OpBancarias.Core.Biz.Enumerations;

namespace OpBancarias.Clientes.Biz
{
    public class Cliente: EntityBase<IClienteRepository>
    {
        #region Private Members

        private IClienteRepository _repo;
        private int _id;

        #endregion

        #region Base Properties

        public int Id
        {
            get
            {
                return _id;
            }
        }
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool EstadoActivo { get; set; }

        public string Identificacion { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int Edad { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        #endregion

        #region Extended properties

        public List<Cuenta.Biz.Cuenta> Cuentas { get; set; }

        #endregion

        #region Constructors

        public Cliente(
            int userId,
            IClienteRepository repo,
            IPrincipal principal,
            Application application,
            IMapper mapper
            )
            : base(
                  repo,
                  principal,
                  application,
                  mapper
                  )
        {
            _repo = repo;
            Load(userId).GetAwaiter().GetResult();
        }

        public Cliente(
            string clienteId,
            IClienteRepository repo,
            IPrincipal principal,
            Application application,
            IMapper mapper
            )
            : base(
                  repo,
                  principal,
                  application,
                  mapper
                  )
        {
            _repo = repo;
            Load(clienteId).GetAwaiter().GetResult();
        }

        public Cliente(
            IClienteRepository repo,
            IPrincipal principal,
            Application application,
            IMapper mapper
            )
            : base(
                  repo,
                  principal,
                  application,
                  mapper
                  )
        {
            _repo = repo;
        }

        #endregion Constructors

        #region Load

        protected async Task Load(int userId)
        {
            Data.Models.Cliente? model = await _repo.GetClienteById(userId);

            if (model == null)
                throw new CustomException(
                                    "Cliente no existente.",
                                    HttpStatusCode.BadRequest,
                                    CustomException.ErrorCodes.ClienteNoExistente);

            Load(model);
        }

        public async Task Load(string clienteId)
        {
            Data.Models.Cliente? model = await _repo.GetClienteByIdentificador(clienteId);

            if (model == null)
                throw new CustomException(
                                    "Cliente no existente.",
                                    HttpStatusCode.BadRequest,
                                    CustomException.ErrorCodes.ClienteNoExistente);

            Load(model);
        }

        public void Load(Data.Models.Cliente model)
        {
            if (model == null)
            {
                model = new Data.Models.Cliente();
            }

            Mapper.Map(model, this);

            _id = model.Id;
        }

        #endregion Load

        public async Task Save()
        {
            Data.Models.Cliente updateModel;
            Data.Models.Cliente newModel;

            updateModel = Mapper.Map<Data.Models.Cliente>(this);
            newModel = await _repo.SaveCliente(updateModel);

            if (newModel != null)
            {
                Load(newModel);
            }
        }

        public async Task Update()
        {
            Data.Models.Cliente updateModel;
            Data.Models.Cliente newModel;

            updateModel = Mapper.Map<Data.Models.Cliente>(this);
            newModel = await _repo.UpdateCliente(updateModel);

            if (newModel != null)
            {
                Load(newModel);
            }
        }

        public async Task<List<ReporteMovimientosModel>> GetMovimientos(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Data.Models.MovimientosCuentaDto> movimientosCuenta = await _repo.GetMovimientosByCliente(Identificacion, fechaInicio, fechaFin);
                
            var movimientos = new List<ReporteMovimientosModel>();

            decimal saldoInicial = 0;

            if (movimientosCuenta != null)
            {
                foreach (var cuenta in movimientosCuenta.OrderBy(cta => cta.Numero))
                {
                    saldoInicial = cuenta.SaldoInicial;

                    if (cuenta.Movimientos != null)
                    {
                        foreach (var movimiento in cuenta.Movimientos.OrderBy(mov => mov.Id))
                        {
                            movimientos.Add(new ReporteMovimientosModel
                            {
                                Cliente = cuenta.NombreCliente,
                                NumeroCuenta = cuenta.Numero,
                                Estado = cuenta.EstadoActivo,
                                Tipo = Enum.GetName(typeof(TipoCuenta), cuenta.Tipo),
                                SaldoInicial = saldoInicial,
                                Fecha = movimiento.Fecha.ToString("dd/MM/yyyy"),
                                Movimiento = movimiento.Valor,
                                SaldoDisponible = movimiento.Saldo
                            });

                            saldoInicial = movimiento.Saldo;
                        }
                    }
                }
            }

            return movimientos;
        }

        public async Task<bool> Remove()
        {
            if (Cuentas.Count() > 0)
                throw new CustomException(
                                    "No es posible eliminar un cliente con cuentas.",
                                    HttpStatusCode.BadRequest,
                                    CustomException.ErrorCodes.ClienteConCuentas);

            return await _repo.RemoveCliente(Mapper.Map<Data.Models.Cliente>(this)) > 0;
        }
    }
}