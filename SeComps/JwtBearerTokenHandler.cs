using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace SeComps
{
    public class JwtBearerTokenHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorageService;

        public JwtBearerTokenHandler(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }
        public async Task<string> GetToken()
        {
            return await _localStorageService.GetItemAsStringAsync("authToken");
        }
        public async Task ClearToken()
        {
            await _localStorageService.RemoveItemAsync("authToken");
        }

        public async Task<bool> IsTokenPresent()
        {
            var token = await GetToken();
            return !string.IsNullOrWhiteSpace(token);
        }
        public async Task SetToken(string token)
        {
            await _localStorageService.SetItemAsStringAsync("authToken", token);
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var savedToken = await _localStorageService.GetItemAsStringAsync("authToken", cancellationToken);

            if (!string.IsNullOrWhiteSpace(savedToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}