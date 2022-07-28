using AutoMapper;
using Microsoft.Extensions.Logging;
using OpBancarias.Core.Biz;
using OpBancarias.Data.Exceptions;
using OpBancarias.Data.Repositories.Usuario;
using System.Net;
using System.Security.Principal;


namespace OpBancarias.Usuarios.Biz
{
    public class Usuario: EntityBase<IUsuarioRepository>
    {
        #region Private Members

        private readonly IUsuarioRepository _repo;

        #endregion

        #region Base Properties

        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        #endregion Base Properties

        #region Constructors

        public Usuario(
            IUsuarioRepository repo,
            IPrincipal? principal,
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
        /// Loads user´s info into model after asking DB for user name
        /// </summary>
        /// <param name="userName">user´s identifier</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public async Task Load(string? userName)
        {
            Data.Models.Usuario? model = await _repo.GetUsuario(userName);

            if (model == null)
                throw new CustomException(
                                    "Usuario no existente.",
                                    HttpStatusCode.BadRequest,
                                    CustomException.ErrorCodes.UsuarioNoExistente);

            Load(model);
        }

        /// <summary>
        /// Loads user info
        /// </summary>
        /// <param name="model"></param>
        public void Load(Data.Models.Usuario? model)
        {
            if (model == null)
            {
                model = new Data.Models.Usuario();
            }

            Mapper.Map(model, this);
        }

        #endregion Load

        /// <summary>
        /// Create new user 
        /// </summary>
        /// <returns></returns>
        public async Task Save()
        {
            Data.Models.Usuario updateModel = Mapper.Map<Data.Models.Usuario>(this);
            Data.Models.Usuario newModel = await _repo.SaveUsuario(updateModel);

            if (newModel != null)
            {
                Load(newModel);
            }
        }
    }
}