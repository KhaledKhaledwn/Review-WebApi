namespace ReviewApiApp.ViewModels
{
    public class ProductionSummary
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FullInfo { get; set; } = string.Empty;

        public string Anything {  get; set; }

        // Note: must Names of variables are the same in summary class and source class else will be null values in output.

    }
}
