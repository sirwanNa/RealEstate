using RealEstate.Application.Commands.RealEstate.Contact;
using RealEstate.Application.DTOs.RealEstate.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebAPI.Controllers.v1.RealEstate
{
    [ApiController]
	[Route("api/v1/[controller]")]
    [Authorize]
    public class ContactController(IMediator mediator) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;
		#region Get/Fetch
		[HttpGet]
		[Route("GetContact/{id}")]
		public async Task<IActionResult> GetContactAsync(Guid id)
		{
			var command = new GetContactCommand
			{
				Id = id
			};
			var model = await _mediator.Send(command);
			return Ok(model);
		}
		[HttpPost]
		[Route("FetchContactsList")]
		public async Task<IActionResult> FetchGetContactsListAsync([FromBody] ContactQueryDTO queryModel)
		{
			var command = new GetContactsListCommand { QueryModel = queryModel };
			var result = await _mediator.Send(command);
			return Ok(result);
		}
		#endregion
		#region Delete
		[HttpDelete]
		[Route("Delete/{id}")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var command = new DeleteContactCommand { Id = id };
			var result = await _mediator.Send(command);
			return Ok(result);
		}
		#endregion
	}
}
