using Domain.Customers;
using Domain.TouristPackages;
using Domain.Reservations;
using Domain.Primitives;
using Domain.Destinations;
using ErrorOr;
using MediatR;

namespace Application.TouristPackages.Update
{
    public sealed class UpdateTouristPackageCommandHandler : IRequestHandler<UpdateTouristPackageCommand, ErrorOr<Unit>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITouristPackageRepository _touristPackageRepository;
        private readonly IDestinationRepository _destinationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTouristPackageCommandHandler(
            IReservationRepository reservationRepository,
            ICustomerRepository customerRepository,
            ITouristPackageRepository touristPackageRepository,
            IDestinationRepository destinationRepository,
            IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _customerRepository = customerRepository;
            _touristPackageRepository = touristPackageRepository;
            _destinationRepository = destinationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateTouristPackageCommand command, CancellationToken cancellationToken)
        {
            if (!await _touristPackageRepository.ExistsAsync(new TouristPackageId(command.Id)))
            {
                return Error.NotFound("Customer.NotFound", "The customer with the provided Id was not found.");
            }

            var touristPackage = await _touristPackageRepository.GetByIdAsync(new TouristPackageId(command.Id));

            touristPackage.Update(
                command.Name,
                command.Description,
                command.TravelDate,
                command.Price);

            if (!command.Items.Any())
            {
                return Error.Conflict("TouristPackage.Detail", "To update a tourist package, you need to specify the line items.");
            }

            var existingLineItemIds = touristPackage.LineItems.Select(li => li.Id).ToList();

            foreach (var item in command.Items)
            {
                if (existingLineItemIds.Contains(new LineItemId(item.DestinationId)))
                {
                    touristPackage.UpdateLineItem(new LineItemId(item.DestinationId), new DestinationId(item.DestinationId));
                }
                else
                {
                    touristPackage.Add(new DestinationId(item.DestinationId));
                }
            }

            var lineItemsToRemove = touristPackage.LineItems.Where(li => !command.Items.Any(item => new LineItemId(item.DestinationId) == li.Id)).ToList();
            foreach (var lineItem in lineItemsToRemove)
            {
                touristPackage.RemoveLineItem(lineItem.Id, _touristPackageRepository);
            }



            foreach (var item in command.Items)
            {
                touristPackage.Add(new DestinationId(item.DestinationId));
            }

            _touristPackageRepository.Update(touristPackage);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
