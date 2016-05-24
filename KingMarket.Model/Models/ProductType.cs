using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMarket.Model.Models
{
    public class ProductType
    {
        [Key]
        [Display(Name = "Product Type")]
        public int ProductTypeId { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(50, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 3)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
