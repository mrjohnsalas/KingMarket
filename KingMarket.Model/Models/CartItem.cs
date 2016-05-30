using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMarket.Model.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public string UserId { get; set; }

        [Display(Name = "Date Created")]
        [Required(ErrorMessage = "You must enter {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public int ProductId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N3}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        public decimal Quantity { get; set; }

        public virtual Product Product { get; set; }
    }
}