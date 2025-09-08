using RealEstate.Application.Commands.Blog.Article;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;

namespace RealEstate.WebAPI.Controllers.enduser
{
    [Route("api/enduser/[controller]")]
    [ApiController]
    public class ArticleController(IMediator mediator) : ControllerBase 
    {     
        private readonly IMediator _mediator = mediator;
        [HttpGet]
        [Route("GetArticlsList")]
        public async Task<IActionResult> GetArticlsListAsync([FromQuery] int totalOfItems, [FromQuery] Language language)
        {           
            var command = new GetArticleListEndUserCommand
            {
                TotalOfItems = totalOfItems,
                Language = language
            };
            var model = await _mediator.Send(command);
            return Ok(model);
        }

        [HttpGet]
        [Route("GetArticle")]
        public async Task<IActionResult> GetArticleAsync([FromQuery] string urlTitle, [FromQuery] Language language)
        {
            var command = new GetArticleEndUserCommand
            {
                UrlTitle = urlTitle,
                Language = language
            };
            var model = await _mediator.Send(command);
            return Ok(model);
        }
    }
}
