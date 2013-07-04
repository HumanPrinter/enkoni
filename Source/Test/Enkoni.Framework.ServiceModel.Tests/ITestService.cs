// --------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ITestService.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Defines a service that is used during the tests.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.ServiceModel;

namespace Enkoni.Framework.ServiceModel.Tests {
  /// <summary>This interface describes a service that is used by the ServiceModel test cases.</summary>
  [ServiceContract(Namespace = "http://test.enkoni.sourceforge.org/contracts")]
  public interface ITestService {
    /// <summary>A dummy method.</summary>
    /// <returns>Some dummy value.</returns>
    [OperationContract]
    string SayHello();

    /// <summary>A dummy method using a complex type.</summary>
    /// <param name="obj">Some dummy parameter.</param>
    /// <returns>Some dummy result.</returns>
    [OperationContract]
    TestDataContract ProcessObject(TestDataContract obj);
  }
}
