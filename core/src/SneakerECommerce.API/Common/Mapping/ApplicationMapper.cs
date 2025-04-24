using AutoMapper;
using Sneaker_Ecommerce.Domain.Entity;
using SneakerECommerce.Application.DTOs.Request;
using SneakerECommerce.Application.DTOs.Response;
using StoreManagement.Application.DTOs.Auth;

namespace SneakerECommerce.API.Common.Mapping
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, RegisterDTO>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();
        }
    }
}
