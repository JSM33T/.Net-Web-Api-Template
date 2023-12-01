using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace almondCoveApi.Models.Domain
{
    public class User
    {
        public int Id{ get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(14)]
        public string? Phone { get; set; }

        [MaxLength(150)]
        public string? Links { get; set; }

        [MaxLength(150)]
        public string? Bio { get; set; }

        public DateTime BDate { get; set; }

        public DateTime DateAdded { get; set; }

        public bool IsActive { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        [MaxLength(200)]
        public string SessionKey{ get; set; }
    }
}
