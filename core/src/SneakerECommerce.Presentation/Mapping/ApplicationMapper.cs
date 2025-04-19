using AutoMapper;
using Sneaker_Ecommerce.Domain.Entity;
using SneakerECommerce.Application.DTOs;
using StoreManagement.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerECommerce.Presentation.Mapping
{
    public class ApplicationMapper :  Profile
    {
        public ApplicationMapper() 
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, RegisterDTO>().ReverseMap();
        }
    }
}
