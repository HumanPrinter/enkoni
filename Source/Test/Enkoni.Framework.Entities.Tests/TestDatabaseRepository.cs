using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>This specific database repository is used by the test cases to perform tests that cannot be performed on the default 
  /// <see cref="DatabaseRepository{TEntity}"/> class.</summary>
  public class TestDatabaseRepository : DatabaseRepository<TestDummy> {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="TestDatabaseRepository"/> class.</summary>
    /// <param name="dataSourceInfo">The datasource information that must be used to access the database.</param>
    public TestDatabaseRepository(DataSourceInfo dataSourceInfo)
      : base(dataSourceInfo) {
    }
    #endregion

    #region DatabaseRepository extensions
    /// <summary>Executes a business rule that will return multiple results.</summary>
    /// <param name="ruleName">The name of the business rule that must be executed.</param>
    /// <param name="ruleArguments">Any arguments that are used during the execution of the business rule.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The results of the business rule, or an empty collection if there were no results.</returns>
    protected override IEnumerable<TestDummy> ExecuteBusinessRuleWithMultipleResults(string ruleName, IEnumerable<object> ruleArguments, DataSourceInfo dataSourceInfo) {
      if(ruleName.Equals("CustomQuery_MultipleResults", StringComparison.OrdinalIgnoreCase)) {
        int divider = (int)ruleArguments.ElementAt(0);
        int desiredResult = (int)ruleArguments.ElementAt(1);
        string query = "SELECT * FROM TestDummy WHERE (NumericValue / @divider) = @desiredResult";

        SqlCeParameter dividerParam = new SqlCeParameter("@divider", divider);
        SqlCeParameter desiredResultParam = new SqlCeParameter("@desiredResult", desiredResult);

        IEnumerable<TestDummy> result = this.SelectDbContext(dataSourceInfo).Set<TestDummy>().SqlQuery(query, dividerParam, desiredResultParam).ToList();
        return result;
      }
      else {
        return base.ExecuteBusinessRuleWithMultipleResults(ruleName, ruleArguments, dataSourceInfo);
      }
    }

    /// <summary>Executes a business rule that will return a single result.</summary>
    /// <param name="ruleName">The name of the business rule that must be executed.</param>
    /// <param name="ruleArguments">Any arguments that are used during the execution of the business rule.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The result of the business rule, or <see langword="null"/> if there were no results.</returns>
    protected override TestDummy ExecuteBusinessRuleWithSingleResult(string ruleName, IEnumerable<object> ruleArguments, DataSourceInfo dataSourceInfo) {
      if(ruleName.Equals("CustomQuery_SingleResult", StringComparison.OrdinalIgnoreCase)) {
        string desiredResult = (string)ruleArguments.ElementAt(0);
        string query = "SELECT TOP 1 * FROM TestDummy WHERE (TextValue = @desiredResult)";

        IEnumerable<TestDummy> result = this.SelectDbContext(dataSourceInfo).Set<TestDummy>().SqlQuery(query, new SqlCeParameter("@desiredResult", desiredResult));
        return result.FirstOrDefault();
      }
      else {
        return base.ExecuteBusinessRuleWithSingleResult(ruleName, ruleArguments, dataSourceInfo);
      }
    }
    #endregion
  }
}
