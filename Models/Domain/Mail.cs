namespace almondCoveApi.Models.Domain
{
    public class Mail
    {
        public Guid Id { get; set; }
        public string MailId { get; set; }
        public string Origin { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
