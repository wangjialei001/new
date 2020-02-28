using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internel.Api.Filters
{
    public class ResultFilterAttribute : Attribute,IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("OnResultExecuted...........");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("OnResultExecuting...........");
        }
    }
}
