//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="StopwatchTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the Stopwatch-class.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading;

using Enkoni.Framework.Timers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the <see cref="Stopwatch"/> class.</summary>
  [TestClass]
  public class StopwatchTest {
    /// <summary>Tests the functionality of the <see cref="Stopwatch"/> class when it is idle.</summary>
    [TestMethod]
    public void TestCase01_Idle() {
      /* Create the test subject */
      Stopwatch testSubject = new Stopwatch();

      /* Test the initial values of the test subject */
      Assert.AreEqual(0, testSubject.LapTimes.Count);
      Assert.AreEqual(0, testSubject.CurrentLapTime.Ticks);

      /* Tests that the methods do not throw unexpected exceptions when the stopwatch is not running */
      testSubject.Stop();
      testSubject.Reset();
      testSubject.NewLap();
    }

    /// <summary>Tests the standard start and stop functionality of the <see cref="Stopwatch"/> class.</summary>
    [TestMethod]
    public void TestCase02_StartStop() {
      /* Create the test subject */
      Stopwatch testSubject = new Stopwatch();

      /* Start the stopwatch and wait a little while */
      testSubject.Start();
      Thread.Sleep(3000);

      /* Check the properties while in running mode */
      Assert.AreEqual(0, testSubject.LapTimes.Count);
      double elapsed = testSubject.CurrentLapTime.TotalMilliseconds;
      Assert.IsTrue(elapsed >= 3000 && elapsed <= 3200);

      /* Stop the stopwatch and check the properties again */
      testSubject.Stop();
      TimeSpan lapTime = testSubject.CurrentLapTime;
      Assert.AreEqual(0, testSubject.LapTimes.Count);
      elapsed = testSubject.CurrentLapTime.TotalMilliseconds;
      Assert.IsTrue(elapsed >= 3000 && elapsed <= 3200);

      /* Wait some more and check the properties again to verify that the stopwatch was really stopped */
      Thread.Sleep(3000);
      Assert.AreEqual(0, testSubject.LapTimes.Count);
      Assert.AreEqual(lapTime.Ticks, testSubject.CurrentLapTime.Ticks);
    }

    /// <summary>Tests the pause and resume functionality of the <see cref="Stopwatch"/> class.</summary>
    [TestMethod]
    public void TestCase03_PauseResume() {
      /* Create the test subject */
      Stopwatch testSubject = new Stopwatch();

      /* Start the stopwatch and wait a little while */
      testSubject.Start();
      Thread.Sleep(2000);

      /* Stop (pause) the stopwatch and wait some more */
      testSubject.Stop();
      Thread.Sleep(2000);

      /* Resume the stopwatch */
      testSubject.Start();
      Thread.Sleep(2000);

      /* Stop the timer and check the properties */
      testSubject.Stop();
      Assert.AreEqual(0, testSubject.LapTimes.Count);
      Assert.IsTrue(testSubject.CurrentLapTime.TotalMilliseconds >= 4000 && testSubject.CurrentLapTime.TotalMilliseconds < 4200);
    }

    /// <summary>Tests the lap time functionality of the <see cref="Stopwatch"/> class.</summary>
    [TestMethod]
    public void TestCase04_NewLaps() {
      /* Create the test subject */
      Stopwatch testSubject = new Stopwatch();

      /* Start the stopwatch and wait a little while */
      testSubject.Start();
      Thread.Sleep(2000);

      /* Start a new lap and check the properties */
      TimeSpan lapTime = testSubject.NewLap();

      Assert.IsTrue(lapTime.TotalMilliseconds >= 2000 && lapTime.TotalMilliseconds <= 2200);
      Assert.IsTrue(testSubject.CurrentLapTime.TotalMilliseconds <= 200);
      Assert.IsTrue(testSubject.LapTimes.Count == 1);
      Assert.AreEqual(lapTime.Ticks, testSubject.LapTimes[0].Ticks);
    }
  }
}
