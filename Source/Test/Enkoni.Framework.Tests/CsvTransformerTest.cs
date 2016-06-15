using System;
using System.Globalization;
using System.Text;

using Enkoni.Framework.Serialization;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the CsvTransformer class.</summary>
  [TestClass]
  public class CsvTransformerTest {
    #region FromString testcases
    /// <summary>Tests the functionality of the <see cref="Transformer{T}.FromString(string)"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
    public void CsvTransformer_FromStringWithNullArgument_ArgumentNullExceptionIsThrown() {
      Transformer<TestDummyWithIntProperties> testSubject = new CsvTransformer<TestDummyWithIntProperties>();
      testSubject.FromString(null);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.FromString(string)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_FromStringOnTypeWithInt32Properties_AllFieldsAreDeserialized() {
      Transformer<TestDummyWithIntProperties> testSubject = new CsvTransformer<TestDummyWithIntProperties>();

      string inputValue = "12,014,\"42\",\"036\",13,015,\"43\",\"037\",38,\"39\"";
      TestDummyWithIntProperties result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(12, result.ColumnA);
      Assert.AreEqual(14, result.ColumnB);
      Assert.AreEqual(42, result.ColumnC);
      Assert.AreEqual(36, result.ColumnD);
      Assert.AreEqual(13, result.ColumnE);
      Assert.AreEqual(15, result.ColumnF);
      Assert.AreEqual(43, result.ColumnG);
      Assert.AreEqual(37, result.ColumnH);
      Assert.AreEqual(38, result.ColumnI);
      Assert.AreEqual(39, result.ColumnJ);

      inputValue = "12,014,\"42\",\"036\",,,\"\",\"\",A,A";
      result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(12, result.ColumnA);
      Assert.AreEqual(14, result.ColumnB);
      Assert.AreEqual(42, result.ColumnC);
      Assert.AreEqual(36, result.ColumnD);
      Assert.IsNull(result.ColumnE);
      Assert.IsNull(result.ColumnF);
      Assert.IsNull(result.ColumnG);
      Assert.IsNull(result.ColumnH);
      Assert.IsNull(result.ColumnI);
      Assert.IsNull(result.ColumnJ);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.FromString(string)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_FromStringOnTypeWithBooleanProperties_AllFieldsAreDeserialized() {
      Transformer<TestDummyWithBoolProperties> testSubject = new CsvTransformer<TestDummyWithBoolProperties>();

      string inputValue = "True,1,\"False\",\"N\",False,0,\"True\",\"J\",True,J,\"True\"";
      TestDummyWithBoolProperties result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(true, result.ColumnA);
      Assert.AreEqual(true, result.ColumnB);
      Assert.AreEqual(false, result.ColumnC);
      Assert.AreEqual(false, result.ColumnD);
      Assert.AreEqual(false, result.ColumnE);
      Assert.AreEqual(false, result.ColumnF);
      Assert.AreEqual(true, result.ColumnG);
      Assert.AreEqual(true, result.ColumnH);
      Assert.AreEqual(true, result.ColumnI);
      Assert.AreEqual(true, result.ColumnJ);
      Assert.AreEqual(true, result.ColumnK);

      inputValue = "True,1,\"False\",\"N\",,,\"\",,A,A,A";
      result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(true, result.ColumnA);
      Assert.AreEqual(true, result.ColumnB);
      Assert.AreEqual(false, result.ColumnC);
      Assert.AreEqual(false, result.ColumnD);
      Assert.IsNull(result.ColumnE);
      Assert.IsNull(result.ColumnF);
      Assert.IsNull(result.ColumnG);
      Assert.IsNull(result.ColumnH);
      Assert.IsNull(result.ColumnI);
      Assert.IsNull(result.ColumnJ);
      Assert.IsNull(result.ColumnK);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.FromString(string)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_FromStringOnTypeWithByteProperties_AllFieldsAreDeserialized() {
      Transformer<TestDummyWithByteProperties> testSubject = new CsvTransformer<TestDummyWithByteProperties>();

      string inputValue = "12,014,\"42\",\"036\",13,015,\"43\",\"037\",38,039";
      TestDummyWithByteProperties result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual((byte)12, result.ColumnA);
      Assert.AreEqual((byte)14, result.ColumnB);
      Assert.AreEqual((byte)42, result.ColumnC);
      Assert.AreEqual((byte)36, result.ColumnD);
      Assert.AreEqual((byte)13, result.ColumnE);
      Assert.AreEqual((byte)15, result.ColumnF);
      Assert.AreEqual((byte)43, result.ColumnG);
      Assert.AreEqual((byte)37, result.ColumnH);
      Assert.AreEqual((byte)38, result.ColumnI);
      Assert.AreEqual((byte)39, result.ColumnJ);

      inputValue = "12,014,\"42\",\"036\",,,\"\",,A,A";
      result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual((byte)12, result.ColumnA);
      Assert.AreEqual((byte)14, result.ColumnB);
      Assert.AreEqual((byte)42, result.ColumnC);
      Assert.AreEqual((byte)36, result.ColumnD);
      Assert.IsNull(result.ColumnE);
      Assert.IsNull(result.ColumnF);
      Assert.IsNull(result.ColumnG);
      Assert.IsNull(result.ColumnH);
      Assert.IsNull(result.ColumnI);
      Assert.IsNull(result.ColumnJ);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.FromString(string)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_FromStringOnTypeWithFloatProperties_AllFieldsAreDeserialized() {
      Transformer<TestDummyWithFloatProperties> testSubject = new CsvTransformer<TestDummyWithFloatProperties>();

      string inputValue = "12.1;01,41;\"42.3\";\"03,26\";13.2;01.50;\"43.5\";\"03.77\";2.34;\"3.45\"";
      TestDummyWithFloatProperties result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(12.1F, result.ColumnA);
      Assert.AreEqual(1.41F, result.ColumnB);
      Assert.AreEqual(42.3F, result.ColumnC);
      Assert.AreEqual(3.26F, result.ColumnD);
      Assert.AreEqual(13.2F, result.ColumnE);
      Assert.AreEqual(1.50F, result.ColumnF);
      Assert.AreEqual(43.5F, result.ColumnG);
      Assert.AreEqual(3.77F, result.ColumnH);
      Assert.AreEqual(2.34F, result.ColumnI);
      Assert.AreEqual(3.45F, result.ColumnJ);

      inputValue = "12.1;01,41;\"42.3\";\"03,26\";;;\"\";\"\";A;A";
      result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(12.1F, result.ColumnA);
      Assert.AreEqual(1.41F, result.ColumnB);
      Assert.AreEqual(42.3F, result.ColumnC);
      Assert.AreEqual(3.26F, result.ColumnD);
      Assert.IsNull(result.ColumnE);
      Assert.IsNull(result.ColumnF);
      Assert.IsNull(result.ColumnG);
      Assert.IsNull(result.ColumnH);
      Assert.IsNull(result.ColumnI);
      Assert.IsNull(result.ColumnJ);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.FromString(string)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_FromStringOnTypeWithDoubleProperties_AllFieldsAreDeserialized() {
      Transformer<TestDummyWithDoubleProperties> testSubject = new CsvTransformer<TestDummyWithDoubleProperties>();

      string inputValue = "12.1;01,41;\"42.3\";\"03,26\";13.2;01.50;\"43.5\";\"03.77\";2.34;\"3.45\"";
      TestDummyWithDoubleProperties result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(12.1D, result.ColumnA);
      Assert.AreEqual(1.41D, result.ColumnB);
      Assert.AreEqual(42.3D, result.ColumnC);
      Assert.AreEqual(3.26D, result.ColumnD);
      Assert.AreEqual(13.2D, result.ColumnE);
      Assert.AreEqual(1.50D, result.ColumnF);
      Assert.AreEqual(43.5D, result.ColumnG);
      Assert.AreEqual(3.77D, result.ColumnH);
      Assert.AreEqual(2.34D, result.ColumnI);
      Assert.AreEqual(3.45D, result.ColumnJ);

      inputValue = "12.1;01,41;\"42.3\";\"03,26\";;;\"\";\"\";A;A";
      result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(12.1D, result.ColumnA);
      Assert.AreEqual(1.41D, result.ColumnB);
      Assert.AreEqual(42.3D, result.ColumnC);
      Assert.AreEqual(3.26D, result.ColumnD);
      Assert.IsNull(result.ColumnE);
      Assert.IsNull(result.ColumnF);
      Assert.IsNull(result.ColumnG);
      Assert.IsNull(result.ColumnH);
      Assert.IsNull(result.ColumnI);
      Assert.IsNull(result.ColumnJ);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.FromString(string)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_FromStringOnTypeWithDecimalProperties_AllFieldsAreDeserialized() {
      Transformer<TestDummyWithDecimalProperties> testSubject = new CsvTransformer<TestDummyWithDecimalProperties>();

      string inputValue = "12.1;01,41;\"42.3\";\"03,26\";13.2;01.50;\"43.5\";\"03.77\";2.34;3.45";
      TestDummyWithDecimalProperties result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(12.1M, result.ColumnA);
      Assert.AreEqual(1.41M, result.ColumnB);
      Assert.AreEqual(42.3M, result.ColumnC);
      Assert.AreEqual(3.26M, result.ColumnD);
      Assert.AreEqual(13.2M, result.ColumnE);
      Assert.AreEqual(1.50M, result.ColumnF);
      Assert.AreEqual(43.5M, result.ColumnG);
      Assert.AreEqual(3.77M, result.ColumnH);
      Assert.AreEqual(2.34M, result.ColumnI);
      Assert.AreEqual(3.45M, result.ColumnJ);

      inputValue = "12.1;01,41;\"42.3\";\"03,26\";;;\"\";\"\";A;A";
      result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(12.1M, result.ColumnA);
      Assert.AreEqual(1.41M, result.ColumnB);
      Assert.AreEqual(42.3M, result.ColumnC);
      Assert.AreEqual(3.26M, result.ColumnD);
      Assert.IsNull(result.ColumnE);
      Assert.IsNull(result.ColumnF);
      Assert.IsNull(result.ColumnG);
      Assert.IsNull(result.ColumnH);
      Assert.IsNull(result.ColumnI);
      Assert.IsNull(result.ColumnJ);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.FromString(string)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_FromStringOnTypeWithInt16Properties_AllFieldsAreDeserialized() {
      Transformer<TestDummyWithShortProperties> testSubject = new CsvTransformer<TestDummyWithShortProperties>();

      string inputValue = "12,014,\"42\",\"036\",13,015,\"43\",\"037\",38,39";
      TestDummyWithShortProperties result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual((short)12, result.ColumnA);
      Assert.AreEqual((short)14, result.ColumnB);
      Assert.AreEqual((short)42, result.ColumnC);
      Assert.AreEqual((short)36, result.ColumnD);
      Assert.AreEqual((short)13, result.ColumnE);
      Assert.AreEqual((short)15, result.ColumnF);
      Assert.AreEqual((short)43, result.ColumnG);
      Assert.AreEqual((short)37, result.ColumnH);
      Assert.AreEqual((short)38, result.ColumnI);
      Assert.AreEqual((short)39, result.ColumnJ);

      inputValue = "12,014,\"42\",\"036\",,,\"\",\"\",A,A";
      result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual((short)12, result.ColumnA);
      Assert.AreEqual((short)14, result.ColumnB);
      Assert.AreEqual((short)42, result.ColumnC);
      Assert.AreEqual((short)36, result.ColumnD);
      Assert.IsNull(result.ColumnE);
      Assert.IsNull(result.ColumnF);
      Assert.IsNull(result.ColumnG);
      Assert.IsNull(result.ColumnH);
      Assert.IsNull(result.ColumnI);
      Assert.IsNull(result.ColumnJ);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.FromString(string)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_FromStringOnTypeWithInt64Properties_AllFieldsAreDeserialized() {
      Transformer<TestDummyWithLongProperties> testSubject = new CsvTransformer<TestDummyWithLongProperties>();

      string inputValue = "12,014,\"42\",\"036\",13,015,\"43\",\"037\",38,039";
      TestDummyWithLongProperties result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(12L, result.ColumnA);
      Assert.AreEqual(14L, result.ColumnB);
      Assert.AreEqual(42L, result.ColumnC);
      Assert.AreEqual(36L, result.ColumnD);
      Assert.AreEqual(13L, result.ColumnE);
      Assert.AreEqual(15L, result.ColumnF);
      Assert.AreEqual(43L, result.ColumnG);
      Assert.AreEqual(37L, result.ColumnH);
      Assert.AreEqual(38L, result.ColumnI);
      Assert.AreEqual(39L, result.ColumnJ);

      inputValue = "12,014,\"42\",\"036\",,,\"\",\"\",A,A";
      result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(12L, result.ColumnA);
      Assert.AreEqual(14L, result.ColumnB);
      Assert.AreEqual(42L, result.ColumnC);
      Assert.AreEqual(36L, result.ColumnD);
      Assert.IsNull(result.ColumnE);
      Assert.IsNull(result.ColumnF);
      Assert.IsNull(result.ColumnG);
      Assert.IsNull(result.ColumnH);
      Assert.IsNull(result.ColumnI);
      Assert.IsNull(result.ColumnJ);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.FromString(string)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_FromStringOnTypeWithCharProperties_AllFieldsAreDeserialized() {
      Transformer<TestDummyWithCharProperties> testSubject = new CsvTransformer<TestDummyWithCharProperties>();

      string inputValue = "d,097,\"e\",\"105\",e,098,\"f\",\"106\",A,B";
      TestDummyWithCharProperties result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual('d', result.ColumnA);
      Assert.AreEqual('a', result.ColumnB);
      Assert.AreEqual('e', result.ColumnC);
      Assert.AreEqual('i', result.ColumnD);
      Assert.AreEqual('e', result.ColumnE);
      Assert.AreEqual('b', result.ColumnF);
      Assert.AreEqual('f', result.ColumnG);
      Assert.AreEqual('j', result.ColumnH);
      Assert.AreEqual('A', result.ColumnI);
      Assert.AreEqual('B', result.ColumnJ);

      inputValue = "d,097,\"e\",\"105\",,,\"\",\"\",<>,<>";
      result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual('d', result.ColumnA);
      Assert.AreEqual('a', result.ColumnB);
      Assert.AreEqual('e', result.ColumnC);
      Assert.AreEqual('i', result.ColumnD);
      Assert.IsNull(result.ColumnE);
      Assert.IsNull(result.ColumnF);
      Assert.IsNull(result.ColumnG);
      Assert.IsNull(result.ColumnH);
      Assert.IsNull(result.ColumnI);
      Assert.IsNull(result.ColumnJ);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.FromString(string)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_FromStringOnTypeWithStringProperties_AllFieldsAreDeserialized() {
      Transformer<TestDummyWithStringProperties> testSubject = new CsvTransformer<TestDummyWithStringProperties>();

      string inputValue = "abc,def  ,\"ghi\",\"jk \",lm,\"no\"";
      TestDummyWithStringProperties result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("abc", result.ColumnA);
      Assert.AreEqual("def", result.ColumnB);
      Assert.AreEqual("ghi", result.ColumnC);
      Assert.AreEqual("jk", result.ColumnD);
      Assert.AreEqual("lm", result.ColumnE);
      Assert.AreEqual("no", result.ColumnF);

      inputValue = ",,\"\",\"\",<null>,<null>";
      result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(string.Empty, result.ColumnA);
      Assert.AreEqual(string.Empty, result.ColumnB);
      Assert.AreEqual(string.Empty, result.ColumnC);
      Assert.AreEqual(string.Empty, result.ColumnD);
      Assert.IsNull(result.ColumnE);
      Assert.IsNull(result.ColumnF);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.FromString(string)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_FromStringOnTypeWithDateTimeProperties_AllFieldsAreDeserialized() {
      Transformer<TestDummyWithDateTimeProperties> testSubject = new CsvTransformer<TestDummyWithDateTimeProperties>();

      string inputValue = "12-07-2013,20130713,\"14-07-2013\",\"20130715\",16-07-2013,20130717,\"18-07-2013\",\"20130719\",13-08-2014,20140914";
      TestDummyWithDateTimeProperties result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(new DateTime(2013, 7, 12), result.ColumnA);
      Assert.AreEqual(new DateTime(2013, 7, 13), result.ColumnB);
      Assert.AreEqual(new DateTime(2013, 7, 14), result.ColumnC);
      Assert.AreEqual(new DateTime(2013, 7, 15), result.ColumnD);
      Assert.AreEqual(new DateTime(2013, 7, 16), result.ColumnE);
      Assert.AreEqual(new DateTime(2013, 7, 17), result.ColumnF);
      Assert.AreEqual(new DateTime(2013, 7, 18), result.ColumnG);
      Assert.AreEqual(new DateTime(2013, 7, 19), result.ColumnH);
      Assert.AreEqual(new DateTime(2014, 8, 13), result.ColumnI);
      Assert.AreEqual(new DateTime(2014, 9, 14), result.ColumnJ);

      inputValue = "12-07-2013,20130713,\"14-07-2013\",\"20130715\",,,\"\",\"\",0,0";
      result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(new DateTime(2013, 7, 12), result.ColumnA);
      Assert.AreEqual(new DateTime(2013, 7, 13), result.ColumnB);
      Assert.AreEqual(new DateTime(2013, 7, 14), result.ColumnC);
      Assert.AreEqual(new DateTime(2013, 7, 15), result.ColumnD);
      Assert.IsNull(result.ColumnE);
      Assert.IsNull(result.ColumnF);
      Assert.IsNull(result.ColumnG);
      Assert.IsNull(result.ColumnH);
      Assert.IsNull(result.ColumnI);
      Assert.IsNull(result.ColumnJ);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.FromString(string)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_FromStringOnTypeWithEnumProperties_AllFieldsAreDeserialized() {
      Transformer<TestDummyWithEnumProperties> testSubject = new CsvTransformer<TestDummyWithEnumProperties>();

      string inputValue = "ValueB,003,\"ValueE\",\"004\",ValueC,004,\"ValueD\",\"001\",ValueB,1";
      TestDummyWithEnumProperties result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(TestEnum.ValueB, result.ColumnA);
      Assert.AreEqual(TestEnum.ValueD, result.ColumnB);
      Assert.AreEqual(TestEnum.ValueE, result.ColumnC);
      Assert.AreEqual(TestEnum.ValueE, result.ColumnD);
      Assert.AreEqual(TestEnum.ValueC, result.ColumnE);
      Assert.AreEqual(TestEnum.ValueE, result.ColumnF);
      Assert.AreEqual(TestEnum.ValueD, result.ColumnG);
      Assert.AreEqual(TestEnum.ValueB, result.ColumnH);
      Assert.AreEqual(TestEnum.ValueB, result.ColumnI);
      Assert.AreEqual(TestEnum.ValueB, result.ColumnJ);

      inputValue = "ValueB,003,\"ValueE\",\"004\",,,\"\",\"\",X,X";
      result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(TestEnum.ValueB, result.ColumnA);
      Assert.AreEqual(TestEnum.ValueD, result.ColumnB);
      Assert.AreEqual(TestEnum.ValueE, result.ColumnC);
      Assert.AreEqual(TestEnum.ValueE, result.ColumnD);
      Assert.IsNull(result.ColumnE);
      Assert.IsNull(result.ColumnF);
      Assert.IsNull(result.ColumnG);
      Assert.IsNull(result.ColumnH);
      Assert.IsNull(result.ColumnI);
      Assert.IsNull(result.ColumnJ);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.FromString(string)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_FromStringOnTypeWithSimpleStringSeparator_AllFieldsAreDeserialized() {
      Transformer<TestDummyWithStringSeparator> testSubject = new CsvTransformer<TestDummyWithStringSeparator>();

      string inputValue = "42[SEP]SomeValue[SEP]True";
      TestDummyWithStringSeparator result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(42, result.ColumnA);
      Assert.AreEqual("SomeValue", result.ColumnB);
      Assert.AreEqual(true, result.ColumnC);
      
      inputValue = "42[SEP][SEP]True";
      result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(42, result.ColumnA);
      Assert.AreEqual(string.Empty, result.ColumnB);
      Assert.AreEqual(true, result.ColumnC);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.FromString(string)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_FromStringOnTypeWithComplexStringSeparator_AllFieldsAreDeserialized() {
      Transformer<TestDummyWithComplexStringSeparator> testSubject = new CsvTransformer<TestDummyWithComplexStringSeparator>();

      string inputValue = "\"SomeValue\",\"Other,Value\",\"True\",\"42\"";
      TestDummyWithComplexStringSeparator result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("SomeValue", result.ColumnA);
      Assert.AreEqual("Other,Value", result.ColumnB);
      Assert.AreEqual(true, result.ColumnC);
      Assert.AreEqual(42, result.ColumnD);

      inputValue = "\"Some,Value\",\"\",\"True\",\"42\"";
      result = testSubject.FromString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("Some,Value", result.ColumnA);
      Assert.AreEqual(string.Empty, result.ColumnB);
      Assert.AreEqual(true, result.ColumnC);
      Assert.AreEqual(42, result.ColumnD);
    }
    #endregion

    #region ToString testcases
    /// <summary>Tests the functionality of the <see cref="Transformer{T}.ToString(T)"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
    public void CsvTransformer_ToStringWithNullArgument_ArgumentNullExceptionIsThrown() {
      Transformer<TestDummyWithIntProperties> testSubject = new CsvTransformer<TestDummyWithIntProperties>();
      testSubject.ToString(null);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.ToString(T)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_ToStringOnTypeWithInt32Properties_AllFieldsAreSerialized() {
      Transformer<TestDummyWithIntProperties> testSubject = new CsvTransformer<TestDummyWithIntProperties>();

      TestDummyWithIntProperties inputValue = new TestDummyWithIntProperties {
        ColumnA = 12,
        ColumnB = 14,
        ColumnC = 42,
        ColumnD = 36,
        ColumnE = 13,
        ColumnF = 15,
        ColumnG = 43,
        ColumnH = 37,
        ColumnI = 38,
        ColumnJ = 39
      };

      string result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("12,014,\"42\",\"036\",13,015,\"43\",\"037\",38,\"39\"", result);

      inputValue.ColumnE = null;
      inputValue.ColumnF = null;
      inputValue.ColumnG = null;
      inputValue.ColumnH = null;
      inputValue.ColumnI = null;
      inputValue.ColumnJ = null;
      result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("12,014,\"42\",\"036\",,,\"\",\"\",A,A", result);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.ToString(T)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_ToStringOnTypeWithBooleanProperties_AllFieldsAreSerialized() {
      Transformer<TestDummyWithBoolProperties> testSubject = new CsvTransformer<TestDummyWithBoolProperties>();

      TestDummyWithBoolProperties inputValue = new TestDummyWithBoolProperties {
        ColumnA = true,
        ColumnB = true,
        ColumnC = false,
        ColumnD = false,
        ColumnE = false,
        ColumnF = false,
        ColumnG = true,
        ColumnH = true,
        ColumnI = false,
        ColumnJ = true,
        ColumnK = true
      };

      string result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("True,1,\"False\",\"N\",False,0,\"True\",\"J\",False,J,\"True\"", result);

      inputValue.ColumnE = null;
      inputValue.ColumnF = null;
      inputValue.ColumnG = null;
      inputValue.ColumnH = null;
      inputValue.ColumnI = null;
      inputValue.ColumnJ = null;
      inputValue.ColumnK = null;
      result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("True,1,\"False\",\"N\",,,\"\",,A,A,A", result);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.ToString(T)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_ToStringOnTypeWithByteProperties_AllFieldsAreSerialized() {
      Transformer<TestDummyWithByteProperties> testSubject = new CsvTransformer<TestDummyWithByteProperties>();

      TestDummyWithByteProperties inputValue = new TestDummyWithByteProperties {
        ColumnA = 12,
        ColumnB = 14,
        ColumnC = 42,
        ColumnD = 36,
        ColumnE = 13,
        ColumnF = 15,
        ColumnG = 43,
        ColumnH = 37,
        ColumnI = 38,
        ColumnJ = 39
      };

      string result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("12,014,\"42\",\"036\",13,015,\"43\",\"037\",38,039", result);

      inputValue.ColumnE = null;
      inputValue.ColumnF = null;
      inputValue.ColumnG = null;
      inputValue.ColumnH = null;
      inputValue.ColumnI = null;
      inputValue.ColumnJ = null;
      result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("12,014,\"42\",\"036\",,,\"\",\"\",A,A", result);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.ToString(T)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_ToStringOnTypeWithFloatProperties_AllFieldsAreSerialized() {
      Transformer<TestDummyWithFloatProperties> testSubject = new CsvTransformer<TestDummyWithFloatProperties>();

      TestDummyWithFloatProperties inputValue = new TestDummyWithFloatProperties {
        ColumnA = 12.1F,
        ColumnB = 1.41F,
        ColumnC = 42.3F,
        ColumnD = 3.26F,
        ColumnE = 13.2F,
        ColumnF = 1.50F,
        ColumnG = 43.5F,
        ColumnH = 3.77F,
        ColumnI = 2.34F,
        ColumnJ = 3.45F
      };

      string result = testSubject.ToString(inputValue);
      string expected = string.Format(CultureInfo.CurrentCulture, "{0};01,41;\"{1}\";\"03,26\";{2};{3:00.00};\"{4}\";\"{5:00.00}\";{6};\"{7:0.00}\"",
        inputValue.ColumnA, inputValue.ColumnC, inputValue.ColumnE, inputValue.ColumnF, inputValue.ColumnG, inputValue.ColumnH, inputValue.ColumnI, inputValue.ColumnJ);
      Assert.IsNotNull(result);
      Assert.AreEqual(expected, result);

      inputValue.ColumnE = null;
      inputValue.ColumnF = null;
      inputValue.ColumnG = null;
      inputValue.ColumnH = null;
      inputValue.ColumnI = null;
      inputValue.ColumnJ = null;
      result = testSubject.ToString(inputValue);
      expected = string.Format(CultureInfo.CurrentCulture, "{0};01,41;\"{1}\";\"03,26\";;;\"\";\"\";A;A",
        inputValue.ColumnA, inputValue.ColumnC);
      Assert.IsNotNull(result);
      Assert.AreEqual(expected, result);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.ToString(T)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_ToStringOnTypeWithDoubleProperties_AllFieldsAreSerialized() {
      Transformer<TestDummyWithDoubleProperties> testSubject = new CsvTransformer<TestDummyWithDoubleProperties>();

      TestDummyWithDoubleProperties inputValue = new TestDummyWithDoubleProperties {
        ColumnA = 12.1D,
        ColumnB = 1.41D,
        ColumnC = 42.3D,
        ColumnD = 3.26D,
        ColumnE = 13.2D,
        ColumnF = 1.50D,
        ColumnG = 43.5D,
        ColumnH = 3.77D,
        ColumnI = 2.34D,
        ColumnJ = 3.45D
      };

      string result = testSubject.ToString(inputValue);
      string expected = string.Format(CultureInfo.CurrentCulture, "{0};01,41;\"{1}\";\"03,26\";{2};{3:00.00};\"{4}\";\"{5:00.00}\";{6};\"{7:0.00}\"",
        inputValue.ColumnA, inputValue.ColumnC, inputValue.ColumnE, inputValue.ColumnF, inputValue.ColumnG, inputValue.ColumnH, inputValue.ColumnI, inputValue.ColumnJ);
      Assert.IsNotNull(result);
      Assert.AreEqual(expected, result);

      inputValue.ColumnE = null;
      inputValue.ColumnF = null;
      inputValue.ColumnG = null;
      inputValue.ColumnH = null;
      inputValue.ColumnI = null;
      inputValue.ColumnJ = null;
      result = testSubject.ToString(inputValue);
      expected = string.Format(CultureInfo.CurrentCulture, "{0};01,41;\"{1}\";\"03,26\";;;\"\";\"\";A;A",
        inputValue.ColumnA, inputValue.ColumnC);
      Assert.IsNotNull(result);
      Assert.AreEqual(expected, result);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.ToString(T)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_ToStringOnTypeWithDecimalProperties_AllFieldsAreSerialized() {
      Transformer<TestDummyWithDecimalProperties> testSubject = new CsvTransformer<TestDummyWithDecimalProperties>();

      TestDummyWithDecimalProperties inputValue = new TestDummyWithDecimalProperties {
        ColumnA = 12.1M,
        ColumnB = 1.41M,
        ColumnC = 42.3M,
        ColumnD = 3.26M,
        ColumnE = 13.2M,
        ColumnF = 1.50M,
        ColumnG = 43.5M,
        ColumnH = 3.77M,
        ColumnI = 2.34M,
        ColumnJ = 3.45M
      };

      string result = testSubject.ToString(inputValue);
      string expected = string.Format(CultureInfo.CurrentCulture, "{0};01,41;\"{1}\";\"03,26\";{2};{3:00.00};\"{4}\";\"{5:00.00}\";{6};{7:0.00}",
        inputValue.ColumnA, inputValue.ColumnC, inputValue.ColumnE, inputValue.ColumnF, inputValue.ColumnG, inputValue.ColumnH, inputValue.ColumnI, inputValue.ColumnJ);
      Assert.IsNotNull(result);
      Assert.AreEqual(expected, result);

      inputValue.ColumnE = null;
      inputValue.ColumnF = null;
      inputValue.ColumnG = null;
      inputValue.ColumnH = null;
      inputValue.ColumnI = null;
      inputValue.ColumnJ = null;
      result = testSubject.ToString(inputValue);
      expected = string.Format(CultureInfo.CurrentCulture, "{0};01,41;\"{1}\";\"03,26\";;;\"\";\"\";A;A",
        inputValue.ColumnA, inputValue.ColumnC);
      Assert.IsNotNull(result);
      Assert.AreEqual(expected, result);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.ToString(T)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_ToStringOnTypeWithInt16Properties_AllFieldsAreSerialized() {
      Transformer<TestDummyWithShortProperties> testSubject = new CsvTransformer<TestDummyWithShortProperties>();

      TestDummyWithShortProperties inputValue = new TestDummyWithShortProperties {
        ColumnA = 12,
        ColumnB = 14,
        ColumnC = 42,
        ColumnD = 36,
        ColumnE = 13,
        ColumnF = 15,
        ColumnG = 43,
        ColumnH = 37,
        ColumnI = 38,
        ColumnJ = 39
      };

      string result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("12,014,\"42\",\"036\",13,015,\"43\",\"037\",38,039", result);

      inputValue.ColumnE = null;
      inputValue.ColumnF = null;
      inputValue.ColumnG = null;
      inputValue.ColumnH = null;
      inputValue.ColumnI = null;
      inputValue.ColumnJ = null;
      result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("12,014,\"42\",\"036\",,,\"\",\"\",A,A", result);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.ToString(T)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_ToStringOnTypeWithInt64Properties_AllFieldsAreSerialized() {
      Transformer<TestDummyWithLongProperties> testSubject = new CsvTransformer<TestDummyWithLongProperties>();

      TestDummyWithLongProperties inputValue = new TestDummyWithLongProperties {
        ColumnA = 12,
        ColumnB = 14,
        ColumnC = 42,
        ColumnD = 36,
        ColumnE = 13,
        ColumnF = 15,
        ColumnG = 43,
        ColumnH = 37,
        ColumnI = 38,
        ColumnJ = 39
      };

      string result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("12,014,\"42\",\"036\",13,015,\"43\",\"037\",38,039", result);

      inputValue.ColumnE = null;
      inputValue.ColumnF = null;
      inputValue.ColumnG = null;
      inputValue.ColumnH = null;
      inputValue.ColumnI = null;
      inputValue.ColumnJ = null;
      result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("12,014,\"42\",\"036\",,,\"\",\"\",A,A", result);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.ToString(T)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_ToStringOnTypeWithCharProperties_AllFieldsAreSerialized() {
      Transformer<TestDummyWithCharProperties> testSubject = new CsvTransformer<TestDummyWithCharProperties>();

      TestDummyWithCharProperties inputValue = new TestDummyWithCharProperties {
        ColumnA = 'd',
        ColumnB = 'a',
        ColumnC = 'e',
        ColumnD = 'i',
        ColumnE = 'e',
        ColumnF = 'b',
        ColumnG = 'f',
        ColumnH = 'j',
        ColumnI = 'k',
        ColumnJ = 'l'
      };

      string result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("d,097,\"e\",\"105\",e,098,\"f\",\"106\",k,108", result);

      inputValue.ColumnE = null;
      inputValue.ColumnF = null;
      inputValue.ColumnG = null;
      inputValue.ColumnH = null;
      inputValue.ColumnI = null;
      inputValue.ColumnJ = null;
      result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("d,097,\"e\",\"105\",,,\"\",\"\",<>,<>", result);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.ToString(T)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_ToStringOnTypeWithStringProperties_AllFieldsAreSerialized() {
      Transformer<TestDummyWithStringProperties> testSubject = new CsvTransformer<TestDummyWithStringProperties>();

      TestDummyWithStringProperties inputValue = new TestDummyWithStringProperties {
        ColumnA = "abc",
        ColumnB = "def",
        ColumnC = "ghi",
        ColumnD = "jk",
        ColumnE = "lm",
        ColumnF = "no"
      };

      string result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("abc,def  ,\"ghi\",\"jk   \",lm,\"no\"", result);

      inputValue.ColumnA = null;
      inputValue.ColumnB = null;
      inputValue.ColumnC = null;
      inputValue.ColumnD = null;
      inputValue.ColumnE = null;
      inputValue.ColumnF = null;

      result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual(",     ,\"\",\"     \",<null>,<null>", result);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.ToString(T)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_ToStringOnTypeWithDateTimeProperties_AllFieldsAreSerialized() {
      Transformer<TestDummyWithDateTimeProperties> testSubject = new CsvTransformer<TestDummyWithDateTimeProperties>();

      TestDummyWithDateTimeProperties inputValue = new TestDummyWithDateTimeProperties {
        ColumnA = new DateTime(2013, 7, 12),
        ColumnB = new DateTime(2013, 7, 13),
        ColumnC = new DateTime(2013, 7, 14),
        ColumnD = new DateTime(2013, 7, 15),
        ColumnE = new DateTime(2013, 7, 16),
        ColumnF = new DateTime(2013, 7, 17),
        ColumnG = new DateTime(2013, 7, 18),
        ColumnH = new DateTime(2013, 7, 19),
        ColumnI = new DateTime(2013, 8, 20),
        ColumnJ = new DateTime(2013, 9, 21)
      };

      string result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      string expected = new StringBuilder()
        .AppendFormat(new CultureInfo("nl-NL"), "{0},", new DateTime(2013, 7, 12))
        .AppendFormat("{0:yyyyMMdd},", new DateTime(2013, 7, 13))
        .AppendFormat(new CultureInfo("nl-NL"), "\"{0}\",", new DateTime(2013, 7, 14))
        .AppendFormat("\"{0:yyyyMMdd}\",", new DateTime(2013, 7, 15))
        .AppendFormat(new CultureInfo("nl-NL"), "{0},", new DateTime(2013, 7, 16))
        .AppendFormat("{0:yyyyMMdd},", new DateTime(2013, 7, 17))
        .AppendFormat(new CultureInfo("nl-NL"), "\"{0}\",", new DateTime(2013, 7, 18))
        .AppendFormat("\"{0:yyyyMMdd}\",", new DateTime(2013, 7, 19))
        .AppendFormat(new CultureInfo("nl-NL"), "{0},", new DateTime(2013, 8, 20))
        .AppendFormat("{0:yyyyMMdd}", new DateTime(2013, 9, 21))
        .ToString();
      Assert.AreEqual(expected, result);

      inputValue.ColumnE = null;
      inputValue.ColumnF = null;
      inputValue.ColumnG = null;
      inputValue.ColumnH = null;
      inputValue.ColumnI = null;
      inputValue.ColumnJ = null;
      result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      expected = new StringBuilder()
        .AppendFormat(new CultureInfo("nl-NL"), "{0},", new DateTime(2013, 7, 12))
        .AppendFormat("{0:yyyyMMdd},", new DateTime(2013, 7, 13))
        .AppendFormat(new CultureInfo("nl-NL"), "\"{0}\",", new DateTime(2013, 7, 14))
        .AppendFormat("\"{0:yyyyMMdd}\",", new DateTime(2013, 7, 15))
        .Append(",,\"\",\"\",0,0")
        .ToString();
      Assert.AreEqual(expected, result);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.ToString(T)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_ToStringOnTypeWithEnumProperties_AllFieldsAreSerialized() {
      Transformer<TestDummyWithEnumProperties> testSubject = new CsvTransformer<TestDummyWithEnumProperties>();

      TestDummyWithEnumProperties inputValue = new TestDummyWithEnumProperties {
        ColumnA = TestEnum.ValueB,
        ColumnB = TestEnum.ValueD,
        ColumnC = TestEnum.ValueE,
        ColumnD = TestEnum.ValueE,
        ColumnE = TestEnum.ValueC,
        ColumnF = TestEnum.ValueE,
        ColumnG = TestEnum.ValueD,
        ColumnH = TestEnum.ValueB,
        ColumnI = TestEnum.ValueB,
        ColumnJ = TestEnum.ValueD
      };

      string result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("ValueB,003,\"ValueE\",\"004\",ValueC,004,\"ValueD\",\"001\",ValueB,3", result);

      inputValue.ColumnE = null;
      inputValue.ColumnF = null;
      inputValue.ColumnG = null;
      inputValue.ColumnH = null;
      inputValue.ColumnI = null;
      inputValue.ColumnJ = null;

      result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("ValueB,003,\"ValueE\",\"004\",,,\"\",\"\",X,X", result);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.ToString(T)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_ToStringOnTypeWithSimpleStringSeparator_AllFieldsAreSerialized() {
      Transformer<TestDummyWithStringSeparator> testSubject = new CsvTransformer<TestDummyWithStringSeparator>();

      TestDummyWithStringSeparator inputValue = new TestDummyWithStringSeparator {
        ColumnA = 42,
        ColumnB = "SomeValue",
        ColumnC = true
      };
      
      string result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("42[SEP]SomeValue[SEP]True", result);

      inputValue = new TestDummyWithStringSeparator {
        ColumnA = 42,
        ColumnC = true
      };

      result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("42[SEP][SEP]True", result);
    }

    /// <summary>Tests the functionality of the <see cref="Transformer{T}.ToString(T)"/> method.</summary>
    [TestMethod]
    public void CsvTransformer_ToStringOnTypeWithComplexStringSeparator_AllFieldsAreSerialized() {
      Transformer<TestDummyWithComplexStringSeparator> testSubject = new CsvTransformer<TestDummyWithComplexStringSeparator>();

      TestDummyWithComplexStringSeparator inputValue = new TestDummyWithComplexStringSeparator {
        ColumnA = "SomeValue",
        ColumnB = "Other,Value",
        ColumnC = true,
        ColumnD = 42
      };

      string result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("\"SomeValue\",\"Other,Value\",\"True\",\"42\"", result);

      inputValue = new TestDummyWithComplexStringSeparator {
        ColumnA = "Some,Value",
        ColumnC = true,
        ColumnD = 42
      };

      result = testSubject.ToString(inputValue);
      Assert.IsNotNull(result);
      Assert.AreEqual("\"Some,Value\",\"\",\"True\",\"42\"", result);
    }
    #endregion

    #region Private helper classes
    /// <summary>A basic dummy class with int properties to support the testcases.</summary>
    [CsvRecord]
    private class TestDummyWithIntProperties {
      /// <summary>Gets or sets the value of the first column.</summary>
      [CsvColumn(0)]
      public int ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      [CsvColumn(1, FormatString = "000")]
      public int ColumnB { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      [CsvColumn(2, FormatString = "\"{0}\"")]
      public int ColumnC { get; set; }

      /// <summary>Gets or sets the value of the fourth column.</summary>
      [CsvColumn(3, FormatString = "\"{0:000}\"")]
      public int ColumnD { get; set; }

      /// <summary>Gets or sets the value of the fifth column.</summary>
      [CsvColumn(4)]
      public int? ColumnE { get; set; }

      /// <summary>Gets or sets the value of the sixth column.</summary>
      [CsvColumn(5, FormatString = "000")]
      public int? ColumnF { get; set; }

      /// <summary>Gets or sets the value of the seventh column.</summary>
      [CsvColumn(6, FormatString = "\"{0}\"")]
      public int? ColumnG { get; set; }

      /// <summary>Gets or sets the value of the eigth column.</summary>
      [CsvColumn(7, FormatString = "\"{0:000}\"")]
      public int? ColumnH { get; set; }

      /// <summary>Gets or sets the value of the ninth column.</summary>
      [CsvColumn(8, NullString = "A")]
      public int? ColumnI { get; set; }

      /// <summary>Gets or sets the value of the tenth column.</summary>
      [CsvColumn(9, NullString = "A", FormatString = "\"{0}\"")]
      public int? ColumnJ { get; set; }
    }

    /// <summary>A basic dummy class with bool properties to support the testcases.</summary>
    [CsvRecord]
    private class TestDummyWithBoolProperties {
      /// <summary>Gets or sets the value of the first column.</summary>
      [CsvColumn(0)]
      public bool ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      [CsvColumn(1, FormatString = "true:1|false:0")]
      public bool ColumnB { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      [CsvColumn(2, FormatString = "\"{0}\"")]
      public bool ColumnC { get; set; }

      /// <summary>Gets or sets the value of the fourth column.</summary>
      [CsvColumn(3, FormatString = "tRue:\"J\"|faLse:\"N\"")]
      public bool ColumnD { get; set; }

      /// <summary>Gets or sets the value of the fifth column.</summary>
      [CsvColumn(4)]
      public bool? ColumnE { get; set; }

      /// <summary>Gets or sets the value of the sixth column.</summary>
      [CsvColumn(5, FormatString = "true:1|false:0")]
      public bool? ColumnF { get; set; }

      /// <summary>Gets or sets the value of the seventh column.</summary>
      [CsvColumn(6, FormatString = "\"{0}\"")]
      public bool? ColumnG { get; set; }

      /// <summary>Gets or sets the value of the eigth column.</summary>
      [CsvColumn(7, FormatString = "tRue:\"J\"|faLse:\"N\"")]
      public bool? ColumnH { get; set; }

      /// <summary>Gets or sets the value of the ninth column.</summary>
      [CsvColumn(8, NullString = "A")]
      public bool? ColumnI { get; set; }

      /// <summary>Gets or sets the value of the tenth column.</summary>
      [CsvColumn(9, NullString = "A", FormatString = "true:J|false:N")]
      public bool? ColumnJ { get; set; }

      /// <summary>Gets or sets the value of the eleventh column.</summary>
      [CsvColumn(10, NullString = "A", FormatString = "\"{0}\"")]
      public bool? ColumnK { get; set; }
    }

    /// <summary>A basic dummy class with byte properties to support the testcases.</summary>
    [CsvRecord]
    private class TestDummyWithByteProperties {
      /// <summary>Gets or sets the value of the first column.</summary>
      [CsvColumn(0)]
      public byte ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      [CsvColumn(1, FormatString = "000")]
      public byte ColumnB { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      [CsvColumn(2, FormatString = "\"{0}\"")]
      public byte ColumnC { get; set; }

      /// <summary>Gets or sets the value of the fourth column.</summary>
      [CsvColumn(3, FormatString = "\"{0:000}\"")]
      public byte ColumnD { get; set; }

      /// <summary>Gets or sets the value of the fifth column.</summary>
      [CsvColumn(4)]
      public byte? ColumnE { get; set; }

      /// <summary>Gets or sets the value of the sixth column.</summary>
      [CsvColumn(5, FormatString = "000")]
      public byte? ColumnF { get; set; }

      /// <summary>Gets or sets the value of the seventh column.</summary>
      [CsvColumn(6, FormatString = "\"{0}\"")]
      public byte? ColumnG { get; set; }

      /// <summary>Gets or sets the value of the eigth column.</summary>
      [CsvColumn(7, FormatString = "\"{0:000}\"")]
      public byte? ColumnH { get; set; }

      /// <summary>Gets or sets the value of the ninth column.</summary>
      [CsvColumn(8, NullString = "A")]
      public byte? ColumnI { get; set; }

      /// <summary>Gets or sets the value of the tenth column.</summary>
      [CsvColumn(9, NullString = "A", FormatString = "000")]
      public byte? ColumnJ { get; set; }
    }

    /// <summary>A basic dummy class with float properties to support the testcases.</summary>
    [CsvRecord(';')]
    private class TestDummyWithFloatProperties {
      /// <summary>Gets or sets the value of the first column.</summary>
      [CsvColumn(0)]
      public float ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      [CsvColumn(1, FormatString = "00.00", CultureName = "nl-NL")]
      public float ColumnB { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      [CsvColumn(2, FormatString = "\"{0}\"")]
      public float ColumnC { get; set; }

      /// <summary>Gets or sets the value of the fourth column.</summary>
      [CsvColumn(3, FormatString = "\"{0:00.00}\"", CultureName = "nl-NL")]
      public float ColumnD { get; set; }

      /// <summary>Gets or sets the value of the fifth column.</summary>
      [CsvColumn(4)]
      public float? ColumnE { get; set; }

      /// <summary>Gets or sets the value of the sixth column.</summary>
      [CsvColumn(5, FormatString = "00.00")]
      public float? ColumnF { get; set; }

      /// <summary>Gets or sets the value of the seventh column.</summary>
      [CsvColumn(6, FormatString = "\"{0}\"")]
      public float? ColumnG { get; set; }

      /// <summary>Gets or sets the value of the eigth column.</summary>
      [CsvColumn(7, FormatString = "\"{0:00.00}\"")]
      public float? ColumnH { get; set; }

      /// <summary>Gets or sets the value of the ninth column.</summary>
      [CsvColumn(8, NullString = "A")]
      public float? ColumnI { get; set; }

      /// <summary>Gets or sets the value of the tenth column.</summary>
      [CsvColumn(9, NullString = "A", FormatString = "\"{0:0.00}\"")]
      public float? ColumnJ { get; set; }
    }

    /// <summary>A basic dummy class with double properties to support the testcases.</summary>
    [CsvRecord(';')]
    private class TestDummyWithDoubleProperties {
      /// <summary>Gets or sets the value of the first column.</summary>
      [CsvColumn(0)]
      public double ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      [CsvColumn(1, FormatString = "00.00", CultureName = "nl-NL")]
      public double ColumnB { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      [CsvColumn(2, FormatString = "\"{0}\"")]
      public double ColumnC { get; set; }

      /// <summary>Gets or sets the value of the fourth column.</summary>
      [CsvColumn(3, FormatString = "\"{0:00.00}\"", CultureName = "nl-NL")]
      public double ColumnD { get; set; }

      /// <summary>Gets or sets the value of the fifth column.</summary>
      [CsvColumn(4)]
      public double? ColumnE { get; set; }

      /// <summary>Gets or sets the value of the sixth column.</summary>
      [CsvColumn(5, FormatString = "00.00")]
      public double? ColumnF { get; set; }

      /// <summary>Gets or sets the value of the seventh column.</summary>
      [CsvColumn(6, FormatString = "\"{0}\"")]
      public double? ColumnG { get; set; }

      /// <summary>Gets or sets the value of the eigth column.</summary>
      [CsvColumn(7, FormatString = "\"{0:00.00}\"")]
      public double? ColumnH { get; set; }

      /// <summary>Gets or sets the value of the ninth column.</summary>
      [CsvColumn(8, NullString = "A")]
      public double? ColumnI { get; set; }

      /// <summary>Gets or sets the value of the tenth column.</summary>
      [CsvColumn(9, NullString = "A", FormatString = "\"{0:0.00}\"")]
      public double? ColumnJ { get; set; }
    }

    /// <summary>A basic dummy class with decimal properties to support the testcases.</summary>
    [CsvRecord(';')]
    private class TestDummyWithDecimalProperties {
      /// <summary>Gets or sets the value of the first column.</summary>
      [CsvColumn(0)]
      public decimal ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      [CsvColumn(1, FormatString = "00.00", CultureName = "nl-NL")]
      public decimal ColumnB { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      [CsvColumn(2, FormatString = "\"{0}\"")]
      public decimal ColumnC { get; set; }

      /// <summary>Gets or sets the value of the fourth column.</summary>
      [CsvColumn(3, FormatString = "\"{0:00.00}\"", CultureName = "nl-NL")]
      public decimal ColumnD { get; set; }

      /// <summary>Gets or sets the value of the fifth column.</summary>
      [CsvColumn(4)]
      public decimal? ColumnE { get; set; }

      /// <summary>Gets or sets the value of the sixth column.</summary>
      [CsvColumn(5, FormatString = "00.00")]
      public decimal? ColumnF { get; set; }

      /// <summary>Gets or sets the value of the seventh column.</summary>
      [CsvColumn(6, FormatString = "\"{0}\"")]
      public decimal? ColumnG { get; set; }

      /// <summary>Gets or sets the value of the eigth column.</summary>
      [CsvColumn(7, FormatString = "\"{0:00.00}\"")]
      public decimal? ColumnH { get; set; }

      /// <summary>Gets or sets the value of the ninth column.</summary>
      [CsvColumn(8, NullString = "A")]
      public decimal? ColumnI { get; set; }

      /// <summary>Gets or sets the value of the tenth column.</summary>
      [CsvColumn(9, NullString = "A", FormatString = "0.00")]
      public decimal? ColumnJ { get; set; }
    }

    /// <summary>A basic dummy class with short properties to support the testcases.</summary>
    [CsvRecord]
    private class TestDummyWithShortProperties {
      /// <summary>Gets or sets the value of the first column.</summary>
      [CsvColumn(0)]
      public short ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      [CsvColumn(1, FormatString = "000")]
      public short ColumnB { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      [CsvColumn(2, FormatString = "\"{0}\"")]
      public short ColumnC { get; set; }

      /// <summary>Gets or sets the value of the fourth column.</summary>
      [CsvColumn(3, FormatString = "\"{0:000}\"")]
      public short ColumnD { get; set; }

      /// <summary>Gets or sets the value of the fifth column.</summary>
      [CsvColumn(4)]
      public short? ColumnE { get; set; }

      /// <summary>Gets or sets the value of the sixth column.</summary>
      [CsvColumn(5, FormatString = "000")]
      public short? ColumnF { get; set; }

      /// <summary>Gets or sets the value of the seventh column.</summary>
      [CsvColumn(6, FormatString = "\"{0}\"")]
      public short? ColumnG { get; set; }

      /// <summary>Gets or sets the value of the eigth column.</summary>
      [CsvColumn(7, FormatString = "\"{0:000}\"")]
      public short? ColumnH { get; set; }

      /// <summary>Gets or sets the value of the ninth column.</summary>
      [CsvColumn(8, NullString = "A")]
      public short? ColumnI { get; set; }

      /// <summary>Gets or sets the value of the tenth column.</summary>
      [CsvColumn(9, NullString = "A", FormatString = "000")]
      public short? ColumnJ { get; set; }
    }

    /// <summary>A basic dummy class with long properties to support the testcases.</summary>
    [CsvRecord]
    private class TestDummyWithLongProperties {
      /// <summary>Gets or sets the value of the first column.</summary>
      [CsvColumn(0)]
      public long ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      [CsvColumn(1, FormatString = "000")]
      public long ColumnB { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      [CsvColumn(2, FormatString = "\"{0}\"")]
      public long ColumnC { get; set; }

      /// <summary>Gets or sets the value of the fourth column.</summary>
      [CsvColumn(3, FormatString = "\"{0:000}\"")]
      public long ColumnD { get; set; }

      /// <summary>Gets or sets the value of the fifth column.</summary>
      [CsvColumn(4)]
      public long? ColumnE { get; set; }

      /// <summary>Gets or sets the value of the sixth column.</summary>
      [CsvColumn(5, FormatString = "000")]
      public long? ColumnF { get; set; }

      /// <summary>Gets or sets the value of the seventh column.</summary>
      [CsvColumn(6, FormatString = "\"{0}\"")]
      public long? ColumnG { get; set; }

      /// <summary>Gets or sets the value of the eigth column.</summary>
      [CsvColumn(7, FormatString = "\"{0:000}\"")]
      public long? ColumnH { get; set; }

      /// <summary>Gets or sets the value of the ninth column.</summary>
      [CsvColumn(8, NullString = "A")]
      public long? ColumnI { get; set; }

      /// <summary>Gets or sets the value of the tenth column.</summary>
      [CsvColumn(9, NullString = "A", FormatString = "000")]
      public long? ColumnJ { get; set; }
    }

    /// <summary>A basic dummy class with char properties to support the testcases.</summary>
    [CsvRecord]
    private class TestDummyWithCharProperties {
      /// <summary>Gets or sets the value of the first column.</summary>
      [CsvColumn(0)]
      public char ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      [CsvColumn(1, FormatString = "000")]
      public char ColumnB { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      [CsvColumn(2, FormatString = "\"{0}\"")]
      public char ColumnC { get; set; }

      /// <summary>Gets or sets the value of the fourth column.</summary>
      [CsvColumn(3, FormatString = "\"{0:000}\"")]
      public char ColumnD { get; set; }

      /// <summary>Gets or sets the value of the fifth column.</summary>
      [CsvColumn(4)]
      public char? ColumnE { get; set; }

      /// <summary>Gets or sets the value of the sixth column.</summary>
      [CsvColumn(5, FormatString = "000")]
      public char? ColumnF { get; set; }

      /// <summary>Gets or sets the value of the seventh column.</summary>
      [CsvColumn(6, FormatString = "\"{0}\"")]
      public char? ColumnG { get; set; }

      /// <summary>Gets or sets the value of the eigth column.</summary>
      [CsvColumn(7, FormatString = "\"{0:000}\"")]
      public char? ColumnH { get; set; }

      /// <summary>Gets or sets the value of the ninth column.</summary>
      [CsvColumn(8, NullString = "<>")]
      public char? ColumnI { get; set; }

      /// <summary>Gets or sets the value of the tenth column.</summary>
      [CsvColumn(9, NullString = "<>", FormatString = "000")]
      public char? ColumnJ { get; set; }
    }

    /// <summary>A basic dummy class with string properties to support the testcases.</summary>
    [CsvRecord]
    private class TestDummyWithStringProperties {
      /// <summary>Gets or sets the value of the first column.</summary>
      [CsvColumn(0)]
      public string ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      [CsvColumn(1, FormatString = "-5")]
      public string ColumnB { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      [CsvColumn(2, FormatString = "\"{0}\"")]
      public string ColumnC { get; set; }

      /// <summary>Gets or sets the value of the fourth column.</summary>
      [CsvColumn(3, FormatString = "\"{0,-5}\"")]
      public string ColumnD { get; set; }

      /// <summary>Gets or sets the value of the fifth column.</summary>
      [CsvColumn(4, NullString = "<null>")]
      public string ColumnE { get; set; }

      /// <summary>Gets or sets the value of the sixth column.</summary>
      [CsvColumn(5, NullString = "<null>", FormatString = "\"{0}\"")]
      public string ColumnF { get; set; }
    }

    /// <summary>A basic dummy class with DateTime properties to support the testcases.</summary>
    [CsvRecord]
    private class TestDummyWithDateTimeProperties {
      /// <summary>Gets or sets the value of the first column.</summary>
      [CsvColumn(0, CultureName = "nl-NL")]
      public DateTime ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      [CsvColumn(1, FormatString = "yyyyMMdd")]
      public DateTime ColumnB { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      [CsvColumn(2, FormatString = "\"{0}\"", CultureName = "nl-NL")]
      public DateTime ColumnC { get; set; }

      /// <summary>Gets or sets the value of the fourth column.</summary>
      [CsvColumn(3, FormatString = "\"{0:yyyyMMdd}\"")]
      public DateTime ColumnD { get; set; }

      /// <summary>Gets or sets the value of the fifth column.</summary>
      [CsvColumn(4, CultureName = "nl-NL")]
      public DateTime? ColumnE { get; set; }

      /// <summary>Gets or sets the value of the sixth column.</summary>
      [CsvColumn(5, FormatString = "yyyyMMdd")]
      public DateTime? ColumnF { get; set; }

      /// <summary>Gets or sets the value of the seventh column.</summary>
      [CsvColumn(6, FormatString = "\"{0}\"", CultureName = "nl-NL")]
      public DateTime? ColumnG { get; set; }

      /// <summary>Gets or sets the value of the eigth column.</summary>
      [CsvColumn(7, FormatString = "\"{0:yyyyMMdd}\"")]
      public DateTime? ColumnH { get; set; }

      /// <summary>Gets or sets the value of the ninth column.</summary>
      [CsvColumn(8, NullString = "0", CultureName = "nl-NL")]
      public DateTime? ColumnI { get; set; }

      /// <summary>Gets or sets the value of the tenth column.</summary>
      [CsvColumn(9, NullString = "0", FormatString = "yyyyMMdd")]
      public DateTime? ColumnJ { get; set; }
    }

    /// <summary>A basic dummy class with enum properties to support the testcases.</summary>
    [CsvRecord]
    private class TestDummyWithEnumProperties {
      /// <summary>Gets or sets the value of the first column.</summary>
      [CsvColumn(0)]
      public TestEnum ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      [CsvColumn(1, FormatString = "000")]
      public TestEnum ColumnB { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      [CsvColumn(2, FormatString = "\"{0}\"")]
      public TestEnum ColumnC { get; set; }

      /// <summary>Gets or sets the value of the fourth column.</summary>
      [CsvColumn(3, FormatString = "\"{0:000}\"")]
      public TestEnum ColumnD { get; set; }

      /// <summary>Gets or sets the value of the fifth column.</summary>
      [CsvColumn(4)]
      public TestEnum? ColumnE { get; set; }

      /// <summary>Gets or sets the value of the sixth column.</summary>
      [CsvColumn(5, FormatString = "000")]
      public TestEnum? ColumnF { get; set; }

      /// <summary>Gets or sets the value of the seventh column.</summary>
      [CsvColumn(6, FormatString = "\"{0}\"")]
      public TestEnum? ColumnG { get; set; }

      /// <summary>Gets or sets the value of the eigth column.</summary>
      [CsvColumn(7, FormatString = "\"{0:000}\"")]
      public TestEnum? ColumnH { get; set; }

      /// <summary>Gets or sets the value of the ninth column.</summary>
      [CsvColumn(8, NullString = "X")]
      public TestEnum? ColumnI { get; set; }

      /// <summary>Gets or sets the value of the tenth column.</summary>
      [CsvColumn(9, NullString = "X", FormatString = "0")]
      public TestEnum? ColumnJ { get; set; }
    }

    /// <summary>A basic dummy class with a string separator to support the testcases.</summary>
    [CsvRecord("[SEP]")]
    private class TestDummyWithStringSeparator {
      /// <summary>Gets or sets the value of the first column.</summary>
      [CsvColumn(0)]
      public int ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      [CsvColumn(1)]
      public string ColumnB { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      [CsvColumn(2)]
      public bool ColumnC { get; set; }
    }

    /// <summary>A basic dummy class with a complex string separator to support the testcases.</summary>
    [CsvRecord("\",\"")]
    private class TestDummyWithComplexStringSeparator {
      /// <summary>Gets or sets the value of the first column.</summary>
      [CsvColumn(0, FormatString = "\"{0}")]
      public string ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      [CsvColumn(1)]
      public string ColumnB { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      [CsvColumn(2)]
      public bool ColumnC { get; set; }

      /// <summary>Gets or sets the value of the fourth column.</summary>
      [CsvColumn(3, FormatString = "{0}\"")]
      public int ColumnD { get; set; }
    }

    /// <summary>A basic dummy enum to support the testcases.</summary>
    private enum TestEnum {
      /// <summary>Defines value A.</summary>
      ValueA,

      /// <summary>Defines value B.</summary>
      ValueB,

      /// <summary>Defines value C.</summary>
      ValueC,

      /// <summary>Defines value D.</summary>
      ValueD,

      /// <summary>Defines value E.</summary>
      ValueE
    }
    #endregion
  }
}
