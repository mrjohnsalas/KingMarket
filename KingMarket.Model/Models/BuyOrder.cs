using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMarket.Model.Models
{
    public class BuyOrder
    {
        [Key]
        [Display(Name = "Buy Order")]
        public int BuyOrderId { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public string UserId { get; set; }

        [Display(Name = "Date Order")]
        [Required(ErrorMessage = "You must enter {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOrder { get; set; }

        [Display(Name = "Supplier")]
        [Required(ErrorMessage = "You must enter {0}")]
        public int SupplierId { get; set; }

        [Display(Name = "Document Type")]
        [Required(ErrorMessage = "You must enter {0}")]
        public int DocumentTypeId { get; set; }

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
                if (BuyOrderDetails != null && BuyOrderDetails.Count > 0)
                    BuyOrderDetails.ToList().ForEach(d => total += d.Total);
                return total;
            }
        }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        [NotMapped]
        public decimal Total { get { return SubTotal + Tax; } }

        public virtual Supplier Supplier { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual Status Status { get; set; }

        public virtual ICollection<BuyOrderDetail> BuyOrderDetails { get; set; }

        public virtual ICollection<IncomingGood> IncomingGoods { get; set; }
    }
}
