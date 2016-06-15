using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the extension methods for the double-struct.</summary>
  [TestClass]
  public class DoubleExtensionTest {
    #region CompareTo (DoubleCompareOption.Margin) test cases
    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void CompareTo_WithZeroMargin_LeftEqualToRight_ZeroIsReturned() {
      /* Use two doubles that are exactly a-like */
      double leftOperand = 0.002;
      double rightOperand = 0.002;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0, DoubleCompareOption.Margin);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void CompareTo_WithMargin_LeftEqualToRight_ZeroIsReturned() {
      /* Use two doubles that are exactly a-like */
      double leftOperand = 0.002;
      double rightOperand = 0.002;

      /* Use the extension method with a small margin */
      int result = leftOperand.CompareTo(rightOperand, 0.01, DoubleCompareOption.Margin);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void CompareTo_WithZeroMargin_LeftLowerThanRight_NegativeNumberIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = 1.0 / 3.0;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0, DoubleCompareOption.Margin);
      Assert.IsTrue(result < 0);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithMargin_LeftLowerThanRightWithinMargin_ZeroIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = 1.0 / 3.0;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0.03, DoubleCompareOption.Margin);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithMargin_LeftLowerThanRightBeyondMargin_NegativeNumberIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = 1.0 / 3.0;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsTrue(result < 0);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void CompareTo_WithZeroMargin_LeftHigherThanRight_PositiveNumberIsReturned() {
      double leftOperand = 1.0 / 3.0;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0, DoubleCompareOption.Margin);
      Assert.IsTrue(result > 0);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithMargin_LeftHigherThanRightWithinMargin_ZeroIsReturned() {
      double leftOperand = 1.0 / 3.0;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0.03, DoubleCompareOption.Margin);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithMargin_LeftHigherThanRightBeyondMargin_PositiveNumberIsReturned() {
      double leftOperand = 1.0 / 3.0;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsTrue(result > 0);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithMargin_LeftIsNaN_NegativeNumberIsReturned() {
      double leftOperand = double.NaN;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsTrue(result < 0);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithMargin_RightIsNaN_PositiveNumberIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = double.NaN;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsTrue(result > 0);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithMargin_LeftAndRightAreNaN_ZeroIsReturned() {
      double leftOperand = double.NaN;
      double rightOperand = double.NaN;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithMargin_LeftIsNegativeInfinity_NegativeNumberIsReturned() {
      double leftOperand = double.NegativeInfinity;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsTrue(result < 0);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithMargin_RightIsNegativeInfinity_PositiveNumberIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = double.NegativeInfinity;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsTrue(result > 0);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithMargin_LeftAndRightAreNegativeInfinity_ZeroIsReturned() {
      double leftOperand = double.NegativeInfinity;
      double rightOperand = double.NegativeInfinity;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithMargin_LeftIsPositiveInfinity_PositiveNumberIsReturned() {
      double leftOperand = double.PositiveInfinity;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsTrue(result > 0);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithMargin_RightIsPositiveInfinity_NegativeNumberIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = double.PositiveInfinity;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsTrue(result < 0);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithMargin_LeftAndRightArePositiveInfinity_ZeroIsReturned() {
      double leftOperand = double.PositiveInfinity;
      double rightOperand = double.PositiveInfinity;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.AreEqual(0, result);
    }
    #endregion

    #region CompareTo (DoubleCompareOption.SignificantDigits) test cases
    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void CompareTo_WithZeroSignigicantDigits_LeftEqualToRight_ZeroIsReturned() {
      /* Use two doubles that are exactly a-like */
      double leftOperand = 0.002;
      double rightOperand = 0.002;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0, DoubleCompareOption.SignificantDigits);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void CompareTo_WithSignificantDigits_LeftEqualToRight_ZeroIsReturned() {
      /* Use two doubles that are exactly a-like */
      double leftOperand = 0.002;
      double rightOperand = 0.002;

      /* Use the extension method with a small margin */
      int result = leftOperand.CompareTo(rightOperand, 1, DoubleCompareOption.SignificantDigits);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void CompareTo_WithMoreSignificantDigitsThanOperands_LeftEqualToRight_ZeroIsReturned() {
      /* Use two doubles that are exactly a-like */
      double leftOperand = 0.002;
      double rightOperand = 0.002;

      /* Use the extension method with a small margin */
      int result = leftOperand.CompareTo(rightOperand, 5, DoubleCompareOption.SignificantDigits);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void CompareTo_WithZeroSignificantDigits_LeftLowerThanRight_ZeroIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = 1.0 / 3.0;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0, DoubleCompareOption.SignificantDigits);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithSignificantDigits_LeftLowerThanRightWithinSignificance_ZeroIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = 1.0 / 3.0;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 3, DoubleCompareOption.SignificantDigits);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithSignificantDigits_LeftLowerThanRightBeyondSignificance_NegativeNumberIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = 1.0 / 3.0;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 4, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(result < 0);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void CompareTo_WithZeroSignificantDigits_LeftHigherThanRight_ZeroIsReturned() {
      double leftOperand = 1.0 / 3.0;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0, DoubleCompareOption.SignificantDigits);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithSignificantDigits_LeftHigherThanRightWithinSignificance_ZeroIsReturned() {
      double leftOperand = 1.0 / 3.0;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 3, DoubleCompareOption.SignificantDigits);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithSignificantDigits_LeftHigherThanRightBeyondSignificance_PositiveNumberIsReturned() {
      double leftOperand = 1.0 / 3.0;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 4, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(result > 0);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithSignificantDigits_LeftIsNaN_NegativeNumberIsReturned() {
      double leftOperand = double.NaN;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 3, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(result < 0);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithSignificantDigits_RightIsNaN_PositiveNumberIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = double.NaN;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 3, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(result > 0);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithSignificantDigits_LeftAndRightAreNaN_ZeroIsReturned() {
      double leftOperand = double.NaN;
      double rightOperand = double.NaN;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 3, DoubleCompareOption.SignificantDigits);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithSignificantDigits_LeftIsNegativeInfinity_NegativeNumberIsReturned() {
      double leftOperand = double.NegativeInfinity;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 3, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(result < 0);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithSignificantDigits_RightIsNegativeInfinity_PositiveNumberIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = double.NegativeInfinity;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 3, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(result > 0);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithSignificant_LeftAndRightAreNegativeInfinity_ZeroIsReturned() {
      double leftOperand = double.NegativeInfinity;
      double rightOperand = double.NegativeInfinity;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 3, DoubleCompareOption.SignificantDigits);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithSignificantDigits_LeftIsPositiveInfinity_PositiveNumberIsReturned() {
      double leftOperand = double.PositiveInfinity;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 3, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(result > 0);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithSignificantDigits_RightIsPositiveInfinity_NegativeNumberIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = double.PositiveInfinity;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 3, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(result < 0);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void CompareTo_WithSignificantDigits_LeftAndRightArePositiveInfinity_ZeroIsReturned() {
      double leftOperand = double.PositiveInfinity;
      double rightOperand = double.PositiveInfinity;

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 3, DoubleCompareOption.SignificantDigits);
      Assert.AreEqual(0, result);
    }
    #endregion

    #region CompareTo additional test cases
    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void CompareTo_WithUnknownCompareOption_LeftEqualToRight_DefaultResultIsReturned() {
      /* Use two doubles that are exactly a-like */
      double leftOperand = 0.002;
      double rightOperand = 0.002;

      int defaultResult = leftOperand.CompareTo(rightOperand);

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0, (DoubleCompareOption)3);
      Assert.AreEqual(defaultResult, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void CompareTo_WithUnknownCompareOption_LeftLowerThanRight_DefaultResultIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = 1.0 / 3.0;

      int defaultResult = leftOperand.CompareTo(rightOperand);

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0, (DoubleCompareOption)3);
      Assert.AreEqual(defaultResult, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CompareTo(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void CompareTo_WithUnknownCompareOption_LeftHigherThanRight_DefaultResultIsReturned() {
      double leftOperand = 1.0 / 3.0;
      double rightOperand = 0.333;

      int defaultResult = leftOperand.CompareTo(rightOperand);

      /* Use the extension method with a margin of 0 */
      int result = leftOperand.CompareTo(rightOperand, 0, (DoubleCompareOption)3);
      Assert.AreEqual(defaultResult, result);
    }
    #endregion

    #region Equals (DoubleCompareOption.Margin) test cases
    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void Equals_WithZeroMargin_LeftEqualToRight_TrueIsReturned() {
      /* Use two doubles that are exactly a-like */
      double leftOperand = 0.002;
      double rightOperand = 0.002;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0, DoubleCompareOption.Margin);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void Equals_WithMargin_LeftEqualToRight_TrueIsReturned() {
      /* Use two doubles that are exactly a-like */
      double leftOperand = 0.002;
      double rightOperand = 0.002;

      /* Use the extension method with a small margin */
      bool result = leftOperand.Equals(rightOperand, 0.01, DoubleCompareOption.Margin);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void Equals_WithZeroMargin_LeftLowerThanRight_FalseIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = 1.0 / 3.0;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0, DoubleCompareOption.Margin);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void Equals_WithMargin_LeftLowerThanRightWithinMargin_TrueIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = 1.0 / 3.0;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0.03, DoubleCompareOption.Margin);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void Equals_WithMargin_LeftLowerThanRightBeyondMargin_FalseIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = 1.0 / 3.0;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void Equals_WithZeroMargin_LeftHigherThanRight_FalseIsReturned() {
      double leftOperand = 1.0 / 3.0;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0, DoubleCompareOption.Margin);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void Equals_WithMargin_LeftHigherThanRightWithinMargin_TrueIsReturned() {
      double leftOperand = 1.0 / 3.0;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0.03, DoubleCompareOption.Margin);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void Equals_WithMargin_LeftHigherThanRightBeyondMargin_FalseIsReturned() {
      double leftOperand = 1.0 / 3.0;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void Equals_WithMargin_LeftIsNaN_FalaseIsReturned() {
      double leftOperand = double.NaN;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void Equals_WithMargin_RightIsNaN_FalseIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = double.NaN;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void Equals_WithMargin_LeftAndRightAreNaN_TrueIsReturned() {
      double leftOperand = double.NaN;
      double rightOperand = double.NaN;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void Equals_WithMargin_LeftIsNegativeInfinity_FalseIsReturned() {
      double leftOperand = double.NegativeInfinity;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void Equals_WithMargin_RightIsNegativeInfinity_FalseIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = double.NegativeInfinity;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void Equals_WithMargin_LeftAndRightAreNegativeInfinity_TrueIsReturned() {
      double leftOperand = double.NegativeInfinity;
      double rightOperand = double.NegativeInfinity;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void Equals_WithMargin_LeftIsPositiveInfinity_FalseIsReturned() {
      double leftOperand = double.NegativeInfinity;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void Equals_WithMargin_RightIsPositiveInfinity_FalseIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = double.PositiveInfinity;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void Equals_WithMargin_LeftAndRightArePositiveInfinity_TrueIsReturned() {
      double leftOperand = double.PositiveInfinity;
      double rightOperand = double.PositiveInfinity;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0.0003, DoubleCompareOption.Margin);
      Assert.IsTrue(result);
    }
    #endregion

    #region Equals (DoubleCompareOption.SignificantDigits) test cases
    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void Equals_WithZeroSignigicantDigits_LeftEqualToRight_TrueIsReturned() {
      /* Use two doubles that are exactly a-like */
      double leftOperand = 0.002;
      double rightOperand = 0.002;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void Equals_WithSignificantDigits_LeftEqualToRight_TrueIsReturned() {
      /* Use two doubles that are exactly a-like */
      double leftOperand = 0.002;
      double rightOperand = 0.002;

      /* Use the extension method with a small margin */
      bool result = leftOperand.Equals(rightOperand, 1, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue( result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void Equals_WithMoreSignificantDigitsThanOperands_LeftEqualToRight_TrueIsReturned() {
      /* Use two doubles that are exactly a-like */
      double leftOperand = 0.002;
      double rightOperand = 0.002;

      /* Use the extension method with a small margin */
      bool result = leftOperand.Equals(rightOperand, 5, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void Equals_WithZeroSignificantDigits_LeftLowerThanRight_TrueIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = 1.0 / 3.0;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void Equals_WithSignificantDigits_LeftLowerThanRightWithinSignificance_TrueIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = 1.0 / 3.0;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 3, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void Equals_WithSignificantDigits_LeftLowerThanRightBeyondSignificance_FalseIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = 1.0 / 3.0;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 4, DoubleCompareOption.SignificantDigits);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void Equals_WithZeroSignificantDigits_LeftHigherThanRight_TrueIsReturned() {
      double leftOperand = 1.0 / 3.0;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void Equals_WithSignificantDigits_LeftHigherThanRightWithinSignificance_TrueIsReturned() {
      double leftOperand = 1.0 / 3.0;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 3, DoubleCompareOption.SignificantDigits);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method./// </summary>
    [TestMethod]
    public void Equals_WithSignificantDigits_LeftHigherThanRightBeyondSignificance_FalseIsReturned() {
      double leftOperand = 1.0 / 3.0;
      double rightOperand = 0.333;

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 4, DoubleCompareOption.SignificantDigits);
      Assert.IsFalse(result);
    }
    #endregion

    #region CompareTo additional test cases
    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void Equals_WithUnknownCompareOption_LeftEqualToRight_DefaultResultIsReturned() {
      /* Use two doubles that are exactly a-like */
      double leftOperand = 0.002;
      double rightOperand = 0.002;

      bool defaultResult = leftOperand.Equals(rightOperand);

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0, (DoubleCompareOption)3);
      Assert.AreEqual(defaultResult, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void Equals_WithUnknownCompareOption_LeftLowerThanRight_DefaultResultIsReturned() {
      double leftOperand = 0.333;
      double rightOperand = 1.0 / 3.0;

      bool defaultResult = leftOperand.Equals(rightOperand);

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0, (DoubleCompareOption)3);
      Assert.AreEqual(defaultResult, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Equals(double, double, double, DoubleCompareOption)"/> extension method.</summary>
    [TestMethod]
    public void Equals_WithUnknownCompareOption_LeftHigherThanRight_DefaultResultIsReturned() {
      double leftOperand = 1.0 / 3.0;
      double rightOperand = 0.333;

      bool defaultResult = leftOperand.Equals(rightOperand);

      /* Use the extension method with a margin of 0 */
      bool result = leftOperand.Equals(rightOperand, 0, (DoubleCompareOption)3);
      Assert.AreEqual(defaultResult, result);
    }
    #endregion
  }
}
