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
using System.Linq;

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
        [TestMethod]
        public async Task DeleteGroup_Sucess()
        {
            var service = new GroupTestService();
            Group group = SampleData.CreateRohan;
            group = await service.InsertAsync(group);

            var controller = new GroupController(service);

            ActionResult<bool> ar = await controller.Delete(group.Id);
            Assert.IsTrue(ar.Result is OkObjectResult);
        }

        [TestMethod]
        public async Task Update_GroupSucess()
        {
            var service = new GroupTestService();
            Group group = SampleData.CreateGondor;
            group = await service.InsertAsync(group);

            group.Title = "Modor";
            var controller = new GroupController(service);

            ActionResult<Group> ar = await controller.Put(group.Id, group);
            Assert.AreEqual("Modor", ar.Value.Title);
        }

        private class GroupTestService : IGroupService
        {
            private Dictionary<int, Group> Items { get; } = new Dictionary<int, Group>();

            public Task<bool> DeleteAsync(int id)
            {
                return Task.FromResult(Items.Remove(id));
            }

            public Task<List<Group>> FetchAllAsync()
            {
                List<Group> groupList = Items.Values.ToList();
                return Task.FromResult(groupList);
            }
#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
            public Task<Group?> FetchByIdAsync(int id)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
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
                foreach (Group group in entity)
                {
                    InsertAsync(group);
                }
                return Task.FromResult(entity);
            }

            public Task<Group?> UpdateAsync(int id, Group entity)
            {
                Items[id] = entity;
                return Task.FromResult<Group?>(Items[id]);
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
