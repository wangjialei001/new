using System;
using System.Collections.Generic;

namespace AuthEFService.Models.DBTest
{
    public partial class CstCustomer
    {
        public long CustId { get; set; }
        public string CustName { get; set; }
        public string CustSource { get; set; }
        public string CustIndustry { get; set; }
        public string CustLevel { get; set; }
        public string CustAddress { get; set; }
        public string CustPhone { get; set; }
    }
}
