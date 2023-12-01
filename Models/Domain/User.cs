namespace almondCoveApi.Models.Domain
{
    public class User
    {
        public int Id{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Links { get; set; }
        public string Bio { get; set; }
        public string BDate { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public string SessionKey{ get; set; }
    }
}
