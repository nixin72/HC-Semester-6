using System.ComponentModel.DataAnnotations;

namespace RAC.RACModels
{
    public class ElementViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You Must Enter a description")]
        [Display(Name = "Description:")]
        public string Description { get; set; }
    }
}