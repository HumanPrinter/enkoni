using System.Configuration;

namespace Enkoni.Framework.DataAnnotations.Configuration {
  /// <summary>Defines the configuration element collection that holds the area codes that should either be added or removed from the list of area
  /// codes that must be validated by the <see cref="DutchPhoneNumberAttribute"/>.</summary>
  public class DutchPhoneNumberAreaCodeCollection : ConfigurationElementCollection {
    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="DutchPhoneNumberAreaCodeCollection"/> class.</summary>
    public DutchPhoneNumberAreaCodeCollection() {
    }

    #endregion

    #region Properties

    /// <summary>Gets the type of the <see cref="ConfigurationElementCollection"/>.</summary>
    public override ConfigurationElementCollectionType CollectionType {
      get {
        return ConfigurationElementCollectionType.AddRemoveClearMap;
      }
    }

    /// <summary>Gets or sets a property, attribute, or child element of this configuration element.</summary>
    /// <param name="index">The index of the <see cref="DutchPhoneNumberAreaCodeConfigElement"/> to access.</param>
    /// <returns>The specified property, attribute, or child element.</returns>
    public DutchPhoneNumberAreaCodeConfigElement this[int index] {
      get {
        return (DutchPhoneNumberAreaCodeConfigElement)this.BaseGet(index);
      }

      set {
        if(this.BaseGet(index) != null) {
          this.BaseRemoveAt(index);
        }

        this.BaseAdd(index, value);
      }
    }

    /// <summary>Gets or sets a property, attribute, or child element of this configuration element.</summary>
    /// <param name="areaCode">The area code of the <see cref="DutchPhoneNumberAreaCodeConfigElement"/> to access.</param>
    /// <returns>The specified property, attribute, or child element.</returns>
    public new DutchPhoneNumberAreaCodeConfigElement this[string areaCode] {
      get { return (DutchPhoneNumberAreaCodeConfigElement)this.BaseGet(areaCode); }
    }

    #endregion

    #region Public methods

    /// <summary>Gets the index of the specified element.</summary>
    /// <param name="element">The element of which the index must be returned.</param>
    /// <returns>The index of the element.</returns>
    public int IndexOf(DutchPhoneNumberAreaCodeConfigElement element) {
      return this.BaseIndexOf(element);
    }

    /// <summary>Adds an element to the collection.</summary>
    /// <param name="element">The element that must be added.</param>
    public void Add(DutchPhoneNumberAreaCodeConfigElement element) {
      this.BaseAdd(element);
    }

    /// <summary>Removes an element from the collection.</summary>
    /// <param name="element">The element that must be removed.</param>
    public void Remove(DutchPhoneNumberAreaCodeConfigElement element) {
      if(element == null) {
        return;
      }

      if(this.BaseIndexOf(element) >= 0) {
        this.BaseRemove(element.AreaCode);
      }
    }

    /// <summary>Removes an element from the collection.</summary>
    /// <param name="areaCode">The key of the element that must be removed.</param>
    public void Remove(string areaCode) {
      this.BaseRemove(areaCode);
    }

    /// <summary>Removes an element from the collection.</summary>
    /// <param name="index">The index of the element that must be removed.</param>
    public void RemoveAt(int index) {
      this.BaseRemoveAt(index);
    }

    /// <summary>Clears the collection.</summary>
    public void Clear() {
      this.BaseClear();
    }

    #endregion

    #region Protected methods

    /// <summary>Creates a new <see cref="ConfigurationElement"/>.</summary>
    /// <returns>A new <see cref="ConfigurationElement"/>.</returns>
    protected override ConfigurationElement CreateNewElement() {
      return new DutchPhoneNumberAreaCodeConfigElement();
    }

    /// <summary>Gets the element key for a specified configuration element.</summary>
    /// <param name="element">The <see cref="ConfigurationElement"/> to return the key for.</param>
    /// <returns>An <see langword="object"/> that acts as the key for the specified <see cref="ConfigurationElement"/>.</returns>
    protected override object GetElementKey(ConfigurationElement element) {
      return ((DutchPhoneNumberAreaCodeConfigElement)element).AreaCode;
    }

    /// <summary>Adds a configuration element to the collection.</summary>
    /// <param name="element">The element to add.</param>
    protected override void BaseAdd(ConfigurationElement element) {
      this.BaseAdd(element, false);
    }

    #endregion
  }
}
