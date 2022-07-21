using Microsoft.AspNetCore.Mvc;

namespace OpBancarias.Api.Core.Controllers
{
    [ApiController]
    [Route("opbancarias/[controller]/")]
    public class OpBancariasControllerBase<ModelFactoryType>: ControllerBase
    {
        public ModelFactoryType ModelFactory { get; set; }

        public OpBancariasControllerBase(ModelFactoryType modelFactory)
        {
            ModelFactory = modelFactory;
        }
    }
}