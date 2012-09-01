//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Logger.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Defines the class that sends logmessages to the Logging Application Block.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Enkoni.Framework.Logging {
  /// <summary>This class sends logs messages using the Logging Application Block which is part of the Microsoft Entrprise Library. An instance of
  /// this class can be retrieved through the <see cref="LogManager"/> class.</summary>
  public sealed class Logger {
    #region Private instance variables
    /// <summary>The actual logger. When set to null, no logging will be performed.</summary>
    private LogWriter logWriter;
    #endregion

    #region Internal constructors
    /// <summary>Initializes a new instance of the <see cref="Logger"/> class using the specified <see cref="LogWriter"/>.</summary>
    /// <param name="logWriter">The actual logger that mist be used. Use <see langword="null"/> to disable logging.</param>
    internal Logger(LogWriter logWriter) {
      this.logWriter = logWriter;
    }
    #endregion

    #region General Log-methods
    /// <summary>Logs the specified <see cref="LogEntry"/> using the pre-configured logger. If <paramref name="logEntry"/> is <see langword="null"/> 
    /// or there is no pre-configured logger, nothing will be logged.</summary>
    /// <param name="logEntry">The entry containing all the information that must be logged.</param>
    public void Log(LogEntry logEntry) {
      if(this.logWriter != null && logEntry != null) {
        this.logWriter.Write(logEntry);
      }
    }

    /// <summary>Logs a message using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="severity">The severity of the message.</param>
    /// <param name="category">The category of the message. Use <see langword="null"/> to ignore the value and use the default.</param>
    /// <param name="priority">The priority of the message. Use <c>-1</c> to ignore the value and use the default.</param>
    /// <param name="title">The title of the message. Use <see langword="null"/> to ignore the value and use the default.</param>
    /// <param name="eventId">The EventId that must be logged. Use <c>-1</c> to ignore the value and use the default.</param>
    /// <param name="activityId">The ActivityId that must be logged. Use <see cref="Guid.Empty"/> to ignore the value and use the default.</param>
    /// <param name="relatedActivityId">The Id of the related activity. Use <see langword="null"/> to ignore the value and use the default.</param>
    public void Log(string message, TraceEventType severity, string category, int priority, string title, int eventId, Guid activityId,
      Guid? relatedActivityId) {
      if(this.logWriter == null || string.IsNullOrEmpty(message) || Enum.IsDefined(typeof(TraceEventType), severity)) {
        return;
      }

      LogEntry logEntry = new LogEntry();
      logEntry.Message = message;
      logEntry.Severity = severity;
      if(category != null) {
        logEntry.Categories.Add(category);
      }

      if(priority != -1) {
        logEntry.Priority = priority;
      }

      if(title != null) {
        logEntry.Title = title;
      }

      if(eventId != -1) {
        logEntry.EventId = eventId;
      }

      if(activityId != Guid.Empty) {
        logEntry.ActivityId = activityId;
      }

      if(relatedActivityId.HasValue) {
        logEntry.RelatedActivityId = relatedActivityId;
      }

      this.Log(logEntry);
    }
    #endregion

    #region Info Log-methods
    /// <summary>Logs an informational message using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Information"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    public void Info(string message) {
      this.Info(message, null);
    }

    /// <summary>Logs an informational message using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Information"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="category">The category of the message. Use <see langword="null"/> to ignore the value and use the default.</param>
    public void Info(string message, string category) {
      this.Info(message, category, -1);
    }

    /// <summary>Logs an informational message using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Information"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="category">The category of the message. Use <see langword="null"/> to ignore the value and use the default.</param>
    /// <param name="priority">The priority of the message. Use <c>-1</c> to ignore the value and use the default.</param>
    public void Info(string message, string category, int priority) {
      this.Log(message, TraceEventType.Information, category, priority, null, -1, Guid.Empty, null);
    }
    #endregion

    #region Verbose Log-methods
    /// <summary>Logs a verbose message using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Verbose"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    public void Verbose(string message) {
      this.Verbose(message, null);
    }

    /// <summary>Logs a verbose message using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Verbose"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="category">The category of the message. Use <see langword="null"/> to ignore the value and use the default.</param>
    public void Verbose(string message, string category) {
      this.Verbose(message, category, -1);
    }

    /// <summary>Logs a verbose message using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Verbose"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="category">The category of the message. Use <see langword="null"/> to ignore the value and use the default.</param>
    /// <param name="priority">The priority of the message. Use <c>-1</c> to ignore the value and use the default.</param>
    public void Verbose(string message, string category, int priority) {
      this.Log(message, TraceEventType.Verbose, category, priority, null, -1, Guid.Empty, null);
    }
    #endregion

    #region Warning Log-methods
    /// <summary>Logs a warning using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Warning"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    public void Warn(string message) {
      this.Warn(message, (string)null);
    }

    /// <summary>Logs a warning using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Warning"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="exception">The exception whose details must be included in the logmessage.</param>
    public void Warn(string message, Exception exception) {
      this.Warn(message, null, exception);
    }

    /// <summary>Logs a warning using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Warning"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="category">The category of the message. Use <see langword="null"/> to ignore the value and use the default.</param>
    public void Warn(string message, string category) {
      this.Warn(message, category, -1);
    }

    /// <summary>Logs a warning using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Warning"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="category">The category of the message. Use <see langword="null"/> to ignore the value and use the default.</param>
    /// <param name="exception">The exception whose details must be included in the logmessage.</param>
    public void Warn(string message, string category, Exception exception) {
      this.Warn(message, category, -1, exception);
    }

    /// <summary>Logs a warning using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Warning"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="category">The category of the message. Use <see langword="null"/> to ignore the value and use the default.</param>
    /// <param name="priority">The priority of the message. Use <c>-1</c> to ignore the value and use the default.</param>
    public void Warn(string message, string category, int priority) {
      this.Log(message, TraceEventType.Warning, category, priority, null, -1, Guid.Empty, null);
    }

    /// <summary>Logs a warning using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Warning"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="category">The category of the message. Use <see langword="null"/> to ignore the value and use the default.</param>
    /// <param name="priority">The priority of the message. Use <c>-1</c> to ignore the value and use the default.</param>
    /// <param name="exception">The exception whose details must be included in the logmessage.</param>
    public void Warn(string message, string category, int priority, Exception exception) {
      if(exception == null) {
        this.Log(message, TraceEventType.Warning, category, priority, null, -1, Guid.Empty, null);
      }
      else if(this.logWriter != null) {
        LogEntry logEntry = new LogEntry();
        logEntry.Message = message;
        logEntry.Severity = TraceEventType.Warning;
        if(category != null) {
          logEntry.Categories.Add(category);
        }

        if(priority != -1) {
          logEntry.Priority = priority;
        }

        logEntry.ExtendedProperties.Add("ExceptionType", exception.GetType());
        logEntry.ExtendedProperties.Add("ExceptionMessage", exception.Message);
        logEntry.ExtendedProperties.Add("ExceptionSource", exception.Source);
        if(exception.InnerException != null) {
          logEntry.ExtendedProperties.Add("InnerException", exception.InnerException);
        }

        logEntry.ExtendedProperties.Add("StackTrace", exception.StackTrace);

        this.Log(logEntry);
      }
    }
    #endregion

    #region Error Log-methods
    /// <summary>Logs an error using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Error"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    public void Error(string message) {
      this.Error(message, (string)null);
    }

    /// <summary>Logs an error using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Error"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="exception">The exception whose details must be included in the logmessage.</param>
    public void Error(string message, Exception exception) {
      this.Error(message, null, exception);
    }

    /// <summary>Logs an error using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Error"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="category">The category of the message. Use <see langword="null"/> to ignore the value and use the default.</param>
    public void Error(string message, string category) {
      this.Error(message, category, -1);
    }

    /// <summary>Logs an error using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Error"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="category">The category of the message. Use <see langword="null"/> to ignore the value and use the default.</param>
    /// <param name="exception">The exception whose details must be included in the logmessage.</param>
    public void Error(string message, string category, Exception exception) {
      this.Error(message, category, -1, exception);
    }

    /// <summary>Logs an error using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Error"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="category">The category of the message. Use <see langword="null"/> to ignore the value and use the default.</param>
    /// <param name="priority">The priority of the message. Use <c>-1</c> to ignore the value and use the default.</param>
    public void Error(string message, string category, int priority) {
      this.Log(message, TraceEventType.Error, category, priority, null, -1, Guid.Empty, null);
    }

    /// <summary>Logs an error using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Error"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="category">The category of the message. Use <see langword="null"/> to ignore the value and use the default.</param>
    /// <param name="priority">The priority of the message. Use <c>-1</c> to ignore the value and use the default.</param>
    /// <param name="exception">The exception whose details must be included in the logmessage.</param>
    public void Error(string message, string category, int priority, Exception exception) {
      if(exception == null) {
        this.Log(message, TraceEventType.Error, category, priority, null, -1, Guid.Empty, null);
      }
      else if(this.logWriter != null) {
        LogEntry logEntry = new LogEntry();
        logEntry.Message = message;
        logEntry.Severity = TraceEventType.Error;
        if(category != null) {
          logEntry.Categories.Add(category);
        }

        if(priority != -1) {
          logEntry.Priority = priority;
        }

        logEntry.ExtendedProperties.Add("ExceptionType", exception.GetType());
        logEntry.ExtendedProperties.Add("ExceptionMessage", exception.Message);
        logEntry.ExtendedProperties.Add("ExceptionSource", exception.Source);
        if(exception.InnerException != null) {
          logEntry.ExtendedProperties.Add("InnerException", exception.InnerException);
        }

        logEntry.ExtendedProperties.Add("StackTrace", exception.StackTrace);

        this.Log(logEntry);
      }
    }
    #endregion

    #region Citical Log-methods
    /// <summary>Logs a critical message using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Critical"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    public void Critical(string message) {
      this.Critical(message, (string)null);
    }

    /// <summary>Logs a critical message using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Critical"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="exception">The exception whose details must be included in the logmessage.</param>
    public void Critical(string message, Exception exception) {
      this.Critical(message, null, exception);
    }

    /// <summary>Logs a critical message using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Critical"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="category">The category of the message. Use <see langword="null"/> to ignore the value and use the default.</param>
    public void Critical(string message, string category) {
      this.Critical(message, category, -1);
    }

    /// <summary>Logs a critical message using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Critical"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="category">The category of the message. Use <see langword="null"/> to ignore the value and use the default.</param>
    /// <param name="exception">The exception whose details must be included in the logmessage.</param>
    public void Critical(string message, string category, Exception exception) {
      this.Critical(message, category, -1, exception);
    }

    /// <summary>Logs a critical message using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Critical"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="category">The category of the message. Use <see langword="null"/> to ignore the value and use the default.</param>
    /// <param name="priority">The priority of the message. Use <c>-1</c> to ignore the value and use the default.</param>
    public void Critical(string message, string category, int priority) {
      this.Log(message, TraceEventType.Critical, category, priority, null, -1, Guid.Empty, null);
    }

    /// <summary>Logs a critical message using the pre-configured logger. If there is no pre-configured logger, nothing will be logged.<br/>
    /// The logmessage will get the severity <see cref="TraceEventType.Critical"/>.</summary>
    /// <param name="message">The message that must be logged. When this value is empty, nothing will be logged.</param>
    /// <param name="category">The category of the message. Use <see langword="null"/> to ignore the value and use the default.</param>
    /// <param name="priority">The priority of the message. Use <c>-1</c> to ignore the value and use the default.</param>
    /// <param name="exception">The exception whose details must be included in the logmessage.</param>
    public void Critical(string message, string category, int priority, Exception exception) {
      if(exception == null) {
        this.Log(message, TraceEventType.Critical, category, priority, null, -1, Guid.Empty, null);
      }
      else if(this.logWriter != null) {
        LogEntry logEntry = new LogEntry();
        logEntry.Message = message;
        logEntry.Severity = TraceEventType.Critical;
        if(category != null) {
          logEntry.Categories.Add(category);
        }

        if(priority != -1) {
          logEntry.Priority = priority;
        }

        logEntry.ExtendedProperties.Add("ExceptionType", exception.GetType());
        logEntry.ExtendedProperties.Add("ExceptionMessage", exception.Message);
        logEntry.ExtendedProperties.Add("ExceptionSource", exception.Source);
        if(exception.InnerException != null) {
          logEntry.ExtendedProperties.Add("InnerException", exception.InnerException);
        }

        logEntry.ExtendedProperties.Add("StackTrace", exception.StackTrace);

        this.Log(logEntry);
      }
    }
    #endregion
  }
}
