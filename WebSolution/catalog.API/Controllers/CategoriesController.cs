using Catalog.Application.Common;
using Catalog.Application.UseCases.Category;
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
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken
    )
    {
        var output = await _mediator.Send(new GetCategoryInput(id), cancellationToken);
        return Ok(output);
    }
}