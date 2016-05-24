using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMarket.Model.Models
{
    public class ProductPhoto
    {
        [Key]
        [Display(Name = "Product Photo")]
        public int ProductPhotoId { get; set; }

        [Display(Name = "File Name")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 4)]
        public string FileName { get; set; }

        [Display(Name = "Content Type")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 4)]
        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        public FileType FileType { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
