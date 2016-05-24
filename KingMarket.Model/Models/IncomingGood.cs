using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMarket.Model.Models
{
    public class IncomingGood
    {
        [Key]
        [Display(Name = "Incoming Good")]
        public int IncomingGoodId { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public int BuyOrderId { get; set; }

        [Display(Name = "Incoming Date")]
        [Required(ErrorMessage = "You must enter {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime IncomingDate { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public int DocumentTypeId { get; set; }

        [Display(Name = "Document Number")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(30, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 8)]
        public string DocumentNumber { get; set; }

        public virtual BuyOrder BuyOrder { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual ICollection<IncomingGoodDetail> IncomingGoodDetails { get; set; }
    }
}
