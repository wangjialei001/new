using New.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace New.Core
{
    public class CustomService : ICustomService
    {
        private List<Customer> customers = new List<Customer>();
        public CustomService()
        {
            customers.Add(new Customer { Id = 1, Name = "gooney", Birthday = DateTime.Now });
            customers.Add(new Customer { Id = 2, Name = "jack", Birthday = DateTime.Now });
        }
        public Customer Call(int i, Customer customer)
        {
            customer.Sex = "1";
            Console.WriteLine(i);
            Console.WriteLine("Execute Call Method");
            return customer;
        }

        public Customer CustomerQuery(int id)
        {
            return customers.FirstOrDefault(t => t.Id == id);
        }

        public Customer QueryCustomer(int id)
        {
            return customers.FirstOrDefault(t => t.Id == id);
        }
    }
}
