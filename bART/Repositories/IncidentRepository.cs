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
            var accounts = incident.Accounts;
            try
            {
                await CheckAccounts(incident);
                await CheckContacts(accounts);

                _context.Entry(incident).State = EntityState.Modified;
                incident.Accounts = accounts;
            }
            catch (Exception)
            {
                throw;
            }
            
            return await _context.SaveChangesAsync();
        }

        public async Task PostIncidentAsync(Incident incident)
        {
            var accounts = incident.Accounts;
            try
            {
                await CheckAccounts(incident);
                //await CheckContacts(accounts);

                _context.Incidents.Add(incident);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " " + ex.InnerException);
            }
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

        private async Task CheckAccounts(Incident incident)
        {
            var accounts = incident.Accounts;
            if (accounts.Count() > 0)
            {
                var tempList = new List<Account>();
                foreach (var account in accounts)
                {
                    var acc = await _context.Accounts.SingleOrDefaultAsync(a => a.Name.Equals(account.Name));
                    if (acc == null)
                        throw new Exception();
                    account.Incident = incident;
                    tempList.Add(acc);
                }
                incident.Accounts = tempList;
            }
            else
            {
                throw new Exception();
            }
        }

        private async Task CheckContacts(IEnumerable<Account> accounts)
        {
            foreach (var account in accounts)
            {
                if(account.Contacts.Count() < 0)
                    throw new Exception();
                foreach (var contact in account.Contacts)
                {
                    var cont = await _context.Contacts.SingleOrDefaultAsync(c => c.Email.Equals(contact.Email));
                    if (cont != null)
                    {
                        if (contact.Account == null)
                        {
                            cont.Account = account;
                            _context.Entry(cont).State = EntityState.Modified;
                        }
                        else
                        {
                            if (!contact.Account.Name.Equals(account.Name))
                            {
                                throw new Exception();
                            }
                        }
                    }
                    else
                    {
                        var contacts = new List<Contact>(account.Contacts);
                        contacts.Add(contact);
                        account.Contacts = contacts;
                        _context.Entry(account).State = EntityState.Modified;
                        //contact.Account = account;
                        //_context.Contacts.Add(contact);
                    }
                }
            }
        }

        public bool IncidentExists(string id)
        {
            return _context.Incidents.Any(e => e.Name == id);
        }
    }
}
