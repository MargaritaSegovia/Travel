using Domain.TouristPackages;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.TouristPackages.Delete;

internal sealed class DeleteTouristPackageCommandHandler : IRequestHandler<DeleteTouristPackageCommand, ErrorOr<Unit>>
{
    private readonly ITouristPackageRepository _touristPackageRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteTouristPackageCommandHandler(ITouristPackageRepository touristPackageRepository, IUnitOfWork unitOfWork)
    {
        _touristPackageRepository = touristPackageRepository ?? throw new ArgumentNullException(nameof(touristPackageRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteTouristPackageCommand command, CancellationToken cancellationToken)
    {
        if (await _touristPackageRepository.GetByIdAsync(new TouristPackageId(command.Id)) is not TouristPackage touristPackage)
        {
            return Error.NotFound("TouristPackage.NotFound", "The touristPackage with the provide Id was not found.");
        }

        _touristPackageRepository.Delete(touristPackage);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
