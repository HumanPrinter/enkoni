//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="InstanceProvider.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Holds a type that is used by the Disposable WCF Service capabilities.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace OscarBrouwer.Framework.ServiceModel {
  /// <summary>This class acts as a default instance provider except that it is able to dispose the service instances 
  /// upon release.</summary>
  public class InstanceProvider : IInstanceProvider {
    #region Instance variables
    /// <summary>The type of the class that implements the service and must be created.</summary>
    private Type serviceType;
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="InstanceProvider"/> class.</summary>
    /// <param name="serviceType">The type of service that must be created.</param>
    public InstanceProvider(Type serviceType) {
      this.serviceType = serviceType;
    }
    #endregion

    #region IInstanceProvider methods
    /// <summary>Returns a service object given the specified <see cref="InstanceContext"/> object.</summary>
    /// <param name="instanceContext">The current <see cref="InstanceContext"/> object.</param>
    /// <param name="message">The message that triggered the creation of a service object.</param>
    /// <returns>The service object.</returns>
    public object GetInstance(InstanceContext instanceContext, Message message) {
      object result = this.serviceType.GetConstructor(Type.EmptyTypes).Invoke(null);

      return result;
    }

    /// <summary>Returns a service object given the specified <see cref="InstanceContext"/> object.</summary>
    /// <param name="instanceContext">The current <see cref="InstanceContext"/> object.</param>
    /// <returns>The service object.</returns>
    public object GetInstance(InstanceContext instanceContext) {
      return this.GetInstance(instanceContext, null);
    }

    /// <summary>Called when an <see cref="InstanceContext"/> object recycles a service object.</summary>
    /// <param name="instanceContext">The service's instance context.</param>
    /// <param name="instance">The service object to be recycled.</param>
    public void ReleaseInstance(InstanceContext instanceContext, object instance) {
      IDisposable disposableInstance = instance as IDisposable;
      if(disposableInstance != null) {
        disposableInstance.Dispose();
      }
    }
    #endregion
  }
}
