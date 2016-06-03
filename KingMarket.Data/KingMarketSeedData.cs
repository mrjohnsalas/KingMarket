using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingMarket.Data.Context;
using KingMarket.Model.Models;
using System.IO;
using System.Web;
using System.Web.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KingMarket.Data
{
    public class KingMarketSeedData : DropCreateDatabaseIfModelChanges<KingMarketContext>
    {
        protected override void Seed(KingMarketContext context)
        {
            GetClassDocumentTypes().ForEach(o => context.ClassDocumentTypes.Add(o));
            GetDocumentTypes().ForEach(o => context.DocumentTypes.Add(o));
            GetEmployeeTypes().ForEach(o => context.EmployeeTypes.Add(o));
            GetProductTypes().ForEach(o => context.ProductTypes.Add(o));
            GetStatus().ForEach(o => context.Status.Add(o));
            GetUnitMeasures().ForEach(o => context.UnitMeasures.Add(o));
            context.SaveChanges();
            
            GetEmployees().ForEach(o => context.Employees.Add(o));
            GetCustomers().ForEach(o => context.Customers.Add(o));
            GetSuppliers().ForEach(o => context.Suppliers.Add(o));
            context.SaveChanges();

            GetCustomerContacts().ForEach(o => context.CustomerContacts.Add(o));
            GetSupplierContacts().ForEach(o => context.SupplierContacts.Add(o));
            GetProducts().ForEach(o => context.Products.Add(o));
            context.SaveChanges();

            GetProductPhotos().ForEach(o => context.ProductPhotos.Add(o));
            context.SaveChanges();

            CreateUsers();
        }

        private static List<ProductPhoto> GetProductPhotos()
        {
            var pictureFolderPath = new DirectoryInfo(HostingEnvironment.ApplicationPhysicalPath).Parent.FullName;
            pictureFolderPath += @"\Documents\Pictures";
            var productPhotos = new List<ProductPhoto>();
            var directories = Directory.GetDirectories(pictureFolderPath);
            foreach (var directory in directories)
            {
                var count = 0;
                var files = Directory.GetFiles(directory);
                foreach (var file in files)
                {
                    count += 1;
                    var photo = new ProductPhoto
                    {
                        FileName = Path.GetFileName(file),
                        FileType = FileType.Photo,
                        ContentType = MimeMapping.GetMimeMapping(file),
                        ProductId = int.Parse(new DirectoryInfo(directory).Name.Substring(0, 2)),
                        IsDefault = count.Equals(1)
                    };
                    photo.Content = File.ReadAllBytes(file);
                    productPhotos.Add(photo);
                }
            }
            return productPhotos;
        }

        private static List<ClassDocumentType> GetClassDocumentTypes()
        {
            return new List<ClassDocumentType>
            {
                new ClassDocumentType{ Name = "Identificacion"},
                new ClassDocumentType{ Name = "Pago"},
                new ClassDocumentType{ Name = "Traslado"}
            };
        }

        private static List<DocumentType> GetDocumentTypes()
        {
            return new List<DocumentType>
            {
                new DocumentType{ Name = "DNI", OnlyForEnterprise = false, ClassDocumentTypeId = 1},
                new DocumentType{ Name = "RUC", OnlyForEnterprise = true, ClassDocumentTypeId = 1},
                new DocumentType{ Name = "Factura", OnlyForEnterprise = true, ClassDocumentTypeId = 2},
                new DocumentType{ Name = "Boleta", OnlyForEnterprise = false, ClassDocumentTypeId = 2},
                new DocumentType{ Name = "Guia", OnlyForEnterprise = false, ClassDocumentTypeId = 3},
            };
        }

        private static List<EmployeeType> GetEmployeeTypes()
        {
            return new List<EmployeeType>
            {
                new EmployeeType{ Name = "Almacenero"},
                new EmployeeType{ Name = "Comprador"}
            };
        }

        private static List<ProductType> GetProductTypes()
        {
            return new List<ProductType>
            {
                new ProductType{ Name = "Celulares"},
                new ProductType{ Name = "Computadoras"},
                new ProductType{ Name = "Laptops"},
                new ProductType{ Name = "Smart Watches"},
                new ProductType{ Name = "Streaming Box"},
                new ProductType{ Name = "Tablets"}
            };
        }

        private static List<Status> GetStatus()
        {
            return new List<Status>
            {
                new Status{ Name = "Abierto"},
                new Status{ Name = "Activo"},
                new Status{ Name = "Anulado"},
                new Status{ Name = "Cerrado"},
                new Status{ Name = "Inactivo"}
            };
        }

        private static List<UnitMeasure> GetUnitMeasures()
        {
            return new List<UnitMeasure>
            {
                new UnitMeasure{ ShortName = "UND", Name = "Unidad"},
                new UnitMeasure{ ShortName = "PZA", Name = "Pieza"}
            };
        }

        private static List<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee
                {
                    DocumentTypeId = 1, 
                    EmployeeTypeId = 2, 
                    DocumentNumber = "47471425", 
                    FirstName = "Juan", 
                    LastName = "Travezano", 
                    SecondLastName = "Carrasco", 
                    Email = "jtravezano@kingmarket.com", 
                    Phone = "986565859"
                },
                new Employee
                {
                    DocumentTypeId = 1, 
                    EmployeeTypeId = 2, 
                    DocumentNumber = "85784812", 
                    FirstName = "Erick", 
                    LastName = "Apuy", 
                    SecondLastName = "Lago", 
                    Email = "eapuy@kingmarket.com", 
                    Phone = "985675110"
                },
                new Employee
                {
                    DocumentTypeId = 1, 
                    EmployeeTypeId = 2, 
                    DocumentNumber = "85789630", 
                    FirstName = "Dante", 
                    LastName = "Ruiz", 
                    SecondLastName = "Prado", 
                    Email = "druiz@kingmarket.com", 
                    Phone = "857432111"
                },
                new Employee
                {
                    DocumentTypeId = 1, 
                    EmployeeTypeId = 1, 
                    DocumentNumber = "96969688", 
                    FirstName = "Romy", 
                    LastName = "Chipana", 
                    SecondLastName = "Montoya", 
                    Email = "rchipana@kingmarket.com", 
                    Phone = "535344110"
                },
                new Employee
                {
                    DocumentTypeId = 1, 
                    EmployeeTypeId = 1, 
                    DocumentNumber = "12010122", 
                    FirstName = "Jacqueline", 
                    LastName = "Villavicencio", 
                    SecondLastName = "Montes", 
                    Email = "jvillavicencio@kingmarket.com", 
                    Phone = "787878440"
                },
                new Employee
                {
                    DocumentTypeId = 1, 
                    EmployeeTypeId = 1, 
                    DocumentNumber = "85745122", 
                    FirstName = "Julio", 
                    LastName = "Rayme", 
                    SecondLastName = "Perez", 
                    Email = "jrayme@kingmarket.com", 
                    Phone = "963232110"
                }
            };
        }

        private static List<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer
                {
                    DocumentTypeId = 2, 
                    DocumentNumber = "20428685411", 
                    BusinessName = "Industrial Papelera Atlas S.A.", 
                    Address = "Carretera Central Km. 19.5 - Chaclacayo", 
                    Email = "contacto@atlas.com.pe", 
                    Web = "http://www.atlas.com.pe", 
                    Phone = "014542544"
                },
                new Customer
                {
                    DocumentTypeId = 2, 
                    DocumentNumber = "20101340211", 
                    BusinessName = "Editorial Tinco S.A.", 
                    Address = "Av. San Borja Sur 1170", 
                    Email = "contacto@editorialtinco.com.pe", 
                    Web = "http://www.editorialtinco.com.pe", 
                    Phone = "014742411"
                },
                new Customer
                {
                    DocumentTypeId = 2, 
                    DocumentNumber = "20101340300", 
                    BusinessName = "Distribuidora de Publicaciones Peru S.A.", 
                    Address = "Av. San Borja Sur 1170", 
                    Email = "contacto@dispubliperu.com.pe", 
                    Web = "http://www.dispubliperu.com.pe", 
                    Phone = "018654566"
                },
                new Customer
                {
                    DocumentTypeId = 2, 
                    DocumentNumber = "20101561659", 
                    BusinessName = "Alivi Panorama S.A.", 
                    Address = "Manuel del Mar 1349", 
                    Email = "contacto@alivi.com.pe", 
                    Web = "http://www.alivi.com.pe", 
                    Phone = "019699999"
                },
                new Customer
                {
                    DocumentTypeId = 2, 
                    DocumentNumber = "20101578543", 
                    BusinessName = "Latina Import S.A.", 
                    Address = "Jr. Quilca Nro. 466", 
                    Email = "contacto@latinaimport.com.pe", 
                    Web = "http://www.latinaimport.com.pe", 
                    Phone = "018588833"
                },
                new Customer
                {
                    DocumentTypeId = 2, 
                    DocumentNumber = "20101587704", 
                    BusinessName = "Rodemar S.A.", 
                    Address = "Av.Sosa Pelaez 132", 
                    Email = "contacto@rodemar.com.pe", 
                    Web = "http://www.rodemar.com.pe", 
                    Phone = "014246699"
                }
            };
        }

        private static List<CustomerContact> GetCustomerContacts()
        {
            return new List<CustomerContact>
            {
                new CustomerContact
                {
                    CustomerId = 1, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "78451241", 
                    FirstName = "Jordan", 
                    LastName = "Condor", 
                    SecondLastName = "Mendoza", 
                    Email = "jcondor@atlas.com.pe", 
                    Phone = "975421451"
                },
                new CustomerContact
                {
                    CustomerId = 1, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "69587435", 
                    FirstName = "Rita", 
                    LastName = "Nakaruro", 
                    SecondLastName = "Pinedo", 
                    Email = "rnakaruro@atlas.com.pe", 
                    Phone = "857484657"
                },
                new CustomerContact
                {
                    CustomerId = 2, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "09057314", 
                    FirstName = "Apolonio", 
                    LastName = "Cordero", 
                    SecondLastName = "Quispe", 
                    Email = "acordero@editorialtinco.com.pe", 
                    Phone = "787475002"
                },
                new CustomerContact
                {
                    CustomerId = 2, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "08871030", 
                    FirstName = "Gladis", 
                    LastName = "Aragon", 
                    SecondLastName = "Cuadros", 
                    Email = "garagon@editorialtinco.com.pe", 
                    Phone = "964515006"
                },
                new CustomerContact
                {
                    CustomerId = 3, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "06972789", 
                    FirstName = "Alejandra", 
                    LastName = "Leon", 
                    SecondLastName = "Venegas", 
                    Email = "aleon@dispubliperu.com.pe", 
                    Phone = "120101220"
                },
                new CustomerContact
                {
                    CustomerId = 3, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "06969256", 
                    FirstName = "Alejandro", 
                    LastName = "Acevedo", 
                    SecondLastName = "Perez", 
                    Email = "aacevedo@dispubliperu.com.pe", 
                    Phone = "986969337"
                },
                new CustomerContact
                {
                    CustomerId = 4, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "08642786", 
                    FirstName = "Enrique", 
                    LastName = "Alberto", 
                    SecondLastName = "Ccondezo", 
                    Email = "ealberto@alivi.com.pe", 
                    Phone = "898975330"
                },
                new CustomerContact
                {
                    CustomerId = 4, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "40051042", 
                    FirstName = "Jimmy", 
                    LastName = "Alcantara", 
                    SecondLastName = "Matencio", 
                    Email = "jalcantara@alivi.com.pe", 
                    Phone = "121212000"
                },
                new CustomerContact
                {
                    CustomerId = 5, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "09138378", 
                    FirstName = "Ignacio", 
                    LastName = "Cardenas", 
                    SecondLastName = "Ortega", 
                    Email = "icardenas@latinaimport.com.pe", 
                    Phone = "231201007"
                },
                new CustomerContact
                {
                    CustomerId = 5, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "09060299", 
                    FirstName = "Carlos", 
                    LastName = "Martinez", 
                    SecondLastName = "Barrientos", 
                    Email = "cmartinez@latinaimport.com.pe", 
                    Phone = "960202005"
                },
                new CustomerContact
                {
                    CustomerId = 6, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "09620718", 
                    FirstName = "Carlos", 
                    LastName = "Pereyra", 
                    SecondLastName = "De La Cruz", 
                    Email = "cpereyra@rodemar.com.pe", 
                    Phone = "587425116"
                },
                new CustomerContact
                {
                    CustomerId = 6, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "09179768", 
                    FirstName = "Tomas", 
                    LastName = "Quiroz", 
                    SecondLastName = "Luyo", 
                    Email = "tquiroz@rodemar.com.pe", 
                    Phone = "361201220"
                }
            };
        }

        private static List<Supplier> GetSuppliers()
        {
            return new List<Supplier>
            {
                new Supplier
                {
                    DocumentTypeId = 2, 
                    DocumentNumber = "20969685961", 
                    BusinessName = "Proveedores Asociados S.A.", 
                    Address = "Calle Los Jeranios 131", 
                    Email = "contacto@pasociados.com", 
                    Web = "http://www.pasociados.com", 
                    Phone = "012451212"
                },
                new Supplier
                {
                    DocumentTypeId = 2, 
                    DocumentNumber = "20454545121", 
                    BusinessName = "Industrias del Acero S.A.", 
                    Address = "La molina 345", 
                    Email = "contacto@indacero.com.pe", 
                    Web = "http://www.indacero.com.pe", 
                    Phone = "014245866"
                },
                new Supplier
                {
                    DocumentTypeId = 2, 
                    DocumentNumber = "20100083362", 
                    BusinessName = "Cosapi Data S.A.", 
                    Address = "AV. Coronel Andres Reyes Nro. 420", 
                    Email = "contacto@cosapidata.com.pe", 
                    Web = "http://www.cosapidata.com.pe", 
                    Phone = "017473200"
                },
                new Supplier
                {
                    DocumentTypeId = 2, 
                    DocumentNumber = "20100083877", 
                    BusinessName = "Corporacion Miyasato S.A.C.", 
                    Address = "AV. Iquitos Nro. 1174", 
                    Email = "contacto@corpmiyasato.com.pe", 
                    Web = "http://www.corpmiyasato.com.pe", 
                    Phone = "013245211"
                },
                new Supplier
                {
                    DocumentTypeId = 2, 
                    DocumentNumber = "20100084172", 
                    BusinessName = "Promotores Electricos S.A.", 
                    Address = "AV. Prol Parinacocha Nro. 765", 
                    Email = "contacto@promelectricos.com.pe", 
                    Web = "http://www.promelectricos.com.pe", 
                    Phone = "017843300"
                },
                new Supplier
                {
                    DocumentTypeId = 2, 
                    DocumentNumber = "20100099528", 
                    BusinessName = "Transporte Litoral Andino S.A.C.", 
                    Address = "Jr. Rio Grande Mz. A Lte. 07 Urb. Las Praderas", 
                    Email = "contacto@tandino.com.pe", 
                    Web = "http://www.tandino.com.pe", 
                    Phone = "017472477"
                }
            };
        }

        private static List<SupplierContact> GetSupplierContacts()
        {
            return new List<SupplierContact>
            {
                new SupplierContact
                {
                    SupplierId = 1, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "41977695", 
                    FirstName = "Rafael", 
                    LastName = "Aguilar", 
                    SecondLastName = "Tocas", 
                    Email = "raguilar@pasociados.com", 
                    Phone = "323232663"
                },
                new SupplierContact
                {
                    SupplierId = 1, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "06592597", 
                    FirstName = "Agustin", 
                    LastName = "Villanueva", 
                    SecondLastName = "Porras", 
                    Email = "avillanueva@pasociados.com", 
                    Phone = "986363663"
                },
                new SupplierContact
                {
                    SupplierId = 2, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "45457898", 
                    FirstName = "Miguel", 
                    LastName = "Zegarra", 
                    SecondLastName = "Urbina", 
                    Email = "mzegarra@indacero.com.pe", 
                    Phone = "986532445"
                },
                new SupplierContact
                {
                    SupplierId = 2, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "58697532", 
                    FirstName = "Marco", 
                    LastName = "Torres", 
                    SecondLastName = "Mejia", 
                    Email = "mtorres@indacero.com.pe", 
                    Phone = "564512771"
                },
                new SupplierContact
                {
                    SupplierId = 3, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "10216401", 
                    FirstName = "Eduardo", 
                    LastName = "Acevedo", 
                    SecondLastName = "Ayala", 
                    Email = "eacevedo@cosapidata.com.pe", 
                    Phone = "124512110"
                },
                new SupplierContact
                {
                    SupplierId = 3, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "07576592", 
                    FirstName = "Daniel", 
                    LastName = "Acosta", 
                    SecondLastName = "Ciriaco", 
                    Email = "dacosta@cosapidata.com.pe", 
                    Phone = "214512001"
                },
                new SupplierContact
                {
                    SupplierId = 4, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "09544175", 
                    FirstName = "Guillermo", 
                    LastName = "Mendoza", 
                    SecondLastName = "Rivera", 
                    Email = "gmendoza@corpmiyasato.com.pe", 
                    Phone = "120101221"
                },
                new SupplierContact
                {
                    SupplierId = 4, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "09950493", 
                    FirstName = "Eddy", 
                    LastName = "Aburto", 
                    SecondLastName = "Correa", 
                    Email = "eaburto@corpmiyasato.com.pe", 
                    Phone = "237541000"
                },
                new SupplierContact
                {
                    SupplierId = 5, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "09626920", 
                    FirstName = "Jorge", 
                    LastName = "Adriano", 
                    SecondLastName = "Campos", 
                    Email = "jadriano@promelectricos.com.pe", 
                    Phone = "329856447"
                },
                new SupplierContact
                {
                    SupplierId = 5, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "07533328", 
                    FirstName = "Juan", 
                    LastName = "Aguilar", 
                    SecondLastName = "Machiavelo", 
                    Email = "jaguilar@promelectricos.com.pe", 
                    Phone = "787878878"
                },
                new SupplierContact
                {
                    SupplierId = 6, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "02624739", 
                    FirstName = "Aide", 
                    LastName = "Torres", 
                    SecondLastName = "Cruz", 
                    Email = "atorres@tandino.com.pe", 
                    Phone = "323232223"
                },
                new SupplierContact
                {
                    SupplierId = 6, 
                    DocumentTypeId = 1, 
                    DocumentNumber = "07504122", 
                    FirstName = "Carmen", 
                    LastName = "Alayo", 
                    SecondLastName = "Sanchez", 
                    Email = "calayo@tandino.com.pe", 
                    Phone = "757575447"
                }
            };
        }

        private static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product { Name = "Apple Watch", Description = "Reloj marca Apple", UnitPrice = 1600.00M, UnitCost = 1200.00M, ProductTypeId = 4, Stock = 0.00M, MinStock = 10.00M, MaxStock = 60.00M, UnitMeasureId = 1},
                new Product { Name = "Apple TV 4", Description = "Streaming Box marca Apple", UnitPrice = 350.00M, UnitCost = 320.00M, ProductTypeId = 5, Stock = 0.00M, MinStock = 30.00M, MaxStock = 90.00M, UnitMeasureId = 1},
                new Product { Name = "HTC One M9", Description = "Celular marca HTC", UnitPrice = 1800.00M, UnitCost = 1500.00M, ProductTypeId = 1, Stock = 0.00M, MinStock = 5.00M, MaxStock = 15.00M, UnitMeasureId = 1},
                new Product { Name = "Samsung Galaxy S6", Description = "Celular marca Samsung", UnitPrice = 1500.00M, UnitCost = 1200.00M, ProductTypeId = 1, Stock = 0.00M, MinStock = 5.00M, MaxStock = 16.00M, UnitMeasureId = 1},
                new Product { Name = "Samsung Galaxy S6 Edge", Description = "Celular marca Samsung", UnitPrice = 1800.00M, UnitCost = 1500.00M, ProductTypeId = 1, Stock = 0.00M, MinStock = 6.00M, MaxStock = 78.00M, UnitMeasureId = 1},
                new Product { Name = "Samsung Galaxy S7", Description = "Celular marca Samsung", UnitPrice = 1900.00M, UnitCost = 1700.00M, ProductTypeId = 1, Stock = 0.00M, MinStock = 6.00M, MaxStock = 25.00M, UnitMeasureId = 1},
                new Product { Name = "Samsung Galaxy S7 Edge", Description = "Celular marca Samsung ", UnitPrice = 2100.00M, UnitCost = 2000.00M, ProductTypeId = 1, Stock = 0.00M, MinStock = 6.00M, MaxStock = 9.00M, UnitMeasureId = 1},
                new Product { Name = "Samsung Galaxy Tap", Description = "tablet marca Samsung", UnitPrice = 1400.00M, UnitCost = 1200.00M, ProductTypeId = 6, Stock = 0.00M, MinStock = 7.00M, MaxStock = 89.00M, UnitMeasureId = 1},
                new Product { Name = "Google Chromecast 2", Description = "Streaming Box marca Google", UnitPrice = 250.00M, UnitCost = 220.00M, ProductTypeId = 5, Stock = 0.00M, MinStock = 10.00M, MaxStock = 40.00M, UnitMeasureId = 1},
                new Product { Name = "iMac 5K", Description = "PC marca Apple", UnitPrice = 4500.00M, UnitCost = 4200.00M, ProductTypeId = 2, Stock = 0.00M, MinStock = 10.00M, MaxStock = 80.00M, UnitMeasureId = 1},
                new Product { Name = "iPad 2", Description = "Tablet marca Apple", UnitPrice = 1500.00M, UnitCost = 1200.00M, ProductTypeId = 6, Stock = 0.00M, MinStock = 6.00M, MaxStock = 50.00M, UnitMeasureId = 1},
                new Product { Name = "iPad Air 2", Description = "Tablet marca Apple", UnitPrice = 2100.00M, UnitCost = 2000.00M, ProductTypeId = 6, Stock = 0.00M, MinStock = 8.00M, MaxStock = 40.00M, UnitMeasureId = 1},
                new Product { Name = "iPad Pro", Description = "Tablet marca Apple", UnitPrice = 3000.00M, UnitCost = 2600.00M, ProductTypeId = 6, Stock = 0.00M, MinStock = 6.00M, MaxStock = 10.00M, UnitMeasureId = 1},
                new Product { Name = "iPhone 5C", Description = "Celular marca Apple", UnitPrice = 1400.00M, UnitCost = 1200.00M, ProductTypeId = 1, Stock = 0.00M, MinStock = 5.00M, MaxStock = 16.00M, UnitMeasureId = 1},
                new Product { Name = "iPhone 6", Description = "Celular marca Apple", UnitPrice = 2500.00M, UnitCost = 2200.00M, ProductTypeId = 1, Stock = 0.00M, MinStock = 5.00M, MaxStock = 10.00M, UnitMeasureId = 1},
                new Product { Name = "iPhone 6S", Description = "Celular marca Apple", UnitPrice = 3500.00M, UnitCost = 3200.00M, ProductTypeId = 1, Stock = 0.00M, MinStock = 2.00M, MaxStock = 6.00M, UnitMeasureId = 1},
                new Product { Name = "Mac Pro", Description = "PC marca Apple", UnitPrice = 3500.00M, UnitCost = 3200.00M, ProductTypeId = 2, Stock = 0.00M, MinStock = 12.00M, MaxStock = 36.00M, UnitMeasureId = 1},
                new Product { Name = "Macbook", Description = "Laptop marca Apple", UnitPrice = 4800.00M, UnitCost = 4500.00M, ProductTypeId = 3, Stock = 0.00M, MinStock = 2.00M, MaxStock = 7.00M, UnitMeasureId = 1},
                new Product { Name = "Macbook Air", Description = "Laptop marca Apple", UnitPrice = 3700.00M, UnitCost = 3500.00M, ProductTypeId = 3, Stock = 0.00M, MinStock = 9.00M, MaxStock = 75.00M, UnitMeasureId = 1},
                new Product { Name = "Macbook Pro", Description = "Laptop marca Apple", UnitPrice = 4200.00M, UnitCost = 4000.00M, ProductTypeId = 3, Stock = 0.00M, MinStock = 3.00M, MaxStock = 60.00M, UnitMeasureId = 1},
                new Product { Name = "Samsung Gear S2", Description = "Reloj marca Samsung", UnitPrice = 1200.00M, UnitCost = 1000.00M, ProductTypeId = 4, Stock = 0.00M, MinStock = 6.00M, MaxStock = 50.00M, UnitMeasureId = 1}
            };
        }

        private static void CreateUsers()
        {
            var db = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //CREATE USERS TO EMPLOYEES
            foreach (var employee in GetEmployees())
            {
                var user = userManager.FindByName(employee.Email);
                if (user != null) continue;
                user = new ApplicationUser
                {
                    UserName = employee.Email,
                    Email = employee.Email
                };
                userManager.Create(user, employee.DocumentNumber);
                //ADD TO ROLE
                if (employee.EmployeeTypeId.Equals(1))
                {
                    if (!userManager.IsInRole(user.Id, "Grocer"))
                        userManager.AddToRole(user.Id, "Grocer");
                }
                else
                {
                    if (!userManager.IsInRole(user.Id, "Buyer"))
                        userManager.AddToRole(user.Id, "Buyer");
                }
            }
            //CREATE USERS TO CUSTOMERS
            foreach (var customer in GetCustomers())
            {
                var user = userManager.FindByName(customer.Email);
                if (user != null) continue;
                user = new ApplicationUser
                {
                    UserName = customer.Email,
                    Email = customer.Email
                };
                userManager.Create(user, customer.DocumentNumber);
                //ADD TO ROLE
                if (!userManager.IsInRole(user.Id, "Customer"))
                    userManager.AddToRole(user.Id, "Customer");
            }
            //CREATE USERS TO SUPPLIERS
            foreach (var supplier in GetSuppliers())
            {
                var user = userManager.FindByName(supplier.Email);
                if (user != null) continue;
                user = new ApplicationUser
                {
                    UserName = supplier.Email,
                    Email = supplier.Email
                };
                userManager.Create(user, supplier.DocumentNumber);
                //ADD TO ROLE
                if (!userManager.IsInRole(user.Id, "Supplier"))
                    userManager.AddToRole(user.Id, "Supplier");
            }

            db.Dispose();
        }
    }
}
