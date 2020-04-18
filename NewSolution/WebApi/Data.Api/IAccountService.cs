using System;
using System.Threading.Tasks;

namespace Data.Api
{
    public interface IAccountService
    {
        Task<object> Transfer();
    }
}
