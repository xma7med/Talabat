using AutoMapper;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using Talabat.Dashboard.Models;

namespace Talabat.Dashboard.Helpers
{
    public class MapsProfile: Profile
    {
        public MapsProfile()
        {
            CreateMap<Product , ProductViewModel>().ReverseMap();   
        }
    }
}
