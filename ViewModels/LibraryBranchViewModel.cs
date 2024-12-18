namespace LibraryManagement.ViewModels
{
    public class LibraryBranchViewModel
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public List<string> BookTitles { get; set; }  // List of book titles available at the branch
    }
}