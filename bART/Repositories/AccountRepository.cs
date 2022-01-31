using bART.Models;
using Microsoft.EntityFrameworkCore;

namespace bART.LogicControllers
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
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account?> GetAccountAsync(string name)
        {
            return await _context.Accounts.FirstOrDefaultAsync();
        }

        public async Task<bool> PutAccount(int id, Account account)
        {
            return await _context.Accounts.AnyAsync();
        }

        public async Task<bool> PostAccount(Account account)
        {
            return await _context.Accounts.AnyAsync();
        }

        public async Task<bool> DeleteAccount(int id)
        {
            return await _context.Accounts.AnyAsync();
        }
    }
}
