using Domain.Interfaces.Repository;
using Domain.Models;
using Infrastructure.Context;

namespace Infrastructure.Repository
{
    public class EmailRepository : Repository<Email>, IEmailRepository
    {
        public EmailRepository(DataContext context) : base(context) { }
    }
}
