using RentACar.Application.Features.Models.Dtos;

namespace RentACar.Application.Features.Models.Models
{
    public class GetListModelModel
    {
        public IList<GetListModelDto> Items { get; set; }
    }
}
