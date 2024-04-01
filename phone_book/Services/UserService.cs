using phone_book.Data;
using phone_book.Models;
using phone_book.Utils;

namespace phone_book.Services
{
    public class UserService
    {
        private readonly PhoneBookDb _dbContext;
        private readonly Authenticate _auth;
        public UserService(PhoneBookDb dbContext) {
            _dbContext = dbContext;
        }

        public int Login(string username, string password)
        {
            try
            {
                var user = _auth.Authenticating(username, password);

                if(user == null)
                {
                    return 401;
                }

                return 200;
            }
            catch
            {
                return 500;
            }
        }

        public int Post(User user)
        {
            try {
                var u = _dbContext.User.FirstOrDefault(i => i.UserName == user.UserName);

                if (u != null)
                {
                    //Existing Contact
                    return 409;
                }
                _dbContext.User.Add(user);
                _dbContext.SaveChanges();
                return 200;
            } catch (Exception e){
                Console.WriteLine(e);
                return 500;
            }

        }

        public User Get(int id)
        {
            try {
                var r = _dbContext.User.Find(id);

                return r;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
