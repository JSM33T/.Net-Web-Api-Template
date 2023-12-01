using System.ComponentModel.DataAnnotations;

namespace almondCoveApi.Models.Domain
{
    public class Mail
    {
        public Guid Id { get; set; }

        [MaxLength(200)]
        [MinLength(1)]
        [Required]
        public string MailId { get; set; }
        [MaxLength(200)]
        public string Origin { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
