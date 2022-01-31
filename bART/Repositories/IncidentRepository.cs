using bART.Models;
using Microsoft.EntityFrameworkCore;

namespace bART.Repositories
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
                .ThenInclude(i => i.Contacts).SingleOrDefaultAsync(i => i.Name.Equals(name));
        }

        public async Task<int> PutIncidentAsync(string id, Incident incident)
        {
            _context.Entry(incident).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> PostIncidentAsync(Incident incident)
        {
            _context.Incidents.Add(incident);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteIncidentAsync(string id)
        {
            var incident = await _context.Incidents.FindAsync(id);
            if (incident == null)
            {
                throw new NullReferenceException();
            }

            _context.Incidents.Remove(incident);
            return await _context.SaveChangesAsync();
        }

        public bool IncidentExists(string id)
        {
            return _context.Incidents.Any(e => e.Name == id);
        }
    }
}
