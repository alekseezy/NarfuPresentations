using NarfuPresentations.Shared.API.Client.API.Settings;

namespace NarfuPresentations.Shared.API.Client.Http.Security;

internal class CommonHandlers
{
    public static HttpClientHandler GetInsecureClientHandler()
    {
        HttpClientHandler handler = new()
        {
            ServerCertificateCustomValidationCallback = (_, _, _, _) => true
        };

        return handler;
    }

    public static HttpClientHandler GetCustomCNClientHandler(APISettings settings)
    {
#if ANDROID
        var handler = new CustomAndroidMessageHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
        {
            if (cert != null && cert.Issuer.Equals("CN=localhost"))
                return true;
            return errors == SslPolicyErrors.None;
        };
#else

        HttpClientHandler handler = new()
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert?.Issuer.Equals(settings.CertificateCN) is true)
                    return true;

                return errors == System.Net.Security.SslPolicyErrors.None;
            }
        };
#endif

        return handler;
    }
}
