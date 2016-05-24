using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMarket.Model.Models
{
    public class Supplier
    {
        [Key]
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }

        [Display(Name = "Document Type")]
        [Required(ErrorMessage = "You must enter {0}")]
        public int DocumentTypeId { get; set; }

        [Display(Name = "Document Number")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(11, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 8)]
        [Index(IsUnique = true)]
        public string DocumentNumber { get; set; }

        [Display(Name = "Business Name")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 5)]
        [Index(IsUnique = true)]
        public string BusinessName { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 10)]
        public string Address { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(80, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 10)]
        [DataType(DataType.EmailAddress)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [DataType(DataType.Url)]
        public string Web { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(15, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 7)]
        [DataType(DataType.PhoneNumber)]
        [Index(IsUnique = true)]
        public string Phone { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual ICollection<SupplierContact> SupplierContacts { get; set; }

        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }

        public virtual ICollection<BuyOrder> BuyOrders { get; set; }
    }
}
