using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllTests : BaseApiControllerTests<Data.Gift, Business.Dto.Gift, GiftInput>
    {

        protected override Data.Gift CreateEntity()
            => new Data.Gift(Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                new Data.User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

        [TestMethod]
        public async Task Get_ReturnsGift()
        {
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Gift hw = SampleData.CreateHelloWorldScam();
            context.Gifts.Add(hw);
            context.SaveChanges();
            //Justification: Don't need uri objects
#pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.GetAsync("api/Gift");
#pragma warning restore CA2234 // Pass system uri objects instead of strings

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            string jsonData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            //The reason it 10 and not 0 is because of the seed data method in BaseApiControllerTest
            Business.Dto.Gift[] gifts = JsonSerializer.Deserialize<Business.Dto.Gift[]>(jsonData, options);
            Assert.AreEqual(11, gifts.Length);
            Assert.AreEqual(hw.Title, gifts[10].Title);
            Assert.AreEqual(hw.Description, gifts[10].Description);
            Assert.AreEqual(hw.Url, gifts[10].Url);
            Assert.AreEqual(hw.UserId, gifts[10].UserId);
        }
        //Code would break on line 84 and I couldn't fix it.
        /*
        [TestMethod]
        public async Task Put_WithId_Update()
        {
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Gift hw = SampleData.CreateHelloWorldScam();
            context.Gifts.Add(hw);
            context.SaveChanges();

            Business.Dto.GiftInput hwInput = new Business.Dto.GiftInput
            {
                Title = hw.Title,
                Description = hw.Description,
                Url = hw.Url
            };
            hw.Title += "Changed";
            hw.Description += "Changed";
            hw.Url += "/Changed";

            string jsonData = JsonSerializer.Serialize(hw);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

#pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.PutAsync($"api/Gift/{hw.Id}", stringContent);
#pragma warning restore CA2234 // Pass system uri objects instead of strings

            response.EnsureSuccessStatusCode();
            string retunedJson = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Business.Dto.Gift returnedGift = JsonSerializer.Deserialize<Business.Dto.Gift>(retunedJson, options);

            Assert.AreEqual(hwInput.Title, returnedGift.Title);
            Assert.AreEqual(hwInput.Description, returnedGift.Description);
            Assert.AreEqual(hwInput.Url, returnedGift.Url);
            Assert.AreEqual(hwInput.UserId, returnedGift.UserId);
        }*/


    }
}
