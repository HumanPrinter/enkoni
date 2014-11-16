using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Enkoni.Framework.Xml {
  public class XmlResourceResolver : XmlUrlResolver {
    #region Instance variables
    /// <summary>The assembly that contains the embedded resources.</summary>
    private Assembly resourceAssembly;

    /// <summary>The namespace of the embedded resources.</summary>
    private string resourceNamespace;
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="XmlResourceResolver"/> class.</summary>
    /// <param name="resourceAssembly">The assembly from which the embedded resources must be retrieved.</param>
    /// <param name="resourceNamespace">The namespace of the embedded resources.</param>
    /// <exception cref="ArgumentNullException"><paramref name="resourceAssembly"/> is <see langword="null"/>.</exception>
    public XmlResourceResolver(Assembly resourceAssembly, string resourceNamespace) {
      if(resourceAssembly == null) {
        throw new ArgumentNullException("resourceAssembly");
      }

      this.resourceAssembly = resourceAssembly;
      this.resourceNamespace = string.IsNullOrEmpty(resourceNamespace) ? string.Empty : string.Format("{0}.", resourceNamespace);
    }
    #endregion

    #region XmlUrlResolver-overrides
    /// <summary>Maps a URI to an object containing the actual resource.</summary>
    /// <param name="absoluteUri">The URI returned from <see cref="XmlResolver.ResolveUri(Uri, String)"/>.</param>
    /// <param name="role">The current implementation does not use this parameter when resolving URIs. This is provided for future extensibility purposes. For example, this can
    /// be mapped to the xlink: role and used as an implementation specific argument in other scenarios.</param>
    /// <param name="ofObjectToReturn">The type of object to return. The current implementation only returns <see cref="Stream"/> objects.</param>
    /// <returns>A <see cref="Stream"/> object or <see langword="null"/> if a type other than stream is specified.</returns>
    public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn) {
      if(absoluteUri != null && absoluteUri.IsFile && (ofObjectToReturn == null || ofObjectToReturn == typeof(Stream))) {
        string fileName = Path.GetFileName(absoluteUri.AbsolutePath);
        Stream resourceStream = this.resourceAssembly.GetManifestResourceStream(this.resourceNamespace + fileName);
        if(resourceStream != null) {
          return resourceStream;
        }
        else {
          /* The resource cannot be found in the assembly. Fall back to the default file resolver */
          return base.GetEntity(absoluteUri, role, ofObjectToReturn);
        }
      }
      else {
        return base.GetEntity(absoluteUri, role, ofObjectToReturn);
      }
    }
    #endregion
  }
}
