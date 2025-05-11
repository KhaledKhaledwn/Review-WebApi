using ReviewApiApp.Domain;

namespace ReviewApiApp.ViewModels
{
    public class ProductForUpdating
    {
        public string Name { get; set; } = string.Empty;

        public List<Brand> Brands { get; set; } = new List<Brand>();
    }
}
