using System;
using System.Runtime.Serialization;

namespace Enkoni.Framework.ServiceModel.Tests {
  /// <summary>Defines a data contract that is used during the tests.</summary>
  [DataContract(Namespace = "http://test.enkoni.sourceforge.org/contracts")]
  public class TestDataContract {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="TestDataContract"/> class.</summary>
    public TestDataContract() {
    }
    #endregion

    #region Properties
    /// <summary>Gets or sets some dummy value.</summary>
    [DataMember(IsRequired = false)]
    public DateTime SomeDate { get; set; }

    /// <summary>Gets or sets some dummy value.</summary>
    [DataMember(IsRequired = true)]
    public string SomeName { get; set; }

    /// <summary>Gets or sets some dummy value.</summary>
    [DataMember(IsRequired = true)]
    public int SomeNumber { get; set; }
    #endregion
  }
}
