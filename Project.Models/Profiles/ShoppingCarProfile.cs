using AutoMapper;
using Project.Models.Models.Entity;
using Project.Models.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Profiles
{
   public class ShoppingCarProfile : Profile
    {
        public ShoppingCarProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartViewModel>();
            CreateMap<LineItem, LineItemViewModel>();
        }
    }
}
