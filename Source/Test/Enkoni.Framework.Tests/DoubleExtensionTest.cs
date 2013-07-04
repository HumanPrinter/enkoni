//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="DoubleExtensionTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the extension methods for the double-struct.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the extension methods for the double-struct.</summary>
  [TestClass]
  public class DoubleExtensionTest {
    #region CompareTo test cases
    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method.
    /// </summary>
    [TestMethod]
    public void TestCase01_CompareToMargin() {
      /* Use two doubles that are exactly a-like */
      double leftOperand = 0.002;
      double rightOperand = 0.002;

      /* Determine the result using the default CompareTo-implementation */
      int defaultResult = leftOperand.CompareTo(rightOperand);
      Assert.AreEqual(0, defaultResult);

      /* Use the extension method with a margin of 0 */
      int extensionResult = leftOperand.CompareTo(rightOperand, 0, DoubleCompareOption.Margin);
      Assert.AreEqual(0, extensionResult);

      /* Use the extension method with a small margin */
      extensionResult = leftOperand.CompareTo(rightOperand, 0.01, DoubleCompareOption.Margin);
      Assert.AreEqual(0, extensionResult);

      /* Use the extension method with a smaller margin */
      extensionResult = leftOperand.CompareTo(rightOperand, 0.001, DoubleCompareOption.Margin);
      Assert.AreEqual(0, extensionResult);

      /* Use the extension method with an even smaller margin */
      extensionResult = leftOperand.CompareTo(rightOperand, 0.0001, DoubleCompareOption.Margin);
      Assert.AreEqual(0, extensionResult);

      leftOperand = 0.333;
      rightOperand = 1.0 / 3.0;

      /* Determine the result using the default CompareTo-implementation */
      defaultResult = leftOperand.CompareTo(rightOperand);
      Assert.IsTrue(defaultResult < 0);

      /* Use the extension method with a margin of 0 */
      extensionResult = leftOperand.CompareTo(rightOperand, 0, DoubleCompareOption.Margin);
      Assert.IsTrue(extensionResult < 0);

      /* Use the extension method with a small margin */
      extensionResult = leftOperand.CompareTo(rightOperand, 0.03, DoubleCompareOption.Margin);
      Assert.AreEqual(0, extensionResult);

      /* Use the extension method with a smaller margin */
      extensionResult = leftOperand.CompareTo(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsTrue(extensionResult < 0);

      /* Determine the result using the default CompareTo-implementation */
      defaultResult = rightOperand.CompareTo(leftOperand);
      Assert.IsTrue(defaultResult > 0);

      /* Use the extension method with a margin of 0 */
      extensionResult = rightOperand.CompareTo(leftOperand, 0, DoubleCompareOption.Margin);
      Assert.IsTrue(extensionResult > 0);

      /* Use the extension method with a small margin */
      extensionResult = rightOperand.CompareTo(leftOperand, 0.03, DoubleCompareOption.Margin);
      Assert.AreEqual(0, extensionResult);

      /* Use the extension method with a smaller margin */
      extensionResult = rightOperand.CompareTo(leftOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsTrue(extensionResult > 0);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method.
    /// </summary>
    [TestMethod]
    public void TestCase02_CompareToSignificantDigits() {
      /* Use two doubles that are exactly a-like */
      double leftOperand = 0.002;
      double rightOperand = 0.002;

      /* Determine the result using the default CompareTo-implementation */
      int defaultResult = leftOperand.CompareTo(rightOperand);
      Assert.AreEqual(0, defaultResult);

      /* Use the extension method with a margin of 0 */
      int extensionResult = leftOperand.CompareTo(rightOperand, 0, DoubleCompareOption.SignificantDigits);
      Assert.AreEqual(0, extensionResult);

      /* Use the extension method with a small margin */
      extensionResult = leftOperand.CompareTo(rightOperand, 1, DoubleCompareOption.SignificantDigits);
      Assert.AreEqual(0, extensionResult);

      /* Use the extension method with a smaller margin */
      extensionResult = leftOperand.CompareTo(rightOperand, 3, DoubleCompareOption.SignificantDigits);
      Assert.AreEqual(0, extensionResult);

      /* Use the extension method with an even smaller margin */
      extensionResult = leftOperand.CompareTo(rightOperand, 4, DoubleCompareOption.SignificantDigits);
      Assert.AreEqual(0, extensionResult);

      leftOperand = 0.333;
      rightOperand = 1.0 / 3.0;

      /* Determine the result using the default CompareTo-implementation */
      defaultResult = leftOperand.CompareTo(rightOperand);
      Assert.IsTrue(defaultResult < 0);

      /* Use the extension method with a margin of 0 */
      extensionResult = leftOperand.CompareTo(rightOperand, 0, DoubleCompareOption.SignificantDigits);
      Assert.AreEqual(0, extensionResult);

      /* Use the extension method with a small margin */
      extensionResult = leftOperand.CompareTo(rightOperand, 3, DoubleCompareOption.SignificantDigits);
      Assert.AreEqual(0, extensionResult);

      /* Use the extension method with a smaller margin */
      extensionResult = leftOperand.CompareTo(rightOperand, 4, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(extensionResult < 0);

      /* Determine the result using the default CompareTo-implementation */
      defaultResult = rightOperand.CompareTo(leftOperand);
      Assert.IsTrue(defaultResult > 0);

      /* Use the extension method with a margin of 0 */
      extensionResult = rightOperand.CompareTo(leftOperand, 0, DoubleCompareOption.SignificantDigits);
      Assert.AreEqual(0, extensionResult);

      /* Use the extension method with a small margin */
      extensionResult = rightOperand.CompareTo(leftOperand, 3, DoubleCompareOption.SignificantDigits);
      Assert.AreEqual(0, extensionResult);

      /* Use the extension method with a smaller margin */
      extensionResult = rightOperand.CompareTo(leftOperand, 4, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(extensionResult > 0);
    }
    #endregion

    #region Equals test cases
    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method.
    /// </summary>
    [TestMethod]
    public void TestCase03_EqualsMargin() {
      /* Use two doubles that are exactly a-like */
      double leftOperand = 0.002;
      double rightOperand = 0.002;

      /* Determine the result using the default Equals-implementation */
      bool defaultResult = leftOperand.Equals(rightOperand);
      Assert.IsTrue(defaultResult);

      /* Use the extension method with a margin of 0 */
      bool extensionResult = leftOperand.Equals(rightOperand, 0, DoubleCompareOption.Margin);
      Assert.IsTrue(extensionResult);

      /* Use the extension method with a small margin */
      extensionResult = leftOperand.Equals(rightOperand, 0.01, DoubleCompareOption.Margin);
      Assert.IsTrue(extensionResult);

      /* Use the extension method with a smaller margin */
      extensionResult = leftOperand.Equals(rightOperand, 0.001, DoubleCompareOption.Margin);
      Assert.IsTrue(extensionResult);

      /* Use the extension method with an even smaller margin */
      extensionResult = leftOperand.Equals(rightOperand, 0.0001, DoubleCompareOption.Margin);
      Assert.IsTrue(extensionResult);

      leftOperand = 0.333;
      rightOperand = 1.0 / 3.0;

      /* Determine the result using the default Equals-implementation */
      defaultResult = leftOperand.Equals(rightOperand);
      Assert.IsFalse(defaultResult);

      /* Use the extension method with a margin of 0 */
      extensionResult = leftOperand.Equals(rightOperand, 0, DoubleCompareOption.Margin);
      Assert.IsFalse(extensionResult);

      /* Use the extension method with a small margin */
      extensionResult = leftOperand.Equals(rightOperand, 0.03, DoubleCompareOption.Margin);
      Assert.IsTrue(extensionResult);

      /* Use the extension method with a smaller margin */
      extensionResult = leftOperand.Equals(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsFalse(extensionResult);

      /* Determine the result using the default CompareTo-implementation */
      defaultResult = rightOperand.Equals(leftOperand);
      Assert.IsFalse(defaultResult);

      /* Use the extension method with a margin of 0 */
      extensionResult = rightOperand.Equals(leftOperand, 0, DoubleCompareOption.Margin);
      Assert.IsFalse(extensionResult);

      /* Use the extension method with a small margin */
      extensionResult = rightOperand.Equals(leftOperand, 0.03, DoubleCompareOption.Margin);
      Assert.IsTrue(extensionResult);

      /* Use the extension method with a smaller margin */
      extensionResult = rightOperand.Equals(leftOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsFalse(extensionResult);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method.
    /// </summary>
    [TestMethod]
    public void TestCase04_EqualsSignificantDigits() {
      /* Use two doubles that are exactly a-like */
      double leftOperand = 0.002;
      double rightOperand = 0.002;

      /* Determine the result using the default Equals-implementation */
      bool defaultResult = leftOperand.Equals(rightOperand);
      Assert.IsTrue(defaultResult);

      /* Use the extension method with no significant digits */
      bool extensionResult = leftOperand.Equals(rightOperand, 0, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(extensionResult);

      /* Use the extension method with some significant digits */
      extensionResult = leftOperand.Equals(rightOperand, 1, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(extensionResult);

      /* Use the extension method with a bit more significant digits */
      extensionResult = leftOperand.Equals(rightOperand, 3, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(extensionResult);

      /* Use the extension method with even more significant digits */
      extensionResult = leftOperand.Equals(rightOperand, 4, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(extensionResult);

      leftOperand = 0.333;
      rightOperand = 1.0 / 3.0;

      /* Determine the result using the default Equals-implementation */
      defaultResult = leftOperand.Equals(rightOperand);
      Assert.IsFalse(defaultResult);

      /* Use the extension method with no significant digits */
      extensionResult = leftOperand.Equals(rightOperand, 0, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(extensionResult);

      /* Use the extension method with some significant digits */
      extensionResult = leftOperand.Equals(rightOperand, 3, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(extensionResult);

      /* Use the extension method with a bit more significant digits */
      extensionResult = leftOperand.Equals(rightOperand, 4, DoubleCompareOption.SignificantDigits);
      Assert.IsFalse(extensionResult);

      /* Determine the result using the default Equals-implementation */
      defaultResult = rightOperand.Equals(leftOperand);
      Assert.IsFalse(defaultResult);

      /* Use the extension method with no significant digits */
      extensionResult = rightOperand.Equals(leftOperand, 0, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(extensionResult);

      /* Use the extension method with some significant digits */
      extensionResult = rightOperand.Equals(leftOperand, 3, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(extensionResult);

      /* Use the extension method with a bit more significant digits */
      extensionResult = rightOperand.Equals(leftOperand, 4, DoubleCompareOption.SignificantDigits);
      Assert.IsFalse(extensionResult);
    }
    #endregion
  }
}
