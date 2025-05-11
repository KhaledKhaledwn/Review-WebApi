using ReviewApiApp.Domain;

namespace ReviewApiApp.DataAccessLayer
{
    public class ProductionDataStore
    {
        public List<Production> Productions { get; set; }

     //   public static ProductionDataStore Current { get; set; } = new ProductionDataStore();

        public ProductionDataStore()
        {
            Productions = new List<Production>()
            {
                new Production(){ Id= 1, Name = "Car", Brands = new List<Brand>()
                { 
                new Brand{Id = 1, Name = "hp"},
                new Brand{Id = 2, Name = "Asus"},
                new Brand{Id = 3, Name = "Acer"},
                new Brand{Id = 4, Name = "Lenevo"},
                new Brand{Id = 5, Name = "Dell"},
                }
                
                },
                new Production(){ Id= 2, Name = "Computer", Brands = new List<Brand>()
                {
                new Brand{Id = 1, Name = "Sara"},
                new Brand{Id = 2, Name = "Zara"},
                new Brand{Id = 3, Name = "BMW"},
                } 
                
                },
                new Production(){ Id= 3, Name = "Chair"},
                new Production(){ Id= 4, Name = "Book"},
            }; 
        }

    }
}
