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

        [TestMethod]
        public async Task DeleteGift_Sucess()
        {
            var service = new GiftTestService();
            Gift gift = SampleData.CreateHelloWorldScam();
            gift = await service.InsertAsync(gift);

            var controller = new GiftController(service);

            ActionResult<bool> ar = await controller.Delete(gift.Id);
            Assert.IsTrue(ar.Result is OkObjectResult);

        }
        [TestMethod]
        public async Task Update_GiftSucess()
        {
            var service = new GiftTestService();
            Gift gift = SampleData.CreateRingGift();
            gift = await service.InsertAsync(gift);

            gift.Title = "JavaTutorial";
            gift.Url = "www.javaisworsethanC#.com";
            gift.Description = "Java tutorial";
            gift.User = SampleData.CreateBilboBaggins();
            var controller = new GiftController(service);

            ActionResult<Gift> ar = await controller.Put(gift.Id, gift);
            Assert.AreEqual("JavaTutorial", ar.Value.Title);
            Assert.AreEqual("www.javaisworsethanC#.com", ar.Value.Url);
            Assert.AreEqual("Java tutorial", ar.Value.Description);
            Assert.AreEqual("Bilbo", ar.Value.User.FirstName);
            Assert.AreEqual("Baggins", ar.Value.User.LastName);
        }

        private class GiftTestService : IGiftService
        {
            private Dictionary<int, Gift> Items { get; } = new Dictionary<int, Gift>();

            public Task<bool> DeleteAsync(int id)
            {
                return Task.FromResult(Items.Remove(id));
            }

            public Task<List<Gift>> FetchAllAsync()
            {
                List<Gift> giftList = Items.Values.ToList();
                return Task.FromResult(giftList);
            }
#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
            public Task<Gift?> FetchByIdAsync(int id)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
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
                foreach (Gift gift in entity)
                {
                    InsertAsync(gift);
                }
                return Task.FromResult(entity);
            }

            public Task<Gift?> UpdateAsync(int id, Gift entity)
            {
                Items[id] = entity;
                return Task.FromResult<Gift?>(Items[id]);
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
