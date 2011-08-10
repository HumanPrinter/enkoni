//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="TestCaseHelper.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Contains methods that support the execution of the test cases.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the CsvSerializer class.</summary>
  public static class TestCaseHelper {
    /// <summary>Checks if the contents of the two files are equal.</summary>
    /// <param name="templatePath">The path to the templatefile.</param>
    /// <param name="outputPath">The path to the file that was produced by the test.</param>
    public static void CheckTestResult(string templatePath, string outputPath) {
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
    public static void CreateOutputDir(string outputPath) {
      FileInfo fileInfo = new FileInfo(outputPath);
      if(!fileInfo.Directory.Exists) {
        fileInfo.Directory.Create();
      }
    }
  }
}
