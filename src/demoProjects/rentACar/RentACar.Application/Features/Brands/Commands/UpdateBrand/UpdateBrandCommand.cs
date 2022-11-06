using AutoMapper;
using MediatR;
using RentACar.Application.Features.Brands.Dtos;
using RentACar.Application.Features.Brands.Rules;
using RentACar.Application.Services.Repositories;
using RentACar.Domain.Entities;

namespace RentACar.Application.Features.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommand: IRequest<UpdatedBrandDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Todo shoult add validate for when data update .
        public class UpdateBrandCommandHanler : IRequestHandler<UpdateBrandCommand, UpdatedBrandDto>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;
            private readonly BrandBusinessRules _brandBusinessRules;

            public UpdateBrandCommandHanler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;
            }

            public async Task<UpdatedBrandDto> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
            {
                await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);
                

                Brand mappedBrand = _mapper.Map<Brand>(request);
                _brandBusinessRules.BrandSouldExistWhenRequested(mappedBrand);

                Brand updatedBrand = await _brandRepository.UpdateAsync(mappedBrand);
                UpdatedBrandDto updatedBrandDto = _mapper.Map<UpdatedBrandDto>(updatedBrand);

                return updatedBrandDto;
            }
        }
    }
}
