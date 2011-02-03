//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpContextHelper.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Holds a class that can be used to simulate an HTTP session.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Web;
using System.Web.SessionState;

namespace OscarBrouwer.Framework.Testing {
  /// <summary>Can be used to create a mocked HttpContext.</summary>
  public static class HttpContextHelper {
    /// <summary>Sets the HTTP context with a valid simulated request.</summary>
    /// <param name="host">The host that must be associated with the session. Normally 'localhost' will suffice.</param>
    /// <param name="application">The name of the application that requires the session. Any name will do.</param>
    public static void SetHttpContextWithSimulatedRequest(string host, string application) {
      if(string.IsNullOrEmpty(host)) {
        throw new ArgumentException("Specify a valid host", "host");
      }

      if(string.IsNullOrEmpty(application)) {
        throw new ArgumentException("Specify a valid application", "application");
      }

      if(HttpContext.Current != null) {
        HttpContext.Current.Session.Clear();
      }
      else {
        /* These values are purely to satisfy the constructors of the HttpContext related objects. They don't have to 
         * represent a real-life web-application.*/
        string appVirtualDir = "/";
        string appPhysicalDir = @"d:\projects\Web\";
        string page = application.Replace("/", string.Empty) + "/default.aspx";
        string query = string.Empty;
        TextWriter output = null;

        SimulatedHttpRequest workerRequest = new SimulatedHttpRequest(appVirtualDir, appPhysicalDir, page, query, output, host);
        HttpContext.Current = new HttpContext(workerRequest);
        HttpSessionStateContainer session = new HttpSessionStateContainer(Guid.NewGuid().ToString(), new SessionStateItemCollection(),
          SessionStateUtility.GetSessionStaticObjects(HttpContext.Current), 30, true, HttpCookieMode.AutoDetect, SessionStateMode.Custom,
          false);

        SessionStateUtility.AddHttpSessionStateToContext(HttpContext.Current, session);
      }
    }
  }
}
