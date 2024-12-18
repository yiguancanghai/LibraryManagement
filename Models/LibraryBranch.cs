namespace LibraryManagement.Models
{
    public class LibraryBranch
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        // Collection of books available at this branch
        public ICollection<Book> Books { get; set; }
    }
}
