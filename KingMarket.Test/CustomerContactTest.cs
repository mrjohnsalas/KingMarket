using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Net;
using KingMarket.Model.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KingMarket.Utility;

namespace KingMarket.Test
{
    /// <summary>
    /// Summary description for CustomerContactTest
    /// </summary>
    [TestClass]
    public class CustomerContactTest
    {
        static string customerContactIdForTest = "18";

        [TestMethod]
        public void CreateCustomerContact()
        {
            var customerContact = new CustomerContact()
            {
                CustomerId = 1,
                DocumentNumber = "11111111",
                DocumentTypeId = 1,
                Email = "fbullon@atlas.com.pe",
                FirstName = "Fernando",
                LastName = "Bullon",
                SecondLastName = "Mendoza",
                Phone = "111111111"
            };

            //CREATE 
            Utilities.CreateEntity(customerContact, "http://localhost:55981/CustomerContactService.svc/CustomerContacts");

            //GET
            var customerContactGet = Utilities.GetEntity<CustomerContact>("http://localhost:55981/CustomerContactService.svc/CustomerContacts/", customerContactIdForTest);

            Assert.AreEqual("11111111", customerContactGet.DocumentNumber);
            Assert.AreEqual("fbullon@atlas.com.pe", customerContactGet.Email);
            Assert.AreEqual("Fernando", customerContactGet.FirstName);
            Assert.AreEqual("Bullon", customerContactGet.LastName);
            Assert.AreEqual("Mendoza", customerContactGet.SecondLastName);
            Assert.AreEqual("111111111", customerContactGet.Phone);
        }

        [TestMethod]
        public void CreateCustomerContactException()
        {
            try
            {
                var customerContact = new CustomerContact()
                {
                    CustomerId = 1,
                    DocumentNumber = "11111111",
                    DocumentTypeId = 1,
                    Email = "fbullon@atlas.com.pe",
                    FirstName = "Fernando",
                    LastName = "Bullon",
                    SecondLastName = "Mendoza",
                    Phone = "111111111"
                };

                //CREATE 
                Utilities.CreateEntity(customerContact, "http://localhost:55981/CustomerContactService.svc/CustomerContacts");
            }
            catch (WebException ex)
            {
                var exx = Utilities.Deserialize<GeneralException>(ex);
                Assert.AreEqual("Cannot insert duplicate value. The duplicate key value is: 11111111", exx.Description);
                //Assert.AreEqual("Cannot insert duplicate value. The duplicate key value is: fbullon@atlas.com.pe", exx.Description);
                //Assert.AreEqual("Cannot insert duplicate value. The duplicate key value is: 111111111", exx.Description);
            }
        }

        [TestMethod]
        public void EditCustomerContact()
        {
            var customerContact = new CustomerContact()
            {
                CustomerContactId = 18,
                CustomerId = 1,
                DocumentNumber = "11111122",
                DocumentTypeId = 1,
                Email = "fbullon22@atlas.com.pe",
                FirstName = "Fernando22",
                LastName = "Bullon22",
                SecondLastName = "Mendoza22",
                Phone = "111111122"
            };

            //CREATE 
            Utilities.EditEntity(customerContact, "http://localhost:55981/CustomerContactService.svc/CustomerContacts");

            //GET
            var customerContactGet = Utilities.GetEntity<CustomerContact>("http://localhost:55981/CustomerContactService.svc/CustomerContacts/", customerContactIdForTest);

            Assert.AreEqual("11111122", customerContactGet.DocumentNumber);
            Assert.AreEqual("fbullon22@atlas.com.pe", customerContactGet.Email);
            Assert.AreEqual("Fernando22", customerContactGet.FirstName);
            Assert.AreEqual("Bullon22", customerContactGet.LastName);
            Assert.AreEqual("Mendoza22", customerContactGet.SecondLastName);
            Assert.AreEqual("111111122", customerContactGet.Phone);
        }

        [TestMethod]
        public void EditCustomerContactException()
        {
            try
            {
                var customerContact = new CustomerContact()
                {
                    CustomerContactId = int.Parse(customerContactIdForTest),
                    DocumentNumber = "78451241",
                    DocumentTypeId = 1,
                    Email = "jcondor@atlas.com.pe",
                    FirstName = "Jordan",
                    LastName = "Condor",
                    SecondLastName = "Mendoza",
                    Phone = "975421451"
                };

                //EDIT 
                Utilities.CreateOrEditEntity(customerContact, "http://localhost:55981/CustomerContactService.svc/CustomerContacts", "PUT");
            }
            catch (WebException ex)
            {
                var exx = Utilities.Deserialize<GeneralException>(ex);
                Assert.AreEqual("Cannot insert duplicate value. The duplicate key value is: 11111111", exx.Description);
                //Assert.AreEqual("Cannot insert duplicate value. The duplicate key value is: fbullon@atlas.com.pe", exx.Description);
                //Assert.AreEqual("Cannot insert duplicate value. The duplicate key value is: 111111111", exx.Description);
            }
        }
    }
}
