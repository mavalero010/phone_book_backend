using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using phone_book.Models;
using phone_book.Services;
using System.Net;
using System.Text.Encodings.Web;

namespace phone_book.Controllers
{
    

    public class ContactTypeController : Controller
    {
        public  ContactTypeService _contactTypeService;

        public ContactTypeController (ContactTypeService contactTypeService)
        {
            _contactTypeService = contactTypeService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ContactType c)  
        {
            try {
                if (c == null)
                {
                   
                    return BadRequest();
                }

                var ct = _contactTypeService.PostContactType(c);

                if (ct == null)
                {
                    return NotFound();
                }

                return Ok(c.TypeName);
            } catch {
                return StatusCode(500);
            }
                }

        [HttpGet]
        public IActionResult GetAll() {
            try {
                var items = _contactTypeService.GetAll();

                if (items == null)
                {
                    return NotFound();
                }

                return Ok(items);

            }
            catch
            {
              return StatusCode(500);

            }
        }

        [HttpGet]
        public IActionResult Get(int id) {
            try {
                var items = _contactTypeService.Get(id);

                if (items == null)
                {
                    return NotFound(items);
                }

                return Ok(items);
            } catch {
                return StatusCode(500);
            }

        }


        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try {

                var response =  await _contactTypeService.Delete(id);

                return response;

            }
            catch(Exception ex)
            {
                return   new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                {
                    Content = new StringContent("Contact Type Not Found")
                }; 
            }

        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(int id, [FromBody] ContactType c)
        {
            try
            {

                var response = await _contactTypeService.Update(id,c);



                return response;

            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                {
                    Content = new StringContent("Contact Type Not Found")
                }; ;
            }

        }

    }

}
