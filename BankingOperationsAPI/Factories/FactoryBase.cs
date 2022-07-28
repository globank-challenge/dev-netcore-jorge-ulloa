using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OpBancarias.Core.Biz;
using System.Security.Claims;

namespace OpBancarias.Api.Core.Factories
{
    public abstract class FactoryBase
    {
        public Application Application { get; set; }

        public ILogger OpBancariasLogger { get; set; }

        public IActionContextAccessor ContextAccessor { get; set; }

        public IMapper Mapper { get; set; }

        public virtual ClaimsPrincipal? CurrentPrincipal => ContextAccessor.ActionContext?.HttpContext.User;

        public FactoryBase(
            Application app, 
            IMapper mapper, 
            ILogger logger,
            IActionContextAccessor actionContextAccessor)
        {
            Application = app;
            ContextAccessor = actionContextAccessor;
            Mapper = mapper;
            OpBancariasLogger = logger;
        }
    }
}
