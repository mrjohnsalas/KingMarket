using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMarket.Model.Models
{
    public class SupplierContact
    {
        [Key]
        [Display(Name = "Supplier Contact")]
        public int SupplierContactId { get; set; }

        [Display(Name = "Supplier")]
        [Required(ErrorMessage = "You must enter {0}")]
        public int SupplierId { get; set; }

        [Display(Name = "Document Type")]
        [Required(ErrorMessage = "You must enter {0}")]
        public int DocumentTypeId { get; set; }

        [Display(Name = "Document Number")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(11, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 8)]
        [Index(IsUnique = true)]
        public string DocumentNumber { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(50, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(50, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string LastName { get; set; }

        [Display(Name = "Second Last Name")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(50, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string SecondLastName { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(80, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 10)]
        [DataType(DataType.EmailAddress)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(15, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 7)]
        [DataType(DataType.PhoneNumber)]
        [Index(IsUnique = true)]
        public string Phone { get; set; }

        [NotMapped]
        public string FullName { get { return string.Format("{0} {1} {2}", LastName, SecondLastName, FirstName); } }

        public virtual DocumentType DocumentType { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
