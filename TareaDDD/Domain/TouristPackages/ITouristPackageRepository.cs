namespace Domain.TouristPackages;

public interface ITouristPackageRepository
{

    Task<TouristPackage?> GetByIdWithLineItemAsync(TouristPackageId id);
    Task<TouristPackage?> GetByIdAsync(TouristPackageId id);
    bool HasOneLineItem(TouristPackage touristpackage);
    Task<List<TouristPackage>> GetAll();
    void Add(TouristPackage touristpackage);
    Task<bool> ExistsAsync(TouristPackageId id);
    void Update(TouristPackage touristpackage);
    void Delete(TouristPackage touristpackage);
}