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
        public ActionResult<Contact> Get()
        {
            var result = new ContactDAL().GetContacts();
            return Ok(result);
        }
    }
}