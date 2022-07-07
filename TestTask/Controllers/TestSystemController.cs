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
    public class TestSystemController : ControllerBase
    {
        private readonly TestAppDbContext _context;
        public TestSystemController(TestAppDbContext dbContext)
        {
            _context = dbContext;
        }

        // request as in example
        // {  
        //     string accountName;
        //     string contactFirstName;
        //     string contactLastName;
        //     string contactEmail;
        //     string incidentDescription; 
        // }

        [HttpPost]
        public async Task<ActionResult<RequestDTO>> PostRecord([FromBody] RequestDTO requestData)
        {
            var account = await _context.Accounts.FindAsync(requestData.AccountName);

            if (account == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FindAsync(requestData.ContactEmail);

            if (contact != null)
            {
                contact.FirstName = requestData.ContactFirstName;
                contact.LastName = requestData.ContactLastName;
               
                if(contact.Account == null)
                {
                    account.Contacts.Add(contact);                
                }
            }
            else
            {
                contact = new Contact();
                contact.Email = requestData.ContactEmail;
                contact.FirstName = requestData.ContactFirstName;
                contact.LastName = requestData.ContactLastName;
                contact.Account = account;
                _context.Contacts.Add(contact);
            }

            var incident = new Incident();

            incident.Description = requestData.IncidentDescription;

            incident.Accounts.Add(account);
            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
