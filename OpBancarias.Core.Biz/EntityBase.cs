using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Security.Principal;

namespace OpBancarias.Core.Biz
{
    /// <summary>
    /// Base class for entitie´s (Cuenta, Movimiento and Cliente)
    /// </summary>
    /// <typeparam name="IRepositoryType"></typeparam>
    public abstract class EntityBase<IRepositoryType>
    {
        public IRepositoryType OpBancariasRepository { get; }
        public Application Application { get; }
        public IMapper Mapper { get; }
        public ILogger OpBancariasLogger { get; }

        private ClaimsPrincipal _principal;
        public ClaimsPrincipal? Principal
        {
            get
            {
               return _principal;
            }
        }

        public EntityBase() 
        { 

        }

        public EntityBase(
            IRepositoryType repo,
            IPrincipal principal,
            Application application,
            IMapper mapper,
            ILogger logger
            )
        {
            OpBancariasRepository = repo;
            _principal = (ClaimsPrincipal)principal; 
            Application = application;
            Mapper = mapper;
            OpBancariasLogger = logger;
        }

    }
}