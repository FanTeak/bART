using bART.Models;
using Microsoft.EntityFrameworkCore;

namespace bART.LogicControllers
{
    public class ContactLogic
    {
        private readonly bARTDbContext _context;

        public ContactLogic(bARTDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact?> GetContactAsync(string name)
        {
            return await _context.Contacts.FirstOrDefaultAsync();
        }

        public async Task<bool> PutContact(int id, Contact contact)
        {
            return await _context.Contacts.AnyAsync();
        }

        public async Task<bool> PostContact(Contact contact)
        {
            return await _context.Contacts.AnyAsync();
        }

        public async Task<bool> DeleteContact(int id)
        {
            return await _context.Contacts.AnyAsync();
        }
    }
}
