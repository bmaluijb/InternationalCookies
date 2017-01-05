using InternationalCookies.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalCookies.Data.Interfaces
{
    public interface IQueueService
    {
        void QueueNewStoreCreation(string userAndStoreData);
    }
}
