using AutoMapper;
using OpBancarias.Core.Biz;
using OpBancarias.Data.Exceptions;
using OpBancarias.Data.Repositories.Movimiento;
using System.Net;
using System.Security.Principal;
using static OpBancarias.Core.Biz.Enumerations;

namespace OpBancarias.Movimientos.Biz
{
    public class Movimiento : EntityBase<IMovimientoRepository>
    {
        #region Private Members

        private readonly IMovimientoRepository _repo;
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
        public DateTime Fecha { get; set; }

        public TipoMovimiento Tipo { get; set; }

        public decimal Valor { get; set; }

        public decimal Saldo { get; set; }

        public int CuentaId { get; set; }

        #endregion

        #region Extended properties

        public string NumeroCuenta { get; set; }

        #endregion

        #region Constructors

        public Movimiento()
        {

        }

        public Movimiento(
            IMovimientoRepository repo,
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

        public void Load(Data.Models.Movimiento model)
        {
            if (model == null)
            {
                model = new Data.Models.Movimiento();
            }

            Mapper.Map(model, this);

            _id = model.Id;
        }

        #endregion Load

        #region Save

        public async Task Save()
        {
            Data.Models.Movimiento newModel = await _repo.SaveMovimiento(Mapper.Map<Data.Models.Movimiento>(this));

            if (newModel == null)
                throw new CustomException(
                    "Error actualizando movimiento en Base de Datos.",
                    HttpStatusCode.InternalServerError,
                    CustomException.ErrorCodes.InternalServerError);

            Load(newModel);
        }


        public async Task Deposito()
        {
            await SetMovimiento();

            await Save();
        }

        public async Task Extraccion()
        {
            await SetMovimiento();

            if (Saldo < 0)
                throw new CustomException(
                    "Saldo no disponible.",
                    HttpStatusCode.BadRequest,
                    CustomException.ErrorCodes.CuentaSinFondos);

            await Save();
        }

        private async Task SetMovimiento()
        {
            // Setear valores para el movimiento
            Fecha = DateTime.Now;
            Tipo = Valor > 0 ? TipoMovimiento.Credito : TipoMovimiento.Debito;

            var cuentaWithSaldoUpdated = await _repo.GetSaldoFromMovimientosCuenta(NumeroCuenta);

            if (cuentaWithSaldoUpdated == null)
                throw new CustomException(
                                    "Cuenta no existente.",
                                    HttpStatusCode.BadRequest,
                                    CustomException.ErrorCodes.CuentaNoExistente);

            if (!cuentaWithSaldoUpdated.EstadoActivo)
                throw new CustomException(
                                    "Cuenta inactiva.",
                                    HttpStatusCode.BadRequest,
                                    CustomException.ErrorCodes.CuentaInactiva);

            if (cuentaWithSaldoUpdated != null)
            {
                Saldo = cuentaWithSaldoUpdated.SaldoInicial + Valor;
                CuentaId = cuentaWithSaldoUpdated.Id;
            }
            
        }

        #endregion Save
    }
}