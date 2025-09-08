using System.Security.Cryptography.Xml;
using RealEstate.Application.Commands.Setting.Constant;
using RealEstate.Application.DTOs.Setting;
using RealEstate.Application.DTOs.Setting.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebAPI.Controllers.v1.Setting
{
    [ApiController]
    [Route("api/v1/[controller]")]
	//[Authorize]
	public class ConstantController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        #region Get/Fetch
        [HttpGet]
        [Route("GetConstant/{id}")]
        public async Task<IActionResult> GetConstant(Guid id)
        {
            var command = new GetConstantCommand
            {
                Id = id
            };
            var model = await _mediator.Send(command);
            return Ok(model);
        }
        [HttpPost]
        [Route("FetchConstantsList")]
        public async Task<IActionResult> FetchContractsListAsync([FromBody] ConstantQueryDTO queryModel)
        {
			var command = new GetConstantsListCommand { QueryModel = queryModel };
			var result = await _mediator.Send(command);
			return Ok(result);
		}
        #endregion
        #region Post/Update
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] ConstanDTO model)
        {
            var command = new CreateConstantCommand { Model = model };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
		[HttpPut]
		[Route("Update")]
		public async Task<IActionResult> UpdateAsync([FromBody] ConstanDTO model)
		{
			var command = new UpdateConstantCommand { Model = model };
			var result = await _mediator.Send(command);
			return Ok(result);
		}
        #endregion
        #region Delete
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteConstantCommand { Id = id };
			var result = await _mediator.Send(command);
			return Ok(result);
		}
		#endregion
	}
}
