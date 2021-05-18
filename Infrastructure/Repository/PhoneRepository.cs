using Domain.Interfaces.Repository;
using Domain.Models;
using Infrastructure.Context;

namespace Infrastructure.Repository
{
    public class PhoneRepository : Repository<Phone>, IPhoneRepository
    {
        public PhoneRepository(DataContext context) : base(context) { }
    }
}
