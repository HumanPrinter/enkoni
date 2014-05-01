using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Enkoni.Framework.Entities {
  /// <summary>This class can be used by the <see cref="ServiceRepository{TEntity}"/> or any of its descendants to retrieve valuable information 
  /// about the service that is to be used. This class is added for improved usability of the DataSourceInfo in combination with the 
  /// ServiceRepository.</summary>
  public class ServiceSourceInfo : DataSourceInfo {
    #region Public constants
    /// <summary>Defines the key that is used to store and retrieve the endpointconfigurationname.</summary>
    public const string EndpointConfigurationNameKey = "EndpointConfigurationName";

    /// <summary>Defines the key that is used to store and retrieve the endpointaddress of the service.</summary>
    public const string RemoteAddressKey = "RemoteAddress";

    /// <summary>Defines the key that is used to store and retrieve the binding that is used to communicate with the service.</summary>
    public const string BindingKey = "Binding";
    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="ServiceSourceInfo"/> class using default values.</summary>
    public ServiceSourceInfo()
      : base() {
    }

    /// <summary>Initializes a new instance of the <see cref="ServiceSourceInfo"/> class using the specified endpointconfigurationname.</summary>
    /// <param name="endpointConfigurationName">The name of the endpointconfiguration in the application's configfile.</param>
    public ServiceSourceInfo(string endpointConfigurationName)
      : this(endpointConfigurationName, DataSourceInfo.DefaultCloneDataSourceItems) {
    }

    /// <summary>Initializes a new instance of the <see cref="ServiceSourceInfo"/> class using the specified endpointconfigurationname.</summary>
    /// <param name="endpointConfigurationName">The name of the endpointconfiguration in the application's configfile.</param>
    /// <param name="cloneDataSourceItems">Indicates whether or not any entity that originate from the data source should be cloned or not.</param>
    public ServiceSourceInfo(string endpointConfigurationName, bool cloneDataSourceItems)
      : base(cloneDataSourceItems) {
      this.EndpointConfigurationName = endpointConfigurationName;
    }

    /// <summary>Initializes a new instance of the <see cref="ServiceSourceInfo"/> class using the specified endpointconfigurationname and service 
    /// address.</summary>
    /// <param name="endpointConfigurationName">The name of the endpointconfiguration in the application's configfile.</param>
    /// <param name="remoteAddress">The address of the remote service.</param>
    public ServiceSourceInfo(string endpointConfigurationName, EndpointAddress remoteAddress)
      : this(endpointConfigurationName, remoteAddress, DataSourceInfo.DefaultCloneDataSourceItems) {
    }

    /// <summary>Initializes a new instance of the <see cref="ServiceSourceInfo"/> class using the specified endpointconfigurationname and service 
    /// address.</summary>
    /// <param name="endpointConfigurationName">The name of the endpointconfiguration in the application's configfile.</param>
    /// <param name="remoteAddress">The address of the remote service.</param>
    /// <param name="cloneDataSourceItems">Indicates whether or not any entity that originate from the data source should be cloned or not.</param>
    public ServiceSourceInfo(string endpointConfigurationName, EndpointAddress remoteAddress, bool cloneDataSourceItems)
      : base(cloneDataSourceItems) {
      this.EndpointConfigurationName = endpointConfigurationName;
      this.RemoteAddress = remoteAddress;
    }

    /// <summary>Initializes a new instance of the <see cref="ServiceSourceInfo"/> class using the specified endpointconfigurationname and service 
    /// address.</summary>
    /// <param name="endpointConfigurationName">The name of the endpointconfiguration in the application's configfile.</param>
    /// <param name="remoteAddress">The address of the remote service.</param>
    public ServiceSourceInfo(string endpointConfigurationName, string remoteAddress)
      : this(endpointConfigurationName, remoteAddress, DataSourceInfo.DefaultCloneDataSourceItems) {
    }

    /// <summary>Initializes a new instance of the <see cref="ServiceSourceInfo"/> class using the specified endpointconfigurationname and service 
    /// address.</summary>
    /// <param name="endpointConfigurationName">The name of the endpointconfiguration in the application's configfile.</param>
    /// <param name="remoteAddress">The address of the remote service.</param>
    /// <param name="cloneDataSourceItems">Indicates whether or not any entity that originate from the data source should be cloned or not.</param>
    public ServiceSourceInfo(string endpointConfigurationName, string remoteAddress, bool cloneDataSourceItems)
      : base(cloneDataSourceItems) {
      this.EndpointConfigurationName = endpointConfigurationName;
      this.RemoteAddress = new EndpointAddress(remoteAddress);
    }

    /// <summary>Initializes a new instance of the <see cref="ServiceSourceInfo"/> class using the specified binding and service address.</summary>
    /// <param name="binding">The binding that must be used to communicate with the service.</param>
    /// <param name="remoteAddress">The address of the remote service.</param>
    public ServiceSourceInfo(Binding binding, EndpointAddress remoteAddress)
      : this(binding, remoteAddress, DataSourceInfo.DefaultCloneDataSourceItems) {
    }

    /// <summary>Initializes a new instance of the <see cref="ServiceSourceInfo"/> class using the specified binding and service address.</summary>
    /// <param name="binding">The binding that must be used to communicate with the service.</param>
    /// <param name="remoteAddress">The address of the remote service.</param>
    /// <param name="cloneDataSourceItems">Indicates whether or not any entity that originate from the data source should be cloned or not.</param>
    public ServiceSourceInfo(Binding binding, EndpointAddress remoteAddress, bool cloneDataSourceItems)
      : base(cloneDataSourceItems) {
      this.Binding = binding;
      this.RemoteAddress = remoteAddress;
    }

    /// <summary>Initializes a new instance of the <see cref="ServiceSourceInfo"/> class using the specified default values. If the default values do 
    /// not specify any of the supported properties, the default values will be used.</summary>
    /// <param name="defaultValues">The default values that are to be used.</param>
    public ServiceSourceInfo(Dictionary<string, object> defaultValues)
      : base(defaultValues) {
      /* Check if the dictionary contains the reserved key and, if so, the key denotes a value of the correct type */
      if(defaultValues.ContainsKey(EndpointConfigurationNameKey) && !(defaultValues[EndpointConfigurationNameKey] is string)) {
        /* The key is present, but the value is of the wrong type; use 'null' as the default value. */
        this.EndpointConfigurationName = null;
      }

      if(defaultValues.ContainsKey(RemoteAddressKey)) {
        if(defaultValues[RemoteAddressKey] is string) {
          this.RemoteAddress = new EndpointAddress((string)defaultValues[RemoteAddressKey]);
        }
        else if(!(defaultValues[RemoteAddressKey] is EndpointAddress)) {
          /* The key is present, but the value is of the wrong type; use 'null' as the default value. */
          this.RemoteAddress = null;
        }
      }

      if(defaultValues.ContainsKey(BindingKey) && !(defaultValues[BindingKey] is Binding)) {
        /* The key is present, but the value is of the wrong type; use 'null' as the default value. */
        this.Binding = null;
      }
    }
    #endregion

    #region Public properties
    /// <summary>Gets or sets the name of the endpointconfiguration in the applicationconfig file.</summary>
    public string EndpointConfigurationName {
      get { return (string)this[EndpointConfigurationNameKey]; }
      set { this[EndpointConfigurationNameKey] = value; }
    }

    /// <summary>Gets or sets the endpointaddress of the service.</summary>
    public EndpointAddress RemoteAddress {
      get { return (EndpointAddress)this[RemoteAddressKey]; }
      set { this[RemoteAddressKey] = value; }
    }

    /// <summary>Gets or sets the binding that must be used to connect to the service.</summary>
    public Binding Binding {
      get { return (Binding)this[BindingKey]; }
      set { this[BindingKey] = value; }
    }
    #endregion

    #region Public static methods
    /// <summary>Determines if the endpointconfigurationname is specified in the source information.</summary>
    /// <param name="dataSourceInfo">The data source information that is queried.</param>
    /// <returns><see langword="true"/> if the value is defined; <see langword="false"/> otherwise.</returns>
    public static bool IsEndpointConfigurationNameSpecified(DataSourceInfo dataSourceInfo) {
      return dataSourceInfo != null && dataSourceInfo.IsValueSpecified(EndpointConfigurationNameKey);
    }

    /// <summary>Selects the endpointconfigurationname from the specified data source information.</summary>
    /// <param name="dataSourceInfo">The data source information that is queried.</param>
    /// <returns>The value that is stored in the data source information or <see langword="null"/> if the value could not be found.</returns>
    public static string SelectEndpointConfigurationName(DataSourceInfo dataSourceInfo) {
      if(IsEndpointConfigurationNameSpecified(dataSourceInfo)) {
        return dataSourceInfo[EndpointConfigurationNameKey] as string;
      }
      else {
        return null;
      }
    }

    /// <summary>Determines if the remote address is specified in the source information.</summary>
    /// <param name="dataSourceInfo">The data source information that is queried.</param>
    /// <returns><see langword="true"/> if the value is defined; <see langword="false"/> otherwise.</returns>
    public static bool IsRemoteAddressSpecified(DataSourceInfo dataSourceInfo) {
      return dataSourceInfo != null && dataSourceInfo.IsValueSpecified(RemoteAddressKey);
    }

    /// <summary>Selects the remote address from the specified data source information.</summary>
    /// <param name="dataSourceInfo">The data source information that is queried.</param>
    /// <returns>The value that is stored in the data source information or <see langword="null"/> if the value could not be found.</returns>
    public static EndpointAddress SelectRemoteAddress(DataSourceInfo dataSourceInfo) {
      if(IsRemoteAddressSpecified(dataSourceInfo)) {
        return dataSourceInfo[RemoteAddressKey] as EndpointAddress;
      }
      else {
        return null;
      }
    }

    /// <summary>Determines if the binding is specified in the source information.</summary>
    /// <param name="dataSourceInfo">The data source information that is queried.</param>
    /// <returns><see langword="true"/> if the value is defined; <see langword="false"/> otherwise.</returns>
    public static bool IsBindingSpecified(DataSourceInfo dataSourceInfo) {
      return dataSourceInfo != null && dataSourceInfo.IsValueSpecified(BindingKey);
    }

    /// <summary>Selects the binding from the specified data source information.</summary>
    /// <param name="dataSourceInfo">The data source information that is queried.</param>
    /// <returns>The value that is stored in the data source information or <see langword="null"/> if the value could not be found.</returns>
    public static Binding SelectBinding(DataSourceInfo dataSourceInfo) {
      if(IsBindingSpecified(dataSourceInfo)) {
        return dataSourceInfo[BindingKey] as Binding;
      }
      else {
        return null;
      }
    }
    #endregion

    #region Public methods
    /// <summary>Determines if the endpointconfigurationname is specified in the source information.</summary>
    /// <returns><see langword="true"/> if the value is defined; <see langword="false"/> otherwise.</returns>
    public bool IsEndpointConfigurationNameSpecified() {
      return this.IsValueSpecified(EndpointConfigurationNameKey);
    }

    /// <summary>Determines if the remote address is specified in the source information.</summary>
    /// <returns><see langword="true"/> if the value is defined; <see langword="false"/> otherwise.</returns>
    public bool IsRemoteAddressSpecified() {
      return this.IsValueSpecified(RemoteAddressKey);
    }

    /// <summary>Determines if the binding is specified in the source information.</summary>
    /// <returns><see langword="true"/> if the value is defined; <see langword="false"/> otherwise.</returns>
    public bool IsBindingSpecified() {
      return this.IsValueSpecified(BindingKey);
    }
    #endregion
  }
}
