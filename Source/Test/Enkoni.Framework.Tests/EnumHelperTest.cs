//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumHelperTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the EnumHelper-class.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Threading;

using Enkoni.Framework.Tests.Properties;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the <see cref="EnumHelper"/> class.</summary>
  [TestClass]
  public class EnumHelperTest {
    #region Private helper enums
    /// <summary>A basic dummy enum to support the testcases.</summary>
    [Flags]
    private enum TestEnum {
      /// <summary>Value A.</summary>
      [LocalizedDescription("ValueA", DefaultDescription = "Default A")]
      ValueA = 1,

      /// <summary>Value B.</summary>
      [LocalizedDescription("ValueB", typeof(Resources), DefaultDescription = "Default B")]
      ValueB = 2,

      /// <summary>Value C.</summary>
      [LocalizedDescription("ValueC")]
      ValueC = 4,

      /// <summary>Value D.</summary>
      [LocalizedDescription("ValueD", typeof(Resources))]
      ValueD = 8,

      /// <summary>Value B+D.</summary>
      [LocalizedDescription("ValueINVALID", typeof(Resources), DefaultDescription = "Default B and D")]
      ValueBD = 10,

      /// <summary>Value E.</summary>
      [LocalizedDescription("ValueE1", DefaultDescription = "ValueE1")]
      [System.ComponentModel.Description("ValueE2")]
      [EnumMember(Value = "ValueE3")]
      ValueE = 16,

      /// <summary>Value F.</summary>
      [System.ComponentModel.Description("ValueF2")]
      [EnumMember(Value = "ValueF3")]
      ValueF = 32,

      /// <summary>Value G.</summary>
      [System.ComponentModel.Description]
      [EnumMember(Value = "ValueG3")]
      ValueG = 64,

      /// <summary>Value H.</summary>
      [System.ComponentModel.Description]
      ValueH = 128,

      /// <summary>Value I.</summary>
      [EnumMember(Value = "ValueI3")]
      ValueI = 256,

      /// <summary>Value J.</summary>
      [EnumMember]
      ValueJ = 512,

      /// <summary>Value K.</summary>
      ValueK = 1024
    }
    #endregion

    /// <summary>Tests the functionality ToString methods of the <see cref="EnumHelper"/> class.</summary>
    [TestMethod]
    public void TestCase01_ToString() {
      CultureInfo english = new CultureInfo("en-GB");
      CultureInfo dutch = new CultureInfo("nl-NL");

      Thread.CurrentThread.CurrentCulture = english;
      Thread.CurrentThread.CurrentUICulture = english;

      /* Test ToString using the pre-specified resources and default culture */
      string expected = Resources.ResourceManager.GetString("ValueB");
      string actual = EnumHelper.ToString(TestEnum.ValueB);
      Assert.AreEqual(expected, actual, false);

      /* Test ToString using the pre-specified resources and specific culture */
      expected = Resources.ResourceManager.GetString("ValueB", dutch);
      actual = EnumHelper.ToString(TestEnum.ValueB, dutch);
      Assert.AreEqual(expected, actual, false);

      /* Test ToString using an invalid resource key */
      expected = "Default B and D";
      actual = EnumHelper.ToString(TestEnum.ValueBD);
      Assert.AreEqual(expected, actual, false);
      actual = EnumHelper.ToString(TestEnum.ValueBD, dutch);
      Assert.AreEqual(expected, actual, false);
      /*=============================================================*/
      /* Test ToString using a specific resource and default culture */
      expected = AlternateResources.ResourceManager.GetString("ValueA");
      actual = EnumHelper.ToString(TestEnum.ValueA, AlternateResources.ResourceManager);
      Assert.AreEqual(expected, actual, false);

      /* Test ToString using a specific resource and specific culture */
      expected = AlternateResources.ResourceManager.GetString("ValueA", dutch);
      actual = EnumHelper.ToString(TestEnum.ValueA, AlternateResources.ResourceManager, dutch);
      Assert.AreEqual(expected, actual, false);

      /* Test ToString using an invalid resource key */
      expected = "Default B and D";
      actual = EnumHelper.ToString(TestEnum.ValueBD, AlternateResources.ResourceManager);
      Assert.AreEqual(expected, actual, false);
      actual = EnumHelper.ToString(TestEnum.ValueBD, AlternateResources.ResourceManager, dutch);
      Assert.AreEqual(expected, actual, false);

      expected = "ValueC";
      actual = EnumHelper.ToString(TestEnum.ValueC, AlternateResources.ResourceManager);
      Assert.AreEqual(expected, actual, false);
      actual = EnumHelper.ToString(TestEnum.ValueC, AlternateResources.ResourceManager);
      Assert.AreEqual(expected, actual, false);
      /*=============================================================*/
      /* Test ToString using a different resource and default culture */
      expected = AlternateResources.ResourceManager.GetString("ValueB");
      actual = EnumHelper.ToString(TestEnum.ValueB, AlternateResources.ResourceManager);
      Assert.AreEqual(expected, actual, false);

      /* Test ToString using a specific resource and specific culture */
      expected = AlternateResources.ResourceManager.GetString("ValueB", dutch);
      actual = EnumHelper.ToString(TestEnum.ValueB, AlternateResources.ResourceManager, dutch);
      Assert.AreEqual(expected, actual, false);

      /* Test ToString using an invalid resource key */
      expected = "Default B and D";
      actual = EnumHelper.ToString(TestEnum.ValueBD, AlternateResources.ResourceManager);
      Assert.AreEqual(expected, actual, false);
      actual = EnumHelper.ToString(TestEnum.ValueBD, AlternateResources.ResourceManager, dutch);
      Assert.AreEqual(expected, actual, false);

      expected = "ValueD";
      actual = EnumHelper.ToString(TestEnum.ValueD, AlternateResources.ResourceManager);
      Assert.AreEqual(expected, actual, false);
      actual = EnumHelper.ToString(TestEnum.ValueD, AlternateResources.ResourceManager);
      Assert.AreEqual(expected, actual, false);

      expected = "ValueE1";
      actual = EnumHelper.ToString(TestEnum.ValueE);
      Assert.AreEqual(expected, actual, false);

      expected = "ValueF2";
      actual = EnumHelper.ToString(TestEnum.ValueF);
      Assert.AreEqual(expected, actual, false);

      expected = "ValueG3";
      actual = EnumHelper.ToString(TestEnum.ValueG);
      Assert.AreEqual(expected, actual, false);

      expected = "ValueH";
      actual = EnumHelper.ToString(TestEnum.ValueH);
      Assert.AreEqual(expected, actual, false);

      expected = "ValueI3";
      actual = EnumHelper.ToString(TestEnum.ValueI);
      Assert.AreEqual(expected, actual, false);

      expected = "ValueJ";
      actual = EnumHelper.ToString(TestEnum.ValueJ);
      Assert.AreEqual(expected, actual, false);

      expected = "ValueK";
      actual = EnumHelper.ToString(TestEnum.ValueK);
      Assert.AreEqual(expected, actual, false);
    }

    /// <summary>Tests the functionality SetFlag methods of the <see cref="EnumHelper"/> class.</summary>
    [TestMethod]
    public void TestCase02_SetFlag() {
      /* Start with zero, then one-by-one set flags */
      TestEnum subject = 0;
      subject = EnumHelper.SetFlag(subject, TestEnum.ValueA);
      Assert.AreEqual(TestEnum.ValueA, subject);
      subject = EnumHelper.SetFlag(subject, TestEnum.ValueB);
      Assert.AreEqual(TestEnum.ValueA | TestEnum.ValueB, subject);
      subject = EnumHelper.SetFlag(subject, TestEnum.ValueC);
      Assert.AreEqual(TestEnum.ValueA | TestEnum.ValueB | TestEnum.ValueC, subject);
      subject = EnumHelper.SetFlag(subject, TestEnum.ValueD);
      Assert.AreEqual(TestEnum.ValueA | TestEnum.ValueB | TestEnum.ValueC | TestEnum.ValueD, subject);

      /* Start with certain value, then set two flags */
      subject = TestEnum.ValueA | TestEnum.ValueC;
      subject = EnumHelper.SetFlag(subject, TestEnum.ValueD);
      Assert.AreEqual(TestEnum.ValueA | TestEnum.ValueC | TestEnum.ValueD, subject);
      subject = EnumHelper.SetFlag(subject, TestEnum.ValueB);
      Assert.AreEqual(TestEnum.ValueA | TestEnum.ValueB | TestEnum.ValueC | TestEnum.ValueD, subject);

      /* Start with certain value, then set flag twice */
      subject = TestEnum.ValueA;
      subject = EnumHelper.SetFlag(subject, TestEnum.ValueD);
      Assert.AreEqual(TestEnum.ValueA | TestEnum.ValueD, subject);
      subject = EnumHelper.SetFlag(subject, TestEnum.ValueD);
      Assert.AreEqual(TestEnum.ValueA | TestEnum.ValueD, subject);

      /* Start with maximum, then set one flag */
      subject = TestEnum.ValueA | TestEnum.ValueB | TestEnum.ValueC | TestEnum.ValueD;
      subject = EnumHelper.SetFlag(subject, TestEnum.ValueD);
      Assert.AreEqual(TestEnum.ValueA | TestEnum.ValueB | TestEnum.ValueC | TestEnum.ValueD, subject);

      /* Start with certain value, then set invalid flag */
      subject = TestEnum.ValueB;
      subject = EnumHelper.SetFlag(subject, (TestEnum)32);
      Assert.IsFalse(Enum.IsDefined(typeof(TestEnum), subject));

      /* Set a combined flag */
      subject = TestEnum.ValueC;
      subject = EnumHelper.SetFlag(subject, TestEnum.ValueBD);
      Assert.AreEqual(TestEnum.ValueB | TestEnum.ValueC | TestEnum.ValueD, subject);

      subject = TestEnum.ValueB;
      subject = EnumHelper.SetFlag(subject, TestEnum.ValueBD);
      Assert.AreEqual(TestEnum.ValueB | TestEnum.ValueD, subject);
    }

    /// <summary>Tests the functionality UnsetFlag methods of the <see cref="EnumHelper"/> class.</summary>
    [TestMethod]
    public void TestCase03_UnsetFlag() {
      /* Start with maximum, then one-by-one unset flags */
      TestEnum subject = TestEnum.ValueA | TestEnum.ValueB | TestEnum.ValueC | TestEnum.ValueD;
      subject = EnumHelper.UnsetFlag(subject, TestEnum.ValueA);
      Assert.AreEqual(TestEnum.ValueB | TestEnum.ValueC | TestEnum.ValueD, subject);
      subject = EnumHelper.UnsetFlag(subject, TestEnum.ValueB);
      Assert.AreEqual(TestEnum.ValueC | TestEnum.ValueD, subject);
      subject = EnumHelper.UnsetFlag(subject, TestEnum.ValueC);
      Assert.AreEqual(TestEnum.ValueD, subject);
      subject = EnumHelper.UnsetFlag(subject, TestEnum.ValueD);
      Assert.AreEqual((TestEnum)0, subject);

      /* Start with certain value, then unset two flags */
      subject = TestEnum.ValueA | TestEnum.ValueC | TestEnum.ValueD;
      subject = EnumHelper.UnsetFlag(subject, TestEnum.ValueA);
      Assert.AreEqual(TestEnum.ValueC | TestEnum.ValueD, subject);
      subject = EnumHelper.UnsetFlag(subject, TestEnum.ValueD);
      Assert.AreEqual(TestEnum.ValueC, subject);

      /* Start with certain value, then unset flag twice */
      subject = TestEnum.ValueA | TestEnum.ValueC;
      subject = EnumHelper.UnsetFlag(subject, TestEnum.ValueC);
      Assert.AreEqual(TestEnum.ValueA, subject);
      subject = EnumHelper.UnsetFlag(subject, TestEnum.ValueC);
      Assert.AreEqual(TestEnum.ValueA, subject);

      /* Start with zero, then unset one flag */
      subject = 0;
      subject = EnumHelper.UnsetFlag(subject, TestEnum.ValueC);
      Assert.AreEqual((TestEnum)0, subject);

      /* Start with certain value, then unset invalid flag */
      subject = TestEnum.ValueB;
      subject = EnumHelper.UnsetFlag(subject, (TestEnum)64);
      Assert.AreEqual(TestEnum.ValueB, subject);

      /* Unset a combined flag */
      subject = TestEnum.ValueB | TestEnum.ValueC | TestEnum.ValueD;
      subject = EnumHelper.UnsetFlag(subject, TestEnum.ValueBD);
      Assert.AreEqual(TestEnum.ValueC, subject);

      subject = TestEnum.ValueB | TestEnum.ValueC;
      subject = EnumHelper.UnsetFlag(subject, TestEnum.ValueBD);
      Assert.AreEqual(TestEnum.ValueC, subject);
    }

    /// <summary>Tests the functionality ToggleFlag methods of the <see cref="EnumHelper"/> class.</summary>
    [TestMethod]
    public void TestCase04_ToggleFlag() {
      /* Start with alternating, then one-by-one toggle flags */
      TestEnum subject = TestEnum.ValueA | TestEnum.ValueC;
      subject = EnumHelper.ToggleFlag(subject, TestEnum.ValueA);
      Assert.AreEqual(TestEnum.ValueC, subject);
      subject = EnumHelper.ToggleFlag(subject, TestEnum.ValueB);
      Assert.AreEqual(TestEnum.ValueB | TestEnum.ValueC, subject);
      subject = EnumHelper.ToggleFlag(subject, TestEnum.ValueC);
      Assert.AreEqual(TestEnum.ValueB, subject);
      subject = EnumHelper.ToggleFlag(subject, TestEnum.ValueD);
      Assert.AreEqual(TestEnum.ValueB | TestEnum.ValueD, subject);

      /* Start with certain value, then toggle flag twice */
      subject = TestEnum.ValueA | TestEnum.ValueC;
      subject = EnumHelper.ToggleFlag(subject, TestEnum.ValueA);
      Assert.AreEqual(TestEnum.ValueC, subject);
      subject = EnumHelper.ToggleFlag(subject, TestEnum.ValueA);
      Assert.AreEqual(TestEnum.ValueA | TestEnum.ValueC, subject);

      /* Start with zero, then toggle one flag */
      subject = 0;
      subject = EnumHelper.ToggleFlag(subject, TestEnum.ValueB);
      Assert.AreEqual(TestEnum.ValueB, subject);

      /* Start with maximum, then toggle one flag */
      subject = TestEnum.ValueA | TestEnum.ValueB | TestEnum.ValueC | TestEnum.ValueD;
      subject = EnumHelper.ToggleFlag(subject, TestEnum.ValueB);
      Assert.AreEqual(TestEnum.ValueA | TestEnum.ValueC | TestEnum.ValueD, subject);

      /* Start with certain value, then toggle invalid flag */
      subject = TestEnum.ValueB;
      subject = EnumHelper.ToggleFlag(subject, (TestEnum)16);
      Assert.IsFalse(Enum.IsDefined(typeof(TestEnum), subject));
      subject = EnumHelper.ToggleFlag(subject, (TestEnum)16);
      Assert.AreEqual(TestEnum.ValueB, subject);

      /* Start with zero, then toggle ValueBD */
      subject = 0;
      subject = EnumHelper.ToggleFlag(subject, TestEnum.ValueBD);
      Assert.AreEqual(TestEnum.ValueB | TestEnum.ValueD, subject);

      /* Start with ValueB, then toggle ValueBD */
      subject = TestEnum.ValueB;
      subject = EnumHelper.ToggleFlag(subject, TestEnum.ValueBD);
      Assert.AreEqual(TestEnum.ValueD, subject);

      /* Start with ValueBD, then toggle ValueBD */
      subject = TestEnum.ValueBD;
      subject = EnumHelper.ToggleFlag(subject, TestEnum.ValueBD);
      Assert.AreEqual((TestEnum)0, subject);

      /* Start with ValueBD, then toggle ValueD */
      subject = TestEnum.ValueBD;
      subject = EnumHelper.ToggleFlag(subject, TestEnum.ValueD);
      Assert.AreEqual(TestEnum.ValueB, subject);
    }
  }
}
