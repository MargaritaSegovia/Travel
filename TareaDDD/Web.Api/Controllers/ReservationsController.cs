using Application.Reservations.Create;
using MediatR;
using ErrorOr;
using Application.Reservations.GetAll;
using Microsoft.AspNetCore.Mvc;
using Application.Reservations.Update;
using Application.Reservations.GetById;
using Application.Reservations.Delete;

namespace Web.Api.Controllers;

[Route("Reservation")]
public class Reservations : ApiController
{
    private readonly ISender _mediator;

    public Reservations(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reservationsResult = await _mediator.Send(new GetAllReservationsQuery());

        return reservationsResult.Match(
            Reservation => Ok(reservationsResult.Value),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReservationCommand command)
    {
        var createReservationResult = await _mediator.Send(command);

        return createReservationResult.Match(
            Reservation => Ok(createReservationResult.Value),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReservationCommand command)
    {
        if (command.Id != id)
        {
            List<Error> errors = new()
            {
                Error.Validation("Reservation.UpdateInvalid", "The request Id does not match with the url Id.")
            };
            return Problem(errors);
        }

        var updateReservationResult = await _mediator.Send(command);

        return updateReservationResult.Match(
            ReservationId => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var reservationResult = await _mediator.Send(new GetReservationByIdQuery(id));

        return reservationResult.Match(
            reservation => Ok(reservation),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteReservationResult = await _mediator.Send(new DeleteReservationCommand(id));

        return deleteReservationResult.Match(
            ReservationId => NoContent(),
            errors => Problem(errors)
        );
    }
}