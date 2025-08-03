using TransporteDigitalRD.Application.Interfaces;
//using TransporteDigitalRD.Data.Conexion;
using TransporteDigitalRD.Data.Entities;

namespace TransporteDigitalRD.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        //private readonly DbTransporteDDataContext _context;

        public UserRepository()
        {
            //_context = context;
        }

        public Task AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        /*   public async Task<User?> GetByEmailAsync(string email)
           {
               return await Task.Run(() =>
                 //  _context.Users.FirstOrDefault(u => u.Email == email));
           }

           public async Task AddAsync(User user)
           {
               _context.Users.InsertOnSubmit(user);
               await Task.Run(() => _context.SubmitChanges());
           }*/
    }
}
