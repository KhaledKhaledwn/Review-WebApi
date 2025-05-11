namespace ReviewApiApp.Domain
{
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        // relationship

        public int ProductionId { get; set; }
    }
}
