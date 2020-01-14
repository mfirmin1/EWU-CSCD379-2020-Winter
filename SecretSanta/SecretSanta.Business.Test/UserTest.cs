using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business.Test
{
    [TestClass]
    class UserTest
    {
        [TestMethod]
        public void User_WithAll_ProperInput()
        {
            const int id = 0;
            const string FirstName = "John";
            const string LastName = "Smith";
            List<Gift> gift = new List<Gift>();
            User user = new User(0, FirstName, LastName, gift);
            Assert.AreEqual<int>(id, user.Id);
            Assert.AreEqual<string>(FirstName, user.FirstName);
            Assert.AreEqual<string>(LastName, user.LastName);
            Assert.AreEqual<List<Gift>>(gift, user.Gift);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void USer_Id_IsNUll()
        {
            const string FirstName = "John";
            const string LastName = "Smith";
            List<Gift> gift = new List<Gift>();
            User user = new User(null!, FirstName, LastName, gift);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void USer_FirstName_IsNUll()
        {
            const int id = 0;
            const string LastName = "Smith";
            List<Gift> gift = new List<Gift>();
            User user = new User(0, null!, LastName, gift);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void USer_LastName_IsNUll()
        {
            const int id = 0;
            const string FirstName = "John";
            List<Gift> gift = new List<Gift>();
            User user = new User(0, FirstName, null!, gift);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void USer_GiftList_IsNUll()
        {
            const int id = 0;
            const string FirstName = "John";
            const string LastName = "Smith";
            User user = new User(0, FirstName, LastName, null!);
        }
    }
}
