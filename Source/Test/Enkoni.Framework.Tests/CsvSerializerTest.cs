//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvSerializerTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the CsvSerializer class.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Enkoni.Framework.Serialization;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the CsvSerializer class.</summary>
  [TestClass]
  public class CsvSerializerTest {
    #region Deserialize testcases
    /// <summary>Tests the functionality of the <see cref="CsvSerializer{T}.Deserialize(string)"/> and
    /// <see cref="CsvSerializer{T}.Deserialize(Stream)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestInputFileWithHeaderUTF8.csv", @"CsvSerializerTest\TestCase01")]
    public void TestCase01_Deserialize_Complete_DefaultEncoding() {
      string inputPath = @"CsvSerializerTest\TestCase01\CsvTestInputFileWithHeaderUTF8.csv";

      CsvSerializer<TestDummyWithHeader> serializer = new CsvSerializer<TestDummyWithHeader>();
      IEnumerable<TestDummyWithHeader> result = serializer.Deserialize(inputPath);

      CheckTestResult(result);

      FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
      result = serializer.Deserialize(stream);
      stream.Close();

      CheckTestResult(result);
    }

    /// <summary>Tests the functionality of the <see cref="CsvSerializer{T}.Deserialize(string)"/> and
    /// <see cref="CsvSerializer{T}.Deserialize(Stream)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestInputFileNoHeaderUTF8.csv", @"CsvSerializerTest\TestCase02")]
    public void TestCase02_Deserialize_Complete_DefaultEncoding_NoHeader() {
      string inputPath = @"CsvSerializerTest\TestCase02\CsvTestInputFileNoHeaderUTF8.csv";

      CsvSerializer<TestDummyNoHeader> serializer = new CsvSerializer<TestDummyNoHeader>();
      IEnumerable<TestDummyNoHeader> result = serializer.Deserialize(inputPath);

      CheckTestResult(result);

      FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
      result = serializer.Deserialize(stream);
      stream.Close();

      CheckTestResult(result);
    }

    /// <summary>Tests the functionality of the <see cref="CsvSerializer{T}.Deserialize(string, Encoding)"/> and
    /// <see cref="CsvSerializer{T}.Deserialize(Stream, Encoding)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestInputFileWithHeaderASCII.csv", @"CsvSerializerTest\TestCase03")]
    public void TestCase03_Deserialize_Complete_CustomEncoding() {
      string inputPath = @"CsvSerializerTest\TestCase03\CsvTestInputFileWithHeaderASCII.csv";

      CsvSerializer<TestDummyWithHeader> serializer = new CsvSerializer<TestDummyWithHeader>();
      IEnumerable<TestDummyWithHeader> result = serializer.Deserialize(inputPath, Encoding.ASCII);

      CheckTestResult(result);

      FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
      result = serializer.Deserialize(stream, Encoding.ASCII);
      stream.Close();

      CheckTestResult(result);
    }

    /// <summary>Tests the functionality of the <see cref="CsvSerializer{T}.Deserialize(string, Encoding)"/> and
    /// <see cref="CsvSerializer{T}.Deserialize(Stream, Encoding)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestInputFileNoHeaderASCII.csv", @"CsvSerializerTest\TestCase04")]
    public void TestCase04_Deserialize_Complete_CustomEncoding_NoHeader() {
      string inputPath = @"CsvSerializerTest\TestCase04\CsvTestInputFileNoHeaderASCII.csv";

      CsvSerializer<TestDummyNoHeader> serializer = new CsvSerializer<TestDummyNoHeader>();
      IEnumerable<TestDummyNoHeader> result = serializer.Deserialize(inputPath, Encoding.ASCII);

      CheckTestResult(result);
      
      FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
      result = serializer.Deserialize(stream, Encoding.ASCII);
      stream.Close();

      CheckTestResult(result);
    }

    /// <summary>Tests the functionality of the <see cref="CsvSerializer{T}.Deserialize(string)"/> and
    /// <see cref="CsvSerializer{T}.Deserialize(Stream)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestInputFileWithHeaderUTF8.csv", @"CsvSerializerTest\TestCase05")]
    public void TestCase05_Deserialize_Partial_DefaultEncoding() {
      string inputPath = @"CsvSerializerTest\TestCase05\CsvTestInputFileWithHeaderUTF8.csv";

      CsvSerializer<PartialTestDummyWithHeader> serializer = new CsvSerializer<PartialTestDummyWithHeader>();
      IEnumerable<PartialTestDummyWithHeader> result = serializer.Deserialize(inputPath);

      CheckTestResult(result);

      FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
      result = serializer.Deserialize(stream);
      stream.Close();

      CheckTestResult(result);
    }

    /// <summary>Tests the functionality of the <see cref="CsvSerializer{T}.Deserialize(string)"/> and
    /// <see cref="CsvSerializer{T}.Deserialize(Stream)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestInputFileNoHeaderUTF8.csv", @"CsvSerializerTest\TestCase06")]
    public void TestCase06_Deserialize_Partial_DefaultEncoding_NoHeader() {
      string inputPath = @"CsvSerializerTest\TestCase06\CsvTestInputFileNoHeaderUTF8.csv";

      CsvSerializer<PartialTestDummyNoHeader> serializer = new CsvSerializer<PartialTestDummyNoHeader>();
      IEnumerable<PartialTestDummyNoHeader> result = serializer.Deserialize(inputPath);

      CheckTestResult(result);

      FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
      result = serializer.Deserialize(stream);
      stream.Close();

      CheckTestResult(result);
    }

    /// <summary>Tests the functionality of the <see cref="CsvSerializer{T}.Deserialize(string, Encoding)"/> and
    /// <see cref="CsvSerializer{T}.Deserialize(Stream, Encoding)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestInputFileWithHeaderASCII.csv", @"CsvSerializerTest\TestCase07")]
    public void TestCase07_Deserialize_Partial_CustomEncoding() {
      string inputPath = @"CsvSerializerTest\TestCase07\CsvTestInputFileWithHeaderASCII.csv";

      CsvSerializer<PartialTestDummyWithHeader> serializer = new CsvSerializer<PartialTestDummyWithHeader>();
      IEnumerable<PartialTestDummyWithHeader> result = serializer.Deserialize(inputPath, Encoding.ASCII);

      CheckTestResult(result);

      FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
      result = serializer.Deserialize(stream, Encoding.ASCII);
      stream.Close();

      CheckTestResult(result);
    }

    /// <summary>Tests the functionality of the <see cref="CsvSerializer{T}.Deserialize(string, Encoding)"/> and
    /// <see cref="CsvSerializer{T}.Deserialize(Stream, Encoding)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestInputFileNoHeaderASCII.csv", @"CsvSerializerTest\TestCase08")]
    public void TestCase08_Deserialize_Partial_CustomEncoding_NoHeader() {
      string inputPath = @"CsvSerializerTest\TestCase08\CsvTestInputFileNoHeaderASCII.csv";

      CsvSerializer<PartialTestDummyNoHeader> serializer = new CsvSerializer<PartialTestDummyNoHeader>();
      IEnumerable<PartialTestDummyNoHeader> result = serializer.Deserialize(inputPath, Encoding.ASCII);

      CheckTestResult(result);

      FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
      result = serializer.Deserialize(stream, Encoding.ASCII);
      stream.Close();

      CheckTestResult(result);
    }
    #endregion

    #region Serialize testcases
    /// <summary>Tests the functionality of the <see cref="CsvSerializer{T}.Serialize(IEnumerable{T},string)"/> and
    /// <see cref="CsvSerializer{T}.Serialize(IEnumerable{T},Stream)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestOutputFileWithHeaderUTF8.csv", @"CsvSerializerTest\TestCase09")]
    public void TestCase09_Serialize_Complete_DefaultEncoding() {
      string templatePath = @"CsvSerializerTest\TestCase09\CsvTestOutputFileWithHeaderUTF8.csv";
      string outputPath = @"CsvSerializerTest\TestCase09\CsvTestOutputFile.csv";
      CreateOutputDir(outputPath);

      List<TestDummyWithHeader> collection = new List<TestDummyWithHeader>();
      TestDummyWithHeader dummyA = new TestDummyWithHeader {
        ColumnA = "ColA_1", ColumnB = 1, ColumnC = new DateTime(2011, 1, 1, 12, 0, 1), ColumnD = 0.1F
      };
      TestDummyWithHeader dummyB = new TestDummyWithHeader {
        ColumnA = "ColA_2", ColumnB = 2, ColumnC = new DateTime(2011, 1, 1, 12, 0, 2), ColumnD = 0.2F
      };
      TestDummyWithHeader dummyC = new TestDummyWithHeader {
        ColumnA = "ColA_3", ColumnB = 3, ColumnC = new DateTime(2011, 1, 1, 12, 0, 3), ColumnD = 0.3F
      };
      TestDummyWithHeader dummyD = new TestDummyWithHeader {
        ColumnA = "ColA_4", ColumnB = 4, ColumnC = new DateTime(2011, 1, 1, 12, 0, 4), ColumnD = 0.4F
      };
      TestDummyWithHeader dummyE = new TestDummyWithHeader {
        ColumnA = "ColA_5", ColumnB = 5, ColumnC = new DateTime(2011, 1, 1, 12, 0, 5), ColumnD = 0.5F
      };
      TestDummyWithHeader dummyF = new TestDummyWithHeader {
        ColumnA = "ColA_6", ColumnB = 6, ColumnC = new DateTime(2011, 1, 1, 12, 0, 6), ColumnD = 0.6F
      };
      collection.Add(dummyA);
      collection.Add(dummyB);
      collection.Add(dummyC);
      collection.Add(dummyD);
      collection.Add(dummyE);
      collection.Add(dummyF);

      CsvSerializer<TestDummyWithHeader> serializer = new CsvSerializer<TestDummyWithHeader>();
      serializer.Serialize(collection, outputPath);

      CheckTestResult(templatePath, outputPath);

      FileStream stream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);
      serializer.Serialize(collection, stream);
      stream.Close();

      CheckTestResult(templatePath, outputPath);
    }

    /// <summary>Tests the functionality of the <see cref="CsvSerializer{T}.Serialize(IEnumerable{T},string)"/> and
    /// <see cref="CsvSerializer{T}.Serialize(IEnumerable{T},Stream)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestOutputFileNoHeaderUTF8.csv", @"CsvSerializerTest\TestCase10")]
    public void TestCase10_Serialize_Complete_DefaultEncoding_NoHeader() {
      string templatePath = @"CsvSerializerTest\TestCase10\CsvTestOutputFileNoHeaderUTF8.csv";
      string outputPath = @"CsvSerializerTest\TestCase10\CsvTestOutputFile.csv";
      CreateOutputDir(outputPath);

      List<TestDummyNoHeader> collection = new List<TestDummyNoHeader>();
      TestDummyNoHeader dummyA = new TestDummyNoHeader {
        ColumnA = "ColA_1", ColumnB = 1, ColumnC = new DateTime(2011, 1, 1, 12, 0, 1), ColumnD = 0.1F
      };
      TestDummyNoHeader dummyB = new TestDummyNoHeader {
        ColumnA = "ColA_2", ColumnB = 2, ColumnC = new DateTime(2011, 1, 1, 12, 0, 2), ColumnD = 0.2F
      };
      TestDummyNoHeader dummyC = new TestDummyNoHeader {
        ColumnA = "ColA_3", ColumnB = 3, ColumnC = new DateTime(2011, 1, 1, 12, 0, 3), ColumnD = 0.3F
      };
      TestDummyNoHeader dummyD = new TestDummyNoHeader {
        ColumnA = "ColA_4", ColumnB = 4, ColumnC = new DateTime(2011, 1, 1, 12, 0, 4), ColumnD = 0.4F
      };
      TestDummyNoHeader dummyE = new TestDummyNoHeader {
        ColumnA = "ColA_5", ColumnB = 5, ColumnC = new DateTime(2011, 1, 1, 12, 0, 5), ColumnD = 0.5F
      };
      TestDummyNoHeader dummyF = new TestDummyNoHeader {
        ColumnA = "ColA_6", ColumnB = 6, ColumnC = new DateTime(2011, 1, 1, 12, 0, 6), ColumnD = 0.6F
      };
      collection.Add(dummyA);
      collection.Add(dummyB);
      collection.Add(dummyC);
      collection.Add(dummyD);
      collection.Add(dummyE);
      collection.Add(dummyF);

      CsvSerializer<TestDummyNoHeader> serializer = new CsvSerializer<TestDummyNoHeader>();
      serializer.Serialize(collection, outputPath);

      CheckTestResult(templatePath, outputPath);

      FileStream stream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);
      serializer.Serialize(collection, stream);
      stream.Close();

      CheckTestResult(templatePath, outputPath);
    }

    /// <summary>Tests the functionality of the <see cref="CsvSerializer{T}.Serialize(IEnumerable{T},string, Encoding)"/> and
    /// <see cref="CsvSerializer{T}.Serialize(IEnumerable{T},Stream, Encoding)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestOutputFileWithHeaderASCII.csv", @"CsvSerializerTest\TestCase11")]
    public void TestCase11_Serialize_Complete_CustomEncoding() {
      string templatePath = @"CsvSerializerTest\TestCase11\CsvTestOutputFileWithHeaderASCII.csv";
      string outputPath = @"CsvSerializerTest\TestCase11\CsvTestOutputFile.csv";
      CreateOutputDir(outputPath);

      List<TestDummyWithHeader> collection = new List<TestDummyWithHeader>();
      TestDummyWithHeader dummyA = new TestDummyWithHeader {
        ColumnA = "ColA_1", ColumnB = 1, ColumnC = new DateTime(2011, 1, 1, 12, 0, 1), ColumnD = 0.1F
      };
      TestDummyWithHeader dummyB = new TestDummyWithHeader {
        ColumnA = "ColA_2", ColumnB = 2, ColumnC = new DateTime(2011, 1, 1, 12, 0, 2), ColumnD = 0.2F
      };
      TestDummyWithHeader dummyC = new TestDummyWithHeader {
        ColumnA = "ColA_3", ColumnB = 3, ColumnC = new DateTime(2011, 1, 1, 12, 0, 3), ColumnD = 0.3F
      };
      TestDummyWithHeader dummyD = new TestDummyWithHeader {
        ColumnA = "ColA_4", ColumnB = 4, ColumnC = new DateTime(2011, 1, 1, 12, 0, 4), ColumnD = 0.4F
      };
      TestDummyWithHeader dummyE = new TestDummyWithHeader {
        ColumnA = "ColA_5", ColumnB = 5, ColumnC = new DateTime(2011, 1, 1, 12, 0, 5), ColumnD = 0.5F
      };
      TestDummyWithHeader dummyF = new TestDummyWithHeader {
        ColumnA = "ColA_6", ColumnB = 6, ColumnC = new DateTime(2011, 1, 1, 12, 0, 6), ColumnD = 0.6F
      };
      collection.Add(dummyA);
      collection.Add(dummyB);
      collection.Add(dummyC);
      collection.Add(dummyD);
      collection.Add(dummyE);
      collection.Add(dummyF);

      CsvSerializer<TestDummyWithHeader> serializer = new CsvSerializer<TestDummyWithHeader>();
      serializer.Serialize(collection, outputPath, Encoding.ASCII);

      CheckTestResult(templatePath, outputPath);

      FileStream stream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);
      serializer.Serialize(collection, stream, Encoding.ASCII);
      stream.Close();

      CheckTestResult(templatePath, outputPath);
    }

    /// <summary>Tests the functionality of the <see cref="CsvSerializer{T}.Serialize(IEnumerable{T},string, Encoding)"/> and
    /// <see cref="CsvSerializer{T}.Serialize(IEnumerable{T},Stream, Encoding)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestOutputFileNoHeaderASCII.csv", @"CsvSerializerTest\TestCase12")]
    public void TestCase12_Serialize_Complete_CustomEncoding_NoHeader() {
      string templatePath = @"CsvSerializerTest\TestCase12\CsvTestOutputFileNoHeaderASCII.csv";
      string outputPath = @"CsvSerializerTest\TestCase12\CsvTestOutputFile.csv";
      CreateOutputDir(outputPath);

      List<TestDummyNoHeader> collection = new List<TestDummyNoHeader>();
      TestDummyNoHeader dummyA = new TestDummyNoHeader {
        ColumnA = "ColA_1", ColumnB = 1, ColumnC = new DateTime(2011, 1, 1, 12, 0, 1), ColumnD = 0.1F
      };
      TestDummyNoHeader dummyB = new TestDummyNoHeader {
        ColumnA = "ColA_2", ColumnB = 2, ColumnC = new DateTime(2011, 1, 1, 12, 0, 2), ColumnD = 0.2F
      };
      TestDummyNoHeader dummyC = new TestDummyNoHeader {
        ColumnA = "ColA_3", ColumnB = 3, ColumnC = new DateTime(2011, 1, 1, 12, 0, 3), ColumnD = 0.3F
      };
      TestDummyNoHeader dummyD = new TestDummyNoHeader {
        ColumnA = "ColA_4", ColumnB = 4, ColumnC = new DateTime(2011, 1, 1, 12, 0, 4), ColumnD = 0.4F
      };
      TestDummyNoHeader dummyE = new TestDummyNoHeader {
        ColumnA = "ColA_5", ColumnB = 5, ColumnC = new DateTime(2011, 1, 1, 12, 0, 5), ColumnD = 0.5F
      };
      TestDummyNoHeader dummyF = new TestDummyNoHeader {
        ColumnA = "ColA_6", ColumnB = 6, ColumnC = new DateTime(2011, 1, 1, 12, 0, 6), ColumnD = 0.6F
      };
      collection.Add(dummyA);
      collection.Add(dummyB);
      collection.Add(dummyC);
      collection.Add(dummyD);
      collection.Add(dummyE);
      collection.Add(dummyF);

      CsvSerializer<TestDummyNoHeader> serializer = new CsvSerializer<TestDummyNoHeader>();
      serializer.Serialize(collection, outputPath, Encoding.ASCII);

      CheckTestResult(templatePath, outputPath);

      FileStream stream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);
      serializer.Serialize(collection, stream, Encoding.ASCII);
      stream.Close();

      CheckTestResult(templatePath, outputPath);
    }

    /// <summary>Tests the functionality of the <see cref="CsvSerializer{T}.Serialize(IEnumerable{T},string)"/> and
    /// <see cref="CsvSerializer{T}.Serialize(IEnumerable{T},Stream)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestOutputFilePartialWithHeaderUTF8.csv", @"CsvSerializerTest\TestCase13")]
    public void TestCase13_Serialize_Partial_DefaultEncoding() {
      string templatePath = @"CsvSerializerTest\TestCase13\CsvTestOutputFilePartialWithHeaderUTF8.csv";
      string outputPath = @"CsvSerializerTest\TestCase13\CsvTestOutputFile.csv";
      CreateOutputDir(outputPath);

      List<PartialTestDummyWithHeader> collection = new List<PartialTestDummyWithHeader>();
      PartialTestDummyWithHeader dummyA = new PartialTestDummyWithHeader {
        ColumnA = "ColA_1", ColumnB = 1, ColumnC = new DateTime(2011, 1, 1, 12, 0, 1), ColumnD = 0.1F
      };
      PartialTestDummyWithHeader dummyB = new PartialTestDummyWithHeader {
        ColumnA = "ColA_2", ColumnB = 2, ColumnC = new DateTime(2011, 1, 1, 12, 0, 2), ColumnD = 0.2F
      };
      PartialTestDummyWithHeader dummyC = new PartialTestDummyWithHeader {
        ColumnA = "ColA_3", ColumnB = 3, ColumnC = new DateTime(2011, 1, 1, 12, 0, 3), ColumnD = 0.3F
      };
      PartialTestDummyWithHeader dummyD = new PartialTestDummyWithHeader {
        ColumnA = "ColA_4", ColumnB = 4, ColumnC = new DateTime(2011, 1, 1, 12, 0, 4), ColumnD = 0.4F
      };
      PartialTestDummyWithHeader dummyE = new PartialTestDummyWithHeader {
        ColumnA = "ColA_5", ColumnB = 5, ColumnC = new DateTime(2011, 1, 1, 12, 0, 5), ColumnD = 0.5F
      };
      PartialTestDummyWithHeader dummyF = new PartialTestDummyWithHeader {
        ColumnA = "ColA_6", ColumnB = 6, ColumnC = new DateTime(2011, 1, 1, 12, 0, 6), ColumnD = 0.6F
      };
      collection.Add(dummyA);
      collection.Add(dummyB);
      collection.Add(dummyC);
      collection.Add(dummyD);
      collection.Add(dummyE);
      collection.Add(dummyF);

      CsvSerializer<PartialTestDummyWithHeader> serializer = new CsvSerializer<PartialTestDummyWithHeader>();
      serializer.Serialize(collection, outputPath);

      CheckTestResult(templatePath, outputPath);

      FileStream stream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);
      serializer.Serialize(collection, stream);
      stream.Close();

      CheckTestResult(templatePath, outputPath);
    }

    /// <summary>Tests the functionality of the <see cref="CsvSerializer{T}.Serialize(IEnumerable{T},string)"/> and
    /// <see cref="CsvSerializer{T}.Serialize(IEnumerable{T},Stream)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestOutputFilePartialNoHeaderUTF8.csv", @"CsvSerializerTest\TestCase14")]
    public void TestCase14_Serialize_Partial_DefaultEncoding_NoHeader() {
      string templatePath = @"CsvSerializerTest\TestCase14\CsvTestOutputFilePartialNoHeaderUTF8.csv";
      string outputPath = @"CsvSerializerTest\TestCase14\CsvTestOutputFile.csv";
      CreateOutputDir(outputPath);

      List<PartialTestDummyNoHeader> collection = new List<PartialTestDummyNoHeader>();
      PartialTestDummyNoHeader dummyA = new PartialTestDummyNoHeader {
        ColumnA = "ColA_1", ColumnB = 1, ColumnC = new DateTime(2011, 1, 1, 12, 0, 1), ColumnD = 0.1F
      };
      PartialTestDummyNoHeader dummyB = new PartialTestDummyNoHeader {
        ColumnA = "ColA_2", ColumnB = 2, ColumnC = new DateTime(2011, 1, 1, 12, 0, 2), ColumnD = 0.2F
      };
      PartialTestDummyNoHeader dummyC = new PartialTestDummyNoHeader {
        ColumnA = "ColA_3", ColumnB = 3, ColumnC = new DateTime(2011, 1, 1, 12, 0, 3), ColumnD = 0.3F
      };
      PartialTestDummyNoHeader dummyD = new PartialTestDummyNoHeader {
        ColumnA = "ColA_4", ColumnB = 4, ColumnC = new DateTime(2011, 1, 1, 12, 0, 4), ColumnD = 0.4F
      };
      PartialTestDummyNoHeader dummyE = new PartialTestDummyNoHeader {
        ColumnA = "ColA_5", ColumnB = 5, ColumnC = new DateTime(2011, 1, 1, 12, 0, 5), ColumnD = 0.5F
      };
      PartialTestDummyNoHeader dummyF = new PartialTestDummyNoHeader {
        ColumnA = "ColA_6", ColumnB = 6, ColumnC = new DateTime(2011, 1, 1, 12, 0, 6), ColumnD = 0.6F
      };
      collection.Add(dummyA);
      collection.Add(dummyB);
      collection.Add(dummyC);
      collection.Add(dummyD);
      collection.Add(dummyE);
      collection.Add(dummyF);

      CsvSerializer<PartialTestDummyNoHeader> serializer = new CsvSerializer<PartialTestDummyNoHeader>();
      serializer.Serialize(collection, outputPath);

      CheckTestResult(templatePath, outputPath);

      FileStream stream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);
      serializer.Serialize(collection, stream);
      stream.Close();

      CheckTestResult(templatePath, outputPath);
    }

    /// <summary>Tests the functionality of the <see cref="CsvSerializer{T}.Serialize(IEnumerable{T},string, Encoding)"/> and
    /// <see cref="CsvSerializer{T}.Serialize(IEnumerable{T},Stream, Encoding)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestOutputFilePartialWithHeaderASCII.csv", @"CsvSerializerTest\TestCase15")]
    public void TestCase15_Serialize_Partial_CustomEncoding() {
      string templatePath = @"CsvSerializerTest\TestCase15\CsvTestOutputFilePartialWithHeaderASCII.csv";
      string outputPath = @"CsvSerializerTest\TestCase15\CsvTestOutputFile.csv";
      CreateOutputDir(outputPath);

      List<PartialTestDummyWithHeader> collection = new List<PartialTestDummyWithHeader>();
      PartialTestDummyWithHeader dummyA = new PartialTestDummyWithHeader {
        ColumnA = "ColA_1", ColumnB = 1, ColumnC = new DateTime(2011, 1, 1, 12, 0, 1), ColumnD = 0.1F
      };
      PartialTestDummyWithHeader dummyB = new PartialTestDummyWithHeader {
        ColumnA = "ColA_2", ColumnB = 2, ColumnC = new DateTime(2011, 1, 1, 12, 0, 2), ColumnD = 0.2F
      };
      PartialTestDummyWithHeader dummyC = new PartialTestDummyWithHeader {
        ColumnA = "ColA_3", ColumnB = 3, ColumnC = new DateTime(2011, 1, 1, 12, 0, 3), ColumnD = 0.3F
      };
      PartialTestDummyWithHeader dummyD = new PartialTestDummyWithHeader {
        ColumnA = "ColA_4", ColumnB = 4, ColumnC = new DateTime(2011, 1, 1, 12, 0, 4), ColumnD = 0.4F
      };
      PartialTestDummyWithHeader dummyE = new PartialTestDummyWithHeader {
        ColumnA = "ColA_5", ColumnB = 5, ColumnC = new DateTime(2011, 1, 1, 12, 0, 5), ColumnD = 0.5F
      };
      PartialTestDummyWithHeader dummyF = new PartialTestDummyWithHeader {
        ColumnA = "ColA_6", ColumnB = 6, ColumnC = new DateTime(2011, 1, 1, 12, 0, 6), ColumnD = 0.6F
      };
      collection.Add(dummyA);
      collection.Add(dummyB);
      collection.Add(dummyC);
      collection.Add(dummyD);
      collection.Add(dummyE);
      collection.Add(dummyF);

      CsvSerializer<PartialTestDummyWithHeader> serializer = new CsvSerializer<PartialTestDummyWithHeader>();
      serializer.Serialize(collection, outputPath, Encoding.ASCII);

      CheckTestResult(templatePath, outputPath);

      FileStream stream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);
      serializer.Serialize(collection, stream, Encoding.ASCII);
      stream.Close();

      CheckTestResult(templatePath, outputPath);
    }

    /// <summary>Tests the functionality of the <see cref="CsvSerializer{T}.Serialize(IEnumerable{T},string, Encoding)"/> and
    /// <see cref="CsvSerializer{T}.Serialize(IEnumerable{T},Stream, Encoding)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestOutputFilePartialNoHeaderASCII.csv", @"CsvSerializerTest\TestCase16")]
    public void TestCase16_Serialize_Partial_CustomEncoding_NoHeader() {
      string templatePath = @"CsvSerializerTest\TestCase16\CsvTestOutputFilePartialNoHeaderASCII.csv";
      string outputPath = @"CsvSerializerTest\TestCase16\CsvTestOutputFile.csv";
      CreateOutputDir(outputPath);

      List<PartialTestDummyNoHeader> collection = new List<PartialTestDummyNoHeader>();
      PartialTestDummyNoHeader dummyA = new PartialTestDummyNoHeader {
        ColumnA = "ColA_1", ColumnB = 1, ColumnC = new DateTime(2011, 1, 1, 12, 0, 1), ColumnD = 0.1F
      };
      PartialTestDummyNoHeader dummyB = new PartialTestDummyNoHeader {
        ColumnA = "ColA_2", ColumnB = 2, ColumnC = new DateTime(2011, 1, 1, 12, 0, 2), ColumnD = 0.2F
      };
      PartialTestDummyNoHeader dummyC = new PartialTestDummyNoHeader {
        ColumnA = "ColA_3", ColumnB = 3, ColumnC = new DateTime(2011, 1, 1, 12, 0, 3), ColumnD = 0.3F
      };
      PartialTestDummyNoHeader dummyD = new PartialTestDummyNoHeader {
        ColumnA = "ColA_4", ColumnB = 4, ColumnC = new DateTime(2011, 1, 1, 12, 0, 4), ColumnD = 0.4F
      };
      PartialTestDummyNoHeader dummyE = new PartialTestDummyNoHeader {
        ColumnA = "ColA_5", ColumnB = 5, ColumnC = new DateTime(2011, 1, 1, 12, 0, 5), ColumnD = 0.5F
      };
      PartialTestDummyNoHeader dummyF = new PartialTestDummyNoHeader {
        ColumnA = "ColA_6", ColumnB = 6, ColumnC = new DateTime(2011, 1, 1, 12, 0, 6), ColumnD = 0.6F
      };
      collection.Add(dummyA);
      collection.Add(dummyB);
      collection.Add(dummyC);
      collection.Add(dummyD);
      collection.Add(dummyE);
      collection.Add(dummyF);

      CsvSerializer<PartialTestDummyNoHeader> serializer = new CsvSerializer<PartialTestDummyNoHeader>();
      serializer.Serialize(collection, outputPath, Encoding.ASCII);

      CheckTestResult(templatePath, outputPath);

      FileStream stream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);
      serializer.Serialize(collection, stream, Encoding.ASCII);
      stream.Close();

      CheckTestResult(templatePath, outputPath);
    }
    #endregion

    #region Private helper methods
    /// <summary>Checks if the items in the collection match the expected results.</summary>
    /// <param name="result">The result that must be checked.</param>
    private static void CheckTestResult(IEnumerable<TestDummy> result) {
      Assert.AreEqual(6, result.Count());
      
      for(int i = 1; i <= 6; ++i) {
        TestDummy selectedDummy = result.ElementAt(i - 1);
        Assert.IsNotNull(selectedDummy);
        Assert.AreEqual("ColA_" + i, selectedDummy.ColumnA);
        Assert.AreEqual(i, selectedDummy.ColumnB);
        Assert.AreEqual(new DateTime(2011, 01, 01, 12, 0, i), selectedDummy.ColumnC);
        Assert.AreEqual(i, (int)(selectedDummy.ColumnD * 10));
      }
    }

    /// <summary>Checks if the items in the collection match the expected results.</summary>
    /// <param name="result">The result that must be checked.</param>
    private static void CheckTestResult(IEnumerable<PartialTestDummy> result) {
      Assert.AreEqual(6, result.Count());

      for(int i = 1; i <= 6; ++i) {
        PartialTestDummy selectedDummy = result.ElementAt(i - 1);
        Assert.IsNotNull(selectedDummy);
        Assert.IsNull(selectedDummy.ColumnA);
        Assert.AreEqual(i, selectedDummy.ColumnB);
        Assert.AreEqual(DateTime.MinValue, selectedDummy.ColumnC);
        Assert.AreEqual(i, (int)(selectedDummy.ColumnD * 10));
      }
    }

    /// <summary>Checks if the contents of the two files are equal.</summary>
    /// <param name="templatePath">The path to the templatefile.</param>
    /// <param name="outputPath">The path to the file that was produced by the test.</param>
    private static void CheckTestResult(string templatePath, string outputPath) {
      Assert.IsTrue(File.Exists(outputPath));

      byte[] templateContent = File.ReadAllBytes(templatePath);
      byte[] outputContent = File.ReadAllBytes(outputPath);

      Assert.AreEqual(templateContent.Length, outputContent.Length);

      for(int i = 0; i < templateContent.Length; ++i) {
        Assert.AreEqual(templateContent[i], outputContent[i]);
      }
    }

    /// <summary>Creates the directory if it does not yet exists.</summary>
    /// <param name="outputPath">The path to the outputfile.</param>
    private static void CreateOutputDir(string outputPath) {
      FileInfo fileInfo = new FileInfo(outputPath);
      if(!fileInfo.Directory.Exists) {
        fileInfo.Directory.Create();
      }
    }
    #endregion

    #region Private helper classes
    /// <summary>A basic dummy class to support the testcases.</summary>
    private abstract class TestDummy {
      /// <summary>Gets or sets the value of the first column.</summary>
      public abstract string ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      public abstract int ColumnB { get; set; }

      /// <summary>Gets or sets the value of the fourth column.</summary>
      public abstract float ColumnD { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      public abstract DateTime ColumnC { get; set; }
    }

    /// <summary>A basic dummy class to support the testcases.</summary>
    private abstract class PartialTestDummy {
      /// <summary>Gets or sets the value of the first column.</summary>
      public abstract string ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      public abstract int ColumnB { get; set; }

      /// <summary>Gets or sets the value of the fourth column.</summary>
      public abstract float ColumnD { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      public abstract DateTime ColumnC { get; set; }
    }

    /// <summary>A basic dummy class to support the testcases.</summary>
    [CsvRecord(';', IgnoreHeaderOnRead = true, WriteHeader = true)]
    private class TestDummyWithHeader : TestDummy {
      /// <summary>Gets or sets the value of the first column.</summary>
      [CsvColumn(0)]
      public override string ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      [CsvColumn(1)]
      public override int ColumnB { get; set; }

      /// <summary>Gets or sets the value of the fourth column.</summary>
      [CsvColumn(3, CultureName = "en-US")]
      public override float ColumnD { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      [CsvColumn(2, FormatString = "yyyy-MM-dd HH:mm:ss")]
      public override DateTime ColumnC { get; set; }
    }

    /// <summary>A basic dummy class to support the testcases.</summary>
    [CsvRecord(';', IgnoreHeaderOnRead = true, WriteHeader = true, CultureName = "en-US")]
    private class PartialTestDummyWithHeader : PartialTestDummy {
      /// <summary>Gets or sets the value of the first column.</summary>
      public override string ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      [CsvColumn(1)]
      public override int ColumnB { get; set; }

      /// <summary>Gets or sets the value of the fourth column.</summary>
      [CsvColumn(3)]
      public override float ColumnD { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      public override DateTime ColumnC { get; set; }
    }

    /// <summary>A basic dummy class to support the testcases.</summary>
    [CsvRecord(';', IgnoreHeaderOnRead = false, WriteHeader = false, CultureName = "nl-NL")]
    private class TestDummyNoHeader : TestDummy {
      /// <summary>Gets or sets the value of the first column.</summary>
      [CsvColumn(0)]
      public override string ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      [CsvColumn(1)]
      public override int ColumnB { get; set; }

      /// <summary>Gets or sets the value of the fourth column.</summary>
      [CsvColumn(3, CultureName = "en")]
      public override float ColumnD { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      [CsvColumn(2, FormatString = "yyyy-MM-dd HH:mm:ss")]
      public override DateTime ColumnC { get; set; }
    }

    /// <summary>A basic dummy class to support the testcases.</summary>
    [CsvRecord(';', IgnoreHeaderOnRead = false, WriteHeader = false)]
    private class PartialTestDummyNoHeader : PartialTestDummy {
      /// <summary>Gets or sets the value of the first column.</summary>
      public override string ColumnA { get; set; }

      /// <summary>Gets or sets the value of the second column.</summary>
      [CsvColumn(1)]
      public override int ColumnB { get; set; }

      /// <summary>Gets or sets the value of the fourth column.</summary>
      [CsvColumn(3, CultureName = "en")]
      public override float ColumnD { get; set; }

      /// <summary>Gets or sets the value of the third column.</summary>
      public override DateTime ColumnC { get; set; }
    }
    #endregion
  }
}
