using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using IG_CoreLibrary.Models;
using IG_CoreLibrary.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IG_CoreLibrary.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private IBaseRepository repo { get; set; }

        public UnitTest1()
        {
            repo = new BaseRepository();
        }
        [TestMethod]
        public void TestCreateMethod()
        {      
            var item = new PhoneBookBase { FirstName = "Ilir", LastName = "Gashi", L_PhoneBook = new List<PhoneBookDetails> { new PhoneBookDetails { Number = "123141", PhoneType = IG_CoreLibrary.Models.Type.Cellphone } } };
            var response = repo.CreatePhoneBook(item).Result;
            Assert.AreEqual(response.HasError, false);
        }

        [TestMethod]
        public void TestUpdateMethod()
        {
            var item = new PhoneBookBase { FirstName = "Ilir", LastName = "Gashi", L_PhoneBook = new List<PhoneBookDetails> { new PhoneBookDetails { Number = "123141", PhoneType = IG_CoreLibrary.Models.Type.Cellphone } } };
            var response = repo.UpdatePhoneBook(item).Result;
            Assert.AreEqual(response.HasError, false);
        } 
        
        [TestMethod]
        public void TestDeleteMethod()
        {
            var item = new PhoneBookBase { FirstName = "Ilir", LastName = "Gashi", L_PhoneBook = new List<PhoneBookDetails> { new PhoneBookDetails { Number = "123141", PhoneType = IG_CoreLibrary.Models.Type.Cellphone } } };
            var response = repo.DeletePhoneBook(item).Result;
            Assert.AreEqual(response.HasError, false);
        }
    }
}
