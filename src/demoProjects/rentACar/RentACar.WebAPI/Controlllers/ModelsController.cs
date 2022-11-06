using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;
using RentACar.Application.Features.Models.Models;
using RentACar.Application.Features.Models.Queries.GetListModel;
using RentACar.Application.Features.Models.Queries.GetListModelByDynamic;

namespace RentACar.WebAPI.Controlllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListModelQuery getListBrandQuery = new() { PageRequest = pageRequest };
            GetListModelModel result = await Mediator.Send(getListBrandQuery);
            return Ok(result);
        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListModelByDynamicQuery getListModelByDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };
            GetListModelModel result = await Mediator.Send(getListModelByDynamicQuery);
            return Ok(result);
        }
    }
}
