using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace ConsoleApplication36
{

    //public class IOCContainer
    //{
    //    private Dictionary<Type, Type> _container = new Dictionary<Type, Type>();

    //    public void Add(Type t1, Type t2)
    //    {
    //        _container.Add(t1, t2);
    //    }

    //    public T GetInstance<T>()
    //    {
    //        var type = typeof(T);
    //        var matchingType = _container[type];
    //        return (T)Activator.CreateInstance(matchingType);
    //    }
    //}

    class Program
    {
        static void Main(string[] args)
        {
            //IDataLayer dl = new ConcreteDataLayer();
            //var container = new IOCContainer();
            //container.Add(typeof(IDataLayer), typeof(ConcreteDataLayer));

            //IDataLayer dl = container.GetInstance<IDataLayer>();

            var kernel = new StandardKernel();
            kernel.Bind<IDataLayer>().To<ConcreteDataLayer>();
            
            //IDataLayer dl = kernel.Get<IDataLayer>();
                
            //MyController mc = new MyController(dl);
            MyController mc = kernel.Get<MyController>();
            //mc.Index();

            using (var webClient = new WebClient())
            {
                string result = webClient.DownloadString(
                    "http://zipcodedistanceapi.redline13.com/rest/2SzBozAecaa6kThWOXpBzo1jlEhmBQtEI2g7CQlP1vVlpdiebHPK5Q4rumN6cgKV/distance.json/08701/11219/mile");

                Console.WriteLine(result);
                
            }

            Console.ReadKey(true);
        }
    }

    public class MyController
    {
        private readonly IDataLayer _dl;

        public MyController(IDataLayer dl)
        {
            _dl = dl;
        }

        public void Index()
        {
            foreach (var person in _dl.GetPeople())
            {
                Console.WriteLine(person);
            }
        }
    }

    public interface IDataLayer
    {
        IEnumerable<Person> GetPeople();
    }

    public class ConcreteDataLayer : IDataLayer
    {
        private List<Person> _persons = new List<Person>
            {
                new Person {FirstName = "Alex", LastName = "Friedman", Age = 32}
            };

        public IEnumerable<Person> GetPeople()
        {
            return _persons;
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
}
