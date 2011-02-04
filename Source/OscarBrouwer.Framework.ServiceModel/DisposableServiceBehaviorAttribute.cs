//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="DisposableServiceBehaviorAttribute.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Holds an attribute that can be used for disposable WCF service-implementations.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Enkoni.Framework.ServiceModel {
  /// <summary>This behavior makes it possible to properly dispose a service instance upon release.</summary>
  [AttributeUsage(AttributeTargets.Class)]
  public sealed class DisposableServiceBehaviorAttribute : Attribute, IServiceBehavior {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="DisposableServiceBehaviorAttribute"/> class.</summary>
    public DisposableServiceBehaviorAttribute() {
    }
    #endregion

    #region IServiceBehavior methods
    /// <summary>This method is not implemented.</summary>
    /// <param name="serviceDescription">The parameter is not used.</param>
    /// <param name="serviceHostBase">The parameter is not used.</param>
    /// <param name="endpoints">The parameter is not used.</param>
    /// <param name="bindingParameters">The parameter is not used.</param>
    public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
      Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters) {
    }

    /// <summary>Provides the ability to change run-time property values or insert custom extension objects such as 
    /// error handlers, message or parameter interceptors, security extensions, and other custom extension objects. 
    /// <br/>
    /// This implementation sets the <c>InstanceProvider</c> of each endpoint to an instance of 
    /// <see cref="InstanceProvider"/>.</summary>
    /// <param name="serviceDescription">The service description.</param>
    /// <param name="serviceHostBase">The host that is currently being built.</param>
    /// <exception cref="ArgumentNullException">one or more parameters are null.</exception>
    public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) {
      if(serviceDescription == null) {
        throw new ArgumentNullException("serviceDescription");
      }

      if(serviceHostBase == null) {
        throw new ArgumentNullException("serviceHostBase");
      }

      foreach(ChannelDispatcherBase dispatcherBase in serviceHostBase.ChannelDispatchers) {
        ChannelDispatcher dispatcher = dispatcherBase as ChannelDispatcher;
        if(dispatcher != null) {
          foreach(EndpointDispatcher endpointDispatcher in dispatcher.Endpoints) {
            endpointDispatcher.DispatchRuntime.InstanceProvider = new InstanceProvider(serviceDescription.ServiceType);
          }
        }
      }
    }

    /// <summary>Provides the ability to inspect the service host and the service description to confirm that the 
    /// service can run successfully. <br/>
    /// This implementation validates if the service type implements the <see cref="IDisposable"/> interface.</summary>
    /// <param name="serviceDescription">The service description.</param>
    /// <param name="serviceHostBase">The host that is currently being constructed.</param>
    /// <exception cref="ArgumentNullException"><paramref name="serviceDescription"/> is null.</exception>
    /// <exception cref="ArgumentException">The service-implementation does not implement <see cref="IDisposable"/>.
    /// </exception>
    public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) {
      if(serviceDescription == null) {
        throw new ArgumentNullException("serviceDescription");
      }

      if(!serviceDescription.ServiceType.GetInterfaces().Contains(typeof(IDisposable))) {
        throw new ArgumentException("The ServiceType does not implement IDisposable");
      }
    }
    #endregion
  }
}
