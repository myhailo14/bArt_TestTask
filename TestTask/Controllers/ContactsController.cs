using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTask.DAL;
using TestTask.Models;
using TestTask.Models.DTOs;

namespace TestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly TestAppDbContext _context;

        public ContactsController(TestAppDbContext context)
        {
            _context = context;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            if (_context.Contacts == null)
            {
                return NotFound();
            }
            return await _context.Contacts.ToListAsync();
        }

        // GET: api/Contacts/5
        [HttpGet("{email}")]
        public async Task<ActionResult<Contact>> GetContact(string email)
        {
            if (_context.Contacts == null)
            {
                return NotFound();
            }
            var contact = await _context.Contacts.FindAsync(email);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // POST: api/Contacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactDTO>> PostContact(ContactDTO contactDto)
        {
            if (_context.Contacts == null)
            {
                return Problem("Entity set 'TestAppDbContext.Contacts'  is null.");
            }
            var contact = new Contact();

            contact.Email = contactDto.Email;
            contact.FirstName = contactDto.FirstName;
            contact.LastName = contactDto.LastName;

            _context.Contacts.Add(contact);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContactExists(contact.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetContact", new { email = contact.Email }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteContact(string email)
        {
            if (_context.Contacts == null)
            {
                return NotFound();
            }
            var contact = await _context.Contacts.FindAsync(email);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(string email)
        {
            return (_context.Contacts?.Any(e => e.Email == email)).GetValueOrDefault();
        }
    }
}
