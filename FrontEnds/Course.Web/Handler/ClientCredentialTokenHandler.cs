using Course.Web.Exceptions;
using Course.Web.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Course.Web.Handler
{
    public class ClientCredentialTokenHandler : DelegatingHandler
    {
        private readonly IClientCrediantialTokenService _clientCrediantialTokenService;

        public ClientCredentialTokenHandler(IClientCrediantialTokenService clientCrediantialTokenService)
        {
            _clientCrediantialTokenService = clientCrediantialTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",await _clientCrediantialTokenService.GetToken());
            
            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode== HttpStatusCode.Unauthorized) throw new UnAuthorizeException(); 

            return response;
        }
    }
}
