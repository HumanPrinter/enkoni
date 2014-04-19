namespace Enkoni.Framework.ServiceModel.Tests {
  /// <summary>Implements the ITestService in a very simple way.</summary>
  public class TestService : ITestService {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="TestService"/> class.</summary>
    public TestService() {
    }
    #endregion

    #region ITestService Members
    /// <summary>A dummy method.</summary>
    /// <returns>Some dummy value.</returns>
    public string SayHello() {
      return "Hello";
    }

    /// <summary>A dummy method using a complex type.</summary>
    /// <param name="obj">Some dummy parameter.</param>
    /// <returns>Some dummy result.</returns>
    public TestDataContract ProcessObject(TestDataContract obj) {
      obj.SomeName = "Hello " + obj.SomeName;
      ++obj.SomeNumber;

      return obj;
    }
    #endregion
  }
}
