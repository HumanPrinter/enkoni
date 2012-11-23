// --------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="TestDataContract.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the FlatWsdlBehaviorTest classes.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace Enkoni.Framework.ServiceModel.Tests {
  /// <summary>Defines a data contract that is used during the tests.</summary>
  [DataContract]
  public class TestDataContract {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="TestDataContract"/> class.</summary>
    public TestDataContract() {
    }
    #endregion

    #region Properties
    /// <summary>Gets or sets some dummy value.</summary>
    [DataMember]
    public DateTime SomeDate { get; set; }

    /// <summary>Gets or sets some dummy value.</summary>
    [DataMember]
    public string SomeName { get; set; }

    /// <summary>Gets or sets some dummy value.</summary>
    [DataMember]
    public int SomeNumber { get; set; }
    #endregion
  }
}
