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
      IEnumerable<RegexCompilationInfo> regexes = Enumerable.Empty<RegexCompilationInfo>()
        .Concat(CreatePhoneNumberExpressions())
        .Concat(CreateIbanExpressions())
        .Concat(CreateEmailExpressions());

      AssemblyName assemblyName = new AssemblyName("Enkoni.Framework.Validation.RegularExpressions, Version=2.0.0.0, Culture=neutral, PublicKeyToken=d6265e3de96a22aa");
      Regex.CompileToAssembly(regexes.ToArray(), assemblyName);
    }

    /// <summary>Creates the regular expressions that are used by the DutchPhoneNumberValidator.</summary>
    /// <returns>The constructed regular expressions.</returns>
    private static IEnumerable<RegexCompilationInfo> CreatePhoneNumberExpressions() {
      /* Construct the default Regex for regular phone numbers */
      IEnumerable<string> areaCodes = Resources.AreaCodes_NL.Split(';');
      IEnumerable<string> shortAreaCodes = areaCodes.Where(code => code.Length == 2);
      IEnumerable<string> longAreaCodes = areaCodes.Where(code => code.Length == 3);
      string shortAreaCodesRegexPart = string.Join("|", shortAreaCodes.ToArray());
      string longAreaCodesRegexPart = string.Join("|", longAreaCodes.ToArray());
      string defaultRegularRegexPattern = string.Format(CultureInfo.InvariantCulture, Resources.RegularRegexPattern_NL, shortAreaCodesRegexPart, longAreaCodesRegexPart);
      string defaultRegularRegexPatternNotInternational = string.Format(CultureInfo.InvariantCulture, Resources.RegularRegexPatternNoCountryAccessCode_NL, shortAreaCodesRegexPart, longAreaCodesRegexPart);
      string defaultRegularRegexPatternWithCarrierPreselect = string.Format(CultureInfo.InvariantCulture, Resources.RegularRegexPatternWithCarrierPreselect_NL, shortAreaCodesRegexPart, longAreaCodesRegexPart);
      string defaultRegularRegexPatternNotInternationalWithCarrierPreselect = string.Format(CultureInfo.InvariantCulture, Resources.RegularRegexPatternNoCountryAccessCodeWithCarrierPreselect_NL, shortAreaCodesRegexPart, longAreaCodesRegexPart);

      RegexCompilationInfo defaultRegularRegex_NL = new RegexCompilationInfo(defaultRegularRegexPattern,
                                                    RegexOptions.Singleline,
                                                    "DutchPhoneValidatorDefaultRegularRegex",
                                                    "Enkoni.Framework.Validation.RegularExpressions",
                                                    true);

      RegexCompilationInfo defaultRegularRegexNoCountryAccessCode_NL = new RegexCompilationInfo(defaultRegularRegexPatternNotInternational,
                                                    RegexOptions.Singleline,
                                                    "DutchPhoneValidatorDefaultRegularRegexNoCountryAccessCode",
                                                    "Enkoni.Framework.Validation.RegularExpressions",
                                                    true);

      RegexCompilationInfo defaultRegularRegexWithCP_NL = new RegexCompilationInfo(defaultRegularRegexPatternWithCarrierPreselect,
                                                    RegexOptions.Singleline,
                                                    "DutchPhoneValidatorDefaultRegularRegexWithCarrierPreselect",
                                                    "Enkoni.Framework.Validation.RegularExpressions",
                                                    true);

      RegexCompilationInfo defaultRegularRegexNoCountryAccessCodeWithCP_NL = new RegexCompilationInfo(defaultRegularRegexPatternNotInternationalWithCarrierPreselect,
                                                    RegexOptions.Singleline,
                                                    "DutchPhoneValidatorDefaultRegularRegexNoCountryAccessCodeWithCarrierPreselect",
                                                    "Enkoni.Framework.Validation.RegularExpressions",
                                                    true);

      /* Construct the Regex for service numbers */
      const string servicenummers0800 = @"(0800-?((((0[0,1,3-8])|(1[0-9])|(4[1,3,6,9])|([2,5-9][0,1]))\d{2})|((0[2,9])|(3[0-9])|(4[0,2,4,5,7,8])|([2,5-9][2-9]))\d{5}))";
      const string servicenummers090x = @"((0900|0906|0909)-?((((0[0-3,5-9])|(1[3-5,7-9])|(8[0,1,3,4,6,8])|(9[2,3,5-8]))\d{2})|((04)|(1[0-2,6])|([2-7][0-9])|(8[2,5,7,9])|(9[0,1,4,9]))\d{5}))";

      RegexCompilationInfo serviceRegex_NL = new RegexCompilationInfo(string.Format(CultureInfo.InvariantCulture, "^({0}|{1})$", servicenummers0800, servicenummers090x),
                                                    RegexOptions.Singleline,
                                                    "DutchPhoneValidatorServiceRegex",
                                                    "Enkoni.Framework.Validation.RegularExpressions",
                                                    true);

      /* Construct the Regex for emergency numbers */
      RegexCompilationInfo emergencyRegex_NL = new RegexCompilationInfo(@"^(112|144|116000|116111|116123)$",
                                                    RegexOptions.Singleline,
                                                    "DutchPhoneValidatorEmergencyRegex",
                                                    "Enkoni.Framework.Validation.RegularExpressions",
                                                    true);

      /* Construct the Regex for mobile numbers */
      RegexCompilationInfo mobileRegex_NL = new RegexCompilationInfo(@"^(((((\+|00)31(\(0\))?6)|(06))-?)|(\(06\)))[1-5,8]\d{7}$",
                                                    RegexOptions.Singleline,
                                                    "DutchPhoneValidatorMobileRegex",
                                                    "Enkoni.Framework.Validation.RegularExpressions",
                                                    true);

      RegexCompilationInfo mobileRegexWithCP_NL = new RegexCompilationInfo(@"^(((((\+|(((16\d{2})|(10[0-5,7-9]\d{2}))?)00)31(\(0\))?6)|((((16\d{2})|(10[0-5,7-9]\d{2}))?)06))-?)|((((16\d{2})|(10[0-5,7-9]\d{2}))?)\(06\)))[1-5,8]\d{7}$",
                                                    RegexOptions.Singleline,
                                                    "DutchPhoneValidatorMobileRegexWithCarrierPreselect",
                                                    "Enkoni.Framework.Validation.RegularExpressions",
                                                    true);

      RegexCompilationInfo mobileRegexNoCountryAccessCode_NL = new RegexCompilationInfo(@"^((06-?)|(\(06\)))[1-5,8]\d{7}$",
                                                    RegexOptions.Singleline,
                                                    "DutchPhoneValidatorMobileRegexNoCountryAccessCode",
                                                    "Enkoni.Framework.Validation.RegularExpressions",
                                                    true);

      RegexCompilationInfo mobileRegexNoCountryAccessCodeWithCP_NL = new RegexCompilationInfo(@"^(((((16\d{2})|(10[0-5,7-9]\d{2}))?)06-?)|((((16\d{2})|(10[0-5,7-9]\d{2}))?)\(06\)))[1-5,8]\d{7}$",
                                                    RegexOptions.Singleline,
                                                    "DutchPhoneValidatorMobileRegexNoCountryAccessCodeWithCarrierPreselect",
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
      string otherNumbersPatternWithCarrierPreselect = string.Format(CultureInfo.InvariantCulture, @"^((16\d{{2}})|(10[0-5,7-9]\d{{2}}))?(({0})|({1})|({2})|({3})|({4})|({5})|({6})|({7})|({8}))$",
        videotexPattern, ipBasedPattern, semaphonePattern, vpnPattern, personalAssistantServicesPattern, electronicServicesPattern, providerServicesPattern, infoServicesPattern, governmentPattern);

      RegexCompilationInfo otherRegex_NL = new RegexCompilationInfo(otherNumbersPattern,
                                                    RegexOptions.Singleline,
                                                    "DutchPhoneValidatorOtherRegex",
                                                    "Enkoni.Framework.Validation.RegularExpressions",
                                                    true);

      RegexCompilationInfo otherRegexWithCP_NL = new RegexCompilationInfo(otherNumbersPatternWithCarrierPreselect,
                                                    RegexOptions.Singleline,
                                                    "DutchPhoneValidatorOtherRegexWithCarrierPreselect",
                                                    "Enkoni.Framework.Validation.RegularExpressions",
                                                    true);


      RegexCompilationInfo[] regexes = 
      { 
        defaultRegularRegex_NL, defaultRegularRegexNoCountryAccessCode_NL, defaultRegularRegexWithCP_NL, defaultRegularRegexNoCountryAccessCodeWithCP_NL, 
        serviceRegex_NL, emergencyRegex_NL, mobileRegex_NL, mobileRegexNoCountryAccessCode_NL, mobileRegexWithCP_NL, mobileRegexNoCountryAccessCodeWithCP_NL, 
        otherRegex_NL, otherRegexWithCP_NL
      };

      return regexes;
    }

    /// <summary>Creates the regular expressions that are used by the IbanValidator.</summary>
    /// <returns>The constructed regular expressions.</returns>
    private static IEnumerable<RegexCompilationInfo> CreateIbanExpressions() {
      RegexCompilationInfo ibanRegex = new RegexCompilationInfo(@"^(?<countrycode>[A-Z]{2})(?<check_digits>\d{2})(?<accountid>[A-Z0-9]{1,30})$",
                                                    RegexOptions.Singleline,
                                                    "IbanValidatorRegex",
                                                    "Enkoni.Framework.Validation.RegularExpressions",
                                                    true);
      return new RegexCompilationInfo[] { ibanRegex };
    }

    /// <summary>Creates the regular expressions that are used by the EmailValidator.</summary>
    /// <returns>The constructed regular expressions.</returns>
    private static IEnumerable<RegexCompilationInfo> CreateEmailExpressions() {
      string commentPattern = @"(?<comment>(\(.*\))?)";

      /* Construct the regex for the domain part */
      /* ... for IP addresses */
      string ipAddressPattern = @"(?<ipAddress>\[.*\])";
      /* ... for domain names */
      string hostNamePattern = @"(?<domain>(([a-zA-Z0-9]{1,63})|([a-zA-Z0-9][a-zA-Z0-9\-]{1,61}[a-zA-Z0-9]))(\.(([a-zA-Z0-9]{1,63})|([a-zA-Z0-9][a-zA-Z0-9\-]{1,61}[a-zA-Z0-9])))*)";
      string domainPartPattern = string.Format(CultureInfo.InvariantCulture, "^{0}({1}|{2}){0}$", commentPattern, hostNamePattern, ipAddressPattern);

      RegexCompilationInfo domainPartRegex = new RegexCompilationInfo(domainPartPattern, 
        RegexOptions.Singleline, 
        "EmailValidatorDomainPartRegex", 
        "Enkoni.Framework.Validation.RegularExpressions", 
        true);

      /* Construct the regexes foor the local part */
      string allowedCharactersBasic = @"a-zA-Z0-9\-_";
      string localPartBasicPattern = string.Format(CultureInfo.InvariantCulture, @"^{0}[{1}]+(\.?[{1}])*{0}$", commentPattern, allowedCharactersBasic);
      RegexCompilationInfo localPartBasicRegex = new RegexCompilationInfo(localPartBasicPattern,
        RegexOptions.Singleline,
        "EmailValidatorLocalPartBasicRegex",
        "Enkoni.Framework.Validation.RegularExpressions",
        true);

      string allowedCharactersExtended = allowedCharactersBasic + @"!#$%&'\*\+/=\?^`\{\|\}~";
      string localPartExtendedPattern = string.Format(CultureInfo.InvariantCulture, @"^{0}[{1}]+(\.?[{1}])*{0}$", commentPattern, allowedCharactersExtended);
      RegexCompilationInfo localPartExtendedRegex = new RegexCompilationInfo(localPartExtendedPattern,
        RegexOptions.Singleline,
        "EmailValidatorLocalPartExtendedRegex",
        "Enkoni.Framework.Validation.RegularExpressions",
        true);

      string quotedPattern = string.Format(CultureInfo.InvariantCulture, "\"([{0}\\. \\(\\),:;\\<\\>@\\[\\]]*|(\\\")*|(\\\\\\\\)*)*\"", allowedCharactersExtended);
      string localPartCompletePattern = string.Format(CultureInfo.InvariantCulture, @"^{0}(({1})|([{2}]+(\.(([{2}]+)|({1})\.[{2}]+))*)){0}$", commentPattern, quotedPattern, allowedCharactersExtended);
      RegexCompilationInfo localPartCompleteRegex = new RegexCompilationInfo(localPartCompletePattern,
        RegexOptions.Singleline,
        "EmailValidatorLocalPartCompleteRegex",
        "Enkoni.Framework.Validation.RegularExpressions",
        true);

      RegexCompilationInfo[] regexes =  { domainPartRegex, localPartBasicRegex, localPartExtendedRegex, localPartCompleteRegex };

      return regexes;
    }
  }
}
