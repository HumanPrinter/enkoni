using System;
using System.Diagnostics;

using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Rhino.Mocks;

namespace Enkoni.Framework.Logging.Tests {
  [TestClass]
  public class LoggerTest {
    /// <summary>Tests the functionality of the <see cref="Logger.Log(LogEntry)"/> method.</summary>
    [TestMethod]
    public void Logger_LogWithLogEntry_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);
      
      /* Call the test subject */
      LogEntry entry = new LogEntry("message", "category", 1, 1000, TraceEventType.Information, "title", null);
      testSubject.Log(entry);
      mockedLogWriter.AssertWasCalled(lw => lw.Write(Arg.Is(entry)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Log(LogEntry)"/> method when a null-reference is passed.</summary>
    [TestMethod]
    public void Logger_LogWithNullEntry_NoExceptionIsThrown() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Log(null);
      mockedLogWriter.AssertWasNotCalled(lw => lw.Write(Arg<LogEntry>.Is.Anything));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Log(LogEntry)"/> method when a null-reference is passed.</summary>
    [TestMethod]
    public void Logger_LogWithNullWriterAndNullEntry_NoExceptionIsThrown() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(null);

      /* Call the test subject */
      testSubject.Log(null);
      mockedLogWriter.AssertWasNotCalled(lw => lw.Write(Arg<LogEntry>.Is.Anything));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Log(string, TraceEventType, string, int, string, int, Guid, Guid?)"/> method.</summary>
    [TestMethod]
    public void Logger_LogWithSeperateParameters_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      Guid activityId = Guid.NewGuid();
      Guid relatedActivityId = Guid.NewGuid();

      testSubject.Log("message", TraceEventType.Information, "category", 1, "title", 1000, activityId, relatedActivityId);
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry => 
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Information &&
          entry.CategoriesStrings[0] == "category" &&
          entry.Priority == 1 &&
          entry.Title == "title" &&
          entry.EventId == 1000 &&
          entry.ActivityId == activityId &&
          entry.RelatedActivityId == relatedActivityId)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Log(string, TraceEventType, string, int, string, int, Guid, Guid?)"/> method.</summary>
    [TestMethod]
    public void Logger_LogWithEmptyMessage_LogWriterIsNotExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      Guid activityId = Guid.NewGuid();
      Guid relatedActivityId = Guid.NewGuid();

      testSubject.Log(string.Empty, TraceEventType.Information, "category", 1, "title", 1000, activityId, relatedActivityId);
      mockedLogWriter.AssertWasNotCalled(lw => lw.Write(Arg<LogEntry>.Is.Anything));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Log(string, TraceEventType, string, int, string, int, Guid, Guid?)"/> method.</summary>
    [TestMethod]
    public void Logger_LogWithNullMessage_LogWriterIsNotExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      Guid activityId = Guid.NewGuid();
      Guid relatedActivityId = Guid.NewGuid();

      testSubject.Log(null, TraceEventType.Information, "category", 1, "title", 1000, activityId, relatedActivityId);
      mockedLogWriter.AssertWasNotCalled(lw => lw.Write(Arg<LogEntry>.Is.Anything));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Log(string, TraceEventType, string, int, string, int, Guid, Guid?)"/> method.</summary>
    [TestMethod]
    public void Logger_LogWithNullCategory_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      Guid activityId = Guid.NewGuid();
      Guid relatedActivityId = Guid.NewGuid();

      testSubject.Log("message", TraceEventType.Information, null, 1, "title", 1000, activityId, relatedActivityId);
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Information &&
          entry.CategoriesStrings.Length == 0 &&
          entry.Priority == 1 &&
          entry.Title == "title" &&
          entry.EventId == 1000 &&
          entry.ActivityId == activityId &&
          entry.RelatedActivityId == relatedActivityId)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Log(string, TraceEventType, string, int, string, int, Guid, Guid?)"/> method.</summary>
    [TestMethod]
    public void Logger_LogWithNullTitle_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      Guid activityId = Guid.NewGuid();
      Guid relatedActivityId = Guid.NewGuid();

      testSubject.Log("message", TraceEventType.Information, "category", -1, null, 1000, activityId, relatedActivityId);
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Information &&
          entry.CategoriesStrings[0] == "category" &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 1000 &&
          entry.ActivityId == activityId &&
          entry.RelatedActivityId == relatedActivityId)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Log(string, TraceEventType, string, int, string, int, Guid, Guid?)"/> method.</summary>
    [TestMethod]
    public void Logger_LogWithNullRelatedActivityId_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Log("message", TraceEventType.Information, "category", 1, "title", 1000, Guid.Empty, null);
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Information &&
          entry.CategoriesStrings[0] == "category" &&
          entry.Priority == 1 &&
          entry.Title == "title" &&
          entry.EventId == 1000 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Info(string)"/> method.</summary>
    [TestMethod]
    public void Logger_InfoWithMessage_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Info("message");
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Information &&
          entry.CategoriesStrings.Length == 0 &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Info(string, string)"/> method.</summary>
    [TestMethod]
    public void Logger_InfoWithMessageAndCategory_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Info("message", "category");
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Information &&
          entry.CategoriesStrings[0] == "category" &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Info(string, string, int)"/> method.</summary>
    [TestMethod]
    public void Logger_InfoWithMessageCategoryAndPriority_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Info("message", "category", 1);
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Information &&
          entry.CategoriesStrings[0] == "category" &&
          entry.Priority == 1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Verbose(string)"/> method.</summary>
    [TestMethod]
    public void Logger_VerboseWithMessage_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Verbose("message");
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Verbose &&
          entry.CategoriesStrings.Length == 0 &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Verbose(string, string)"/> method.</summary>
    [TestMethod]
    public void Logger_VerboseWithMessageAndCategory_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Verbose("message", "category");
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Verbose &&
          entry.CategoriesStrings[0] == "category" &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Verbose(string, string, int)"/> method.</summary>
    [TestMethod]
    public void Logger_VerboseWithMessageCategoryAndPriority_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Verbose("message", "category", 1);
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Verbose &&
          entry.CategoriesStrings[0] == "category" &&
          entry.Priority == 1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Warn(string)"/> method.</summary>
    [TestMethod]
    public void Logger_WarnWithMessage_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Warn("message");
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Warning &&
          entry.CategoriesStrings.Length == 0 &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Warn(string, string)"/> method.</summary>
    [TestMethod]
    public void Logger_WarnWithMessageAndCategory_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Warn("message", "category");
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Warning &&
          entry.CategoriesStrings[0] == "category" &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Warn(string, string, int)"/> method.</summary>
    [TestMethod]
    public void Logger_WarnWithMessageCategoryAndPriority_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Warn("message", "category", 1);
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Warning &&
          entry.CategoriesStrings[0] == "category" &&
          entry.Priority == 1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Warn(string, Exception)"/> method.</summary>
    [TestMethod]
    public void Logger_WarnWithMessageAndNullException_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Warn("message", (InvalidOperationException)null);
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Warning &&
          entry.CategoriesStrings.Length == 0 &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Warn(string, Exception)"/> method.</summary>
    [TestMethod]
    public void Logger_WarnWithMessageAndException_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Warn("message", new InvalidOperationException("Some invalid operation"));
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Warning &&
          entry.CategoriesStrings.Length == 0 &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null &&
          (Type)entry.ExtendedProperties["ExceptionType"] == typeof(InvalidOperationException) &&
          (string)entry.ExtendedProperties["ExceptionMessage"] == "Some invalid operation")));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Warn(string, Exception)"/> method.</summary>
    [TestMethod]
    public void Logger_WarnWithMessageExceptionAndInnerException_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Warn("message", new InvalidOperationException("Some invalid operation", new NotSupportedException("base exception")));
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Warning &&
          entry.CategoriesStrings.Length == 0 &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null &&
          (Type)entry.ExtendedProperties["ExceptionType"] == typeof(InvalidOperationException) &&
          (string)entry.ExtendedProperties["ExceptionMessage"] == "Some invalid operation" &&
          ((NotSupportedException)entry.ExtendedProperties["InnerException"]).Message == "base exception")));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Warn(string, Exception)"/> method.</summary>
    [TestMethod]
    public void Logger_WarnWithMessageCategoryAndException_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Warn("message", "category", new InvalidOperationException("Some invalid operation"));
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Warning &&
          entry.CategoriesStrings[0] == "category" &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null &&
          (Type)entry.ExtendedProperties["ExceptionType"] == typeof(InvalidOperationException) &&
          (string)entry.ExtendedProperties["ExceptionMessage"] == "Some invalid operation")));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Warn(string, Exception)"/> method.</summary>
    [TestMethod]
    public void Logger_WarnWithMessageCategoryPriorityAndException_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Warn("message", "category", 1, new InvalidOperationException("Some invalid operation"));
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Warning &&
          entry.CategoriesStrings[0] == "category" &&
          entry.Priority == 1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null &&
          (Type)entry.ExtendedProperties["ExceptionType"] == typeof(InvalidOperationException) &&
          (string)entry.ExtendedProperties["ExceptionMessage"] == "Some invalid operation")));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Error(string)"/> method.</summary>
    [TestMethod]
    public void Logger_ErrorWithMessage_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Error("message");
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Error &&
          entry.CategoriesStrings.Length == 0 &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Error(string, string)"/> method.</summary>
    [TestMethod]
    public void Logger_ErrorWithMessageAndCategory_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Error("message", "category");
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Error &&
          entry.CategoriesStrings[0] == "category" &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Error(string, string, int)"/> method.</summary>
    [TestMethod]
    public void Logger_ErrorWithMessageCategoryAndPriority_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Error("message", "category", 1);
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Error &&
          entry.CategoriesStrings[0] == "category" &&
          entry.Priority == 1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Error(string, Exception)"/> method.</summary>
    [TestMethod]
    public void Logger_ErrorWithMessageAndNullException_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Error("message", (InvalidOperationException)null);
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Error &&
          entry.CategoriesStrings.Length == 0 &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Error(string, Exception)"/> method.</summary>
    [TestMethod]
    public void Logger_ErrorWithMessageAndException_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Error("message", new InvalidOperationException("Some invalid operation"));
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Error &&
          entry.CategoriesStrings.Length == 0 &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null &&
          (Type)entry.ExtendedProperties["ExceptionType"] == typeof(InvalidOperationException) &&
          (string)entry.ExtendedProperties["ExceptionMessage"] == "Some invalid operation")));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Error(string, Exception)"/> method.</summary>
    [TestMethod]
    public void Logger_ErrorWithMessageExceptionAndInnerException_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Error("message", new InvalidOperationException("Some invalid operation", new NotSupportedException("base exception")));
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Error &&
          entry.CategoriesStrings.Length == 0 &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null &&
          (Type)entry.ExtendedProperties["ExceptionType"] == typeof(InvalidOperationException) &&
          (string)entry.ExtendedProperties["ExceptionMessage"] == "Some invalid operation" &&
          ((NotSupportedException)entry.ExtendedProperties["InnerException"]).Message == "base exception")));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Error(string, Exception)"/> method.</summary>
    [TestMethod]
    public void Logger_ErrorWithMessageCategoryAndException_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Error("message", "category", new InvalidOperationException("Some invalid operation"));
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Error &&
          entry.CategoriesStrings[0] == "category" &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null &&
          (Type)entry.ExtendedProperties["ExceptionType"] == typeof(InvalidOperationException) &&
          (string)entry.ExtendedProperties["ExceptionMessage"] == "Some invalid operation")));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Error(string, Exception)"/> method.</summary>
    [TestMethod]
    public void Logger_ErrorWithMessageCategoryPriorityAndException_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Error("message", "category", 1, new InvalidOperationException("Some invalid operation"));
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Error &&
          entry.CategoriesStrings[0] == "category" &&
          entry.Priority == 1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null &&
          (Type)entry.ExtendedProperties["ExceptionType"] == typeof(InvalidOperationException) &&
          (string)entry.ExtendedProperties["ExceptionMessage"] == "Some invalid operation")));
    }
    
    /// <summary>Tests the functionality of the <see cref="Logger.Critical(string)"/> method.</summary>
    [TestMethod]
    public void Logger_CriticalWithMessage_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Critical("message");
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Critical &&
          entry.CategoriesStrings.Length == 0 &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Critical(string, string)"/> method.</summary>
    [TestMethod]
    public void Logger_CriticalWithMessageAndCategory_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Critical("message", "category");
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Critical &&
          entry.CategoriesStrings[0] == "category" &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Critical(string, string, int)"/> method.</summary>
    [TestMethod]
    public void Logger_CriticalWithMessageCategoryAndPriority_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Critical("message", "category", 1);
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Critical &&
          entry.CategoriesStrings[0] == "category" &&
          entry.Priority == 1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Critical(string, Exception)"/> method.</summary>
    [TestMethod]
    public void Logger_CriticalWithMessageAndNullException_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Critical("message", (InvalidOperationException)null);
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Critical &&
          entry.CategoriesStrings.Length == 0 &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null)));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Critical(string, Exception)"/> method.</summary>
    [TestMethod]
    public void Logger_CriticalWithMessageAndException_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Critical("message", new InvalidOperationException("Some invalid operation"));
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Critical &&
          entry.CategoriesStrings.Length == 0 &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null &&
          (Type)entry.ExtendedProperties["ExceptionType"] == typeof(InvalidOperationException) &&
          (string)entry.ExtendedProperties["ExceptionMessage"] == "Some invalid operation")));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Critical(string, Exception)"/> method.</summary>
    [TestMethod]
    public void Logger_CriticalWithMessageExceptionAndInnerException_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Critical("message", new InvalidOperationException("Some invalid operation", new NotSupportedException("base exception")));
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Critical &&
          entry.CategoriesStrings.Length == 0 &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null &&
          (Type)entry.ExtendedProperties["ExceptionType"] == typeof(InvalidOperationException) &&
          (string)entry.ExtendedProperties["ExceptionMessage"] == "Some invalid operation" &&
          ((NotSupportedException)entry.ExtendedProperties["InnerException"]).Message == "base exception")));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Critical(string, Exception)"/> method.</summary>
    [TestMethod]
    public void Logger_CriticalWithMessageCategoryAndException_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Critical("message", "category", new InvalidOperationException("Some invalid operation"));
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Critical &&
          entry.CategoriesStrings[0] == "category" &&
          entry.Priority == -1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null &&
          (Type)entry.ExtendedProperties["ExceptionType"] == typeof(InvalidOperationException) &&
          (string)entry.ExtendedProperties["ExceptionMessage"] == "Some invalid operation")));
    }

    /// <summary>Tests the functionality of the <see cref="Logger.Critical(string, Exception)"/> method.</summary>
    [TestMethod]
    public void Logger_CriticalWithMessageCategoryPriorityAndException_LogWriterIsExecuted() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Create the logger */
      Logger testSubject = new Logger(mockedLogWriter);

      /* Call the test subject */
      testSubject.Critical("message", "category", 1, new InvalidOperationException("Some invalid operation"));
      mockedLogWriter.AssertWasCalled(lw => lw.Write(
        Arg<LogEntry>.Matches(entry =>
          entry.Message == "message" &&
          entry.Severity == TraceEventType.Critical &&
          entry.CategoriesStrings[0] == "category" &&
          entry.Priority == 1 &&
          entry.Title == string.Empty &&
          entry.EventId == 0 &&
          entry.ActivityId == Guid.Empty &&
          entry.RelatedActivityId == null &&
          (Type)entry.ExtendedProperties["ExceptionType"] == typeof(InvalidOperationException) &&
          (string)entry.ExtendedProperties["ExceptionMessage"] == "Some invalid operation")));
    }
  }
}
