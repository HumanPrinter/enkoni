using System;
using System.IO;
using System.Web;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Testing.Tests {
  /// <summary>Tests the functionality of the <see cref="HttpContextHelper"/> class.</summary>
  [TestClass]
  public class HttpContextHelperTest {
    /// <summary>Tests the functionality of the <see cref="SimulatedHttpRequest(string, string, string, string, TextWriter, string)"/> constructor.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void HttpContextHelper_SetHttpContextWithSimulatedRequestWithNullHost_ExceptionIsThrown() {
      HttpContextHelper.SetHttpContextWithSimulatedRequest(null, "application");
    }

    /// <summary>Tests the functionality of the <see cref="SimulatedHttpRequest(string, string, string, string, TextWriter, string)"/> constructor.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void HttpContextHelper_SetHttpContextWithSimulatedRequestWithEmptyHost_ExceptionIsThrown() {
      HttpContextHelper.SetHttpContextWithSimulatedRequest(string.Empty, "application");
    }

    /// <summary>Tests the functionality of the <see cref="SimulatedHttpRequest(string, string, string, string, TextWriter, string)"/> constructor.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void HttpContextHelper_SetHttpContextWithSimulatedRequestWithNullApplication_ExceptionIsThrown() {
      HttpContextHelper.SetHttpContextWithSimulatedRequest("myserver", null);
    }

    /// <summary>Tests the functionality of the <see cref="SimulatedHttpRequest(string, string, string, string, TextWriter, string)"/> constructor.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void HttpContextHelper_SetHttpContextWithSimulatedRequestWithEmptyApplication_ExceptionIsThrown() {
      HttpContextHelper.SetHttpContextWithSimulatedRequest("myserver", string.Empty);
    }

    /// <summary>Tests the functionality of the <see cref="SimulatedHttpRequest.GetServerName()"/> method.</summary>
    [TestMethod]
    public void HttpContextHelper_NoCurrentContext_ContextIsCreated() {
      /* Make sure there is no current context */
      HttpContext.Current = null;

      HttpContextHelper.SetHttpContextWithSimulatedRequest("myserver", "application");

      Assert.IsNotNull(HttpContext.Current);
      Assert.AreEqual("myserver", HttpContext.Current.Request.Params.GetValues("SERVER_NAME")[0], false);
      Assert.AreEqual(0, HttpContext.Current.Session.Count);
    }

    /// <summary>Tests the functionality of the <see cref="SimulatedHttpRequest.GetServerName()"/> method.</summary>
    [TestMethod]
    public void HttpContextHelper_CurrentContext_ContextSessionIsCleared() {
      /* Make sure there is a current context */
      HttpContext.Current = null;
      HttpContextHelper.SetHttpContextWithSimulatedRequest("myserver", "application");
      HttpContext.Current.Session.Add("mySessionKey", "someValue");
      Assert.AreEqual(1, HttpContext.Current.Session.Count);

      HttpContextHelper.SetHttpContextWithSimulatedRequest("myotherserver", "anotherApplication");

      Assert.IsNotNull(HttpContext.Current);
      /* Only the session should be cleared, therefore the original server name should be returned */
      Assert.AreEqual("myserver", HttpContext.Current.Request.Params.GetValues("SERVER_NAME")[0], false);
      Assert.AreEqual(0, HttpContext.Current.Session.Count);
    }
  }
}
