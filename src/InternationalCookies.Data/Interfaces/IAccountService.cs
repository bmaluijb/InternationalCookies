using InternationalCookies.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalCookies.Data.Interfaces
{
    public interface IAccountService
    {
        Task<Guid> GetStoreIdFromUser(string userId);
        void RegisterNewStoreAndUser(Register storeAndUserData);

        Task<string> CreateNewUser(Register userInfo, string storeId);
    }
}
