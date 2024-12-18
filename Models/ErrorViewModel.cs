namespace LibraryManagement.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // Add this property to include an error message
        public string Message { get; set; }
    }
}