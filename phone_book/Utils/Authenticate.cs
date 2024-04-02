using Microsoft.AspNetCore.Mvc;
using phone_book.Data;
using phone_book.Models;
using System.Net;

namespace phone_book.Utils
{
    public class Authenticate
    {

        public  PhoneBookDb _dbContext;

        public Authenticate( PhoneBookDb dbContext) { 
         
            _dbContext = dbContext;
        }

        public User Authenticating(string _username, string _password)
        {
            try {
                var existingUser = _dbContext.User.FirstOrDefault(ct => ct.UserName == _username);
                

                if (existingUser == null || existingUser.Password != _password)
                {
                    return null;
                }


                return existingUser;

            }
            catch (Exception e){
                Console.WriteLine(e);
                return null;
            }
            
        
        }
    }
}
