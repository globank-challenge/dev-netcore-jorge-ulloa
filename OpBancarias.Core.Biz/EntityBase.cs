using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Security.Principal;

namespace OpBancarias.Core.Biz
{
    public abstract class EntityBase<IRepositoryType>
    {
        public IRepositoryType OpBancariasRepository { get; }
        public Application Application { get; }
        public IMapper Mapper { get; }
      //  public ILogger Logger { get; }

        private ClaimsPrincipal _principal;
        public ClaimsPrincipal Principal
        {
            get
            {
               return _principal;
            }
        }

        public EntityBase() { }

        public EntityBase(
            IRepositoryType repo,
            IPrincipal principal,
            Application application,
            IMapper mapper//,
        //    ILogger logger
            )
        {
            OpBancariasRepository = repo;
            _principal = (ClaimsPrincipal)principal; //todo check security
            Application = application;
            Mapper = mapper;
    //        Logger = logger;
        }

    }
}