using Core.Persistence.Repositories;
using RentACar.Application.Services.Repositories;
using RentACar.Domain.Entities;
using RentACar.Persistance.Contexts;

namespace RentACar.Persistance.Repositories
{
    public class BrandRepository: EfRepositoryBase<Brand, BaseDbContext>, IBrandRepository
    {
        public BrandRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
