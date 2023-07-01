using ErrorOr;
using MediatR;
using Application.TouristPackages.Common;

namespace Application.TouristPackages.GetAll;

public record GetAllTouristPackagesQuery() : IRequest<ErrorOr<IReadOnlyList<TouristPackageResponse>>>;