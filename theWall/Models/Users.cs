using System.ComponentModel.DataAnnotations;

namespace logreg.Models
{
    public abstract class BaseEntity {}
    public class Reg: BaseEntity
    {
        [Display(Name = "First Name")]
        [Required]
        [MinLength(2)]
        [RegularExpression("[a-zA-Z]*")]
        public string fname { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [MinLength(2)]
        [RegularExpression("[a-zA-Z]*")]
        public string lname { get; set; }
        
        [Display(Name = "Confirm Password")]
        [Required]
        [Compare(nameof(pw))]
        [DataType(DataType.Password)]
        public string cpw { get; set; }
        
        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string email { get; set; }
        
        [Display(Name = "Password")]
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string pw { get; set; }
    }

    public class Log: BaseEntity
    {
        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string email { get; set; }
        
        [Display(Name = "Password")]
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string pw { get; set; }
    }

    public class Index: BaseEntity
    {
        public Reg regdetails { get; set; } 
        public Log logdetails { get; set; }
    }
} 