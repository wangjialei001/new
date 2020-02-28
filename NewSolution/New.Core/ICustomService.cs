using AspectCore.DynamicProxy;
using New.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace New.Core
{
    public interface ICustomService
    {
        //[CustomInterceptor]//
        [ServiceInterceptor(typeof(CustomInterceptorAttribute))]
        Customer Call(int i, Customer customer);
    }
}
