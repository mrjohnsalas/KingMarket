using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMarket.Model.Models
{
    public class SaleOrderDetail
    {
        [Key]
        [Display(Name = "Sale Order Detail")]
        public int SaleOrderDetailId { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public int SaleOrderId { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(50, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Unit Price")]
        [Required(ErrorMessage = "You must enter {0}")]
        [DisplayFormat(DataFormatString = "{0:C5}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:N3}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        public decimal Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [NotMapped]
        public decimal Total { get { return Quantity*UnitPrice; } }

        public virtual SaleOrder SaleOrder { get; set; }

        public virtual Product Product { get; set; }
    }
}