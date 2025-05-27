using almondCoveApi.Models.Domain;
using almondCoveApi.Models.DTO;
using almondCoveApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace almondCoveApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [IgnoreAntiforgeryToken]
    public class MailsController : ControllerBase
    {
        private readonly IMailRepository _mailRepository;
        private readonly ILogger<MailsController> _logger;
        public MailsController(ILogger<MailsController> logger,IMailRepository mailRepository)
        {
            _logger = logger;
            _mailRepository = mailRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddMailToList([FromBody] MailDTO mailDTO)
        {
            var mail = new Mail()
            {
                // Note: MailId in the Mail domain model is being populated from the Email property of the MailDTO.
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
                _logger.LogInformation("Mail {mail} added to list on {datetime}", mailDTO.Email, DateTime.Now);
                return Ok(mail);
            }
            catch(Exception ex) {
                _logger.LogError("exception in submitting a new mail: {mail}, message: {message}", mailDTO.Email, ex.Message);
                return BadRequest("something went wrong");
            }
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<Mail>>> GetMailingList()
        {
            var mailingList = await _mailRepository.GetMailingListAsync();
            return Ok(mailingList ?? Enumerable.Empty<Mail>());
        }

        // GET: api/Mails/5
        [HttpGet("get/{id}")]
        public async Task<ActionResult<Mail>> GetMail(Guid id)
        {
            var mail = await _mailRepository.GetMailAsync(id);

            if (mail == null)
            {
                _logger.LogInformation("Mail with Id {Id} not found", id);
                return NotFound();
            }

            return Ok(mail);
        }


    }
}
