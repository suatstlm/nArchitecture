using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using RentACar.Application.Features.Brands.Models;
using RentACar.Application.Services.Repositories;
using RentACar.Domain.Entities;

namespace RentACar.Application.Features.Brands.Queries.GetListBrand
{
    public class GetListBrandQuery: IRequest<GetListBrandModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetLİstBrandQueryHandler : IRequestHandler<GetListBrandQuery, GetListBrandModel>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;

            public GetLİstBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
            }

            public async Task<GetListBrandModel> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Brand> brands = await _brandRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                GetListBrandModel mappedGetListBrandModel = _mapper.Map<GetListBrandModel>(brands);
                return mappedGetListBrandModel;
            }
        }
    }
}
