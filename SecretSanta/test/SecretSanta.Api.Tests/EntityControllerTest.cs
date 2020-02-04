using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public abstract class EntityControllerTest<TEntity> where TEntity : EntityBase
    {

    }
}
