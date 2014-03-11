namespace Enkoni.Framework.Entities.Database.Bugs {
  /// <summary>This specific databaserepository is used by the testcases to perform tests that cannot be performed on the default 
  /// <see cref="DatabaseRepository{TEntity}"/> class.</summary>
  public class TestDatabaseRepository : DatabaseRepository<TestDummy> {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="TestDatabaseRepository"/> class.</summary>
    /// <param name="dataSourceInfo">The datasource information that must be used to access the database.</param>
    public TestDatabaseRepository(DataSourceInfo dataSourceInfo)
      : base(dataSourceInfo) {
    }
    #endregion
  }
}
