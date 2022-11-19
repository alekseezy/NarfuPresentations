using System.Net.Security;

using NarfuPresentations.Presentation.MAUI.API.ServerAPI.Settings;
#if ANDROID
using Xamarin.Android.Net;
#endif

namespace NarfuPresentations.Presentation.MAUI.API.Http.Security;

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

#if ANDROID
    public static AndroidMessageHandler GetCustomCNClientHandler(APISettings settings)
    {

        var handler = new CustomAndroidMessageHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == SslPolicyErrors.None;
            }
        };
        return handler;
    }
#else
    public static HttpClientHandler GetCustomCNClientHandler(APISettings settings)
    {
        HttpClientHandler handler = new()
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert?.Issuer.Equals(settings.CertificateCN) is true)
                    return true;

                return errors == System.Net.Security.SslPolicyErrors.None;
            }
        };
        return handler;
    }
#endif

#if ANDROID
    internal sealed class CustomAndroidMessageHandler : Xamarin.Android.Net.AndroidMessageHandler
    {
        protected override Javax.Net.Ssl.IHostnameVerifier GetSSLHostnameVerifier(Javax.Net.Ssl.HttpsURLConnection connection)
            => new CustomHostnameVerifier();

        private sealed class CustomHostnameVerifier : Java.Lang.Object, Javax.Net.Ssl.IHostnameVerifier
        {
            public bool Verify(string hostname, Javax.Net.Ssl.ISSLSession session)
            {
                return Javax.Net.Ssl.HttpsURLConnection.DefaultHostnameVerifier.Verify(hostname, session) ||
                    hostname == "10.0.2.2" && session.PeerPrincipal?.Name == "CN=localhost";
            }
        }
    }
#elif IOS
        public bool IsHttpsLocalhost(NSUrlSessionHandler sender, string url, Security.SecTrust trust)
        {
            if (url.StartsWith("https://localhost"))
                return true;
            return false;
        }
#endif
}
