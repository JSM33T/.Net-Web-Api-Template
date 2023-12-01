using almondCoveApi.Data;
using almondCoveApi.Models.Domain;
using almondCoveApi.Models.DTO;
using almondCoveApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace almondCoveApi.Repositories.Implementations
{
    public class MailRepository : IMailRepository
    {
        public readonly AlmondDbContext _context;
        public MailRepository(AlmondDbContext almondDbContext)
        {
            _context = almondDbContext;
        }


        [HttpPost("/api/mail/add")]
        [IgnoreAntiforgeryToken]
        public async Task<Mail> CreateAsync(Mail mail)
        {
            await _context.MailingList.AddAsync(mail);
            await _context.SaveChangesAsync();
            return mail;
        }

        public async Task<bool> ExistsAsync(string mailId)
        {
            return await _context.MailingList.AnyAsync(m => m.MailId == mailId);
        }

        public async Task<Mail> GetMailAsync(Guid id)
        {
           return await _context.MailingList.FindAsync(id);
        }

        public async Task<IEnumerable<Mail>> GetMailingListAsync()
        {
            return await _context.MailingList.ToListAsync();
        }
    }
}
