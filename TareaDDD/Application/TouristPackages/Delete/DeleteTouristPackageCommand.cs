using ErrorOr;
using MediatR;

namespace Application.TouristPackages.Delete;

public record DeleteTouristPackageCommand(Guid Id) : IRequest<ErrorOr<Unit>>;