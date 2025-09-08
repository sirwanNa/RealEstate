using RealEstate.Application.Commands.RealEstate.PropertyInventory;
using RealEstate.Application.DTOs.RealEstate;
using RealEstate.Application.DTOs.RealEstate.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebAPI.Controllers.v1.RealEstate
{
    [ApiController]
    [Route("api/v1/[controller]")]
    //[Authorize]
    public class PropertyInventoryController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        #region Get/Fetch
        [HttpGet]
        [Route("GetPropertyInventory/{id}")]
        public async Task<IActionResult> GetPropertyInventoryAsync(Guid id)
        {
            var command = new GetPropertyInventoryCommand
            {
                Id = id
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
        #endregion
        #region Post/Update
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] PropertyInventoryDTO model)
        {
            var command = new CreatePropertyInventoryCommand { Model = model };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost]
        [Route("CreateFromFiles")]
        public async Task<IActionResult> CreateFromFilesAsync([FromBody] PropertyInventoryFromFilesDTO model)
        {
            var command = new CreatePropertyInventoryFromFilesCommand { Model = model };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] PropertyInventoryDTO model)
        {
            var command = new UpdatePropertyInventoryCommand { Model = model };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        #endregion
        #region Delete
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeletePropertyInventoryCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        #endregion
    }
}
