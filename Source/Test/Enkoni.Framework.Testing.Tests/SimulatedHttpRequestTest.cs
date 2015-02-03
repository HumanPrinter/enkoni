using System;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Testing.Tests {
  /// <summary>Tests the functionality of the <see cref="SimulatedHttpRequest"/> class.</summary>
  [TestClass]
  public class SimulatedHttpRequestTest {
    /// <summary>Tests the functionality of the <see cref="SimulatedHttpRequest(string, string, string, string, TextWriter, string)"/> constructor.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SimulatedHttpRequest_CtorWithNullHost_ExceptionIsThrown() {
      SimulatedHttpRequest testSubject = new SimulatedHttpRequest("appVirtualDir", "appPhysicalDir", "page", "query", new StringWriter(), null);
    }

    /// <summary>Tests the functionality of the <see cref="SimulatedHttpRequest(string, string, string, string, TextWriter, string)"/> constructor.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SimulatedHttpRequest_CtorWithEmptyHost_ExceptionIsThrown() {
      SimulatedHttpRequest testSubject = new SimulatedHttpRequest("appVirtualDir", "appPhysicalDir", "page", "query", new StringWriter(), string.Empty);
    }

    /// <summary>Tests the functionality of the <see cref="SimulatedHttpRequest.GetServerName()"/> method.</summary>
    [TestMethod]
    public void SimulatedHttpRequest_GetServerName_ValueOfHostIsReturned() {
      SimulatedHttpRequest testSubject = new SimulatedHttpRequest("appVirtualDir", "appPhysicalDir", "page", "query", new StringWriter(), "myserver");

      string result = testSubject.GetServerName();
      Assert.AreEqual("myserver", result, false);
    }

    /// <summary>Tests the functionality of the <see cref="SimulatedHttpRequest.MapPath(string)"/> method.</summary>
    [TestMethod]
    public void SimulatedHttpRequest_MapPath_VirtualDirJoinedWithVirtualPathIsReturned() {
      SimulatedHttpRequest testSubject = new SimulatedHttpRequest("appVirtualDir", "appPhysicalDir", "page", "query", new StringWriter(), "myserver");

      string result = testSubject.MapPath("myVirtualPath");
      Assert.AreEqual(@"appVirtualDir\myVirtualPath", result, false);
    }
  }
}
