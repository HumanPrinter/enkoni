using System.Collections.Generic;
using System.Data.Entity;

namespace Enkoni.Framework.Entities {
  /// <summary>This class can be used by the <see cref="DatabaseRepository{TEntity}"/> to retrieve valuable information about the database that is to 
  /// be used. This class is added for improved usability of the DataSourceInfo in combination with the DatabaseRepository.</summary>
  public class DatabaseSourceInfo : DataSourceInfo {
    #region Public constants
    /// <summary>Defines the key that is used to store and retrieve the <see cref="DbContext"/>.</summary>
    public const string DbContextKey = "DbContext";

    /// <summary>Defines the key that is used to store and retrieve the <see cref="SaveGraph"/>.</summary>
    public const string SaveGraphKey = "SaveGraph";

    /// <summary>The default value for the <see cref="SaveGraph"/> that will be used when no custom value has been specified.</summary>
    public const bool DefaultSaveGraph = true;
    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="DatabaseSourceInfo"/> class using a default value of <see langword="null"/> for the 
    /// <see cref="DbContext"/>.</summary>
    public DatabaseSourceInfo()
      : this((DbContext)null) {
    }

    /// <summary>Initializes a new instance of the <see cref="DatabaseSourceInfo"/> class using the specified <see cref="DbContext"/> instance.
    /// </summary>
    /// <param name="dbContext">The database context that must be use to communicate with the database.</param>
    public DatabaseSourceInfo(DbContext dbContext)
      : this(dbContext, DataSourceInfo.DefaultCloneDataSourceItems) {
    }

    /// <summary>Initializes a new instance of the <see cref="DatabaseSourceInfo"/> class using the specified <see cref="DbContext"/> instance.
    /// </summary>
    /// <param name="dbContext">The database context that must be use to communicate with the database.</param>
    /// <param name="cloneDataSourceItems">Indicates whether or not any entity that originate from the data source should be cloned or not.</param>
    public DatabaseSourceInfo(DbContext dbContext, bool cloneDataSourceItems)
      : this(dbContext, cloneDataSourceItems, DatabaseSourceInfo.DefaultSaveGraph) {
    }

    /// <summary>Initializes a new instance of the <see cref="DatabaseSourceInfo"/> class using the specified <see cref="DbContext"/> instance.
    /// </summary>
    /// <param name="dbContext">The database context that must be use to communicate with the database.</param>
    /// <param name="cloneDataSourceItems">Indicates whether or not any entity that originate from the data source should be cloned or not.</param>
    /// <param name="saveGraph">Indicates whether or not the entire graph should be saved when saving an entity.</param>
    public DatabaseSourceInfo(DbContext dbContext, bool cloneDataSourceItems, bool saveGraph)
      : base(cloneDataSourceItems) {
      this.DbContext = dbContext;
      this.SaveGraph = saveGraph;
    }

    /// <summary>Initializes a new instance of the <see cref="DatabaseSourceInfo"/> class using the specified default values. If the default values 
    /// do not specify the <see cref="DbContext"/> using the correct key and/or type, the value <see langword="null"/> will be used.</summary>
    /// <param name="defaultValues">The default values that are to be used.</param>
    public DatabaseSourceInfo(Dictionary<string, object> defaultValues)
      : base(defaultValues) {
      /* Check if the dictionary contains the reserved key and, if so, the key denotes a value of the correct type */
      if(defaultValues.ContainsKey(DbContextKey) && !(defaultValues[DbContextKey] is DbContext)) {
        /* The key is present, but the value is of the wrong type; use 'null' as the default value. */
        this.DbContext = null;
      }

      if(defaultValues.ContainsKey(SaveGraphKey) && !(defaultValues[SaveGraphKey] is bool)) {
        /* The key is present, but the value is of the wrong type; use 'false' as the default value. */
        this.SaveGraph = DefaultSaveGraph;
      }
    }
    #endregion

    #region Public properties
    /// <summary>Gets or sets the <see cref="DbContext"/> that is to be used by the DatabaseRepository.</summary>
    public DbContext DbContext {
      get { return (DbContext)this[DbContextKey]; }
      set { this[DbContextKey] = value; }
    }

    /// <summary>Gets or sets a value indicating whether the entire graph should be saved when saving an entity.</summary>
    public bool SaveGraph {
      get { return (bool)this[SaveGraphKey]; }
      set { this[SaveGraphKey] = value; }
    }
    #endregion

    #region Public static methods
    /// <summary>Determines if the <see cref="DbContext"/> is specified in the source information.</summary>
    /// <param name="dataSourceInfo">The data source information that is queried.</param>
    /// <returns><see langword="true"/> if the <see cref="DbContext"/> is defined; <see langword="false"/> otherwise.</returns>
    public static bool IsDbContextSpecified(DataSourceInfo dataSourceInfo) {
      return dataSourceInfo != null && dataSourceInfo.IsValueSpecified(DbContextKey);
    }

    /// <summary>Selects the <see cref="DbContext"/> from the specified data source information.</summary>
    /// <param name="dataSourceInfo">The data source information that is queried.</param>
    /// <returns>The <see cref="DbContext"/> that is stored in the data source information or <see langword="null"/> if the <see cref="DbContext"/> could not be found.</returns>
    public static DbContext SelectDbContext(DataSourceInfo dataSourceInfo) {
      if(IsDbContextSpecified(dataSourceInfo)) {
        return dataSourceInfo[DbContextKey] as DbContext;
      }
      else {
        return null;
      }
    }

    /// <summary>Determines if the <see cref="SaveGraph"/> is specified in the source information.</summary>
    /// <param name="dataSourceInfo">The data source information that is queried.</param>
    /// <returns><see langword="true"/> if the <see cref="SaveGraph"/> is defined; <see langword="false"/> otherwise.</returns>
    public static bool IsSaveGraphSpecified(DataSourceInfo dataSourceInfo) {
      return dataSourceInfo != null && dataSourceInfo.IsValueSpecified(SaveGraphKey);
    }

    /// <summary>Selects the <see cref="SaveGraph"/> from the specified data source information.</summary>
    /// <param name="dataSourceInfo">The data source information that is queried.</param>
    /// <returns>The <see cref="SaveGraph"/> that is stored in the data source information or <see langword="null"/> if the <see cref="SaveGraph"/> could not be found.</returns>
    public static bool SelectSaveGraph(DataSourceInfo dataSourceInfo) {
      if(IsSaveGraphSpecified(dataSourceInfo)) {
        return (bool)dataSourceInfo[SaveGraphKey];
      }
      else {
        return DatabaseSourceInfo.DefaultSaveGraph;
      }
    }
    #endregion

    #region Public methods
    /// <summary>Determines if the <see cref="DbContext"/> is specified in the source information.</summary>
    /// <returns><see langword="true"/> if the <see cref="DbContext"/> is defined; <see langword="false"/> otherwise.</returns>
    public bool IsDbContextSpecified() {
      return this.IsValueSpecified(DbContextKey);
    }

    /// <summary>Determines if the <see cref="SaveGraph"/> is specified in the source information.</summary>
    /// <returns><see langword="true"/> if the <see cref="SaveGraph"/> is defined; <see langword="false"/> otherwise.</returns>
    public bool IsSaveGraphSpecified() {
      return this.IsValueSpecified(SaveGraphKey);
    }
    #endregion
  }
}
