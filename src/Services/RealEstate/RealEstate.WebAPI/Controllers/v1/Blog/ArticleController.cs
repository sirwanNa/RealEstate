using RealEstate.Application.Commands.Blog.Article;
using RealEstate.Application.DTOs.Blog;
using RealEstate.Application.DTOs.Blog.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;

namespace RealEstate.WebAPI.Controllers.v1.Blog
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ArticleController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        #region Get/Fetch
        [HttpGet]
        [Route("GetArticle/{id}")]
        public async Task<IActionResult> GetArticleAsync(Guid id)
        {
            var command = new GetArticleCommand
            {
                Id = id               
            };
            var model = await _mediator.Send(command);
            return Ok(model);
        }

        [HttpPost]
        [Route("FetchArticlesList")]
        public async Task<IActionResult> FetchArticlesListAsync([FromBody] ArticleQueryDTO queryModel)
        {
            var command = new GetArticlesListCommand { QueryModel = queryModel };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        #endregion
        #region Post/Update
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] ArticleDTO model)
        {
            var command = new CreateArticleCommand { Model = model };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] ArticleDTO model)
        {
            var command = new UpdateArticleCommand { Model = model };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        #endregion
        #region Delete
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteArticleCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        #endregion
    }
}
