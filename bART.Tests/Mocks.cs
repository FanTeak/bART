using bART.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART.Tests
{
    internal static class Mocks
    {
        public static IEnumerable<Incident> GetMock()
        {
            return Merge();
        }
        private static IEnumerable<Incident> Merge()
        {
            var incidents = GetIncidents();
            Incident incident = incidents.First();
            var accounts = GetAccounts();
            var contacts = GetContacts();

            incident.Accounts = accounts;
            int counter = 0, contactsPerAccount = contacts.Count() / accounts.Count();
            foreach (var account in incident.Accounts)
            {
                account.Incident = incident;
                account.Contacts = contacts.Skip(counter).Take(contactsPerAccount).ToList();
                counter += counter;

                foreach (var contact in account.Contacts)
                {
                    contact.Account = account;
                }
            }

            return new List<Incident>() { incident };
        }
        private static IEnumerable<Contact> GetContacts()
        {
            return new List<Contact>()
            {
                new Contact() { Id = 1, FirstName = "Bohdan", LastName = "Dutchak", Email = "Qwerty@gmail.com"},
                new Contact() { Id = 2, FirstName ="Tim", LastName = "Timoslav", Email= "Tim@gmail.com"},
                new Contact() { Id = 3, FirstName ="Okeh", LastName = "Olehoslav", Email= "Oleh@gmail.com"},
                new Contact() { Id = 4, FirstName ="Yuri", LastName = "Yurislav", Email= "Yuri@gmail.com"}
            };
        }

        private static IEnumerable<Account> GetAccounts()
        {
            return new List<Account>()
            {
                new Account() { Id = 1, Name = "Lviv Company"},
                new Account() { Id = 2, Name = "Kyiv Company"}
            };
        }

        private static IEnumerable<Incident> GetIncidents()
        {
            return new List<Incident>()
            {
                new Incident() { Name = "New Year", Description = "1 January in the Lviv"}
            };
        }
    }
}
