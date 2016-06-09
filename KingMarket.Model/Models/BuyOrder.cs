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

        [Display(Name = "Date Order")]
        [Required(ErrorMessage = "You must enter {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOrder { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public int DocumentTypeId { get; set; }

        //[Display(Name = "Document Number")]
        //[Required(ErrorMessage = "You must enter {0}")]
        //[StringLength(30, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 8)]
        //public string DocumentNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = false)]
        public decimal Tax { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public int StatusId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual Status Status { get; set; }

        public virtual ICollection<BuyOrderDetail> BuyOrderDetails { get; set; }

        public virtual ICollection<IncomingGood> IncomingGoods { get; set; }
    }
}
