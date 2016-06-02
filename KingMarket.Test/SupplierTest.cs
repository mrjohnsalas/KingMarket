using System;
using System.Text;
using System.Collections.Generic;
using System.ServiceModel;
using KingMarket.Model.Models;
using KingMarket.Test.SupplierService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KingMarket.Test
{
    /// <summary>
    /// Summary description for SupplierTest
    /// </summary>
    [TestClass]
    public class SupplierTest
    {
        [TestMethod]
        public void GetSupplier()
        {
            var proxy = new SupplierServiceClient();
            var supplier = proxy.GetSupplier(4);
            Assert.AreEqual("20100083877", supplier.DocumentNumber);
            Assert.AreEqual("Corporacion Miyasato S.A.C.", supplier.BusinessName);
            Assert.AreEqual("013245211", supplier.Phone);
            Assert.AreEqual("contacto@corpmiyasato.com.pe", supplier.Email);
        }

        [TestMethod]
        public void GetSuppliers()
        {
            var proxy = new SupplierServiceClient();
            var supplier = proxy.GetSuppliers();
            Assert.AreEqual(6, supplier.Count);
        }

        [TestMethod]
        public void CreateSupplier()
        {
            var proxy = new SupplierServiceClient();
            var supplier = new Supplier
            {
                DocumentTypeId = 2,
                DocumentNumber = "20777777777",
                BusinessName = "PROVEEDOR DE PRUEBA",
                Address = "DIRECCION DE PRUEBA",
                Email = "prueba@proveedordeprueba.com",
                Web = "www.proveedordeprueba.com",
                Phone = "017777777"
            };
            proxy.CreateSupplier(supplier);
            Assert.AreEqual("20777777777", supplier.DocumentNumber);
            Assert.AreEqual("PROVEEDOR DE PRUEBA", supplier.BusinessName);
            Assert.AreEqual("prueba@proveedordeprueba.com", supplier.Email);
        }

        [TestMethod]
        public void CreateSupplierException()
        {
            var proxy = new SupplierServiceClient();
            var supplier = new Supplier
            {
                DocumentTypeId = 2,
                DocumentNumber = "20777777777",
                BusinessName = "PROVEEDOR DE PRUEBA",
                Address = "DIRECCION DE PRUEBA",
                Email = "prueba@proveedordeprueba.com",
                Web = "www.proveedordeprueba.com",
                Phone = "017777777"
            };
            try
            {
                proxy.CreateSupplier(supplier);
            }
            catch (FaultException<GeneralException> ex)
            {
                Assert.AreEqual("Error when trying to create.", ex.Reason.ToString());
                //Assert.AreEqual("Cannot insert duplicate value. The duplicate key value is: 20777777777", String.Format("Cannot insert duplicate value. The duplicate key value is: {0}", supplier.DocumentNumber));
                //Assert.AreEqual("Cannot insert duplicate value. The duplicate key value is: PROVEEDOR DE PRUEBA", String.Format("Cannot insert duplicate value. The duplicate key value is: {0}", supplier.BusinessName));
                //Assert.AreEqual("Cannot insert duplicate value. The duplicate key value is: www.proveedordeprueba.com", String.Format("Cannot insert duplicate value. The duplicate key value is: {0}", supplier.Email));
                //Assert.AreEqual("Cannot insert duplicate value. The duplicate key value is: 017777777", String.Format("Cannot insert duplicate value. The duplicate key value is: {0}", supplier.Phone));
            }
        }

        [TestMethod]
        public void EditSupplier()
        {
            var proxy = new SupplierServiceClient();
            var supplier = new Supplier
            {
                SupplierId = 7,
                DocumentTypeId = 2,
                DocumentNumber = "20888888888",
                BusinessName = "PROVEEDOR DE PRUEBA EDITADO",
                Address = "DIRECCION DE PRUEBA EDITADO",
                Email = "prueba@proveedordeprueba.com",
                Web = "www.proveedordeprueba.com",
                Phone = "018888888"
            };
            proxy.EditSupplier(supplier);
            Assert.AreEqual("20888888888", supplier.DocumentNumber);
            Assert.AreEqual("PROVEEDOR DE PRUEBA EDITADO", supplier.BusinessName);
            Assert.AreEqual("018888888", supplier.Phone);
        }

        [TestMethod]
        public void EditSupplierException()
        {
            var proxy = new SupplierServiceClient();
            var supplier = new Supplier
            {
                SupplierId = 7,
                DocumentTypeId = 2,
                DocumentNumber = "20100083877",
                BusinessName = "Corporacion Miyasato S.A.C.",
                Address = "DIRECCION DE PRUEBA EDITADO",
                Email = "contacto@corpmiyasato.com.pe",
                Web = "www.proveedordeprueba.com",
                Phone = "013245211"
            };
            try
            {
                proxy.EditSupplier(supplier);
            }
            catch (FaultException<GeneralException> ex)
            {
                Assert.AreEqual("Error when trying to edit.", ex.Reason.ToString());
                //Assert.AreEqual("Cannot insert duplicate value. The duplicate key value is: 20100083877", String.Format("Cannot insert duplicate value. The duplicate key value is: {0}", supplier.DocumentNumber));
                //Assert.AreEqual("Cannot insert duplicate value. The duplicate key value is: Corporacion Miyasato S.A.C.", String.Format("Cannot insert duplicate value. The duplicate key value is: {0}", supplier.BusinessName));
                //Assert.AreEqual("Cannot insert duplicate value. The duplicate key value is: contacto@corpmiyasato.com.pe", String.Format("Cannot insert duplicate value. The duplicate key value is: {0}", supplier.Email));
                //Assert.AreEqual("Cannot insert duplicate value. The duplicate key value is: 013245211", String.Format("Cannot insert duplicate value. The duplicate key value is: {0}", supplier.Phone));
            }
        }

        [TestMethod]
        public void DeleteSupplier()
        {
            var proxy = new SupplierServiceClient();
            proxy.DeleteSupplier(7);
            var supplier = proxy.GetSupplier(7);
            Assert.AreEqual(null, supplier);
        }
    }
}
