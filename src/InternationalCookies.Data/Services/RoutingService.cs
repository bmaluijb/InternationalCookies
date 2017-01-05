using InternationalCookies.Data.Interfaces;
using InternationalCookies.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace InternationalCookies.Data.Services
{
    public class RoutingService : IRoutingService
    {
        private IAccountService _accountService;
        private ConnectionStrings _connectionStrings;
        private IDistributedCache _cache;
        private CookieContext _currentContext;

        public RoutingService(IAccountService accountService, IOptions<ConnectionStrings> connectionStrings,
            IDistributedCache cache, CookieContext context)
        {

            _accountService = accountService;
            _connectionStrings = connectionStrings.Value;
            _cache = cache;
            _currentContext = context;

        }

        public CookieContext GetRoutedContext(Guid storeId)
        {
            string connectionString = GetDataStoreForStoreId(storeId);

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(connectionString);
            return new CookieContext(optionsBuilder.Options);
        }

        private string GetDataStoreForStoreId(Guid storeId)
        {
            string server = string.Empty;
            string database = string.Empty;
            string connectionString = string.Empty;

            if (string.IsNullOrEmpty(_cache.GetString("connectionString" + storeId.ToString())))
            {
                var store = _currentContext.Stores.Where(s => s.Id == storeId)
                    .Include(s => s.DatabaseServer)
                    .FirstOrDefault();

                if (store != null)
                {
                    connectionString = string.Format(_connectionStrings.CookieDBConnectionTemplate, store.DatabaseServer.DatabaseServer, store.DatabaseServer.DatabaseName);

                    _cache.SetString("connectionString" + storeId.ToString(), connectionString);
                }
            }
            else
            {
                connectionString = _cache.GetString("connectionString" + storeId.ToString());
            }

            return connectionString;
        }

        public void SyncStoresToDatabases()
        {
            var storesToSync = _currentContext.Stores.ToList();

            var servers = _currentContext.DatabaseServers.ToList();

            foreach (var server in servers)
            {
                var optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder.UseSqlServer(GetDataStoreForDatabaseServer(server));
                var context = new CookieContext(optionsBuilder.Options);

                try
                {
                    var stores = context.Stores.ToList();

                    //loop over the stores to sync
                    foreach (var store in storesToSync)
                    {
                        //if the store is not in the current context's stores list
                        if (stores.Where(s => s.Name == store.Name).FirstOrDefault() == null)
                        {
                            //build a new storeobject, so that the connection to the old DBContext is gone
                            Store storeToAdd = new Store
                            {
                                Name = store.Name,
                                Country = store.Country,
                                DatabaseServer = context.DatabaseServers.Where(d => d.Id == store.DatabaseServer.Id).FirstOrDefault(),
                                Orders = store.Orders
                            };

                            context.Stores.Add(storeToAdd);
                            context.SaveChanges();
                        }
                    }
                }
                catch (SqlException ex)
                {
                    //database cannot be openened for some reason
                }
            }
        }
        private string GetDataStoreForDatabaseServer(DatabaseServers server)
        {
            return string.Format(_connectionStrings.CookieDBConnectionTemplate, server.DatabaseServer, server.DatabaseName);
        }
    }
}
