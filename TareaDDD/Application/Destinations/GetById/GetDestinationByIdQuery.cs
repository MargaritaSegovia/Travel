using Application.Destinations.Common;
using ErrorOr;
using MediatR;

namespace Application.Destinations.GetById;

public record GetDestinationByIdQuery(Guid Id) : IRequest<ErrorOr<DestinationResponse>>;