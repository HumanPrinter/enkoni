//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceRepository.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Holds the default implementation of a repository that uses a WCF-service as datasource.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System.ServiceModel;
using System.ServiceModel.Channels;

namespace OscarBrouwer.Framework.Entities {
  /// <summary>This abstract class extends the abstract <see cref="Repository{T}"/> class and implements some of the 
  /// functionality using WCF datacommunication. This implementation can be used a base for any WCF-service repositories.
  /// </summary>
  /// <typeparam name="TEntity">The type of the entity that is handled by this repository.</typeparam>
  public abstract class ServiceRepository<TEntity> : Repository<TEntity>
    where TEntity : class, new() {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="ServiceRepository{TEntity}"/> class using the specified
    /// <see cref="DataSourceInfo"/>.</summary>
    /// <param name="dataSourceInfo">The datasource information that must be used to access the sourcefile.</param>
    protected ServiceRepository(DataSourceInfo dataSourceInfo)
      : base() {
      /* Determine if the supported properties have been specified */
      if(ServiceSourceInfo.IsEndpointConfigurationNameSpecified(dataSourceInfo)) {
        this.EndpointConfigurationName = ServiceSourceInfo.SelectEndpointConfigurationName(dataSourceInfo);
      }

      if(ServiceSourceInfo.IsRemoteAddressSpecified(dataSourceInfo)) {
        this.RemoteAddress = ServiceSourceInfo.SelectRemoteAddress(dataSourceInfo);
      }

      if(ServiceSourceInfo.IsBindingSpecified(dataSourceInfo)) {
        this.Binding = ServiceSourceInfo.SelectBinding(dataSourceInfo);
      }
    }
    #endregion

    #region Protected properties
    /// <summary>Gets the endpointconfigurationname that references the used endpointconfiguration.</summary>
    protected string EndpointConfigurationName { get; private set; }

    /// <summary>Gets the address of the remote service.</summary>
    protected EndpointAddress RemoteAddress { get; private set; }

    /// <summary>Gets the binding that is used to communicate with the remote service.</summary>
    protected Binding Binding { get; private set; }
    #endregion

    #region Repository<T> overrides
    /// <summary>Creates a new entity of type <typeparamref name="TEntity"/>. This is done by calling the default
    /// constructor of <typeparamref name="TEntity"/>.</summary>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The created entity.</returns>
    protected override TEntity CreateEntityCore(DataSourceInfo dataSourceInfo) {
      TEntity entity = new TEntity();
      return entity;
    }
    #endregion
  }
}
