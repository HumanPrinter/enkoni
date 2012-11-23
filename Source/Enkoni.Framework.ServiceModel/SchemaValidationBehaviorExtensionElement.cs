//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="SchemaValidationBehaviorExtensionElement.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//   Implementatie van een extension element waarmee de SchemaValidationBehavior aan een endpoint gekoppeld kan worden via de configuratie.
// </summary>
// <remark>
//   Implementatie gebaseerd op de code van Microsoft (http://msdn.microsoft.com/en-us/library/ff647820.aspx)
// </remark>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.ServiceModel.Configuration;
using System.Xml;
using System.Xml.Schema;

namespace Enkoni.Framework.ServiceModel {
  /// <summary>Implements a behavior extension element with which a <see cref="SchemaValidationBehavior"/> can be connected to an endpoint through 
  /// configuration.</summary>
  /// <remarks>The implementation is based on the code of Microsoft (http://msdn.microsoft.com/en-us/library/ff647820.aspx).</remarks>
  public class SchemaValidationBehaviorExtensionElement : BehaviorExtensionElement {
    #region Constants
    /// <summary>Defines the name of the 'enabled'-configuration attribute.</summary>
    private const string EnabledAttributeName = "enabled";

    /// <summary>Defines the name of the 'schema'-configuration attribute.</summary>
    private const string SchemaFileAttributeName = "schema";
    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="SchemaValidationBehaviorExtensionElement"/> class.</summary>
    public SchemaValidationBehaviorExtensionElement() {
    }
    #endregion

    #region Properties
    /// <summary>Gets or sets a value indicating whether the behavior must be enabled or not.</summary>
    [ConfigurationProperty(EnabledAttributeName, DefaultValue = true, IsRequired = false)]
    public bool Enabled {
      get { return (bool)this[EnabledAttributeName]; }
      set { this[EnabledAttributeName] = value; }
    }

    /// <summary>Gets or sets the location of the schema file that must be used.</summary>
    [ConfigurationProperty(SchemaFileAttributeName, IsRequired = true)]
    public string SchemaFile {
      get { return (string)this[SchemaFileAttributeName]; }
      set { this[SchemaFileAttributeName] = value; }
    }

    /// <summary>Gets the type of behavior that is handled by this extension element.</summary>
    public override Type BehaviorType {
      get { return typeof(SchemaValidationBehavior); }
    }
    #endregion

    #region BehaviorExtensionElement-overrides
    /// <summary>Creates a new instance of the behavior.</summary>
    /// <returns>The created behavior.</returns>
    protected override object CreateBehavior() {
      XmlSchemaSet schemaSet = new XmlSchemaSet();

      if(this.SchemaFile == null || string.IsNullOrEmpty(this.SchemaFile)) {
        throw new InvalidOperationException("The schema file must be specified");
      }

      Stream stream = null;
      try {
        if(this.SchemaFile.StartsWith("resource://", StringComparison.OrdinalIgnoreCase)) {
          string[] resourcenameParts = this.SchemaFile.Substring(11).Split(new char[] { ',' }, 2);
          Assembly assembly;
          if(resourcenameParts.Length == 1) {
            assembly = Assembly.GetExecutingAssembly();
          }
          else {
            assembly = Assembly.ReflectionOnlyLoad(resourcenameParts[1]);
          }

          stream = assembly.GetManifestResourceStream(resourcenameParts[0]);
        }
        else {
          stream = File.OpenRead(this.SchemaFile);
        }

        using(XmlReader reader = XmlReader.Create(stream)) {
          XmlSchema schema = XmlSchema.Read(reader, null);
          schemaSet.Add(schema);
        }
      }
      finally {
        if(stream != null) {
          stream.Dispose();
        }
      }

      return new SchemaValidationBehavior(this.Enabled, schemaSet);
    }
    #endregion
  }
}