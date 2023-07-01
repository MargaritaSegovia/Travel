namespace Domain.Customers;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(CustomerId id);
    Task<List<Customer>> GetAll();
    Task Add(Customer customer);
    Task<bool> ExistsAsync(CustomerId id);
    void UpdateCustomer(Customer customer);
    void Delete(Customer customer);
}