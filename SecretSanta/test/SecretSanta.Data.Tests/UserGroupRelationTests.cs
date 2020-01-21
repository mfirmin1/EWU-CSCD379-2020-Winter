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
    public class UserGroupRelationTests : TestBase
    {
        [TestMethod]
        public async Task UserGroupRelations_CreateMultipleGroups_Sucess()
        {
            //Arrange
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta => hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));
            var gift = new Gift
            {
                Title = "A Gfft",
                Description = "A Description",
                Url = "www.ThisIsAUrl.com"
            };
            var user = new User
            {
                FirstName = "John",
                LastName = "Smith"
            };

            var groupOne = new Group
            {
                Name = "Group One"
            };

            var groupTwo = new Group
            {
                Name = "Group Two"
            };

            //Act
            gift.User = user;
            user.UserGroupRelations = new List<UserGroupRelation>();
            user.UserGroupRelations.Add(new UserGroupRelation { User = user, Group = groupOne});
            user.UserGroupRelations.Add(new UserGroupRelation { User = user, Group = groupTwo });

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Gift.Add(gift);
                await dbContext.SaveChangesAsync();
            }
            //Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var retrievedGift = await dbContext.Gift.Where(g => g.Id == gift.Id).Include(g => g.User).ThenInclude(g => g.UserGroupRelations).ThenInclude(g => g.Group).SingleOrDefaultAsync();

                Assert.IsNotNull(retrievedGift);
                Assert.AreEqual(2, retrievedGift.User.UserGroupRelations.Count);
                Assert.IsNotNull(retrievedGift.User.UserGroupRelations[0]);
                Assert.IsNotNull(retrievedGift.User.UserGroupRelations[1]);
            }
        }
    }
}
