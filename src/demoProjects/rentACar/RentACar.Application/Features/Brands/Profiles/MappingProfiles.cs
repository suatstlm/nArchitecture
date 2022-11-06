using AutoMapper;
using Core.Persistence.Paging;
using RentACar.Application.Features.Brands.Commands.CreateBrand;
using RentACar.Application.Features.Brands.Commands.DeleteBrand;
using RentACar.Application.Features.Brands.Commands.UpdateBrand;
using RentACar.Application.Features.Brands.Dtos;
using RentACar.Application.Features.Brands.Models;
using RentACar.Domain.Entities;

namespace RentACar.Application.Features.Brands.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Brand, CreatedBrandDto>().ReverseMap();
            CreateMap<Brand, CreateBrandCommand>().ReverseMap();
            CreateMap<IPaginate<Brand>, GetListBrandModel>().ReverseMap();
            CreateMap<Brand, GetListBrandDto>().ReverseMap();
            CreateMap<Brand, GetByIdBrandDto>().ReverseMap(); 
            CreateMap<Brand, DeletedBrandDto>().ReverseMap();
            CreateMap<Brand, UpdatedBrandDto>().ReverseMap();
            CreateMap<Brand, UpdateBrandCommand>().ReverseMap();

        }
    }
}
