//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="SimulatedHttpRequest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Holds a simulated HTTP request that is used by the HttpContextHelper.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Web.Hosting;

namespace Enkoni.Framework.Testing {
  /// <summary>Used to simulate an HttpRequest.</summary>
  public class SimulatedHttpRequest : SimpleWorkerRequest {
    /// <summary>The simulated host.</summary>
    private string host;

    /// <summary>Initializes a new instance of the <see cref="SimulatedHttpRequest"/> class.</summary>
    /// <param name="appVirtualDir">The virtual directory of the application.</param>
    /// <param name="appPhysicalDir">The physical directory of the application.</param>
    /// <param name="page">The name of the page that is requested.</param>
    /// <param name="query">The query that is passed in the request.</param>
    /// <param name="output">The writer that must be used to write the output to.</param>
    /// <param name="host">The host of the application.</param>
    public SimulatedHttpRequest(string appVirtualDir, string appPhysicalDir, string page, string query, TextWriter output, string host)
      : base(appVirtualDir, appPhysicalDir, page, query, output) {
      if(host == null || host.Length == 0) {
        throw new ArgumentNullException("host", "Host cannot be null nor empty.");
      }

      this.host = host;
    }

    /// <summary>Gets the name of the server.</summary>
    /// <returns>The name of the server.</returns>
    public override string GetServerName() {
      return this.host;
    }

    /// <summary>Maps the path to a filesystem path.</summary>
    /// <param name="virtualPath">Virtual path.</param>
    /// <returns>The result of the operation.</returns>
    public override string MapPath(string virtualPath) {
      return Path.Combine(this.GetAppPath(), virtualPath);
    }
  }
}
