using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internel.Api.Filters
{
    public class EncryRequiredAttribute:Attribute
    {
        public readonly bool isRequired;
        public EncryRequiredAttribute(bool isRequired=true)
        {
            this.isRequired = isRequired;
        }
    }
}
