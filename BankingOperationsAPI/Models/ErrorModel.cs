namespace OpBancarias.Api.Core.Models
{
    public class ErrorModel
    {
        public int HttpStatus { get; set; }
        public string? Message { get; set; }
        public int ErrorCode { get; set; }
        public string? ErrorId { get; set; }
        public string? Reference { get; set; }

    }
}
