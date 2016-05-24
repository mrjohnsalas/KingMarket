using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMarket.Model.Models
{
    public class IncomingGoodDetail
    {
        [Key]
        [Display(Name = "Incoming Good Detail")]
        public int IncomingGoodDetailId { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public int IncomingGoodId { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        [DisplayFormat(DataFormatString = "{0:N3}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        public decimal Quantity { get; set; }

        public virtual IncomingGood IncomingGood { get; set; }

        public virtual Product Product { get; set; }
    }
}
