using System.Data.Entity;

using Microsoft.SqlServer.Dac;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>Contains test cases that replay discovered bug scenarios to ensure a bug doesn't re-appear in a future release.</summary>
  //[TestClass]
  [DeploymentItem(@"Enkoni.Framework.Entities.Tests.Database.dacpac")]
  [DeploymentItem(@"EntityFramework.SqlServer.dll")]
  [DeploymentItem(@"EntityFramework.SqlServerCompact.dll")]
  public class BugScenarios {
    /// <summary>Initializes the test methods in this class.</summary>
    /// <param name="context">The test context.</param>
    [ClassInitialize]
    public static void Initialize(TestContext context) {
      DacDeployOptions options = new DacDeployOptions {
        CreateNewDatabase = true,
        DeployDatabaseInSingleUserMode= true,
        BlockOnPossibleDataLoss = false
      };
      
      DacPackage package = DacPackage.Load(@"Enkoni.Framework.Entities.Tests.Database.dacpac", DacSchemaModelStorageType.File);
      DacServices services = new DacServices(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=Enkoni.Framework.Entities.Tests.Database;Integrated Security=True;Pooling=False;Connect Timeout=30");
      services.Deploy(package, "Enkoni.Framework.Entities.Tests.Database", upgradeExisting: true, options: options);
    }

    /// <summary>Replays the bug described in bug 12. Querying entities by RecordID causes an entity framework exception.</summary>
    /// <seealso href="https://humanprinter.visualstudio.com/DefaultCollection/Enkoni.Framework/_workitems/edit/12"/>
    //[TestMethod]
    public void Bug01_QueryById() {
      Database.SetInitializer<DatabaseRepositoryTestContext>(null);
      DbContext context = new DatabaseRepositoryTestContext("BugScenarios");
      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context);

      SubDomainModel<TestDummy> subDomain = new TestSubDomainModel(new TestDatabaseRepository(sourceInfo));

      TestDummy result = subDomain.FindEntityById(2);
      
      Assert.IsNotNull(result);
      Assert.AreEqual(2, result.RecordId);
      Assert.AreEqual("TestDummy 2", result.TextValue);
    }
  }
}
