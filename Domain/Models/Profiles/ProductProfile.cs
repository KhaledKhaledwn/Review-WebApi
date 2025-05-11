using AutoMapper;
using ReviewApiApp.ViewModels;

namespace ReviewApiApp.Domain.Models.Profiles
{
    public class ProductProfile :Profile
    {

        public ProductProfile()
        {
          //  CreateMap<source,Desination>();
            CreateMap<Production,ProductionSummary>()
                // if variables names are different between dis and source
            .ForMember(d => d.FullInfo,s => s.MapFrom(s =>s.Id +" - "+s.Name))
            .ForMember(d => d.Anything, s => s.Ignore());
        }
    }
}
