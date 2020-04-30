using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseBasic.DataFramework;
using DatabaseBasic.DataFramework.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseBasic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Contact>> Get()
        {
            var result = new ContactDAL().GetContacts();
            return Ok(result);
        }

        [HttpGet("{contactId}")]
        public ActionResult<Contact> GetById(int contactId)
        {
            var result = new ContactDAL().GetContact(contactId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<Contact> AddContact([FromBody] Contact contact)
        {
            var contactsDAL = new ContactDAL();
            var result = contactsDAL.InsertContact(contact);
            return Ok(result);
        }

        [HttpPut("{contactId}")]
        public ActionResult<Contact> UpdateContact(int contactId, [FromBody] Contact contact)
        {
            var contactsDAL = new ContactDAL();
            contact.Id = contactId;
            var result = contactsDAL.UpdateContact(contact);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete("{contactId}")]
        public ActionResult<bool> DeleteContact(int contactId)
        {
            var contactsDAL = new ContactDAL();
            var result = contactsDAL.DeleteContact(contactId);
            return Ok(result);
        }



    }
}