using RealEstate.Application.Commands.RealEstate.PropertyInventory;
using RealEstate.Application.DTOs.RealEstate.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;

namespace RealEstate.WebAPI.Controllers.enduser
{
	[ApiController]
	[Route("api/enduser/[controller]")]
	public class PropertyInventoryController(IMediator mediator) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;
		[HttpGet]
		[Route("GetPropertyInventory")]
		public async Task<IActionResult> GetPropertyInventoryAsync([FromQuery] string urlTitle, [FromQuery] Language language, [FromQuery] int totalOfRandomItems)
		{
			var command = new GetPropertyInventoryEndUserCommand
			{
				UrlTitle = urlTitle,
				Language = language,
				TotalOfRandomItems = totalOfRandomItems
			};
			var model = await _mediator.Send(command);
			return Ok(model);
		}
        [HttpPost]
        [Route("FetchPropertyInventoriesList")]
        public async Task<IActionResult> FetchPropertyInventoriesListAsync([FromBody] PropertyInventoryQueryDTO queryModel)
        {
            var command = new GetPropertyInventoriesListCommand { QueryModel = queryModel };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
