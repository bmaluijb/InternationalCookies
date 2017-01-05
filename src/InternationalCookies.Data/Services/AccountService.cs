using InternationalCookies.Data.Interfaces;
using InternationalCookies.Data.Models;
using InternationalCookies.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InternationalCookies.Data.Services
{
    public class AccountService : IAccountService
    {
        private AzureAd _adSettings;
        private IQueueService _queueService;

        public AccountService(IOptions<AzureAd> adSettings, IQueueService queueService)
        {
            _adSettings = adSettings.Value;
            _queueService = queueService;
        }

        public async Task<Guid> GetStoreIdFromUser(string userId)
        {
            Guid storeId = new Guid();

            string accessToken = await GetBearerAccesToken();

            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, GetUserUrl(userId)))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    using (var response = await client.SendAsync(request))
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var json = JObject.Parse(await response.Content.ReadAsStringAsync());
                            storeId = Guid.Parse(json?["physicalDeliveryOfficeName"]?.ToString());
                        }
                    }
                }
            }

            return storeId;
        }

        public void RegisterNewStoreAndUser(Register storeAndUserData)
        {
            _queueService.QueueNewStoreCreation(JsonConvert.SerializeObject(storeAndUserData));
        }

        public async Task<string> CreateNewUser(Register userInfo, string storeId)
        {
            string result = string.Empty;

            string accessToken = await GetBearerAccesToken();

            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, GetCreateUserUrl()))
                {
                    dynamic newUser = CreateNewUserObject(userInfo, storeId);
                    result = newUser.userPrincipalName;

                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    request.Content = new StringContent(newUser.ToString(), Encoding.UTF8, "application/json");

                    using (var response = await client.SendAsync(request))
                    {
                        if (response.StatusCode != HttpStatusCode.Created)
                        {
                            result = await response.Content.ReadAsStringAsync();                          
                        }
                    }
                }
            }

            return result;
        }


        #region private methods

        private static dynamic CreateNewUserObject(Register userInfo, string storeId)
        {
            var newUser = (dynamic)new JObject();
            newUser.accountEnabled = "true";
            newUser.displayName = userInfo.PersonFirstName + " " + userInfo.PersonLastName;
            newUser.mailNickname = userInfo.PersonFirstName + userInfo.PersonLastName;
            newUser.passwordProfile = (dynamic)new JObject();
            newUser.passwordProfile.password = "IntCookies123";
            newUser.passwordProfile.forceChangePasswordNextLogin = "true";
            newUser.userPrincipalName = userInfo.PersonEmail.Substring(0, userInfo.PersonEmail.IndexOf("@")) + "@internationalcookies.onmicrosoft.com";
            newUser.physicalDeliveryOfficeName = storeId;

            return newUser;
        }

        private string GetUserUrl(string userPrincipalName)
        {
            return string.Format("https://graph.windows.net/{0}/users/{1}?{2}", _adSettings.TenantId, userPrincipalName, "api-version=1.6");
        }

        private string GetCreateUserUrl()
        {
            return string.Format("https://graph.windows.net/{0}/users?{1}", _adSettings.TenantId, "api-version=1.6");
        }

        private async Task<string> GetBearerAccesToken()
        {
            string result = string.Empty;

            // Get OAuth token using client credentials 
            string authString = "https://login.microsoftonline.com/" + _adSettings.TenantId;

            AuthenticationContext authenticationContext = new AuthenticationContext(authString, false);

            // Config for OAuth client credentials  
            ClientCredential clientCred = new ClientCredential(_adSettings.ClientId, _adSettings.AppKey);
            string resource = "https://graph.windows.net";

            AuthenticationResult authenticationResult = await authenticationContext.AcquireTokenAsync(resource, clientCred);
            result = authenticationResult.AccessToken;

            return result;
        }

        #endregion
    }
}
