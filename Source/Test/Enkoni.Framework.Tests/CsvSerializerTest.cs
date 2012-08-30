//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvSerializerTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
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
using System.Threading;

using Enkoni.Framework.Serialization;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the CsvSerializer class.</summary>
  [TestClass]
  public class CsvSerializerTest {
    #region Instance variables
    /// <summary>A helper variable to verify the functionality of the asynchronous methods.</summary>
    private Guid expectedObjectState;
    #endregion

    #region Deserialize testcases
    /// <summary>Tests the functionality of the <see cref="Serializer{T}.Deserialize(string)"/> and <see cref="Serializer{T}.Deserialize(Stream)"/> 
    /// method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestInputFileWithHeaderUTF8.csv", @"CsvSerializerTest\TestCase01")]
    public void TestCase01_Deserialize_Complete_DefaultEncoding() {
      string inputPath = @"CsvSerializerTest\TestCase01\CsvTestInputFileWithHeaderUTF8.csv";

      Serializer<TestDummyWithHeader> serializer = new CsvSerializer<TestDummyWithHeader>();
      /* Test synchronous deserialization using a file */
      IEnumerable<TestDummyWithHeader> result = serializer.Deserialize(inputPath);
      CheckTestResult(result);

      /* Test synchronous deserialization using a stream */
      using(FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read)) {
        result = serializer.Deserialize(stream);
      }

      CheckTestResult(result);
    }

    /// <summary>Tests the functionality of the <see cref="Serializer{T}.Deserialize(string)"/> and <see cref="Serializer{T}.Deserialize(Stream)"/> 
    /// method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestInputFileNoHeaderUTF8.csv", @"CsvSerializerTest\TestCase02")]
    public void TestCase02_Deserialize_Complete_DefaultEncoding_NoHeader() {
      string inputPath = @"CsvSerializerTest\TestCase02\CsvTestInputFileNoHeaderUTF8.csv";

      Serializer<TestDummyNoHeader> serializer = new CsvSerializer<TestDummyNoHeader>();
      IEnumerable<TestDummyNoHeader> result = serializer.Deserialize(inputPath);

      CheckTestResult(result);

      using(FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read)) {
        result = serializer.Deserialize(stream);
      }

      CheckTestResult(result);
    }

    /// <summary>Tests the functionality of the <see cref="Serializer{T}.Deserialize(string, Encoding)"/> and 
    /// <see cref="Serializer{T}.Deserialize(Stream, Encoding)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestInputFileWithHeaderASCII.csv", @"CsvSerializerTest\TestCase03")]
    public void TestCase03_Deserialize_Complete_CustomEncoding() {
      string inputPath = @"CsvSerializerTest\TestCase03\CsvTestInputFileWithHeaderASCII.csv";

      Serializer<TestDummyWithHeader> serializer = new CsvSerializer<TestDummyWithHeader>();
      IEnumerable<TestDummyWithHeader> result = serializer.Deserialize(inputPath, Encoding.ASCII);

      CheckTestResult(result);

      using(FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read)) {
        result = serializer.Deserialize(stream, Encoding.ASCII);
      }

      CheckTestResult(result);
    }

    /// <summary>Tests the functionality of the <see cref="Serializer{T}.Deserialize(string, Encoding)"/> and 
    /// <see cref="Serializer{T}.Deserialize(Stream, Encoding)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestInputFileNoHeaderASCII.csv", @"CsvSerializerTest\TestCase04")]
    public void TestCase04_Deserialize_Complete_CustomEncoding_NoHeader() {
      string inputPath = @"CsvSerializerTest\TestCase04\CsvTestInputFileNoHeaderASCII.csv";

      Serializer<TestDummyNoHeader> serializer = new CsvSerializer<TestDummyNoHeader>();
      IEnumerable<TestDummyNoHeader> result = serializer.Deserialize(inputPath, Encoding.ASCII);

      CheckTestResult(result);

      using(FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read)) {
        result = serializer.Deserialize(stream, Encoding.ASCII);
      }

      CheckTestResult(result);
    }

    /// <summary>Tests the functionality of the <see cref="Serializer{T}.Deserialize(string)"/> and <see cref="Serializer{T}.Deserialize(Stream)"/> 
    /// method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestInputFileWithHeaderUTF8.csv", @"CsvSerializerTest\TestCase05")]
    public void TestCase05_Deserialize_Partial_DefaultEncoding() {
      string inputPath = @"CsvSerializerTest\TestCase05\CsvTestInputFileWithHeaderUTF8.csv";

      Serializer<PartialTestDummyWithHeader> serializer = new CsvSerializer<PartialTestDummyWithHeader>();
      IEnumerable<PartialTestDummyWithHeader> result = serializer.Deserialize(inputPath);

      CheckTestResult(result);

      using(FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read)) {
        result = serializer.Deserialize(stream);
      }

      CheckTestResult(result);
    }

    /// <summary>Tests the functionality of the <see cref="Serializer{T}.Deserialize(string)"/> and <see cref="Serializer{T}.Deserialize(Stream)"/> 
    /// method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestInputFileNoHeaderUTF8.csv", @"CsvSerializerTest\TestCase06")]
    public void TestCase06_Deserialize_Partial_DefaultEncoding_NoHeader() {
      string inputPath = @"CsvSerializerTest\TestCase06\CsvTestInputFileNoHeaderUTF8.csv";

      Serializer<PartialTestDummyNoHeader> serializer = new CsvSerializer<PartialTestDummyNoHeader>();
      IEnumerable<PartialTestDummyNoHeader> result = serializer.Deserialize(inputPath);

      CheckTestResult(result);

      using(FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read)) {
        result = serializer.Deserialize(stream);
      }

      CheckTestResult(result);
    }

    /// <summary>Tests the functionality of the <see cref="Serializer{T}.Deserialize(string, Encoding)"/> and 
    /// <see cref="Serializer{T}.Deserialize(Stream, Encoding)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestInputFileWithHeaderASCII.csv", @"CsvSerializerTest\TestCase07")]
    public void TestCase07_Deserialize_Partial_CustomEncoding() {
      string inputPath = @"CsvSerializerTest\TestCase07\CsvTestInputFileWithHeaderASCII.csv";

      Serializer<PartialTestDummyWithHeader> serializer = new CsvSerializer<PartialTestDummyWithHeader>();
      IEnumerable<PartialTestDummyWithHeader> result = serializer.Deserialize(inputPath, Encoding.ASCII);

      CheckTestResult(result);

      using(FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read)) {
        result = serializer.Deserialize(stream, Encoding.ASCII);
      }

      CheckTestResult(result);
    }

    /// <summary>Tests the functionality of the <see cref="Serializer{T}.Deserialize(string, Encoding)"/> and
    /// <see cref="Serializer{T}.Deserialize(Stream, Encoding)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestInputFileNoHeaderASCII.csv", @"CsvSerializerTest\TestCase08")]
    public void TestCase08_Deserialize_Partial_CustomEncoding_NoHeader() {
      string inputPath = @"CsvSerializerTest\TestCase08\CsvTestInputFileNoHeaderASCII.csv";

      Serializer<PartialTestDummyNoHeader> serializer = new CsvSerializer<PartialTestDummyNoHeader>();
      IEnumerable<PartialTestDummyNoHeader> result = serializer.Deserialize(inputPath, Encoding.ASCII);

      CheckTestResult(result);

      using(FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read)) {
        result = serializer.Deserialize(stream, Encoding.ASCII);
      }

      CheckTestResult(result);
    }
    #endregion

    #region Serialize testcases
    /// <summary>Tests the functionality of the <see cref="Serializer{T}.Serialize(IEnumerable{T},string)"/> and
    /// <see cref="Serializer{T}.Serialize(IEnumerable{T},Stream)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestOutputFileWithHeaderUTF8.csv", @"CsvSerializerTest\TestCase09")]
    public void TestCase09_Serialize_Complete_DefaultEncoding() {
      string templatePath = @"CsvSerializerTest\TestCase09\CsvTestOutputFileWithHeaderUTF8.csv";
      string outputPath = @"CsvSerializerTest\TestCase09\CsvTestOutputFile.csv";
      TestCaseHelper.CreateOutputDir(outputPath);

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

      Serializer<TestDummyWithHeader> serializer = new CsvSerializer<TestDummyWithHeader>();
      serializer.Serialize(collection, outputPath);

      TestCaseHelper.CheckTestResult(templatePath, outputPath);

      using(FileStream stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write)) {
        StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
        /* Write an empty string to enforce the output of the encoding bytes. */
        writer.Write(string.Empty);
        writer.Flush();

        serializer.Serialize(collection, stream);
      }

      TestCaseHelper.CheckTestResult(templatePath, outputPath);
    }

    /// <summary>Tests the functionality of the <see cref="Serializer{T}.Serialize(IEnumerable{T},string)"/> and 
    /// <see cref="Serializer{T}.Serialize(IEnumerable{T},Stream)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestOutputFileNoHeaderUTF8.csv", @"CsvSerializerTest\TestCase10")]
    public void TestCase10_Serialize_Complete_DefaultEncoding_NoHeader() {
      string templatePath = @"CsvSerializerTest\TestCase10\CsvTestOutputFileNoHeaderUTF8.csv";
      string outputPath = @"CsvSerializerTest\TestCase10\CsvTestOutputFile.csv";
      TestCaseHelper.CreateOutputDir(outputPath);

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

      Serializer<TestDummyNoHeader> serializer = new CsvSerializer<TestDummyNoHeader>();
      serializer.Serialize(collection, outputPath);

      TestCaseHelper.CheckTestResult(templatePath, outputPath);

      using(FileStream stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write)) {
        StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
        /* Write an empty string to enforce the output of the encoding bytes. */
        writer.Write(string.Empty);
        writer.Flush();

        serializer.Serialize(collection, stream);
      }

      TestCaseHelper.CheckTestResult(templatePath, outputPath);
    }

    /// <summary>Tests the functionality of the <see cref="Serializer{T}.Serialize(IEnumerable{T},string, Encoding)"/> and
    /// <see cref="Serializer{T}.Serialize(IEnumerable{T},Stream, Encoding)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestOutputFileWithHeaderASCII.csv", @"CsvSerializerTest\TestCase11")]
    public void TestCase11_Serialize_Complete_CustomEncoding() {
      string templatePath = @"CsvSerializerTest\TestCase11\CsvTestOutputFileWithHeaderASCII.csv";
      string outputPath = @"CsvSerializerTest\TestCase11\CsvTestOutputFile.csv";
      TestCaseHelper.CreateOutputDir(outputPath);

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

      Serializer<TestDummyWithHeader> serializer = new CsvSerializer<TestDummyWithHeader>();
      serializer.Serialize(collection, outputPath, Encoding.ASCII);

      TestCaseHelper.CheckTestResult(templatePath, outputPath);

      using(FileStream stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write)) {
        StreamWriter writer = new StreamWriter(stream, Encoding.ASCII);
        /* Write an empty string to enforce the output of the encoding bytes. */
        writer.Write(string.Empty);
        writer.Flush();

        serializer.Serialize(collection, stream, Encoding.ASCII);
      }

      TestCaseHelper.CheckTestResult(templatePath, outputPath);
    }

    /// <summary>Tests the functionality of the <see cref="Serializer{T}.Serialize(IEnumerable{T},string, Encoding)"/> and
    /// <see cref="Serializer{T}.Serialize(IEnumerable{T},Stream, Encoding)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestOutputFileNoHeaderASCII.csv", @"CsvSerializerTest\TestCase12")]
    public void TestCase12_Serialize_Complete_CustomEncoding_NoHeader() {
      string templatePath = @"CsvSerializerTest\TestCase12\CsvTestOutputFileNoHeaderASCII.csv";
      string outputPath = @"CsvSerializerTest\TestCase12\CsvTestOutputFile.csv";
      TestCaseHelper.CreateOutputDir(outputPath);

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

      Serializer<TestDummyNoHeader> serializer = new CsvSerializer<TestDummyNoHeader>();
      serializer.Serialize(collection, outputPath, Encoding.ASCII);

      TestCaseHelper.CheckTestResult(templatePath, outputPath);

      using(FileStream stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write)) {
        StreamWriter writer = new StreamWriter(stream, Encoding.ASCII);
        /* Write an empty string to enforce the output of the encoding bytes. */
        writer.Write(string.Empty);
        writer.Flush();

        serializer.Serialize(collection, stream, Encoding.ASCII);
      }

      TestCaseHelper.CheckTestResult(templatePath, outputPath);
    }

    /// <summary>Tests the functionality of the <see cref="Serializer{T}.Serialize(IEnumerable{T},string)"/> and
    /// <see cref="Serializer{T}.Serialize(IEnumerable{T},Stream)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestOutputFilePartialWithHeaderUTF8.csv", @"CsvSerializerTest\TestCase13")]
    public void TestCase13_Serialize_Partial_DefaultEncoding() {
      string templatePath = @"CsvSerializerTest\TestCase13\CsvTestOutputFilePartialWithHeaderUTF8.csv";
      string outputPath = @"CsvSerializerTest\TestCase13\CsvTestOutputFile.csv";
      TestCaseHelper.CreateOutputDir(outputPath);

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

      Serializer<PartialTestDummyWithHeader> serializer = new CsvSerializer<PartialTestDummyWithHeader>();
      serializer.Serialize(collection, outputPath);

      TestCaseHelper.CheckTestResult(templatePath, outputPath);

      using(FileStream stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write)) {
        StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
        /* Write an empty string to enforce the output of the encoding bytes. */
        writer.Write(string.Empty);
        writer.Flush();

        serializer.Serialize(collection, stream);
      }

      TestCaseHelper.CheckTestResult(templatePath, outputPath);
    }

    /// <summary>Tests the functionality of the <see cref="Serializer{T}.Serialize(IEnumerable{T},string)"/> and
    /// <see cref="Serializer{T}.Serialize(IEnumerable{T},Stream)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestOutputFilePartialNoHeaderUTF8.csv", @"CsvSerializerTest\TestCase14")]
    public void TestCase14_Serialize_Partial_DefaultEncoding_NoHeader() {
      string templatePath = @"CsvSerializerTest\TestCase14\CsvTestOutputFilePartialNoHeaderUTF8.csv";
      string outputPath = @"CsvSerializerTest\TestCase14\CsvTestOutputFile.csv";
      TestCaseHelper.CreateOutputDir(outputPath);

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

      Serializer<PartialTestDummyNoHeader> serializer = new CsvSerializer<PartialTestDummyNoHeader>();
      serializer.Serialize(collection, outputPath);

      TestCaseHelper.CheckTestResult(templatePath, outputPath);

      using(FileStream stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write)) {
        StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
        /* Write an empty string to enforce the output of the encoding bytes. */
        writer.Write(string.Empty);
        writer.Flush();

        serializer.Serialize(collection, stream);
      }

      TestCaseHelper.CheckTestResult(templatePath, outputPath);
    }

    /// <summary>Tests the functionality of the <see cref="Serializer{T}.Serialize(IEnumerable{T},string, Encoding)"/> and
    /// <see cref="Serializer{T}.Serialize(IEnumerable{T},Stream, Encoding)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestOutputFilePartialWithHeaderASCII.csv", @"CsvSerializerTest\TestCase15")]
    public void TestCase15_Serialize_Partial_CustomEncoding() {
      string templatePath = @"CsvSerializerTest\TestCase15\CsvTestOutputFilePartialWithHeaderASCII.csv";
      string outputPath = @"CsvSerializerTest\TestCase15\CsvTestOutputFile.csv";
      TestCaseHelper.CreateOutputDir(outputPath);

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

      Serializer<PartialTestDummyWithHeader> serializer = new CsvSerializer<PartialTestDummyWithHeader>();
      serializer.Serialize(collection, outputPath, Encoding.ASCII);

      TestCaseHelper.CheckTestResult(templatePath, outputPath);

      using(FileStream stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write)) {
        StreamWriter writer = new StreamWriter(stream, Encoding.ASCII);
        /* Write an empty string to enforce the output of the encoding bytes. */
        writer.Write(string.Empty);
        writer.Flush();

        serializer.Serialize(collection, stream, Encoding.ASCII);
      }

      TestCaseHelper.CheckTestResult(templatePath, outputPath);
    }

    /// <summary>Tests the functionality of the <see cref="Serializer{T}.Serialize(IEnumerable{T},string, Encoding)"/> and
    /// <see cref="Serializer{T}.Serialize(IEnumerable{T},Stream, Encoding)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestOutputFilePartialNoHeaderASCII.csv", @"CsvSerializerTest\TestCase16")]
    public void TestCase16_Serialize_Partial_CustomEncoding_NoHeader() {
      string templatePath = @"CsvSerializerTest\TestCase16\CsvTestOutputFilePartialNoHeaderASCII.csv";
      string outputPath = @"CsvSerializerTest\TestCase16\CsvTestOutputFile.csv";
      TestCaseHelper.CreateOutputDir(outputPath);

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

      Serializer<PartialTestDummyNoHeader> serializer = new CsvSerializer<PartialTestDummyNoHeader>();
      serializer.Serialize(collection, outputPath, Encoding.ASCII);

      TestCaseHelper.CheckTestResult(templatePath, outputPath);

      using(FileStream stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write)) {
        StreamWriter writer = new StreamWriter(stream, Encoding.ASCII);
        /* Write an empty string to enforce the output of the encoding bytes. */
        writer.Write(string.Empty);
        writer.Flush();

        serializer.Serialize(collection, stream, Encoding.ASCII);
      }

      TestCaseHelper.CheckTestResult(templatePath, outputPath);
    }
    #endregion

    #region Asynchronous testcases
    /// <summary>Tests the functionality of the <see cref="Serializer{T}.BeginDeserialize(string, AsyncCallback, object)"/> and 
    /// <see cref="Serializer{T}.BeginDeserialize(Stream, AsyncCallback, object)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestInputFileWithHeaderUTF8.csv", @"CsvSerializerTest\TestCase17")]
    public void TestCase17_AsyncDeserialize_DefaultEncoding() {
      string inputPath = @"CsvSerializerTest\TestCase17\CsvTestInputFileWithHeaderUTF8.csv";

      Serializer<TestDummyWithHeader> serializer = new CsvSerializer<TestDummyWithHeader>();

      /* Test asynchronous deserialization using a file and no callback */
      Guid objectState = Guid.NewGuid();
      IAsyncResult asyncResult = serializer.BeginDeserialize(inputPath, null, objectState);
      Assert.AreEqual(objectState, asyncResult.AsyncState);
      IEnumerable<TestDummyWithHeader> result = serializer.EndDeserialize(asyncResult);
      CheckTestResult(result);

      /* Test asynchronous deserialization using a file and a callback */
      this.expectedObjectState = Guid.NewGuid();
      asyncResult = serializer.BeginDeserialize(inputPath, this.AsyncCallback, this.expectedObjectState);
      Assert.AreEqual(this.expectedObjectState, asyncResult.AsyncState);
      while(!asyncResult.IsCompleted) {
        Thread.Yield();
      }

      result = serializer.EndDeserialize(asyncResult);
      CheckTestResult(result);

      /* Test asynchronous deserialization using a stream and no callback */
      using(FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read)) {
        objectState = Guid.NewGuid();
        asyncResult = serializer.BeginDeserialize(stream, null, objectState);
        Assert.AreEqual(objectState, asyncResult.AsyncState);
        result = serializer.EndDeserialize(asyncResult);
      }

      CheckTestResult(result);

      /* Test asynchronous deserialization using a stream and a callback */
      using(FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read)) {
        this.expectedObjectState = Guid.NewGuid();
        asyncResult = serializer.BeginDeserialize(stream, this.AsyncCallback, this.expectedObjectState);
        Assert.AreEqual(this.expectedObjectState, asyncResult.AsyncState);
        while(!asyncResult.IsCompleted) {
          Thread.Yield();
        }

        result = serializer.EndDeserialize(asyncResult);
      }

      CheckTestResult(result);
    }

    /// <summary>Tests the functionality of the <see cref="Serializer{T}.BeginDeserialize(string, Encoding, AsyncCallback, object)"/> and 
    /// <see cref="Serializer{T}.BeginDeserialize(Stream, Encoding, AsyncCallback, object)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestInputFileWithHeaderASCII.csv", @"CsvSerializerTest\TestCase18")]
    public void TestCase18_AsyncDeserialize_CustomEncoding() {
      string inputPath = @"CsvSerializerTest\TestCase18\CsvTestInputFileWithHeaderASCII.csv";

      Serializer<TestDummyWithHeader> serializer = new CsvSerializer<TestDummyWithHeader>();
      /* Test asynchronous deserialization using a file and no callback */
      Guid objectState = Guid.NewGuid();
      IAsyncResult asyncResult = serializer.BeginDeserialize(inputPath, Encoding.ASCII, null, objectState);
      Assert.AreEqual(objectState, asyncResult.AsyncState);
      IEnumerable<TestDummyWithHeader> result = serializer.EndDeserialize(asyncResult);
      CheckTestResult(result);

      /* Test asynchronous deserialization using a file and a callback */
      this.expectedObjectState = Guid.NewGuid();
      asyncResult = serializer.BeginDeserialize(inputPath, Encoding.ASCII, this.AsyncCallback, this.expectedObjectState);
      Assert.AreEqual(this.expectedObjectState, asyncResult.AsyncState);
      while(!asyncResult.IsCompleted) {
        Thread.Yield();
      }

      result = serializer.EndDeserialize(asyncResult);
      CheckTestResult(result);

      /* Test asynchronous deserialization using a stream and no callback */
      using(FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read)) {
        objectState = Guid.NewGuid();
        asyncResult = serializer.BeginDeserialize(inputPath, Encoding.ASCII, null, objectState);
        Assert.AreEqual(objectState, asyncResult.AsyncState);
        result = serializer.EndDeserialize(asyncResult);
      }

      CheckTestResult(result);

      /* Test asynchronous deserialization using a file and a callback */
      using(FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read)) {
        this.expectedObjectState = Guid.NewGuid();
        asyncResult = serializer.BeginDeserialize(inputPath, Encoding.ASCII, this.AsyncCallback, this.expectedObjectState);
        Assert.AreEqual(this.expectedObjectState, asyncResult.AsyncState);
        while(!asyncResult.IsCompleted) {
          Thread.Yield();
        }

        result = serializer.EndDeserialize(asyncResult);
      }

      CheckTestResult(result);
    }

    /// <summary>Tests the functionality of the <see cref="Serializer{T}.BeginSerialize(IEnumerable{T},string, AsyncCallback, object)"/> and
    /// <see cref="Serializer{T}.BeginSerialize(IEnumerable{T},Stream, AsyncCallback, object)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestOutputFileWithHeaderUTF8.csv", @"CsvSerializerTest\TestCase19")]
    public void TestCase19_AsyncSerialize_DefaultEncoding() {
      string templatePath = @"CsvSerializerTest\TestCase19\CsvTestOutputFileWithHeaderUTF8.csv";
      string outputPath = @"CsvSerializerTest\TestCase19\CsvTestOutputFile.csv";
      TestCaseHelper.CreateOutputDir(outputPath);

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

      Serializer<TestDummyWithHeader> serializer = new CsvSerializer<TestDummyWithHeader>();
      /* Test asynchronous serialization using a file and no callback */
      Guid objectState = Guid.NewGuid();
      IAsyncResult asyncResult = serializer.BeginSerialize(collection, outputPath, null, objectState);
      Assert.AreEqual(objectState, asyncResult.AsyncState);
      int result = serializer.EndSerialize(asyncResult);
      TestCaseHelper.CheckTestResult(templatePath, outputPath);

      /* Test asynchronous serialization using a file and a callback */
      this.expectedObjectState = Guid.NewGuid();
      asyncResult = serializer.BeginSerialize(collection, outputPath, this.AsyncCallback, this.expectedObjectState);
      Assert.AreEqual(this.expectedObjectState, asyncResult.AsyncState);
      while(!asyncResult.IsCompleted) {
        Thread.Yield();
      }

      result = serializer.EndSerialize(asyncResult);
      TestCaseHelper.CheckTestResult(templatePath, outputPath);

      /* Test asynchronous serialization using a stream and no callback */
      using(FileStream stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write)) {
        /* Write an empty string to enforce the output of the encoding bytes. */
        StreamWriter writer = new StreamWriter(stream, serializer.DefaultEncoding);
        writer.Write(string.Empty);
        writer.Flush();

        objectState = Guid.NewGuid();
        asyncResult = serializer.BeginSerialize(collection, stream, null, objectState);
        Assert.AreEqual(objectState, asyncResult.AsyncState);
        result = serializer.EndSerialize(asyncResult);
      }

      TestCaseHelper.CheckTestResult(templatePath, outputPath);

      /* Test asynchronous serialization using a stream and a callback */
      using(FileStream stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write)) {
        /* Write an empty string to enforce the output of the encoding bytes. */
        StreamWriter writer = new StreamWriter(stream, serializer.DefaultEncoding);
        writer.Write(string.Empty);
        writer.Flush();

        this.expectedObjectState = Guid.NewGuid();
        asyncResult = serializer.BeginSerialize(collection, stream, this.AsyncCallback, this.expectedObjectState);
        Assert.AreEqual(this.expectedObjectState, asyncResult.AsyncState);
        while(!asyncResult.IsCompleted) {
          Thread.Yield();
        }

        result = serializer.EndSerialize(asyncResult);
      }

      TestCaseHelper.CheckTestResult(templatePath, outputPath);
    }

    /// <summary>Tests the functionality of the <see cref="Serializer{T}.BeginSerialize(IEnumerable{T},string, Encoding, AsyncCallback, object)"/> 
    /// and <see cref="Serializer{T}.BeginSerialize(IEnumerable{T},Stream, Encoding, AsyncCallback, object)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Tests\TestData\CsvTestOutputFileWithHeaderASCII.csv", @"CsvSerializerTest\TestCase20")]
    public void TestCase20_AsyncSerialize_CustomEncoding() {
      string templatePath = @"CsvSerializerTest\TestCase20\CsvTestOutputFileWithHeaderASCII.csv";
      string outputPath = @"CsvSerializerTest\TestCase20\CsvTestOutputFile.csv";
      TestCaseHelper.CreateOutputDir(outputPath);

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

      Serializer<TestDummyWithHeader> serializer = new CsvSerializer<TestDummyWithHeader>();
      /* Test asynchronous serialization using a file and no callback */
      Guid objectState = Guid.NewGuid();
      IAsyncResult asyncResult = serializer.BeginSerialize(collection, outputPath, Encoding.ASCII, null, objectState);
      Assert.AreEqual(objectState, asyncResult.AsyncState);
      int result = serializer.EndSerialize(asyncResult);
      TestCaseHelper.CheckTestResult(templatePath, outputPath);

      /* Test asynchronous serialization using a file and a callback */
      this.expectedObjectState = Guid.NewGuid();
      asyncResult = serializer.BeginSerialize(collection, outputPath, Encoding.ASCII, this.AsyncCallback, this.expectedObjectState);
      Assert.AreEqual(this.expectedObjectState, asyncResult.AsyncState);
      while(!asyncResult.IsCompleted) {
        Thread.Yield();
      }

      result = serializer.EndSerialize(asyncResult);
      TestCaseHelper.CheckTestResult(templatePath, outputPath);

      /* Test asynchronous serialization using a stream and no callback */
      using(FileStream stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write)) {
        /* Write an empty string to enforce the output of the encoding bytes. */
        StreamWriter writer = new StreamWriter(stream, Encoding.ASCII);
        writer.Write(string.Empty);
        writer.Flush();

        objectState = Guid.NewGuid();
        asyncResult = serializer.BeginSerialize(collection, stream, Encoding.ASCII, null, objectState);
        Assert.AreEqual(objectState, asyncResult.AsyncState);
        result = serializer.EndSerialize(asyncResult);
      }

      TestCaseHelper.CheckTestResult(templatePath, outputPath);

      /* Test asynchronous serialization using a stream and a callback */
      using(FileStream stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write)) {
        /* Write an empty string to enforce the output of the encoding bytes. */
        StreamWriter writer = new StreamWriter(stream, Encoding.ASCII);
        writer.Write(string.Empty);
        writer.Flush();

        this.expectedObjectState = Guid.NewGuid();
        asyncResult = serializer.BeginSerialize(collection, stream, Encoding.ASCII, this.AsyncCallback, this.expectedObjectState);
        Assert.AreEqual(this.expectedObjectState, asyncResult.AsyncState);
        while(!asyncResult.IsCompleted) {
          Thread.Yield();
        }

        result = serializer.EndSerialize(asyncResult);
      }

      TestCaseHelper.CheckTestResult(templatePath, outputPath);
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

    /// <summary>Serves as a callback method to test the functionality of the asynchronous methods.</summary>
    /// <param name="asyncResult">The async result that references the asynchronous operation.</param>
    private void AsyncCallback(IAsyncResult asyncResult) {
      Assert.AreEqual(this.expectedObjectState, asyncResult.AsyncState);
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
