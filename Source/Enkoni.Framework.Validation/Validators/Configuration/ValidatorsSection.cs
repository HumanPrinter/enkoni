using System.Configuration;
using System.Xml;

namespace Enkoni.Framework.Validation.Validators.Configuration {
  /// <summary>Defines the configuration section that can be used to configure the Enkoni validators.</summary>
  public class ValidatorsSection : ConfigurationSection {
    #region Constants

    /// <summary>The default name for the ConfigurationSection in the config file.</summary>
    public const string DefaultSectionName = "Enkoni.Validators";

    #endregion

    #region Constructor

    /// <summary>Initializes a new instance of the <see cref="ValidatorsSection"/> class.</summary>
    public ValidatorsSection() {
      this.DutchPhoneNumberValidators = new DutchPhoneNumberValidatorConfigDictionary();
      this.EmailValidators = new EmailValidatorConfigDictionary();
    }

    #endregion

    #region Properties

    /// <summary>Gets the configurations for the <see cref="DutchPhoneNumberValidator"/>s that were specified in the configuration file.</summary>
    public DutchPhoneNumberValidatorConfigDictionary DutchPhoneNumberValidators { get; private set; }

    /// <summary>Gets the configurations for the <see cref="EmailValidator"/>s that were specified in the configuration file.</summary>
    public EmailValidatorConfigDictionary EmailValidators { get; private set; }

    #endregion

    #region Public methods

    /// <summary>Gets a value indicating whether the <see cref="ConfigurationElement"/> object is read-only.</summary>
    /// <returns><see langword="false"/> as the configuration in this section can not be modified from code.</returns>
    public override bool IsReadOnly() {
      return true;
    }

    #endregion

    #region Protected methods

    /// <summary>Reads XML from the configuration file.</summary>
    /// <param name="reader">The <see cref="XmlReader"/> that reads from the configuration file.</param>
    /// <param name="serializeCollectionKey"><see langword="true"/> to serialize only the collection key properties; otherwise, <see langword="false"/>.</param>
    /// <exception cref="ConfigurationErrorsException">The element to read is locked.- or -An attribute of the current node is not recognized.- or -
    /// The lock status of the current node cannot be determined.</exception>
    protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey) {
      while(reader.Read()) {
        if(reader.MoveToContent() != XmlNodeType.Element) {
          continue;
        }

        switch(reader.Name) {
          case "DutchPhoneNumberValidator": {
              DutchPhoneNumberValidatorConfigElement configElement = new DutchPhoneNumberValidatorConfigElement();
              configElement.ReadFromConfig(reader, serializeCollectionKey);
              this.DutchPhoneNumberValidators[configElement.Name] = configElement;
            }

            break;
          case "EmailValidator": {
              EmailValidatorConfigElement configElement = new EmailValidatorConfigElement();
              configElement.ReadFromConfig(reader, serializeCollectionKey);
              this.EmailValidators[configElement.Name] = configElement;
            }

            break;
          default:
            throw new ConfigurationErrorsException("Discovered an unrecognized element '" + reader.Name + "' in the configuration", reader);
        }
      }
    }

    #endregion
  }
}
