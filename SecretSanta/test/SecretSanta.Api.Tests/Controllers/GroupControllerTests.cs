using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business.Dto;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTests : BaseApiControllerTests<Data.Group, Business.Dto.Group, GroupInput>
    {
        protected override Data.Group CreateEntity()
                    => new Data.Group(Guid.NewGuid().ToString());
    }
}