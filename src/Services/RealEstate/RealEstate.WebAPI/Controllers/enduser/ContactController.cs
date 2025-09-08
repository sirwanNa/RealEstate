using RealEstate.Application.Commands.RealEstate.Contact;
using RealEstate.Application.DTOs.RealEstate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebAPI.Controllers.enduser
{
    [Route("api/enduser/[controller]")]
    [ApiController]
    public class ContactController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        #region Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] ContactDTO model)
        {
            var command = new CreateContactCommand { Model = model };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        #endregion
    }
}
