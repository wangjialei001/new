using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace New.Core
{
    public interface IScopeService
    {
        Task SayHello(ITransientService _transientService);
    }
}
