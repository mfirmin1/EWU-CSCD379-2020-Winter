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
    public class GiftControllerTest
    {
        [TestMethod]
        public void Create_GiftController_Success()
        {
            //Arrange
            //Act
            //Assert
            // Arrange
            var service = new GiftTestService();

            // Act
            _ = new GiftController(service);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_NUllException()
        {
            _ = new GiftController(null!);
        }

        [TestMethod]
        public async Task GetById_ExistingUser()
        {
            var service = new GiftTestService();
            Gift gift = SampleData.CreateHelloWorldScam();
            gift = await service.InsertAsync(gift);

            var controller = new GiftController(service);

            ActionResult<Gift> ar = await controller.Get(gift.Id);

            Assert.IsTrue(ar.Result is OkObjectResult);
        }

        private class GiftTestService : IGiftService
        {
            private Dictionary<int, Gift> Items { get; } = new Dictionary<int, Gift>();

            public Task<bool> DeleteAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<List<Gift>> FetchAllAsync()
            {
                throw new NotImplementedException();
            }

            public Task<Gift?> FetchByIdAsync(int id)
            {
                if (Items.TryGetValue(id, out Gift? gift))
                {
                    Task<Gift?> t1 = Task.FromResult<Gift?>(gift);
                    return t1;
                }
                Task<Gift?> t2 = Task.FromResult<Gift?>(null);
                return t2;
            }

            public Task<Gift> InsertAsync(Gift entity)
            {
                int id = Items.Count + 1;
                Items[id] = new TestGift(entity, id);
                return Task.FromResult(Items[id]);
            }

            public Task<Gift[]> InsertAsync(params Gift[] entity)
            {
                throw new NotImplementedException();
            }

            public Task<Gift?> UpdateAsync(int id, Gift entity)
            {
                throw new NotImplementedException();
            }

            private class TestGift : Gift
            {
                public TestGift(Gift gift, int id)
               : base((gift ?? throw new ArgumentNullException(nameof(gift))).Title,
                     gift.Url, gift.Description, gift.User)
                {
                    Id = id;
                }
            }

        }
    }
}
