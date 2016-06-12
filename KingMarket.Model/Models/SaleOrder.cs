using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMarket.Model.Models
{
    public class SaleOrder
    {
        [Key]
        [Display(Name = "Sale Order")]
        public int SaleOrderId { get; set; }

        [Display(Name = "Date Order")]
        [Required(ErrorMessage = "You must enter {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOrder { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public int DocumentTypeId { get; set; }

        [Display(Name = "Document Number")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(30, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 8)]
        public string DocumentNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        [NotMapped]
        public decimal Tax { get { return SubTotal * 0.18M; } }

        [Required(ErrorMessage = "You must enter {0}")]
        public int StatusId { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        [NotMapped]
        public decimal SubTotal
        {
            get
            {
                var total = 0M;
                if(SaleOrderDetails != null && SaleOrderDetails.Count > 0)
                    SaleOrderDetails.ToList().ForEach(d => total += d.Total);
                return total;
            }
        }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        [NotMapped]
        public decimal Total { get {return SubTotal + Tax;} }

        public virtual Customer Customer { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual Status Status { get; set; }

        public virtual ICollection<SaleOrderDetail> SaleOrderDetails { get; set; }
    }
}
