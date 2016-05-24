using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMarket.Model.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(50, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 3)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Unit Price")]
        [Required(ErrorMessage = "You must enter {0}")]
        [DisplayFormat(DataFormatString = "{0:C5}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        //[GreaterThan("UnitCost")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Unit Cost")]
        [Required(ErrorMessage = "You must enter {0}")]
        [DisplayFormat(DataFormatString = "{0:C5}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        public decimal UnitCost { get; set; }

        [Display(Name = "Product Type")]
        [Required(ErrorMessage = "You must enter {0}")]
        public int ProductTypeId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N3}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        public decimal Stock { get; set; }

        [Display(Name = "Min Stock")]
        [Required(ErrorMessage = "You must enter {0}")]
        [DisplayFormat(DataFormatString = "{0:N3}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        public decimal MinStock { get; set; }

        [Display(Name = "Max Stock")]
        [Required(ErrorMessage = "You must enter {0}")]
        [DisplayFormat(DataFormatString = "{0:N3}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        //[GreaterThan("MinStock")]
        public decimal MaxStock { get; set; }

        [Display(Name = "Unit Measure")]
        [Required(ErrorMessage = "You must enter {0}")]
        public int UnitMeasureId { get; set; }

        public virtual ProductType ProductType { get; set; }

        public virtual UnitMeasure UnitMeasure { get; set; }

        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }

        public virtual ICollection<SaleOrderDetail> SaleOrderDetails { get; set; }

        public virtual ICollection<BuyOrderDetail> BuyOrderDetails { get; set; }

        public virtual ICollection<IncomingGoodDetail> IncomingGoodDetails { get; set; }

        public virtual ICollection<ProductPhoto> ProductPhotos { get; set; }
    }
}
