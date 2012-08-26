//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="UIDispatcherTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the UIDispatcher class.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Threading;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.UI.Tests {
  /// <summary>Tests the functionality of the <see cref="UIDispatcher"/> class.</summary>
  [TestClass]
  public class UIDispatcherTest {
    #region TestCases
    /// <summary>Tests the functionality of the <see cref="UIDispatcher.BeginInvoke(System.Action)"/> class using values that should easily pass the validation.
    /// </summary>
    [TestMethod]
    public void TestCase01_ValidValues() {
      string threadName;
      bool proceed = false;

      Thread testThread = new Thread(() =>
        UIDispatcher.BeginInvoke(() => {
          threadName = Thread.CurrentThread.Name;
          proceed = true;
        })
      );

      testThread.Name = "TestThread";
      testThread.Start();

      while(!proceed) {
        Thread.Yield();
      }

      /* Use valid values in combination with an Inclusive-Exclusive boundary combination. */
      Assert.IsTrue(true);
    }
    #endregion
  }

  #region Helper classes
  public class TestApplication : Application {
  }
  #endregion
}
