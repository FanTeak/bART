using bART.Models;
using Microsoft.EntityFrameworkCore;

namespace bART.Repositories
{
    public class AccountRepository
    {
        private readonly bARTDbContext _context;

        public AccountRepository(bARTDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            return await _context.Accounts.Include(a => a.Incident).Include(a => a.Contacts).ToListAsync();
        }

        public async Task<Account?> GetAccountAsync(int id)
        {
            return await _context.Accounts.Include(a => a.Incident).Include(a => a.Contacts).SingleOrDefaultAsync(a=>a.Id == id);
        }

        public async Task<int> PutAccountAsync(int id, Account account)
        {
            _context.Entry(account).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> PostAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAccountAsync(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                throw new NullReferenceException();
            }

            if (CanDeleteAccount(account.Incident))
            {
                _context.Accounts.Remove(account);
            }
            return await _context.SaveChangesAsync();
        }

        private bool CanDeleteAccount(Incident incident)
        {
            return incident.Accounts.Count() > 1 ? true : false;
        }

        public bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }

        public bool ContactsNotExists(Account account) => account.Contacts.Count() < 1;
    }
}
