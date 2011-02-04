//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Holds some WCF related extension methods.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.ServiceModel;

namespace Enkoni.Framework.ServiceModel {
  /// <summary>This class contains some extension-methods that apply to ServiceModel-related types.</summary>
  public static class Extensions {
    /// <summary>Tries to gracefully close the clientobject. If this fails, it aborts the connection.</summary>
    /// <typeparam name="TChannel">The channel to be used to connect to the service.</typeparam>
    /// <param name="source">The instance that is connected to the service.</param>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
    public static void SafeClose<TChannel>(this ClientBase<TChannel> source) where TChannel : class {
      if(source == null) {
        throw new ArgumentNullException("source");
      }

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