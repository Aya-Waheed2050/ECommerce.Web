using System.Net;

namespace Shared.ErrorModels
{
    public class ValidationErrorResponse
    {
        public int StatusCode { get; set; } 
        public string ErrorMessage { get; set; } = default!;
        public IEnumerable<ValidationError> Errors { get; set; } = [];

    }
}
