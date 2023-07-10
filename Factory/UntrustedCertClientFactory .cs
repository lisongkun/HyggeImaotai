using Flurl.Http.Configuration;
using System.Net.Http;

namespace hygge_imaotai.Factory
{
    public class UntrustedCertClientFactory : DefaultHttpClientFactory
    {
        public override HttpMessageHandler CreateMessageHandler()
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
            return handler;
        }
    }
}
