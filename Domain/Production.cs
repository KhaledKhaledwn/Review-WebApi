namespace ReviewApiApp.Domain
{
    public class Production
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Brand> Brands { get; set; }= new List<Brand>();
    }
}
