using Domain.TouristPackages;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class TouristPackageRepository : ITouristPackageRepository
    {
        private readonly ApplicationDbContext _context;

        public TouristPackageRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Add(TouristPackage touristPackage) => _context.Add(touristPackage);
        public void Update(TouristPackage touristPackage) => _context.TouristPackages.Update(touristPackage);
        public void Delete(TouristPackage touristPackage) => _context.TouristPackages.Remove(touristPackage);

        public async Task<bool> ExistsAsync(TouristPackageId id) => await _context.TouristPackages.AnyAsync(touristPackage => touristPackage.Id == id);

        public async Task<TouristPackage?> GetByIdWithLineItemAsync(TouristPackageId id)
        {
            return await _context.TouristPackages
                .Include(o => o.LineItems)
                .SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<TouristPackage?> GetByIdAsync(TouristPackageId id)
        {
            return await _context.TouristPackages
                .Include(o => o.LineItems)
                .SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<TouristPackage>> GetAll()
        {
            return await _context.TouristPackages
                .Include(o => o.LineItems)
                .ToListAsync();
        }

        public bool HasOneLineItem(TouristPackage touristpackage)
        {
            throw new NotImplementedException();
        }
    }
}
