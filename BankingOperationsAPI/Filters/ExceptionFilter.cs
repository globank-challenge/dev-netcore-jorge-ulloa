using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OpBancarias.Data.Exceptions;
using static OpBancarias.Data.Exceptions.CustomException;

namespace OpBancarias.Api.Core.Filters
{
    public class ExceptionFilter : IActionFilter, IOrderedFilter
    {
        private ILogger<ExceptionFilter> _logger;

        public int Order => int.MaxValue;

        public ExceptionFilter(
            ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Models.ErrorModel errorModel;
            CustomException? custom;
            Exception? originalException = context.Exception;
            Exception? error = context.Exception;

            if (error != null)
            {
                errorModel = new Models.ErrorModel();
                custom = error as CustomException;

                while (custom == null && error.InnerException != null)
                {
                    error = error.InnerException;
                    custom = error as CustomException;
                }

                errorModel.Reference = context.ActionDescriptor.DisplayName;
                if (custom != null)
                {
                    errorModel.HttpStatus = (int)custom.StatusCode;
                    errorModel.Message = custom.Message;
                    errorModel.ErrorCode = (int)custom.ErrorCode;
                }
                else
                {
                    //an unhandled error
                    errorModel.HttpStatus = (int)System.Net.HttpStatusCode.InternalServerError;
                    errorModel.Message = "Internal server error.";
                    errorModel.ErrorCode = (int)ErrorCodes.ApplicationError;
                }
 
                var eventId = new EventId(4000, "OperacionesBancariasApiError");

                errorModel.ErrorId = eventId.Id.ToString();

                _logger.LogError(eventId, "BankingOperations: Error procesando requerimiento {Address}", errorModel.Message);


                context.ExceptionHandled = true;
                context.Result = new ObjectResult(errorModel) { StatusCode = errorModel.HttpStatus };
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Models.ErrorModel errorModel;

            if (!context.ModelState.IsValid)
            {
                errorModel = new Models.ErrorModel();
                errorModel.HttpStatus = (int)System.Net.HttpStatusCode.BadRequest;
                errorModel.ErrorCode = (int)ErrorCodes.ModelStateError;
                errorModel.Reference = context.ActionDescriptor.DisplayName;

                errorModel.Message = string.Join(", ",
                    context.ModelState.
                    Where(ms => ms.Value?.Errors.Count > 0).
                    Select(ms => string.Join(", ", ms.Value?.Errors.Select(e => e.ErrorMessage) ?? new List<string>())));

                context.Result = new ObjectResult(errorModel) { StatusCode = errorModel.HttpStatus };
            }
        }
    }
}