using Application.TouristPackages.Common;
using ErrorOr;
using MediatR;

namespace Application.TouristPackages.GetById;

public record GetTouristPackageByIdQuery(Guid Id) : IRequest<ErrorOr<TouristPackageResponse>>;