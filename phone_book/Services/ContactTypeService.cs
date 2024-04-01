using phone_book.Data;
using System.Reflection.Metadata;
using phone_book.Models;
using System.Net;
using System.Text.Encodings.Web;
using System.Text;

namespace phone_book.Services
{
    public class ContactTypeService
    {
        public PhoneBookDb _dbContext;

        public ContactTypeService(PhoneBookDb dbContext)
        {
             _dbContext = dbContext;
        }



        public ContactType PostContactType(ContactType contactType)
        {
            try
            {

                _dbContext.ContactType.Add(contactType);
                _dbContext.SaveChanges();

                return contactType;
            }
            catch (Exception ex)
            {
                

                return null;
            }
        }

        public List<ContactType> GetAll() {

            try {
                var items = _dbContext.ContactType.ToList();
                return items;

            }
            catch {
                return null;
            }
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                var item =  _dbContext.ContactType.FirstOrDefault(i => i.ContactTypeId == id);

                if (item == null)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Contact Type Not Found", Encoding.UTF8, "text/plain")
                    };

                    return response;
                };

               
                
                try
                {

                    _dbContext.Remove(item);
                    _dbContext.SaveChanges();
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Contact type removed")
                    };
                }
                catch
                {
                    return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                    {
                        Content = new StringContent("Server error")
                    };
                }
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                {
                    Content = new StringContent("Server error")
                };              
            }
        }

        public async Task<HttpResponseMessage> Update(int id, ContactType CTU)
        {
            try
            {
                var CT = _dbContext.ContactType.FirstOrDefault(i => i.ContactTypeId == id);

                if (CT == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(HtmlEncoder.Default.Encode("Contact Type Not Found"))
                    };
                }
                try
                {
                    CT.TypeName = CTU.TypeName;

                    await _dbContext.SaveChangesAsync();

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Contact type updated")
                    };
                }
                catch
                {
                    return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                    {
                        Content = new StringContent("Server error")
                    };
                }
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                {
                    Content = new StringContent("Server error")
                };
            }
        }

        public ContactType Get(int id) {
            try {
                var CT = _dbContext.ContactType.Find(id);
              return CT;

            }
            catch {
                return null;
            }
        }
    }
}

