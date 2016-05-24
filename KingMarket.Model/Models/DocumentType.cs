using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMarket.Model.Models
{
    public class DocumentType
    {
        [Key]
        [Display(Name = "Document Type")]
        public int DocumentTypeId { get; set; }

        [Display(Name = "Document Type Name")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(50, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 3)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Display(Name = "Only For Enterprise?")]
        public bool OnlyForEnterprise { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public int ClassDocumentTypeId { get; set; }

        public virtual ClassDocumentType ClassDocumentType { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }

        public virtual ICollection<SupplierContact> SupplierContacts { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        public virtual ICollection<CustomerContact> CustomerContacts { get; set; }

        public virtual ICollection<SaleOrder> SaleOrders { get; set; }

        public virtual ICollection<BuyOrder> BuyOrders { get; set; }

        public virtual ICollection<IncomingGood> IncomingGoods { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
