using Core.Persistence.Paging;
using RentACar.Application.Features.Brands.Dtos;

namespace RentACar.Application.Features.Brands.Models
{
    public class GetListBrandModel: BasePageableModel
    {
        public IList<GetListBrandDto> Items { get; set; }
    }
}
