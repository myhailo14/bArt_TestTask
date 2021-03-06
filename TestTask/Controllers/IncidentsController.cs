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
    public class IncidentsController : ControllerBase
    {
        private readonly TestAppDbContext _context;

        public IncidentsController(TestAppDbContext context)
        {
            _context = context;
        }

        // GET: api/Incidents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncidentDTO>>> GetIncidents()
        {
            if (_context.Incidents == null)
            {
                return NotFound();
            }

            var incidents = await _context.Incidents.ToListAsync();
            var result = new List<IncidentDTO>();
            foreach(var i in incidents)
            {
                result.Add(new IncidentDTO() { Description = i.Description, AccountsNames = i.Accounts.Select(a => a.Name).ToList() });
            }

            return result;
        }

        // GET: api/Incidents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IncidentDTO>> GetIncident(string id)
        {
            if (_context.Incidents == null)
            {
                return NotFound();
            }
            var incident = await _context.Incidents.FindAsync(id);

            if (incident == null)
            {
                return NotFound();
            }

            return new IncidentDTO() { Description = incident.Description, AccountsNames = incident.Accounts.Select(a => a.Name).ToList() };
        }

        // POST: api/Incidents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IncidentCreationRequest>> PostIncident(IncidentCreationRequest incidentDto)
        {
            if (_context.Incidents == null)
            {
                return Problem("Entity set 'TestAppDbContext.Incidents'  is null.");
            }

            var account = await _context.Accounts.FindAsync(incidentDto.AccountName);

            if(account == null)
            {
                return NotFound("This account does not exist");
            }

            var incident = new Incident();
            incident.Description = incidentDto.Description;
            incident.Accounts.Add(account);

            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncident", new { id = incident.Name }, incident);
        }

        // DELETE: api/Incidents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncident(string id)
        {
            if (_context.Incidents == null)
            {
                return NotFound();
            }
            var incident = await _context.Incidents.FindAsync(id);
            if (incident == null)
            {
                return NotFound();
            }

            _context.Incidents.Remove(incident);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IncidentExists(string id)
        {
            return (_context.Incidents?.Any(e => e.Name == id)).GetValueOrDefault();
        }
    }
}
