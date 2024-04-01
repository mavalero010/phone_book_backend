using Microsoft.AspNetCore.Mvc;
using phone_book.Models;
using phone_book.Services;

namespace phone_book.Controllers
{
    
   
    public class ContactController : ControllerBase
    {
        private readonly ContactService _contactService;

        public ContactController(ContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public IActionResult Get(string username)
        {
            try {
                var contact = _contactService.GetContactByUsername(username);

                if (contact == null)
                {
                    return NotFound();
                }
                
                return Ok(contact);
            } catch {
                return StatusCode(500);
            }

        }

        [HttpPost]
        public IActionResult Post([FromBody] ContactFields info)
        {
            try
            {
                string username = info.username;
                string password = info.password;
                Contact contact = info.C;
                AdditionalFields additionalFields = info.AF;
                if (contact == null)
                {
                    return BadRequest();
                }
                var ct = _contactService.PostContact(contact, additionalFields,username,password);
                if (ct == 409) {
                    return StatusCode(ct);
                }
           
                if (ct == 500)
                {
                    return StatusCode(500);
                }

                return Ok(contact);
            }
            catch(Exception e)

            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }



        [HttpDelete]
        public IActionResult Delete([FromBody] ContactFields info)
        {
            try {
                Contact c = info.C;
                string username = info.username;
                string password = info.password;
                int id = c.Id;
                var ct = _contactService.DeleteContact(username,password,id);
                if (ct == 409)
                {
                    //No Existing Contact
                    return StatusCode(ct);
                }

                if (ct == 500)
                {
                    return StatusCode(ct);
                }

                if (ct == 404)
                {
                    return StatusCode(ct);
                }
                return Ok();
            } catch { 

                return StatusCode(500);
            }
        }

        [HttpPut]
        public IActionResult Update(ContactFields info)
        {
            try {
                Contact c = info.C;
                string username = info.username;
                string password = info.password;
                int id = c.Id;
                var ct = _contactService.UpdateContact(username, password, id,c);
                if (ct == 409)
                {
                    //No Existing Contact
                    return StatusCode(ct);
                }

                if (ct == 500)
                {
                    return StatusCode(ct);
                }

                if (ct == 404)
                {
                    return StatusCode(ct);
                }

                return Ok();
            }
            catch {
                return StatusCode(500);
            }
        }
    }
}
