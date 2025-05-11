using System.ComponentModel.DataAnnotations;

namespace ReviewApiApp.ViewModels
{
    public class BrandForCreation
    {
        [Required(ErrorMessage = "pleae fill this field because it is  rquired..") ]
        public string Name { get; set; }
    }
}
