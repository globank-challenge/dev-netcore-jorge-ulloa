using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OpBancarias.Api.Core.Controllers
{
    [ApiController]
    [Route("opbancarias/[controller]/")]
    //[Authorize(AuthenticationSchemes = "Vue.Api")] //TODO: AuthenticationSchemes
    public class OpBancariasControllerBase<ModelFactoryType>: ControllerBase
    {
        public ModelFactoryType ModelFactory { get; set; }

        public OpBancariasControllerBase(ModelFactoryType modelFactory)
        {
            ModelFactory = modelFactory;
        }
    }
}