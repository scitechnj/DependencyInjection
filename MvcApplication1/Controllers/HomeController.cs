using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public HomeController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public ActionResult Index()
        {
            return View(_personRepository.GetPersons());
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return String.Format("First Name: {0}, Last Name: {1}, Age: {2}",
                                 this.FirstName, this.LastName, this.Age);
        }
    }

    public interface IPersonRepository
    {
        IEnumerable<Person> GetPersons();
    }

    public class PersonRepository : IPersonRepository
    {
        private List<Person> _persons = new List<Person>
            {
                new Person {FirstName = "Alex", LastName = "Friedman", Age = 32},
                new Person{FirstName = "Shaya", LastName = "WhosHeGonnaPick", Age = 30}
            };

        public IEnumerable<Person> GetPersons()
        {
            return _persons;
        }
    }
}
