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
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Data.Tests;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class GroupControllerTest
    {
        [TestMethod]
        public void Create_GroupController_Success()
        {
            //Arrange
            //Act
            //Assert
            var service = new GroupTestService();

            _ = new GroupController(service);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_NullException()
        {
            _ = new GroupController(null!);
        }
        [TestMethod]
        public async Task GetById_ExistingUser()
        {
            var service = new GroupTestService();
            Group group = SampleData.CreateGondor;
            group = await service.InsertAsync(group);

            var controller = new GroupController(service);

            ActionResult<Group> ar = await controller.Get(group.Id);

            Assert.IsTrue(ar.Result is OkObjectResult);
        }
        private class GroupTestService : IGroupService
        {
            private Dictionary<int, Group> Items { get; } = new Dictionary<int, Group>();

            public Task<bool> DeleteAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<List<Group>> FetchAllAsync()
            {
                throw new NotImplementedException();
            }

            public Task<Group?> FetchByIdAsync(int id)
            {
                if (Items.TryGetValue(id, out Group? group))
                {
                    Task<Group?> t1 = Task.FromResult<Group?>(group);
                    return t1;
                }
                Task<Group?> t2 = Task.FromResult<Group?>(null);
                return t2;
            }

            public Task<Group> InsertAsync(Group entity)
            {
                int id = Items.Count + 1;
                Items[id] = new TestGroup(entity, id);
                return Task.FromResult(Items[id]);
            }

            public Task<Group[]> InsertAsync(params Group[] entity)
            {
                throw new NotImplementedException();
            }

            public Task<Group?> UpdateAsync(int id, Group entity)
            {
                throw new NotImplementedException();
            }

            private class TestGroup : Group
            {
                public TestGroup(Group group, int id)
               : base((group ?? throw new ArgumentNullException(nameof(group))).Title)
                {
                    Id = id;
                }
            }

        }

    }
}
