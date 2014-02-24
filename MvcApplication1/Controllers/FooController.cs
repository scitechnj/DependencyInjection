using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class FooController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public FooController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public void Index()
        {
            Response.Write("<h1>" + _personRepository.GetPersons().Count() + "</h1>");
        }

    }
}
