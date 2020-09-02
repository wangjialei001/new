using New.Common.Util;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace UnitTestProject
{
    public class UidTest
    {
        public ITestOutputHelper output;
        public UidTest(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public void Test1()
        {
            //for (var i = 0; i < 20; i++)
            //{
            //    var uid = UidGenerator.Uid();
            //    output.WriteLine(uid.ToString());
            //}

            for (var i = 0; i < 20; i++)
            {
                var uid1 = UidGenerator1.Uid();
                output.WriteLine(uid1.ToString());
            }
        }
    }
}
