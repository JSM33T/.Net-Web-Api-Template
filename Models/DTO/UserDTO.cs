using System.ComponentModel.DataAnnotations;

namespace almondCoveApi.Models.DTO
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Links { get; set; }
        public string Bio { get; set; }
        public string Password{ get; set; }
    }
}
