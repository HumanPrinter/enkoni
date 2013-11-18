//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Contains the execution code that compiles regular expressions into a single assembly.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

using Enkoni.Framework.Validation.Properties;

namespace RegexBuilder {
  /// <summary>Contains the main entry point of the executable that constructs the compiled regular expressions.</summary>
  public class Program {
    /// <summary>The main entry point of the executable that constructs the compiled regular expressions.</summary>
    private static void Main(string[] args) {
      /* Construct the default Regex for regular phone numbers */
      IEnumerable<string> areaCodes = Resources.AreaCodes_NL.Split(';');
      IEnumerable<string> shortAreaCodes = areaCodes.Where(code => code.Length == 2);
      IEnumerable<string> longAreaCodes = areaCodes.Where(code => code.Length == 3);
      string shortAreaCodesRegexPart = string.Join("|", shortAreaCodes.ToArray());
      string longAreaCodesRegexPart = string.Join("|", longAreaCodes.ToArray());
      string defaultRegularRegexPattern = string.Format(CultureInfo.InvariantCulture, Resources.RegularRegexPattern_NL, shortAreaCodesRegexPart, longAreaCodesRegexPart);
      string defaultRegularRegexPatternNotInternational = string.Format(CultureInfo.InvariantCulture, Resources.RegularRegexPatternNoCountryAccessCode_NL, shortAreaCodesRegexPart, longAreaCodesRegexPart);

      RegexCompilationInfo defaultRegularRegex_NL = new RegexCompilationInfo(defaultRegularRegexPattern,
                                                    RegexOptions.Singleline,
                                                    "DefaultRegularRegexNetherlands",
                                                    "Enkoni.Framework.Validation.RegularExpressions",
                                                    true);

      RegexCompilationInfo defaultRegularRegexNoCountryAccessCode_NL = new RegexCompilationInfo(defaultRegularRegexPatternNotInternational,
                                                    RegexOptions.Singleline,
                                                    "DefaultRegularRegexNetherlandsNoCountryAccessCode",
                                                    "Enkoni.Framework.Validation.RegularExpressions",
                                                    true);

      /* Construct the Regex for service numbers */
      const string servicenummers0800 = @"(0800-?((((0[0,1,3-8])|(1[0-9])|(4[1,3,6,9])|([2,5-9][0,1]))\d{2})|((0[2,9])|(3[0-9])|(4[0,2,4,5,7,8])|([2,5-9][2-9]))\d{5}))";
      const string servicenummers090x = @"((0900|0906|0909)-?((((0[0-3,5-9])|(1[3-5,7-9])|(8[0,1,3,4,6,8])|(9[2,3,5-8]))\d{2})|((04)|(1[0-2,6])|([2-7][0-9])|(8[2,5,7,9])|(9[0,1,4,9]))\d{5}))";
      
      RegexCompilationInfo serviceRegex_NL = new RegexCompilationInfo(string.Format(CultureInfo.InvariantCulture, "^({0}|{1})$", servicenummers0800, servicenummers090x),
                                                    RegexOptions.Singleline,
                                                    "ServiceRegexNetherlands",
                                                    "Enkoni.Framework.Validation.RegularExpressions",
                                                    true);

      /* Construct the Regex for emergency numbers */
      RegexCompilationInfo emergencyRegex_NL = new RegexCompilationInfo(@"^(112|144|116000|116111|116123)$",
                                                    RegexOptions.Singleline,
                                                    "EmergencyRegexNetherlands",
                                                    "Enkoni.Framework.Validation.RegularExpressions",
                                                    true);

      /* Construct the Regex for mobile numbers */
      RegexCompilationInfo mobileRegex_NL = new RegexCompilationInfo(@"^(((((\+|00)31(\(0\))?6)|(06))-?)|(\(06\)))[1-5,8]\d{7}$",
                                                    RegexOptions.Singleline,
                                                    "MobileRegexNetherlands",
                                                    "Enkoni.Framework.Validation.RegularExpressions",
                                                    true);

      RegexCompilationInfo mobileRegexNoCountryAccessCode_NL = new RegexCompilationInfo(@"^((06-?)|(\(06\)))[1-5,8]\d{7}$",
                                                    RegexOptions.Singleline,
                                                    "MobileRegexNetherlandsNoCountryAccessCode",
                                                    "Enkoni.Framework.Validation.RegularExpressions",
                                                    true);

      /* Construct the Regex for other numbers */
      const string videotexPattern = @"((067281|067284|067364)\d{4})|(067[0,1,4,5,7-9]\d{2})|(0672[0-7][0-9])|067280|(06728[2,3,5-9])|(0673[0-5,7-9]\d)|(06736[0-3,5-9])";
      const string ipBasedPattern = @"06760-?\d{5}";
      const string semaphonePattern = @"066-?\d{7}";
      const string vpnPattern = @"082-?\d+";
      const string personalAssistantServicesPattern = @"(084|087)-?\d{7}";
      const string electronicServicesPattern = @"(0970-?\d{8})|((085|091)-?\d{7})";
      const string providerServicesPattern = @"(120[0-4])|1233|1234|1244|(130[0-9])|(13[3-9][0-9])";
      const string infoServicesPattern = @"(180[0-9])|(181[0-7])";
      const string governmentPattern = @"1400|1451|(140((10|13|15|20|23|24|26|30|33|35|36|38|40|43|45|46|50|53|55|58)|((11|16|17|18|22|25|29|31|32|34|41|47|48|49|51|52|54|56|57|59)\d)))";
      string otherNumbersPattern = string.Format(CultureInfo.InvariantCulture, "^(({0})|({1})|({2})|({3})|({4})|({5})|({6})|({7})|({8}))$", 
        videotexPattern, ipBasedPattern, semaphonePattern, vpnPattern, personalAssistantServicesPattern, electronicServicesPattern, providerServicesPattern, infoServicesPattern, governmentPattern);
      RegexCompilationInfo otherRegex_NL = new RegexCompilationInfo(otherNumbersPattern,
                                                    RegexOptions.Singleline,
                                                    "OtherRegexNetherlands",
                                                    "Enkoni.Framework.Validation.RegularExpressions",
                                                    true);

      RegexCompilationInfo[] regexes = { defaultRegularRegex_NL, defaultRegularRegexNoCountryAccessCode_NL, serviceRegex_NL, emergencyRegex_NL, mobileRegex_NL, mobileRegexNoCountryAccessCode_NL, otherRegex_NL };
      AssemblyName assemblyName = new AssemblyName("Enkoni.Framework.Validation.RegularExpressions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d6265e3de96a22aa");
      Regex.CompileToAssembly(regexes, assemblyName);
    }
  }
}
