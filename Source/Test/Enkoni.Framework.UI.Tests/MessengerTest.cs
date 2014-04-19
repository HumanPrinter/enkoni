using System;
using System.Threading;
using Enkoni.Framework.UI.Mvvm;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.UI.Tests {
  /// <summary>Tests the functionality of the <see cref="Messenger"/> class.</summary>
  [TestClass]
  public class MessengerTest {
    #region General Testcases
    /// <summary>Tests the functionality of the <see cref="Messenger.Default"/> property.</summary>
    [TestMethod]
    public void Messenger_DefaultMessenger() {
      /* First check the system default */
      IMessenger messenger = Messenger.Default;

      Assert.IsNotNull(messenger);
      Assert.IsInstanceOfType(messenger, typeof(Messenger));

      /* Secondly, check if it is possible to register a custom messenger */
      Messenger.Default = new DummyMessenger();

      messenger = Messenger.Default;

      Assert.IsNotNull(messenger);
      Assert.IsInstanceOfType(messenger, typeof(DummyMessenger));
    }
    #endregion

    #region Register-and-Send Testcases
    /// <summary>Tests the functionality of the <see cref="Messenger.Register{TMessage}(object,Action{TMessage})"/> and 
    /// <see cref="Messenger.Send{TMessage}(TMessage,object)"/> methods.</summary>
    [TestMethod]
    public void Messenger_RegisterAndSend_NoToken() {
      /* Create an instance of the messenger */
      IMessenger messenger = new Messenger();

      /* Register without a token */
      messenger.Register<TestMessage>(this, this.ReceiveMessage);

      TestMessage testMessage = new TestMessage();

      /* Send the message without a token */
      messenger.Send(testMessage);

      /* Allow for the messenger to process the send-request and the registered action to complete */
      int roundtrips = 0;
      while(roundtrips < 5 && !testMessage.IsHandled) {
        ++roundtrips;
        Thread.Yield();
      }

      /* Check if the message was handled */
      Assert.IsTrue(testMessage.IsHandled);

      /* Reset the test */
      testMessage.IsHandled = false;

      /* Send the message again using some sort of token */
      Guid token = Guid.NewGuid();
      messenger.Send(testMessage, token);

      /* Allow for the messenger to process the send-request and the registered action to complete */
      roundtrips = 0;
      while(roundtrips < 5 && !testMessage.IsHandled) {
        ++roundtrips;
        Thread.Yield();
      }

      /* Check if the message is not received (as expected) */
      Assert.IsFalse(testMessage.IsHandled);

      /* Prepare a derived message type */
      testMessage = new SubTestMessage();

      /* Send the message */
      messenger.Send(testMessage, token);

      /* Allow for the messenger to process the send-request and the registered action to complete */
      roundtrips = 0;
      while(roundtrips < 5 && !testMessage.IsHandled) {
        ++roundtrips;
        Thread.Yield();
      }

      /* Check if the message is not received (as expected) */
      Assert.IsFalse(testMessage.IsHandled);
    }

    /// <summary>Tests the functionality of the <see cref="Messenger.Register{TMessage}(object,Action{TMessage})"/> and 
    /// <see cref="Messenger.Send{TMessage}(TMessage,object)"/> methods.</summary>
    [TestMethod]
    public void Messenger_RegisterAndSendDerived_NoToken() {
      /* Create an instance of the messenger */
      IMessenger messenger = new Messenger();

      /* Register without a token */
      messenger.Register<TestMessage>(this, this.ReceiveMessage, true);

      TestMessage testMessage = new SubTestMessage();

      /* Send the message without a token */
      messenger.Send(testMessage);

      /* Allow for the messenger to process the send-request and the registered action to complete */
      int roundtrips = 0;
      while(roundtrips < 5 && !testMessage.IsHandled) {
        ++roundtrips;
        Thread.Yield();
      }

      /* Check if the message was handled */
      Assert.IsTrue(testMessage.IsHandled);

      /* Reset the test */
      testMessage.IsHandled = false;

      /* Send the message again using some sort of token */
      Guid token = Guid.NewGuid();
      messenger.Send(testMessage, token);

      /* Allow for the messenger to process the send-request and the registered action to complete */
      roundtrips = 0;
      while(roundtrips < 5 && !testMessage.IsHandled) {
        ++roundtrips;
        Thread.Yield();
      }

      /* Check if the message is not received (as expected) */
      Assert.IsFalse(testMessage.IsHandled);
    }

    /// <summary>Tests the functionality of the <see cref="Messenger.Register{TMessage}(object,Action{TMessage},bool,object)"/> and 
    /// <see cref="Messenger.Send{TMessage}(TMessage,object)"/> methods.</summary>
    [TestMethod]
    public void Messenger_RegisterAndSend_WithToken() {
      /* Create an instance of the messenger */
      IMessenger messenger = new Messenger();

      /* Create token for the test */
      Guid token = Guid.NewGuid();

      /* Register with a token */
      messenger.Register<TestMessage>(this, this.ReceiveMessage, false, token);

      TestMessage testMessage = new TestMessage();

      /* Send the message with a token */
      messenger.Send(testMessage, token);

      /* Allow for the messenger to process the send-request and the registered action to complete */
      int roundtrips = 0;
      while(roundtrips < 5 && !testMessage.IsHandled) {
        ++roundtrips;
        Thread.Yield();
      }

      /* Check if the message was handled */
      Assert.IsTrue(testMessage.IsHandled);

      /* Reset the test */
      testMessage.IsHandled = false;

      /* Send the message again with a different token */
      Guid otherToken = Guid.NewGuid();
      messenger.Send(testMessage, otherToken);

      /* Allow for the messenger to process the send-request and the registered action to complete */
      roundtrips = 0;
      while(roundtrips < 5 && !testMessage.IsHandled) {
        ++roundtrips;
        Thread.Yield();
      }

      /* Check if the message is not received (as expected) */
      Assert.IsFalse(testMessage.IsHandled);

      /* Reset the test */
      testMessage.IsHandled = false;

      /* Send the message again without a token */
      messenger.Send(testMessage);

      /* Allow for the messenger to process the send-request and the registered action to complete */
      roundtrips = 0;
      while(roundtrips < 5 && !testMessage.IsHandled) {
        ++roundtrips;
        Thread.Yield();
      }

      /* Check if the message is received */
      Assert.IsTrue(testMessage.IsHandled);

      /* Prepare a derived message type */
      testMessage = new SubTestMessage();

      /* Send the message */
      messenger.Send(testMessage, token);

      /* Allow for the messenger to process the send-request and the registered action to complete */
      roundtrips = 0;
      while(roundtrips < 5 && !testMessage.IsHandled) {
        ++roundtrips;
        Thread.Yield();
      }

      /* Check if the message is not received (as expected) */
      Assert.IsFalse(testMessage.IsHandled);
    }

    /// <summary>Tests the functionality of the <see cref="Messenger.Register{TMessage}(object,Action{TMessage},bool,object)"/> and 
    /// <see cref="Messenger.Send{TMessage}(TMessage,object)"/> methods.</summary>
    [TestMethod]
    public void Messenger_RegisterAndSendDerived_WithToken() {
      /* Create an instance of the messenger */
      IMessenger messenger = new Messenger();

      /* Create token for the test */
      Guid token = Guid.NewGuid();

      /* Register with a token */
      messenger.Register<TestMessage>(this, this.ReceiveMessage, true, token);

      TestMessage testMessage = new SubTestMessage();

      /* Send the message with a token */
      messenger.Send(testMessage, token);

      /* Allow for the messenger to process the send-request and the registered action to complete */
      int roundtrips = 0;
      while(roundtrips < 5 && !testMessage.IsHandled) {
        ++roundtrips;
        Thread.Yield();
      }

      /* Check if the message was handled */
      Assert.IsTrue(testMessage.IsHandled);

      /* Reset the test */
      testMessage.IsHandled = false;

      /* Send the message again with a different token */
      Guid otherToken = Guid.NewGuid();
      messenger.Send(testMessage, otherToken);

      /* Allow for the messenger to process the send-request and the registered action to complete */
      roundtrips = 0;
      while(roundtrips < 5 && !testMessage.IsHandled) {
        ++roundtrips;
        Thread.Yield();
      }

      /* Check if the message is not received (as expected) */
      Assert.IsFalse(testMessage.IsHandled);

      /* Reset the test */
      testMessage.IsHandled = false;

      /* Send the message again without a token */
      messenger.Send(testMessage);

      /* Allow for the messenger to process the send-request and the registered action to complete */
      roundtrips = 0;
      while(roundtrips < 5 && !testMessage.IsHandled) {
        ++roundtrips;
        Thread.Yield();
      }

      /* Check if the message is received */
      Assert.IsTrue(testMessage.IsHandled);
    }
    #endregion

    #region Deregister Testcases
    /// <summary>Tests the functionality of the <see cref="Messenger.Deregister{TMessage}(object,Action{TMessage})"/> method.</summary>
    [TestMethod]
    public void Messenger_Deregister_NoToken() {
      /* Create an instance of the messenger */
      IMessenger messenger = new Messenger();

      /* Register without a token */
      messenger.Register<TestMessage>(this, this.ReceiveMessage);

      /* Deregister again (deregister all actions) */
      messenger.Deregister<TestMessage>(this, null);

      TestMessage testMessage = new TestMessage();

      /* Send the message without a token */
      messenger.Send(testMessage);

      /* Allow for the messenger to process the send-request and the registered action to complete */
      int roundtrips = 0;
      while(roundtrips < 5 && !testMessage.IsHandled) {
        ++roundtrips;
        Thread.Yield();
      }

      /* Check if the message was handled (not expected) */
      Assert.IsFalse(testMessage.IsHandled);

      /* Register without a token */
      messenger.Register<TestMessage>(this, this.ReceiveMessage);
      messenger.Register<TestMessage>(this, this.ReceiveMessage2);

      /* Deregister again (deregister a specific action) */
      messenger.Deregister<TestMessage>(this, this.ReceiveMessage);

      /* Send the message with a token */
      messenger.Send(testMessage);

      /* Allow for the messenger to process the send-request and the registered action to complete */
      roundtrips = 0;
      while(roundtrips < 5 && !testMessage.IsHandled && testMessage.Counter == 0) {
        ++roundtrips;
        Thread.Yield();
      }

      /* Check if the message was handled (not expected) */
      Assert.IsFalse(testMessage.IsHandled);

      /* Check if the message was handled */
      Assert.AreEqual(1, testMessage.Counter);
    }

    /// <summary>Tests the functionality of the <see cref="Messenger.Deregister{TMessage}(object,Action{TMessage},object)"/> method.</summary>
    [TestMethod]
    public void Messenger_Deregister_WithToken() {
      /* Create an instance of the messenger */
      IMessenger messenger = new Messenger();

      /* Create token for the test */
      Guid token = Guid.NewGuid();

      /* Register without a token */
      messenger.Register<TestMessage>(this, this.ReceiveMessage, false, token);

      /* Deregister again (deregister all actions) */
      messenger.Deregister<TestMessage>(this, null, token);

      TestMessage testMessage = new TestMessage();

      /* Send the message with a token */
      messenger.Send(testMessage, token);

      /* Allow for the messenger to process the send-request and the registered action to complete */
      int roundtrips = 0;
      while(roundtrips < 5 && !testMessage.IsHandled) {
        ++roundtrips;
        Thread.Yield();
      }

      /* Check if the message was handled (not expected) */
      Assert.IsFalse(testMessage.IsHandled);

      /* Register without a token */
      messenger.Register<TestMessage>(this, this.ReceiveMessage, false, token);
      messenger.Register<TestMessage>(this, this.ReceiveMessage2, false, token);

      /* Deregister again (deregister a specific action) */
      messenger.Deregister<TestMessage>(this, this.ReceiveMessage, token);

      /* Send the message with a token */
      messenger.Send(testMessage, token);

      /* Allow for the messenger to process the send-request and the registered action to complete */
      roundtrips = 0;
      while(roundtrips < 5 && !testMessage.IsHandled && testMessage.Counter == 0) {
        ++roundtrips;
        Thread.Yield();
      }

      /* Check if the message was handled (not expected) */
      Assert.IsFalse(testMessage.IsHandled);

      /* Check if the message was handled */
      Assert.AreEqual(1, testMessage.Counter);

      /* Deregister again (using a different token) */
      messenger.Deregister<TestMessage>(this, this.ReceiveMessage2, Guid.NewGuid());

      /* Send the message with a token */
      messenger.Send(testMessage, token);

      /* Allow for the messenger to process the send-request and the registered action to complete */
      roundtrips = 0;
      while(roundtrips < 5 && testMessage.Counter == 1) {
        ++roundtrips;
        Thread.Yield();
      }

      /* Check if the message was handled (not expected) */
      Assert.AreEqual(2, testMessage.Counter);
    }
    #endregion

    #region Private helper methods
    /// <summary>A helper method that acts as a receiver for messages.</summary>
    /// <param name="message">The received message.</param>
    private void ReceiveMessage(TestMessage message) {
      message.IsHandled = true;
    }

    /// <summary>A helper method that acts as a receiver for messages.</summary>
    /// <param name="message">The received message.</param>
    private void ReceiveMessage2(TestMessage message) {
      ++message.Counter;
    }
    #endregion

    #region Helper classes
    /// <summary>A dummy messenger for testing purposes only.</summary>
    public class DummyMessenger : Messenger {
    }

    /// <summary>A test message for testing purposes only.</summary>
    public class TestMessage : Message {
      /// <summary>Gets or sets a value indicating whether this message has been handled or not.</summary>
      public bool IsHandled { get; set; }

      /// <summary>Gets or sets a counter for testing purposes.</summary>
      public int Counter { get; set; }
    }

    /// <summary>A test message for testing purposes only.</summary>
    public class SubTestMessage : TestMessage {
    }
    #endregion
  }
}
