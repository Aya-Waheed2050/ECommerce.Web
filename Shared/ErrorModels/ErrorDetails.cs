namespace Shared.ErrorModels
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; } = default!;
        public List<string>? Errors { get; set; }
    
    }
}
