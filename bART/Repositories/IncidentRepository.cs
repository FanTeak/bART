using bART.Models;
using Microsoft.EntityFrameworkCore;

namespace bART.LogicControllers
{
    public class IncidentRepository
    {
        private readonly bARTDbContext _context;

        public IncidentRepository(bARTDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Incident>> GetIncidentsAsync()
        {
            return await _context.Incidents.Include(i => i.Accounts)
                .ThenInclude(a => a.Contacts).ToListAsync();
        }

        public async Task<Incident?> GetIncidentAsync(string name)
        {
            return await _context.Incidents.Include(i => i.Accounts)
                .ThenInclude(i => i.Contacts).FirstOrDefaultAsync(i => i.Name.Equals(name));
        }

        public async Task<bool> PutIncident(string id, Incident incident)
        {
            return await _context.Incidents.AnyAsync();
;        }

        public async Task<bool> PostIncident(Incident incident)
        {
            return await _context.Incidents.AnyAsync();
        }

        public async Task<bool> DeleteIncident(string id)
        {
            return await _context.Incidents.AnyAsync();
        }
    }
}
