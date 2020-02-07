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
using System.Linq;

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

        [TestMethod]
        public async Task DeleteUser_Sucess()
        {
            var service = new UserTestService();    
            User user = SampleData.CreateBilboBaggins();
            user = await service.InsertAsync(user);

            var controller = new UserController(service);

            ActionResult<bool> ar = await controller.Delete(user.Id);
            Assert.IsTrue(ar.Result is OkObjectResult);
        }

        [TestMethod]
        public async Task Update_UserSucess()
        {
            var service = new UserTestService();
            User user = SampleData.CreateInigyoMontoya();
            user = await service.InsertAsync(user);
            user.FirstName = "She";
            user.LastName = "lob";
            var controller = new UserController(service);

            ActionResult<User> ar = await controller.Put(user.Id, user);
            Assert.AreEqual("She", ar.Value.FirstName);
            Assert.AreEqual("lob", ar.Value.LastName);
        }

        [TestMethod]
        public async Task Get_ExistingUser_NoId()
        {
            var service = new UserTestService();
            User user = SampleData.CreateBilboBaggins();
            user = await service.InsertAsync(user);

            var controller = new UserController(service);

            ActionResult<User> ar = await controller.Get(0);
            Assert.IsTrue(ar.Result is NotFoundResult);
        }
        /*[TestMethod]
        public async Task ModififedBy_Sucess()
        {
            var service = new UserTestService();
            User user = SampleData.CreateBilboBaggins();
            User user2 = SampleData.CreateInigyoMontoya();
            var controller = new UserController(service);

            User[] array = await service.InsertAsync(user, user2);
            ActionResult<User> ar = await controller.Put(array[0].Id, array[0]);
            ActionResult<User> arl = await controller.Put(array[1].Id, array[1]);

            Assert.IsTrue(SampleData.BilboBaggin, ar.Value.CreatedBy);
        }*/
       
        private class UserTestService : IUserService
        {
            private Dictionary<int, User> Items { get; } = new Dictionary<int, User>();

            public Task<bool> DeleteAsync(int id)
            {
                return Task.FromResult(Items.Remove(id));
            }

            public Task<List<User>> FetchAllAsync()
            {
                List<User> userList = Items.Values.ToList();
                return Task.FromResult(userList);
            }

#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
            public Task<User?> FetchByIdAsync(int id)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
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
                foreach (User user in entity)
                {
                    InsertAsync(user);
                }
                return Task.FromResult(entity); 
            }

            public Task<User?> UpdateAsync(int id, User entity)
            {
                Items[id] = entity;
                return Task.FromResult<User?>(Items[id]);
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
