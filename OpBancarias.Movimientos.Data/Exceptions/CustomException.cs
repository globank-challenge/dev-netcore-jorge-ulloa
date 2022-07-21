using System.ComponentModel;
using System.Net;

namespace OpBancarias.Data.Exceptions
{
    public class CustomException : Exception
    {
        public enum ErrorCodes
        {
            ApplicationError,
            ModelStateError,
            BadRequest = 400,
            UnauthorizedRequest = 401,
            AuthTokenExpired = 410,
            [Description("Not Found")]
            NotFound = 404,
            [Description("Internal Server Error")]
            InternalServerError = 500,
            [Description("Saldo no disponible.")]
            CuentaSinFondos = 400001,
            [Description("Cuenta no existente.")]
            CuentaNoExistente = 400002,
            [Description("Cuenta inactiva.")]
            CuentaInactiva = 400003,
            [Description("Cuenta tiene movimientos.")]
            CuentaConMovimientos = 400004,
            [Description("Cliente tiene cuentas.")]
            ClienteConCuentas = 400005,
            [Description("Cliente no existe.")]
            ClienteNoExistente = 400006
        }

        public ErrorCodes ErrorCode { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public CustomException(
            string message,
            HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest,
            ErrorCodes errorCode = ErrorCodes.ApplicationError)
            : base(message)
        {
            StatusCode = httpStatusCode;
            ErrorCode = errorCode;
        }

        public CustomException(
            Exception ex,
            string message,
            HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest,
            ErrorCodes errorCode = ErrorCodes.ApplicationError)
            : base(message, ex)
        {
            StatusCode = httpStatusCode;
            ErrorCode = errorCode;
        }
    }

}
