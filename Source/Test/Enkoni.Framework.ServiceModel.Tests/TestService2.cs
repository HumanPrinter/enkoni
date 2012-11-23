// --------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="TestService2.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Implements a service that is used during the tests.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Enkoni.Framework.ServiceModel.Tests {
  /// <summary>Implements the ITestService in a very simple way.</summary>
  [FlatWsdlBehavior]
  public class TestService2 : ITestService {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="TestService2"/> class.</summary>
    public TestService2() {
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
