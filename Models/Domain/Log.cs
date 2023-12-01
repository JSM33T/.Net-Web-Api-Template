namespace almondCoveApi.Models.Domain
{
    public class Log
    {
        public Guid Id{ get; set; }
        public string Message { get; set; }
        public string Remark { get; set; }
        public string Origin { get; set; }
        public DateTime DateAdded{ get; set; }
    }
}
