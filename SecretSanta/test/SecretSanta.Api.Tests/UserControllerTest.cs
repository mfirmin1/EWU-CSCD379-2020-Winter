using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System.Threading.Tasks;
using SecretSanta.Data.Tests;
using Microsoft.AspNetCore.Mvc;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void Create_UserController_Success()
        {
            //Arrange
            //Act
            //Assert
            var service = new UserTestService();

            // Act
            _ = new UserController(service);

        }
        //Arrange
        //Act
        //Assert
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Null()
        {
            _ = new UserController(null!);
        }

        [TestMethod]
        public async Task GetById_ExistingUser()
        {
            var service = new UserTestService();
            User user = SampleData.CreateBilboBaggins();
            user = await service.InsertAsync(user);

            var controller = new UserController(service);

            ActionResult<User> ar = await controller.Get(user.Id);

            Assert.IsTrue(ar.Result is OkObjectResult);
        }


       
        private class UserTestService : IUserService
        {
            private Dictionary<int, User> Items { get; } = new Dictionary<int, User>();

            public Task<bool> DeleteAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<List<User>> FetchAllAsync()
            {
                throw new NotImplementedException();
            }

            public Task<User?> FetchByIdAsync(int id)
            {
                if (Items.TryGetValue(id, out User? user))
                {
                    Task<User?> t1 = Task.FromResult<User?>(user);
                    return t1;
                }
                Task<User?> t2 = Task.FromResult<User?>(null);
                return t2;
            }

            public Task<User> InsertAsync(User entity)
            {
                int id = Items.Count + 1;
                Items[id] = new TestUser(entity, id);
                return Task.FromResult(Items[id]);
            }

            public Task<User[]> InsertAsync(params User[] entity)
            {
                throw new NotImplementedException();
            }

            public Task<User?> UpdateAsync(int id, User entity)
            {
                throw new NotImplementedException();
            }

            private class TestUser : User
            {
                public TestUser(User user, int id)
               : base((user ?? throw new ArgumentNullException(nameof(user))).FirstName,
                     user.LastName)
                {
                    Id = id;
                }
            }

        }

        
    }
}
