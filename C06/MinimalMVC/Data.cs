using MinimalMVC.Models;
using System.Collections.Immutable;

namespace MinimalMVC.Data
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> AllAsync(
            CancellationToken cancellationToken);

        Task<Customer> CreateAsync(
            Customer customer,
            CancellationToken cancellationToken);

        Task<Customer?> DeleteAsync(
            int customerId,
            CancellationToken cancellationToken);

        Task<Customer?> FindAsync(
            int customerId,
            CancellationToken cancellationToken);

        Task<Customer?> UpdateAsync(
            Customer customer,
            CancellationToken cancellationToken);
    }
    public class CustomerRepository : ICustomerRepository
    {
        public Task<Customer?> FindAsync(int customerId, CancellationToken cancellationToken)
        {
            var entity = MemoryDataStore.Customers.Find(x => x.Id == customerId);
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<Customer>> AllAsync(CancellationToken cancellationToken)
        {
            var entities = MemoryDataStore.Customers.ToImmutableArray().AsEnumerable();
            return Task.FromResult(entities);
        }

        public Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken)
        {
            var lastId = FindLastCustomerId();
            var lastContractId = FindLastContractId();
            var contracts = customer.Contracts
                .Select(contract => contract with
                {
                    Id = ++lastContractId
                })
                .ToList()
            ;

            var newCustomer = customer with
            {
                Id = lastId + 1,
                Contracts = contracts
            };

            MemoryDataStore.Customers.Add(newCustomer);
            return Task.FromResult(newCustomer);
        }

        public Task<Customer?> UpdateAsync(Customer customer, CancellationToken cancellationToken)
        {
            var index = MemoryDataStore.Customers.FindIndex(x => x.Id == customer.Id);
            if (index == -1)
            {
                return Task.FromResult(default(Customer));
            }
            MemoryDataStore.Customers[index] = customer;
            return Task.FromResult<Customer?>(customer);
        }

        public Task<Customer?> DeleteAsync(int customerId, CancellationToken cancellationToken)
        {
            var index = MemoryDataStore.Customers.FindIndex(x => x.Id == customerId);
            if (index == -1)
            {
                return Task.FromResult(default(Customer));
            }
            var customer = MemoryDataStore.Customers[index];
            MemoryDataStore.Customers.RemoveAt(index);
            return Task.FromResult<Customer?>(customer);
        }

        private int FindLastCustomerId()
        {
            if (MemoryDataStore.Customers.Count == 0)
            {
                return 0;
            }
            return MemoryDataStore.Customers.Max(x => x.Id);
        }

        private int FindLastContractId()
        {
            if (MemoryDataStore.Customers.Count == 0)
            {
                return 0;
            }
            var contracts = MemoryDataStore.Customers.SelectMany(c => c.Contracts).Select(x => x.Id);
            if (!contracts.Any())
            {
                return 0;
            }
            return contracts.Max();
        }
    }
    internal static class MemoryDataStore
    {
        public static List<Customer> Customers { get; } = new();

        public static void Seed()
        {
            Customers.Add(new Customer(
                Id: 1,
                Name: "Jonny Boy Inc.",
                Contracts: new List<Contract>
                {
                new Contract(
                    Id: 1,
                    Name: "First contract",
                    Description: "This is the first contract.",
                    PrimaryContact: new ContactInformation(
                        FirstName: "John",
                        LastName: "Doe",
                        Email: "john.doe@jonnyboy.com"
                    ),
                    Status: new WorkStatus(
                        TotalWork: 100,
                        WorkDone: 100
                    )
                ),
                new Contract(
                    Id: 2,
                    Name: "Some other contract",
                    Description: "This is another contract.",
                    PrimaryContact: new ContactInformation(
                        FirstName: "Jane",
                        LastName: "Doe",
                        Email: "jane.doe@jonnyboy.com"
                    ),
                    Status: new WorkStatus(
                        TotalWork: 100,
                        WorkDone: 25
                    )
                )
                }
            ));
            Customers.Add(new Customer(
                Id: 2,
                Name: "Some mega-corporation",
                Contracts: new List<Contract>
                {
                new Contract(
                    Id: 3,
                    Name: "Huge contract",
                    Description: "This is a huge contract.",
                    PrimaryContact: new ContactInformation(
                        FirstName: "Kory",
                        LastName: "O'Neill",
                        Email: "kory.oneill@megacorp.com"
                    ),
                    Status: new WorkStatus(
                        TotalWork: 15000,
                        WorkDone: 0
                    )
                )
                }
            ));
        }
    }
}
