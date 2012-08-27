//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewModelTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the ViewModel class.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.ComponentModel;
using System.Threading;

using Enkoni.Framework.UI.Mvvm;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.UI.Tests {
  /// <summary>Tests the functionality of the <see cref="ViewModel"/> class.</summary>
  [TestClass]
  public class ViewModelTest {
    #region Events Testcases
    /// <summary>Tests the functionality of the <see cref="ViewModel.PropertyChanged"/> event.</summary>
    [TestMethod]
    public void TestCase01_PropertyChanged() {
      /* First, create an instance of the viewmodel */
      DummyViewModel testSubject = new DummyViewModel();

      /* Set some properties and fixate the test subject */
      testSubject.BooleanValue = true;
      testSubject.IntegerValue = 42;
      testSubject.Fixate();

      /* Register handlers */
      testSubject.PropertyChanged += this.ReceivePropertyChangedEvent;
      testSubject.Messenger.Register<PropertyChangedMessage<bool>>(this, this.ReceivePropertyChangedMessage);
      testSubject.Messenger.Register<PropertyChangedMessage<int>>(this, this.ReceivePropertyChangedMessage);

      /* Change the first property */
      testSubject.BooleanValue = false;

      /* Allow for the messenger to process the send-request and the registered action to complete */
      int roundtrips = 0;
      while(roundtrips < 5 && !testSubject.IsHandledByEvent && !testSubject.IsHandledByMessage) {
        ++roundtrips;
        Thread.Yield();
      }

      /* Check if any events or messages where received */
      Assert.IsTrue(testSubject.IsHandledByEvent);
      Assert.IsTrue(testSubject.IsHandledByMessage);

      /* Reset the handled-flags */
      testSubject.IsHandledByEvent = false;
      testSubject.IsHandledByMessage = false;

      /* Change the second property */
      testSubject.IntegerValue = 88;

      /* Allow for the messenger to process the send-request and the registered action to complete */
      roundtrips = 0;
      while(roundtrips < 5 && !testSubject.IsHandledByEvent && !testSubject.IsHandledByMessage) {
        ++roundtrips;
        Thread.Yield();
      }

      /* Check if any events or messages where received */
      Assert.IsTrue(testSubject.IsHandledByEvent);
      Assert.IsFalse(testSubject.IsHandledByMessage);
    }
    #endregion
    
    #region Events Testcases
    /// <summary>Tests the validation functionality of the <see cref="ViewModel"/> class.</summary>
    [TestMethod]
    public void TestCase02_Validation() {
      /* First, create an instance of the viewmodel */
      ValidatedDummyViewModel testSubject = new ValidatedDummyViewModel();

      /* Set a valid value first */
      testSubject.IntegerValue = 42;

      /* Check if the viewmodel is valid */
      Assert.IsTrue(testSubject.IsEachPropertyValid());
      Assert.IsTrue(testSubject.IsPropertyValid<int>(t => testSubject.IntegerValue));
      Assert.AreEqual(string.Empty, testSubject.Error);
      Assert.AreEqual(string.Empty, testSubject["IntegerValue"]);
    }
    #endregion

    #region Private helper methods
    /// <summary>A helper method that acts as a receiver for events.</summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">Information regarding the event.</param>
    private void ReceivePropertyChangedEvent(object sender, PropertyChangedEventArgs e) {
      DummyViewModel testSubject = sender as DummyViewModel;
      if(testSubject != null) {
        testSubject.IsHandledByEvent = true;
      }
    }

    /// <summary>A helper method that acts as a receiver for messages.</summary>
    /// <param name="message">The received message.</param>
    private void ReceivePropertyChangedMessage(PropertyChangedMessage<bool> message) {
      DummyViewModel testSubject = message.Sender as DummyViewModel;
      if(testSubject != null) {
        if(message.PropertyName == "BooleanValue" && message.OldValue == testSubject.OriginalBooleanValue && message.NewValue == testSubject.BooleanValue) {
          testSubject.IsHandledByMessage = true;
        }
      }
    }

    /// <summary>A helper method that acts as a receiver for messages.</summary>
    /// <param name="message">The received message.</param>
    private void ReceivePropertyChangedMessage(PropertyChangedMessage<int> message) {
      DummyViewModel testSubject = message.Sender as DummyViewModel;
      if(testSubject != null) {
        if(message.PropertyName == "IntegerValue" && message.OldValue == testSubject.OriginalIntegerValue && message.NewValue == testSubject.IntegerValue) {
          testSubject.IsHandledByMessage = true;
        }
      }
    }
    #endregion

    #region Helper classes
    /// <summary>A dummy viewmodel for testing purposes only.</summary>
    public class DummyViewModel : ViewModel {
      #region Instance variables
      /// <summary>The actual value of the boolean property.</summary>
      private bool boolValue;

      /// <summary>The actual value of the integer property.</summary>
      private int intValue;

      /// <summary>Indicates if the values of this class are fixated or not.</summary>
      private bool fixated;
      #endregion

      #region Properties
      /// <summary>Gets or sets a value indicating whether this message has been handled or not.</summary>
      public bool IsHandledByEvent { get; set; }

      /// <summary>Gets or sets a value indicating whether this message has been handled or not.</summary>
      public bool IsHandledByMessage { get; set; }

      /// <summary>Gets or sets a value for testing purposes only.</summary>
      public bool BooleanValue {
        get {
          return this.boolValue;
        }
        set {
          if(this.boolValue != value) {
            this.boolValue = value;
            if(this.fixated) {
              this.RaisePropertyChanged(() => this.BooleanValue, !this.boolValue, this.boolValue);
            }
          }
        }
      }

      /// <summary>Gets the value of <see cref="BooleanValue"/> at the time of the fixation.</summary>
      public bool OriginalBooleanValue { get; private set; }

      /// <summary>Gets or sets a value for testing purposes only.</summary>
      public int IntegerValue {
        get { return this.intValue; }
        set {
          if(this.intValue != value) {
            int oldValue = this.intValue;
            this.intValue = value;
            if(this.fixated) {
              this.RaisePropertyChanged(() => this.IntegerValue);
            }
          }
        }
      }

      /// <summary>Gets the value of <see cref="IntegerValue"/> at the time of the fixation.</summary>
      public int OriginalIntegerValue { get; private set; }
      #endregion

      #region Methods
      /// <summary>Fixates the property values.</summary>
      public void Fixate() {
        this.OriginalBooleanValue = this.BooleanValue;
        this.OriginalIntegerValue = this.IntegerValue;
        this.fixated = true;
      }

      /// <summary>Releases (de-fixates) the property values.</summary>
      public void Release() {
        this.fixated = false;
      }
      #endregion
    }

    /// <summary>A dummy viewmodel for testing purposes only.</summary>
    public class ValidatedDummyViewModel : ViewModel {
      #region Instance variables
      /// <summary>The actual value of the boolean property.</summary>
      private bool boolValue;

      /// <summary>The actual value of the integer property.</summary>
      private int intValue;
      #endregion

      #region Constructor
      /// <summary>Initializes a new instance of the <see cref="ValidatedDummyViewModel"/> class.</summary>
      public ValidatedDummyViewModel() {
        this.AddValidationRule(() => this.IntegerValue, this.ValidateIntegerValue);
      }
      #endregion

      #region Properties
      /// <summary>Gets or sets a value for testing purposes only.</summary>
      public int IntegerValue {
        get { return this.intValue; }
        set { this.intValue = value; }
      }
      #endregion

      #region Methods
      private string ValidateIntegerValue() {
        if(this.IntegerValue < 40 || this.IntegerValue > 44) {
          return "IntegerValue must be between 40 and 44";
        }
        else {
          return string.Empty;
        }
      }
      #endregion
    }
    #endregion
  }
}
