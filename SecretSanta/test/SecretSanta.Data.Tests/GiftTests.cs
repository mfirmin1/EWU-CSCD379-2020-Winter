using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class GiftTests : TestBase
    {
        [TestMethod]
        public async Task Gift_CreatedForeignRelation_Sucess()
        {
            //Arrange
            Gift gift = new Gift
            {
                Title = "This is a book",
                Description = "This is a magical fantasy book",
                Url = "https://www.ThisIsAMagicalUrl.com"
            };

            User user = new User
            {
                FirstName = "Doctor",
                LastName = "Who",
                Santa = null,
                Gifts = new List<Gift>(),
                UserGroupRelations = new List<UserGroupRelation>()
            };

            //Act
            gift.User = user;
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Add(gift);
                await dbContext.SaveChangesAsync();
            }

            //Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                Gift gifts = await dbContext.Gift.Include(g => g.User).SingleOrDefaultAsync();
                Assert.AreEqual(1, gift.Id);
                Assert.AreEqual(gift.Title, gifts.Title);
                Assert.AreNotEqual(0, gifts.Id);
                Assert.AreEqual("Doctor", gifts.User.FirstName);
                Assert.AreEqual("Who", gifts.User.LastName);
                Assert.AreEqual("This is a magical fantasy book", gifts.Description);
            }
        }
    }
}
