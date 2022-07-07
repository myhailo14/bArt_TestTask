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
    public class AccountsController : ControllerBase
    {
        private readonly TestAppDbContext _context;
       
        public AccountsController(TestAppDbContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            return await _context.Accounts.ToListAsync();
        }

        // GET: api/Accounts/Test
        [HttpGet("{name}")]
        public async Task<ActionResult<Account>> GetAccount(string name)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(name);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountDTO>> PostAccount(AccountDTO accountDto)
        {

            if (_context.Accounts == null)
            {
                return Problem("Entity set 'TestAppDbContext.Accounts'  is null.");
            }

            var contact = await _context.Contacts.FindAsync(accountDto.ContactEmail);

            if(contact == null)
            {
                return BadRequest(new string("This contact does not exist"));
            }

            if (contact.Account != null)
            {
                return BadRequest(new string("This contact is already taken"));
            }

            var account = new Account();
            account.Name = accountDto.AccountName;
            account.Contacts.Add(contact);
            contact.Account = account;

            _context.Accounts.Add(account);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountExists(account.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccount", new { name = account.Name }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteAccount(string name)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(name);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(string name)
        {
            return (_context.Accounts?.Any(e => e.Name == name)).GetValueOrDefault();
        }
    }
}
