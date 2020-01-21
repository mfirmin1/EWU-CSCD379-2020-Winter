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
            var gift = new Gift
            {
                Title = "This is a book",
                Description = "This is a magical fantasy book",
                Url = "https://www.ThisIsAMagicalUrl.com"
            };

            var user = new User
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
                var gifts = await dbContext.Gift.Include(g => g.User).ToListAsync();
                Assert.AreEqual(1, gifts.Count);
                Assert.AreEqual(gift.Title, gifts[0].Title);
                Assert.AreNotEqual(0, gifts[0].Id);
            }
        }
    }
}
