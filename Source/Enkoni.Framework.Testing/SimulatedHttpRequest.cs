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
      Guard.ArgumentIsNotNullOrEmpty(host, nameof(host), "Host cannot be null nor empty.");

      this.host = host;
    }

    /// <summary>Gets the name of the server.</summary>
    /// <returns>The name of the server.</returns>
    public override string GetServerName() {
      return this.host;
    }

    /// <summary>Maps the path to a file system path.</summary>
    /// <param name="virtualPath">Virtual path.</param>
    /// <returns>The result of the operation.</returns>
    public override string MapPath(string virtualPath) {
      return Path.Combine(this.GetAppPath(), virtualPath);
    }
  }
}
