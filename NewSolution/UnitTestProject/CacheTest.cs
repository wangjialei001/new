using New.Cache;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTestProject
{
    public class CacheTest
    {
        [Fact]
        public void Test1()
        {
            var id=CacheHelper.GenerateId();
            Console.WriteLine(id);
        }
    }
}
