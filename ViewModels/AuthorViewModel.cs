namespace LibraryManagement.ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }  // Combine FirstName and LastName into FullName
        public List<string> BookTitles { get; set; }  // List of book titles the author has written
    }
}