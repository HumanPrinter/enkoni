//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseRepositoryTestContext.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Contains a helper class that is used by the repository testcases.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System.Data.Entity;

namespace OscarBrouwer.Framework.Entities.Tests {
  /// <summary>A helper class to support the testcases.</summary>
  public class DatabaseRepositoryTestContext : DbContext {
    /// <summary>Initializes a new instance of the <see cref="DatabaseRepositoryTestContext"/> class.</summary>
    public DatabaseRepositoryTestContext() {
    }

    /// <summary>Gets or sets the db-set for the TestDummy-table.</summary>
    public DbSet<TestDummy> TestDummies { get; set; }
  }
}
