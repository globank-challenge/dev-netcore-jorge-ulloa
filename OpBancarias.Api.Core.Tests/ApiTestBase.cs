using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OpBancarias.Biz.Core.Tests;

namespace OpBancarias.Api.Core.Tests
{
    public class ApiTestBase : UnitTestBase
    {
        public IActionContextAccessor ActionContextAccessor { get; }

        public ApiTestBase() : base()
        {
            ActionContextAccessor = new ActionContextAccessor();
            ActionContextAccessor.ActionContext = new ActionContext();
            ActionContextAccessor.ActionContext.HttpContext = new DefaultHttpContext();
        }

        public TResult GetResult<TResult>(ActionResult<TResult> actionResult)
        {
            TResult typedResult;
            ObjectResult objResult;

            objResult = (ObjectResult)actionResult.Result;
            typedResult = (TResult)objResult.Value;

            return typedResult;
        }
    }
}