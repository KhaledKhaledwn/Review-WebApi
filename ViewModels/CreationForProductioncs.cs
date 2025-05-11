using ReviewApiApp.Domain;

namespace ReviewApiApp.ViewModels
{
    public class CreationForProductioncs
    {
        public string Name { get; set; }

        public List<Brand> Brands { get; set; } = new List<Brand>();
    }
}
