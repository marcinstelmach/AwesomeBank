namespace AwesomeBank.Api.Models
{
    using System.Collections.Generic;

    public class ErrorResponseViewModel
    {
        public string Message { get; set; }

        public IEnumerable<ErrorViewModel> Errors { get; set; }
    }
}