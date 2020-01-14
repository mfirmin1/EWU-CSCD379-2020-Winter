using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace SecretSanta.Business.Test
{
    [TestClass]
    public class GiftTest
    {
        [TestMethod]
        public void Gift_WithAll_ProperInput()
        {
            const int id = 0;
            const string title = "Book";
            const string description = "Gift";
            const string url = "Url";
            User user = new User(id, "John", "Smith", new List<Gift>());
            Gift gift = new Gift(0, title, description, url, user);
             
            Assert.AreEqual<int>(id, gift.Id);
            Assert.AreEqual<string>(title, gift.Title);
            Assert.AreEqual<string>(description, gift.Description);
            Assert.AreEqual<string>(url, gift.Url);
            Assert.AreEqual<User>(user, gift.User);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_Title_IsNull()
        {
            const int id = 0;
            const string description = "Gift";
            const string url = "Url";
            User user = new User(19, "John", "Smith", new List<Gift>());
            Gift gift = new Gift(id, null!, description, url, user);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_Description_IsNull()
        {
            const int id = 0;
            const string title = "Book";
            const string url = "Url";
            User user = new User(19, "John", "Smith", new List<Gift>());
            Gift gift = new Gift(id, title, null!, url, user);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_URL_IsNull()
        {
            const int id = 0;
            const string title = "Book";
            const string description = "Gift";
            User user = new User(19, "John", "Smith", new List<Gift>());
            Gift gift = new Gift(id, title, description, null!, user);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_User_IsNull()
        {
            const int id = 0;
            const string title = "Book";
            const string description = "Gift";
            const string url = "Url";
            Gift gift = new Gift(id, title, description, url, null!);
        }
    }
}
