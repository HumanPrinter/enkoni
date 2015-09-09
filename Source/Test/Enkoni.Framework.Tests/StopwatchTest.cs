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
      testSubject.Pause();
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
      double elapsedBeforeWait = testSubject.CurrentLapTime.TotalMilliseconds;
      Thread.Sleep(500);
      double elapsedAfterWait = testSubject.CurrentLapTime.TotalMilliseconds;

      /* Check the properties while in running mode */
      Assert.IsNotNull(testSubject.LapTimes);
      Assert.AreEqual(0, testSubject.LapTimes.Count);
      Assert.IsTrue(elapsedAfterWait > elapsedBeforeWait);

      /* Stop the stopwatch and check the properties again */
      testSubject.Stop();
      TimeSpan lapTime = testSubject.CurrentLapTime;
      double elapsedAfterStop = lapTime.TotalMilliseconds;
      Assert.AreEqual(1, testSubject.LapTimes.Count);
      Assert.IsTrue(elapsedAfterStop >= elapsedAfterWait);
      Assert.AreEqual(testSubject.LapTimes[0], lapTime);

      /* Wait some more and check the properties again to verify that the stopwatch was really stopped */
      Thread.Sleep(500);
      Assert.AreEqual(1, testSubject.LapTimes.Count);
      Assert.AreEqual(lapTime.Ticks, testSubject.CurrentLapTime.Ticks);
      Assert.AreEqual(testSubject.LapTimes[0], lapTime);
    }

    /// <summary>Tests the pause and resume functionality of the <see cref="Stopwatch"/> class.</summary>
    [TestMethod]
    public void TestCase03_PauseResume() {
      /* Create the test subject */
      Stopwatch testSubject = new Stopwatch();

      /* Start the stopwatch and wait a little while */
      testSubject.Start();
      Thread.Sleep(500);

      /* Pause the stopwatch and wait some more */
      testSubject.Pause();
      double elapsedBeforeWait = testSubject.CurrentLapTime.TotalMilliseconds;
      Thread.Sleep(500);
      double elapsedAfterWait = testSubject.CurrentLapTime.TotalMilliseconds;
      Assert.AreEqual(elapsedAfterWait, elapsedBeforeWait);

      /* Resume the stopwatch */
      testSubject.Resume();
      Thread.Sleep(500);

      /* Stop the timer and check the properties */
      testSubject.Stop();
      double elapsedAfterStop = testSubject.CurrentLapTime.TotalMilliseconds;

      Assert.AreEqual(1, testSubject.LapTimes.Count);
      Assert.IsTrue(elapsedAfterStop > elapsedAfterWait);
    }

    /// <summary>Tests the lap time functionality of the <see cref="Stopwatch"/> class.</summary>
    [TestMethod]
    public void TestCase04_NewLaps() {
      /* Create the test subject */
      Stopwatch testSubject = new Stopwatch();

      /* Start the stopwatch and wait a little while */
      testSubject.Start();
      Thread.Sleep(500);

      /* Start a new lap and check the properties */
      TimeSpan lapTime = testSubject.NewLap();

      Assert.AreEqual(1, testSubject.LapTimes.Count);
      Assert.AreEqual(lapTime.Ticks, testSubject.LapTimes[0].Ticks);
      Assert.IsTrue(testSubject.CurrentLapTime.TotalMilliseconds < lapTime.TotalMilliseconds);
    }
  }
}
