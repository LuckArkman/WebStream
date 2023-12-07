using catalog.API.ApiModels;
using Catalog.Application.Common;
using Catalog.Application.UseCases.Category;
using Catalog.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace catalog.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator logger)
        => _mediator = logger;

    [HttpPost]
    [ProducesResponseType(typeof(CategoryModelOutput), 
        StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), 
        StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), 
        StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), 
        StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryInput input , CancellationToken _cancellationToken)
    {
        var _output = await _mediator.Send(input, _cancellationToken);
        return CreatedAtAction(nameof(Create), new {_output.Id}, _output);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CategoryModelOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken
    )
    {
        var output = await _mediator.Send(new  GetCategoryInput(id), cancellationToken);
        return Ok(output);
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<CategoryModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(
        [FromBody] UpdateCategoryApiInput apiInput,
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var input = new UpdateCategoryInput(
            id,
            apiInput.Name,
            apiInput.Description,
            apiInput.IsActive
        );
        var output = await _mediator.Send(input, cancellationToken);
        return Ok(new ApiResponse<CategoryModelOutput>(output));
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await _mediator.Send(new DeleteCategoryInput(id), cancellationToken);
        return NoContent();
    }
}