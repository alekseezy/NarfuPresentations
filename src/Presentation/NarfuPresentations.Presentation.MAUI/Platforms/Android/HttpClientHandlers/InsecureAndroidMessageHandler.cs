﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarfuPresentations.Presentation.MAUI.Platforms.Android.HttpClientHandlers;
internal class InsecureAndroidMessageHandler /*: Android.Net.AndroidMessageHandler*/
{
    //protected override Javax.Net.Ssl.IHostnameVerifier GetSSLHostnameVerifier(Javax.Net.Ssl.HttpsURLConnection connection)
    //        => new CustomHostnameVerifier();

    //private sealed class CustomHostnameVerifier : Java.Lang.Object, Javax.Net.Ssl.IHostnameVerifier
    //{
    //    public bool Verify(string? hostname, Javax.Net.Ssl.ISSLSession? session)
    //    {
    //        return
    //            Javax.Net.Ssl.HttpsURLConnection.DefaultHostnameVerifier.Verify(hostname, session)
    //            || hostname == "10.0.2.2" && session.PeerPrincipal?.Name == "CN=localhost";
    //    }
    //}
}
