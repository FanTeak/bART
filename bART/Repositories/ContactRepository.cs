using bART.Models;
using Microsoft.EntityFrameworkCore;

namespace bART.LogicControllers
{
    public class ContactRepository
    {
        private readonly bARTDbContext _context;

        public ContactRepository(bARTDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            return await _context.Contacts.Include(c => c.Account).ToListAsync();
        }

        public async Task<Contact?> GetContactAsync(int id)
        {
            return await _context.Contacts.Include(c => c.Account).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> PutContactAsync(int id, Contact contact)
        {
            return await _context.Contacts.AnyAsync();
        }

        public async Task<bool> PostContactAsync(Contact contact)
        {
            return await _context.Contacts.AnyAsync();
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            return await _context.Contacts.AnyAsync();
        }
    }
}
