using Microsoft.EntityFrameworkCore;
using ReviewApiApp.Domain;
using System.Collections.Generic;
using System.Reflection.Emit;
namespace ReviewApiApp
{
    public class helperFolder
    {
        public static void SeedData(ModelBuilder moderbuilder)
        {
            List<Production> listpr = new List<Production>
            {
               new Production{Id=343,Name="cara"} ,
               new Production{Id=893,Name="cara"} ,
               new Production{Id=90,Name="cara"} 
            };

            moderbuilder.Entity<Production>().HasData(listpr);

            

        }
    }
}
