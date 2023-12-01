using almondCoveApi.Models.Domain;

namespace almondCoveApi.Repositories.Interfaces
{
    public interface IMailRepository
    {
        Task<Mail> CreateAsync(Mail mail);
        Task<IEnumerable<Mail>> GetMailingListAsync();
        Task<Mail> GetMailAsync(Guid id);
        Task<bool> ExistsAsync(string mailId);

    }
}
