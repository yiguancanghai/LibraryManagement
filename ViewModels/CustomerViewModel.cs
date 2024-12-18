namespace LibraryManagement.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MembershipDate { get; set; }  // Display the formatted date as a string
    }
}