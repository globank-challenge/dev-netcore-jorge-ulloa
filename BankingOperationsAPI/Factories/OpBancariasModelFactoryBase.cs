using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OpBancarias.Core.Biz;

namespace OpBancarias.Api.Core.Factories
{
    public class OpBancariasModelFactoryBase<IRepositoryType>: FactoryBase
    {
        public IRepositoryType OpBancariasRepo { get; set; }
       // public ILogger Logger { get; set; }

        public OpBancariasModelFactoryBase(
            IRepositoryType opBancariasRepo,
            //ILogger logger,
            Application app,
            IMapper mapper,
            IActionContextAccessor actionContextAccessor)
                : base(app, mapper, actionContextAccessor)
        {
            OpBancariasRepo = opBancariasRepo;
          //  Logger = logger;
        }
    }
}
