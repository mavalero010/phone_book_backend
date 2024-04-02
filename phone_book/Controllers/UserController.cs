using Microsoft.AspNetCore.Mvc;
using phone_book.Models;
using phone_book.Services;

namespace phone_book.Controllers
{
    public class UserController  : ControllerBase
    {
        private  UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            try
            {
                var user = _userService.Get(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch
            {
                return StatusCode(500);
            }

        }



        [HttpPost]
        public object Login(string username, string password)
        {
            try
            {

                if (username == null && password == null)
                {
                return null;
                }

                var ct = _userService.Login(username, password);
                if (ct == null)
                {
                    return null;
                }


                return ct;
            }
            catch (Exception e)

            {
                return StatusCode(500);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {

            try {
                var response = _userService.Post(user);
                return Ok(user);
            } catch {
                return StatusCode(500);
            }
        }

    }
}
