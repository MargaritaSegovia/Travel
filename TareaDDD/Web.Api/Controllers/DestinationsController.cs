using Application.Destinations;
using MediatR;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Application.Destinations.GetAll;
using Web.Api.Controllers;
using Application.Destinations.GetById;
using Application.Destinations.Update;
using Application.Destinations.Delete;


[Route("destinations")]
public class Destinations : ApiController
{
    private readonly ISender _mediator;

    public Destinations(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var destinationsResult = await _mediator.Send(new GetAllDestinationsQuery());

        return destinationsResult.Match(
            Destination => Ok(destinationsResult.Value),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var destinationResult = await _mediator.Send(new GetDestinationByIdQuery(id));

        return destinationResult.Match(
            destination => Ok(destination),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDestinationCommand command)
    {
        var createDestinationResult = await _mediator.Send(command);

        return createDestinationResult.Match(
            Destination => Ok(createDestinationResult.Value),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDestinationCommand command)
    {
        if (command.Id != id)
        {
            List<Error> errors = new()
            {
                Error.Validation("Destination.UpdateInvalid", "The request Id does not match with the url Id.")
            };
            return Problem(errors);
        }

        var updateResult = await _mediator.Send(command);

        return updateResult.Match(
            destinationId => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteResult = await _mediator.Send(new DeleteDestinationCommand(id));

        return deleteResult.Match(
            destinationId => NoContent(),
            errors => Problem(errors)
        );
    }
}