using System;
using System.ServiceModel;

namespace Enkoni.Framework.ServiceModel {
  /// <summary>This class contains some extension-methods that apply to ServiceModel-related types.</summary>
  public static class Extensions {
    /// <summary>Tries to gracefully close the communication object with regards for the current state of the communications object. If this fails due
    /// to a <see cref="CommunicationException"/> or a <see cref="TimeoutException"/>, it aborts the connection. If another exception is raised during
    /// the closing of the communication channel, the connection is still aborted but after the channel has been aborted the exception is rethrown.
    /// </summary>
    /// <param name="source">The communication object that must be closed.</param>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
    public static void SafeClose(this ICommunicationObject source) {
      Guard.ArgumentIsNotNull(source, nameof(source));

      try {
        if(source.State != CommunicationState.Created && source.State != CommunicationState.Closed) {
          if(source.State != CommunicationState.Faulted) {
            source.Close();
          }
          else {
            source.Abort();
          }
        }
      }
      catch(CommunicationException) {
        if(source.State != CommunicationState.Created && source.State != CommunicationState.Closed) {
          source.Abort();
        }
      }
      catch(TimeoutException) {
        if(source.State != CommunicationState.Created && source.State != CommunicationState.Closed) {
          source.Abort();
        }
      }
      catch(Exception) {
        if(source.State != CommunicationState.Created && source.State != CommunicationState.Closed) {
          source.Abort();
        }

        throw;
      }
    }
  }
}