using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Servers
{
    public class UserServer : IUserServer
    {
        private readonly DataContext _context;
        public UserServer(DataContext context)
        {
            _context = context;
        }
        public User Create(User user)
        {
            _context.Users.Add(user);
            user.Id =_context.SaveChanges();

            return user;
        }
        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }

        public User GetById(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
           return user;
        }
    }
}
