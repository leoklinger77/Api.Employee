using Domain.Interfaces.Repository;
using Domain.Models;
using Infrastructure.Context;

namespace Infrastructure.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(DataContext context) : base(context) { }
        
    }
}
