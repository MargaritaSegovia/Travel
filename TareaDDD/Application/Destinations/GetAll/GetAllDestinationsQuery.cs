using ErrorOr;
using MediatR;
using Application.Destinations.Common;

namespace Application.Destinations.GetAll;

public record GetAllDestinationsQuery() : IRequest<ErrorOr<IReadOnlyList<DestinationResponse>>>;