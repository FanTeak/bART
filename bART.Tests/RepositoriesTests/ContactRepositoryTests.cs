using bART.LogicControllers;
using bART.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace bART.Tests
{
    public class ContactRepositoryTests
    {
        [Fact]
        public void ContactLogicGetContactsAsyncTest()
        {
            var contacts = Mocks.GetContactsMock().AsQueryable();

            var mockSet = new Mock<DbSet<Contact>>();
            mockSet.As<IQueryable<Contact>>().Setup(c => c.Provider).Returns(contacts.Provider);
            mockSet.As<IQueryable<Contact>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockSet.As<IQueryable<Contact>>().Setup(c => c.ElementType).Returns(contacts.ElementType);
            mockSet.As<IQueryable<Contact>>().Setup(c => c.GetEnumerator()).Returns(contacts.GetEnumerator());

            var optionsBuilder = new DbContextOptionsBuilder<bARTDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=bART;Trusted_Connection=True;");
            
            var mock = new Mock<bARTDbContext>(optionsBuilder.Options);
            mock.Setup(context => context.Contacts).Returns(mockSet.Object);

            var contactLogic = new ContactRepository(mock.Object);
            var dataFromLogic = contactLogic.GetContactsAsync().Result;

            Assert.Equal(contacts.AsEnumerable(), dataFromLogic);
        }

        //[Fact]
        //public void ContactLogicGetContactAsyncTest()
        //{

        //}

        //[Fact]
        //public void ContactLogicPutContactAsyncTest()
        //{

        //}

        //[Fact]
        //public void ContactLogicPostContactAsyncTest()
        //{

        //}

        //[Fact]
        //public void ContactLogicDeleteContactAsyncTest()
        //{

        //}
    }
}
