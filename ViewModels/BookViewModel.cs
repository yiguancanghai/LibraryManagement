namespace LibraryManagement.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string PublishedDate { get; set; }  // We'll format the date as a string for display purposes
    }
}