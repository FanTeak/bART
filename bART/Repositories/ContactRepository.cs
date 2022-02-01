using bART.Models;
using Microsoft.EntityFrameworkCore;

namespace bART.Repositories
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
            return await _context.Contacts.Include(c => c.Account).SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int> PutContactAsync(int id, Contact contact)
        {
            _context.Entry(contact).State = EntityState.Modified;
            await CheckAccount(contact);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> PostContactAsync(Contact contact)
        {
            await CheckAccount(contact);
            _context.Contacts.Add(contact);
            return await _context.SaveChangesAsync();
        }

        private async Task CheckAccount(Contact contact)
        {
            if(contact.Account != null)
            {
                var acc = await _context.Accounts.SingleOrDefaultAsync(a => a.Name.Equals(contact.Account.Name));
                if (acc != null)
                {
                    var contacts = new List<Contact>();
                    foreach(var cont in acc.Contacts)
                    {
                        contacts.Add(cont);
                    }
                    contacts.Add(contact);
                    _context.Entry(acc).State = EntityState.Modified;
                }
                else
                {
                    contact.Account.Contacts = new List<Contact>() { contact };
                    await _context.Accounts.AddAsync(contact.Account);
                }
            }
        }

        public async Task<int> DeleteContactAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                throw new NullReferenceException();
            }

            if (CanDeleteContact(contact.Account))
            {
                _context.Contacts.Remove(contact);
            }
            return await _context.SaveChangesAsync();
        }

        private bool CanDeleteContact(Account account)
        {
            return account.Contacts.Count() > 1 ? true : false;
        }

        public bool ContactExists(string email)
        {
            return _context.Contacts.Any(e => e.Email == email);
        }
    }
}
