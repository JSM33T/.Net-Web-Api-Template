using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using almondCoveApi.Data;
using almondCoveApi.Models.Domain;
using almondCoveApi.Models.DTO;
using almondCoveApi.Repositories.Interfaces;

namespace almondCoveApi.Controllers
{
    [ApiController]
    public class MailsController : ControllerBase
    {
        public readonly IMailRepository _mailRepository;
        private readonly ILogger<MailsController> _logger;
        public MailsController(ILogger<MailsController> logger,IMailRepository mailRepository)
        {
            _logger = logger;
            _mailRepository = mailRepository;
        }

        [HttpPost("/api/mail/add")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddMailToList([FromBody] MailDTO mailDTO)
        {
            var mail = new Mail()
            {
                MailId = mailDTO.Email,
                Origin = mailDTO.Origin,
                DateAdded = DateTime.Now
            };

            if (await _mailRepository.ExistsAsync(mailDTO.Email))
            {
                _logger.LogInformation("mail- {mail} already on list", mailDTO.Email);
                return Conflict("Email already exists");
            }
            try
            {
                await _mailRepository.CreateAsync(mail);
                _logger.LogError("mail- {mail} addded to list on {datetime}", mailDTO.Email, DateTime.Now);
                return Ok(mail);
            }
            catch(Exception ex) {
                _logger.LogError("exception in submitting a new mail: {mail}, message: {message}", mailDTO.Email, ex.Message);
                return BadRequest("something went wrong");
            }
        }

        [HttpGet("/api/mail/list")]
        public async Task<ActionResult<IEnumerable<Mail>>> GetMailingList()
        {
            var mailingList = await _mailRepository.GetMailingListAsync();

            if (mailingList == null || !mailingList.Any())
            {
                return NotFound();
            }
            return Ok(mailingList);
        }

        // GET: api/Mails/5
        [HttpGet("/api/mail/get/{id}")]
        public async Task<ActionResult<Mail>> GetMail(Guid id)
        {
            var mail = await _mailRepository.GetMailAsync(id);

            if (mail == null)
            {
                return NotFound();
            }

            return Ok(mail);
        }


        //// PUT: api/Mails/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMail(Guid id, Mail mail)
        //{
        //    if (id != mail.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(mail).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MailExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}


        // DELETE: api/Mails/5
        //[HttpPost("/api/mail/delete/{id}")]
        //public async Task<IActionResult> DeleteMail(Guid id)
        //{
        //    if (_context.MailingList == null)
        //    {
        //        return NotFound("invalid id provided");
        //    }
        //    var mail = await _context.MailingList.FindAsync(id);
        //    if (mail == null)
        //    {
        //        return NotFound("invalid id provided");
        //    }

        //    _context.MailingList.Remove(mail);
        //    await _context.SaveChangesAsync();

        //    return Ok("email deleted successfully");
        //}

        //private bool MailExists(string Email)
        //{
        //    return (_context.MailingList?.Any(e => e.MailId == Email)).GetValueOrDefault();
        //}
    }
}
