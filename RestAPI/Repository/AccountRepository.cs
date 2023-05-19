using RestAPI.Context;
using RestAPI.Contracts;
using RestAPI.Model;

namespace RestAPI.Repository
{
    public class AccountRepository : IUniversityRepository<Account>
    {
        private readonly BookingManagementContext _context;

        public AccountRepository(BookingManagementContext context)
        {
            _context = context;
        }
        public Account Create(Account Account)
        {
            try
            {
                _context.Set<Account>().Add(Account);
                _context.SaveChanges();
                return Account;
            }
            catch
            {
                return new Account();
            }
        }

        public bool Delete(Guid guid)
        {
            try
            {
                var account = GetByGuid(guid);
                if (account is null)
                {
                    return false;
                }
                _context.Set<Account>().Remove(account);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<Account> GetAll()
        {
            return _context.Set<Account>().ToList();
        }

        public Account GetByGuid(Guid guid)
        {
            return _context.Set<Account>().Find(guid);
        }

        public bool Update(Account account)
        {
            try
            {
                _context.Set<Account>().Update(account);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
