using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMarket.Model.Models
{
    public class EmployeeType
    {
        [Key]
        [Display(Name = "Employee Type")]
        public int EmployeeTypeId { get; set; }

        [Display(Name = "Employee Type Name")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(50, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 3)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
