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
            UserController userController = new UserController(new Mock<IUserService>().Object);
            Assert.IsNotNull(userController);

        }
        //Arrange
        //Act
        //Assert
        [TestMethod]
        public async Task GetById_WithExistingUser_Sucess()
        {
            var service = new Mock<UserService>();
            User user = SampleData.CreateBilboBaggins();
            service.Setup(u => u.InsertAsync(user)).ReturnsAsync(user);

        }
       

        
    }
}
