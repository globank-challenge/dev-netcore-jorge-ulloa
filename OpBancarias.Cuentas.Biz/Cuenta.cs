using AutoMapper;
using Microsoft.Extensions.Logging;
using OpBancarias.Core.Biz;
using OpBancarias.Data.Exceptions;
using OpBancarias.Data.Repositories.Cuenta;
using OpBancarias.Movimientos.Biz;
using System.Net;
using System.Security.Principal;
using static OpBancarias.Core.Biz.Enumerations;

namespace OpBancarias.Cuenta.Biz
{
    public class Cuenta : EntityBase<ICuentaRepository>
    {
        #region Private Members

        private ICuentaRepository _repo;
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
        public string? Numero { get; set; }

        public TipoCuenta Tipo { get; set; }

        public decimal SaldoInicial { get; set; }

        public bool EstadoActivo { get; set; }

        public int ClienteId { get; set; }

        #endregion

        #region Extended properties

        public List<Movimiento>? Movimientos { get; set; }

        #endregion

        #region Constructors

        public Cuenta(): base()
        {

        }

        public Cuenta(
            string cuentaId,
            ICuentaRepository repo,
            IPrincipal principal,
            Application application,
            IMapper mapper,
            ILogger logger
            )
            : base(
                  repo,
                  principal,
                  application,
                  mapper,
                  logger
                  )
        {
            _repo = repo;
            Load(cuentaId).GetAwaiter().GetResult();
        }

        public Cuenta(
            ICuentaRepository repo,
            IPrincipal principal,
            Application application,
            IMapper mapper,
            ILogger logger
            )
            : base(
                  repo,
                  principal,
                  application,
                  mapper,
                  logger
                  )
        {
            _repo = repo;
        }

        #endregion Constructors

        #region Load

        /// <summary>
        /// Loads account info into model after asking DB for account´s number
        /// </summary>
        /// <param name="Id">account record Id</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        protected async Task Load(int Id)
        {
            Data.Models.Cuenta? model = await _repo.GetCuentaById(Id);

            if (model == null)
                throw new CustomException(
                                    "Cuenta no existente.",
                                    HttpStatusCode.BadRequest,
                                    CustomException.ErrorCodes.CuentaNoExistente);

            Load(model);
        }

        /// <summary>
        /// Loads account info into model after asking DB for account´s number
        /// </summary>
        /// <param name="cuentaId">account number</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public async Task Load(string cuentaId)
        {
            Data.Models.Cuenta? model = await _repo.GetCuentaByNumero(cuentaId);

            if (model == null)
                throw new CustomException(
                                    "Cuenta no existente.",
                                    HttpStatusCode.BadRequest,
                                    CustomException.ErrorCodes.CuentaNoExistente);

            Load(model);
        }

        /// <summary>
        /// Loads account info into entity model from DB model
        /// </summary>
        /// <param name="model"></param>
        public void Load(Data.Models.Cuenta model)
        {
            if (model == null)
            {
                model = new Data.Models.Cuenta();
            }

            Mapper.Map(model, this);

            _id = model.Id;
        }

        #endregion Load

        /// <summary>
        /// Saves account´s info on registry, used to create new accounts
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public async Task Save()
        {
            Data.Models.Cuenta updateModel = Mapper.Map<Data.Models.Cuenta>(this);
            Data.Models.Cuenta newModel = await _repo.SaveCuenta(updateModel);

            if (newModel == null)
                throw new CustomException(
                                    "Error actualizando cuenta en Base de Datos.",
                                    HttpStatusCode.InternalServerError,
                                    CustomException.ErrorCodes.InternalServerError);

            Load(newModel);
        }

        /// <summary>
        /// Updates account info from entity model
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public async Task Update()
        {
            Data.Models.Cuenta updateModel = Mapper.Map<Data.Models.Cuenta>(this);
            Data.Models.Cuenta newModel = await _repo.UpdateCuenta(updateModel);

            if (newModel == null)
                throw new CustomException(
                                    "Error actualizando cuenta en Base de Datos.",
                                    HttpStatusCode.InternalServerError,
                                    CustomException.ErrorCodes.InternalServerError);

            Load(newModel);
        }

        /// <summary>
        /// Removes account from registry
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public async Task<bool> Remove() 
        {
            //if (Movimientos?.Count > 0)
            //    throw new CustomException(
            //                        "No es posible eliminar cuenta con movimientos.",
            //                        HttpStatusCode.BadRequest,
            //                        CustomException.ErrorCodes.CuentaConMovimientos);
            //In a real world, account should not be allowed to be removed if it has history (movimientos)
            return await _repo.RemoveCuenta(Mapper.Map<Data.Models.Cuenta>(this)) > 0;
        }
    }
}