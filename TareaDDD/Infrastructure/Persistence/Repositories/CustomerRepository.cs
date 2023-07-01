using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{

    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<List<Customer>> GetAll() => await _context.Customers.ToListAsync();
    public async Task<bool> ExistsAsync(CustomerId id) => await _context.Customers.AnyAsync(customer => customer.Id == id);
    public async Task<Customer?> GetByIdAsync(CustomerId id) => await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
    public async Task Add(Customer customer) => await _context.Customers.AddAsync(customer);
    public void UpdateCustomer(Customer customer) => _context.Customers.Update(customer);
    public void Delete(Customer customer) => _context.Customers.Remove(customer);

}