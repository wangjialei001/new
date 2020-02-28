using New.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace New.Core
{
    public class CustomService : ICustomService
    {
        public Customer Call(int i, Customer customer)
        {
            customer.Sex = "1";
            Console.WriteLine(i);
            Console.WriteLine("Execute Call Method");
            return customer;
        }
    }
}
