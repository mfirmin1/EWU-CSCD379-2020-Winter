using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business.Dto;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests : BaseApiControllerTests<Data.User, Business.Dto.User, UserInput>
    {
        protected override Data.User CreateEntity()
                    => new Data.User(Guid.NewGuid().ToString(),
                        Guid.NewGuid().ToString());
    }
}
