using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using phone_book.Models;
using phone_book.Data;
using phone_book.Utils;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace phone_book.Services
{
    public class ContactService
    {
        private readonly PhoneBookDb _dbContext;
        private readonly Authenticate _auth;

        public ContactService(PhoneBookDb dbContext)
        {
            _dbContext = dbContext;
        }

        public object GetContactByUsername(string username)
        {
            var r = _dbContext.Contact.FirstOrDefault(i => i.UserName == username);

            return r;
        }

        public int PostContact(Contact contact, AdditionalFields additionalFields, string username, string password)
        {
            try
            {
                var user = _dbContext.User.FirstOrDefault(ct => ct.UserName == username);
                Console.WriteLine("USER: ",user);
                if (user == null || user.Password != password )
                {
                    return 401;
                }
               
                var c = _dbContext.Contact.FirstOrDefault(i => i.UserName == contact.UserName);

                if (c != null)
                {
                    //Existing Contact
                    return 409;
                }
                
                if (additionalFields != null) {
                    try { 
                        _dbContext.AdditionalFields.Add(additionalFields);
                        _dbContext.SaveChanges();

                        contact.AdditionalFields = additionalFields.FieldId;
                    }
                    catch {
                        //Server error
                        return 500;
                    }
                }
                user.ContactsList.Add(contact.UserName);
                _dbContext.Contact.Add(contact);
                _dbContext.SaveChanges();

                return 200;
            }
            catch (Exception e)
            {
                //Server Error
                return 500;
            }
        }

        public int DeleteContact(string username, string password, int id)
        {
            try
            {
                var user = _dbContext.User.FirstOrDefault(ct => ct.UserName == username);

                if (user == null || user.Password != password)
                {
                    if (user == null)
                    {
                        return 404;
                    }
                    return 401;
                }
                var c = _dbContext.Contact.FirstOrDefault(i => i.Id== id);

                if (c == null)
                {
                    //No Existing Contact
                    return 404;
                }

       

                _dbContext.Contact.Remove(c);
                user.ContactsList.Remove(c.UserName);

                _dbContext.SaveChanges();
                return 200;
            }
            catch
            {
                //Server Error
                return 500;
            }
        }

        public int UpdateContact(string username, string password, int id, Contact newContactData) {

            try {
                 var user = _dbContext.User.FirstOrDefault(ct => ct.UserName == username);

                if (user == null || user.Password != password)
                {
                    if (user == null)
                    {
                        return 404;
                    }
                    return 401;
                }

                var existingContact = _dbContext.Contact.FirstOrDefault(i => i.Id == id);

                if (existingContact == null)
                {
                    //No Existing Contact
                    return 404;
                }

                existingContact.Name = newContactData.Name;
                existingContact.UserName = newContactData.UserName;
                existingContact.PhoneNumber = newContactData.PhoneNumber;
                existingContact.ContactTypeId = newContactData.ContactTypeId;
                if (newContactData.AdditionalFields!=null)
                {
                    existingContact.AdditionalFields = newContactData.AdditionalFields;
                }

                _dbContext.SaveChanges();
                return 200;
            }
            catch {
                return 500;
            }
        }
        
    }
}
