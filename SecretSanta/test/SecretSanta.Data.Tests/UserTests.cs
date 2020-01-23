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
    public class UserTests : TestBase
    {
        [TestMethod]
        public async Task CreateUser_SaveToDatabase_Sucess()
        {
            int userId = -1;
            //Assert
            using (ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options))
            {
                User user = new User
                {
                    FirstName = "Robo",
                    LastName = "Cop",
                    Santa = null,
                    Gifts = new List<Gift>(),
                    UserGroupRelations = new List<UserGroupRelation>()
                };
                applicationDbContext.User.Add(user);

                User user2 = new User
                {
                    FirstName = "Iron",
                    LastName = "Maiden",
                    Santa = null,
                    Gifts = new List<Gift>(),
                    UserGroupRelations = new List<UserGroupRelation>()
                };
                applicationDbContext.User.Add(user2);
                await applicationDbContext.SaveChangesAsync();
                userId = user.Id;

            }
            //Act
            //Assert
            using (ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options))
            {
                User user = await applicationDbContext.User.Where(u => u.Id == userId).SingleOrDefaultAsync();

                Assert.IsNotNull(user);
                Assert.AreEqual("Robo", user.FirstName);
            }
        }
        [TestMethod]
        public async Task CreateUser_CreatedByAndModifed_Sucess()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta => hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "iromad"));
            int userId = -1;
            //Arrange
            using (ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                User user = new User
                {
                    FirstName = "Robo",
                    LastName = "Cop",
                    Santa = null,
                    Gifts = new List<Gift>(),
                    UserGroupRelations = new List<UserGroupRelation>()
                };
                applicationDbContext.User.Add(user);

                User user2 = new User
                {
                    FirstName = "Iron",
                    LastName = "Maiden",
                    Santa = null,
                    Gifts = new List<Gift>(),
                    UserGroupRelations = new List<UserGroupRelation>()
                };
                applicationDbContext.User.Add(user2);
                await applicationDbContext.SaveChangesAsync();
                userId = user.Id;
            }
            using (ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                User user = await applicationDbContext.User.Where(u => u.Id == userId).SingleOrDefaultAsync();

                Assert.IsNotNull(user);
                Assert.AreEqual("iromad", user.CreatedBy);
                Assert.AreEqual("iromad", user.ModifiedBy);
            }
        }
        [TestMethod]
        public async Task CreateAuthor_ModifedBbyAnother_Sucess()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta => hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "iromad"));
            int userId = -1;
            using (ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                User user = new User
                {
                    FirstName = "Robo",
                    LastName = "Cop",
                    Santa = null,
                    Gifts = new List<Gift>(),
                    UserGroupRelations = new List<UserGroupRelation>()
                };
                applicationDbContext.User.Add(user);

                User user2 = new User
                {
                    FirstName = "Iron",
                    LastName = "Maiden",
                    Santa = null,
                    Gifts = new List<Gift>(),
                    UserGroupRelations = new List<UserGroupRelation>()
                };
                applicationDbContext.User.Add(user2);
                await applicationDbContext.SaveChangesAsync();
                userId = user.Id;
            }
            //Act
            httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta => hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "robocop"));
            using (ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                User user = await applicationDbContext.User.Where(u => u.Id == userId).SingleOrDefaultAsync();
                user.FirstName = "Avatar";
                user.LastName = "Aang";

                await applicationDbContext.SaveChangesAsync();
            }
            //Arrange
            using (ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                User user = await applicationDbContext.User.Where(u => u.Id == userId).SingleOrDefaultAsync();

                Assert.IsNotNull(user);
                Assert.AreEqual("iromad", user.CreatedBy);
                Assert.AreEqual("robocop", user.ModifiedBy);
            }
        }
    }
}
