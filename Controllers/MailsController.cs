﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using almondCoveApi.Data;
using almondCoveApi.Models.Domain;

namespace almondCoveApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailsController : ControllerBase
    {
        private readonly AlmondDbContext _context;

        public MailsController(AlmondDbContext context)
        {
            _context = context;
        }

        // GET: api/Mails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mail>>> GetMailingList()
        {
          if (_context.MailingList == null)
          {
              return NotFound();
          }
            return await _context.MailingList.ToListAsync();
        }

        // GET: api/Mails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mail>> GetMail(Guid id)
        {
          if (_context.MailingList == null)
          {
              return NotFound();
          }
            var mail = await _context.MailingList.FindAsync(id);

            if (mail == null)
            {
                return NotFound();
            }

            return mail;
        }

        // PUT: api/Mails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMail(Guid id, Mail mail)
        {
            if (id != mail.Id)
            {
                return BadRequest();
            }

            _context.Entry(mail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Mails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mail>> PostMail(Mail mail)
        {
          if (_context.MailingList == null)
          {
              return Problem("Entity set 'AlmondDbContext.MailingList'  is null.");
          }
            _context.MailingList.Add(mail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMail", new { id = mail.Id }, mail);
        }

        // DELETE: api/Mails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMail(Guid id)
        {
            if (_context.MailingList == null)
            {
                return NotFound();
            }
            var mail = await _context.MailingList.FindAsync(id);
            if (mail == null)
            {
                return NotFound();
            }

            _context.MailingList.Remove(mail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MailExists(Guid id)
        {
            return (_context.MailingList?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}