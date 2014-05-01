//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Messenger.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Defines a default implementation of the IMessenger interface.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Enkoni.Framework.UI.Mvvm {
  /// <summary>Default implementation of the <see cref="IMessenger"/> interface.</summary>
  public class Messenger : IMessenger, IDisposable {
    #region Instance variables
    /// <summary>The interval which the garbage will be collected.</summary>
    private int cleanupInterval;

    /// <summary>The timer that controls the garbage collection.</summary>
    private Timer cleanupTimer;

    /// <summary>Contains the recipients that listen for a specific message type.</summary>
    private Dictionary<Type, List<IRecipient>> recipients = new Dictionary<Type, List<IRecipient>>();

    /// <summary>Contains the recipients that listen for a specific message type including derived messages.</summary>
    private Dictionary<Type, List<IRecipient>> derivedRecipients = new Dictionary<Type, List<IRecipient>>();
    #endregion

    #region Constructors
    /// <summary>Initializes static members of the <see cref="Messenger"/> class.</summary>
    static Messenger() {
      Default = new Messenger { CleanupInterval = 1000 * 60 };
    }

    /// <summary>Initializes a new instance of the <see cref="Messenger"/> class.</summary>
    public Messenger() {
    }
    #endregion

    #region Private interfaces
    /// <summary>Private interface for accessing the generic <see cref="Recipient{TMessage}"/> class in a non generic way.</summary>
    private interface IRecipient {
      /// <summary>Gets a value indicating whether the owner is still alive.</summary>
      bool IsAlive { get; }

      /// <summary>Gets the owner (recipient).</summary>
      object Owner { get; }

      /// <summary>Gets the token for additinal message filtering.</summary>
      object Token { get; }

      /// <summary>Gets the action called when a message is send.</summary>
      Delegate Action { get; }

      /// <summary>Processes the specified message.</summary>
      /// <param name="message">The message.</param>
      void Process(object message);

      /// <summary>Marks an object for deletion.</summary>
      void MarkForDeletion();
    }
    #endregion

    #region Properties
    /// <summary>Gets or sets the default instance of a messenger.</summary>
    public static IMessenger Default { get; set; }

    /// <summary>Gets or sets the interval with which dereferenced recepients are removed from the cache.</summary>
    public int CleanupInterval {
      get {
        return this.cleanupInterval;
      }

      set {
        this.cleanupInterval = value;
        if(this.cleanupInterval > 0) {
          if(this.cleanupTimer == null) {
            this.cleanupTimer = new Timer(o => {
              this.Cleanup();
              this.cleanupTimer.Change(this.cleanupInterval, Timeout.Infinite);
            }, null, this.cleanupInterval, Timeout.Infinite);
          }
          else {
            this.cleanupTimer.Change(this.cleanupInterval, Timeout.Infinite);
          }
        }
        else if(this.cleanupTimer != null) {
          this.cleanupTimer.Change(Timeout.Infinite, Timeout.Infinite);
          this.cleanupTimer.Dispose();
          this.cleanupTimer = null;
        }
      }
    }
    #endregion

    #region Public methods
    /// <summary>Registers a recipient for a type of <typeparamref name="TMessage"/>. The action parameter will be executed when a corresponding 
    /// message is sent.
    /// <para>Registering a recipient does not create a hard reference to it, so if this recipient is deleted, no memory leak is caused.</para>
    /// </summary>
    /// <typeparam name="TMessage">The type of message that the recipient registers for.</typeparam>
    /// <param name="recipient">The recipient that will receive the messages.</param>
    /// <param name="action">The action that will be executed when a <typeparamref name="TMessage"/> is sent.</param>
    public void Register<TMessage>(object recipient, Action<TMessage> action) where TMessage : IMessage {
      this.Register(recipient, action, false);
    }

    /// <summary>Registers a recipient for a type of <typeparamref name="TMessage"/>. The action parameter will be executed when a corresponding 
    /// message is sent.
    /// <para>Registering a recipient does not create a hard reference to it, so if this recipient is deleted, no memory leak is caused.</para>
    /// </summary>
    /// <typeparam name="TMessage">The type of message that the recipient registers for.</typeparam>
    /// <param name="recipient">The recipient that will receive the messages.</param>
    /// <param name="action">The action that will be executed when a <typeparamref name="TMessage"/> is sent.</param>
    /// <param name="includeDerivedMessages">If <see langword="true"/>, message types deriving from <typeparamref name="TMessage"/> will also be 
    /// transmitted to the recipient. For example, if a <c>SendOrderMessage</c> and an <c>ExecuteOrderMessage</c> derive from <c>OrderMessage</c>, 
    /// registering for <c>OrderMessage</c> and setting <paramref name="includeDerivedMessages"/> to <see langword="true"/> will send 
    /// <c>SendOrderMessage</c> and <c>ExecuteOrderMessage</c> to the recipient that registered.
    /// <para>Also, if <typeparamref name="TMessage"/> is an interface, message types implementing <typeparamref name="TMessage"/> will also be
    /// transmitted to the recipient. For example, if a <c>SendOrderMessage</c> and an <c>ExecuteOrderMessage</c> implement <c>IOrderMessage</c>, 
    /// registering for <c>IOrderMessage</c> and setting <paramref name="includeDerivedMessages"/> to true will send <c>SendOrderMessage</c> and 
    /// <c>ExecuteOrderMessage</c> to the recipient that registered.</para></param>
    public void Register<TMessage>(object recipient, Action<TMessage> action, bool includeDerivedMessages) where TMessage : IMessage {
      this.Register(recipient, action, includeDerivedMessages, null);
    }

    /// <summary>Registers a recipient for a type of <typeparamref name="TMessage"/>. The action parameter will be executed when a corresponding 
    /// message is sent.
    /// <para>Registering a recipient does not create a hard reference to it, so if this recipient is deleted, no memory leak is caused.</para>
    /// </summary>
    /// <typeparam name="TMessage">The type of message that the recipient registers for.</typeparam>
    /// <param name="recipient">The recipient that will receive the messages.</param>
    /// <param name="action">The action that will be executed when a <typeparamref name="TMessage"/> is sent.</param>
    /// <param name="includeDerivedMessages">If <see langword="true"/>, message types deriving from <typeparamref name="TMessage"/> will also be 
    /// transmitted to the recipient. For example, if a <c>SendOrderMessage</c> and an <c>ExecuteOrderMessage</c> derive from <c>OrderMessage</c>, 
    /// registering for <c>OrderMessage</c> and setting <paramref name="includeDerivedMessages"/> to <see langword="true"/> will send 
    /// <c>SendOrderMessage</c> and <c>ExecuteOrderMessage</c> to the recipient that registered.
    /// <para>Also, if <typeparamref name="TMessage"/> is an interface, message types implementing <typeparamref name="TMessage"/> will also be
    /// transmitted to the recipient. For example, if a <c>SendOrderMessage</c> and an <c>ExecuteOrderMessage</c> implement <c>IOrderMessage</c>, 
    /// registering for <c>IOrderMessage</c> and setting <paramref name="includeDerivedMessages"/> to true will send <c>SendOrderMessage</c> and 
    /// <c>ExecuteOrderMessage</c> to the recipient that registered.</para></param>
    /// <param name="token">A token for a messaging channel. If a recipient registers using a token, and a sender sends a message using the same 
    /// token, then this message will be delivered to the recipient. Other recipients who did not use a token when registering (or who used a 
    /// different token) will not get the message. Similarly, messages sent without any token, or with a different token, will not be delivered to 
    /// that recipient.</param>
    public void Register<TMessage>(object recipient, Action<TMessage> action, bool includeDerivedMessages, object token) 
      where TMessage : IMessage {
      if(recipient == null) {
        throw new ArgumentNullException("recipient");
      }

      if(action == null) {
        throw new ArgumentNullException("action");
      }

      /* Select the correct collection */
      Dictionary<Type, List<IRecipient>> dictionary = includeDerivedMessages ? this.derivedRecipients : this.recipients;
      Type key = typeof(TMessage);
      List<IRecipient> list;
      /* First, check if there is already a registration for the message type without locking */
      if(!dictionary.TryGetValue(key, out list)) {
        lock(dictionary) {
          /* If there were no results, check again inside a lock */
          if(!dictionary.TryGetValue(key, out list)) {
            /* Still no results, add a new list for this message type */
            list = new List<IRecipient>();
            dictionary.Add(key, list);
          }
        }
      }

      lock(dictionary) {
        list.Add(new Recipient<TMessage>(recipient, action, token));
      }

      Cleanup(dictionary, key);
    }

    /// <summary>Deregisters a message recipient for a given type of messages. Other message types will still be transmitted to the recipient (if it 
    /// registered for them previously).</summary>
    /// <typeparam name="TMessage">The type of messages that the recipient wants to unregister from.</typeparam>
    /// <param name="recipient">The recipient that must be unregistered.</param>
    public void Deregister<TMessage>(object recipient) where TMessage : IMessage {
      this.Deregister(recipient, (Action<TMessage>)null);
    }

    /// <summary>Deregisters a message recipient for a given type of messages, for a given action. Other message types will still be transmitted to 
    /// the recipient (if it registered for them previously).</summary>
    /// <typeparam name="TMessage">The type of messages that the recipient wants to unregister from.</typeparam>
    /// <param name="recipient">The recipient that must be unregistered.</param>
    /// <param name="action">The action that must be unregistered for the recipient and for the message type <typeparamref name="TMessage"/>.</param>
    public void Deregister<TMessage>(object recipient, Action<TMessage> action) where TMessage : IMessage {
      this.Deregister(recipient, action, null);
    }

    /// <summary>Deregisters a message recipient for a given type of messages, for a given action and a given token. Other message types will still 
    /// be transmitted to the recipient (if it registered for them previously). Other actions that have been registered for the message type 
    /// <typeparamref name="TMessage"/>, for the given recipient and other tokens (if available) will also remain available.</summary>
    /// <typeparam name="TMessage">The type of messages that the recipient wants to unregister from.</typeparam>
    /// <param name="recipient">The recipient that must be unregistered.</param>
    /// <param name="action">The action that must be unregistered for the recipient and for the message type <typeparamref name="TMessage"/>.</param>
    /// <param name="token">The token for which the recipient must be unregistered.</param>
    public void Deregister<TMessage>(object recipient, Action<TMessage> action, object token) where TMessage : IMessage {
      if(recipient == null) {
        throw new ArgumentNullException("recipient");
      }

      Type key = typeof(TMessage);
      for(int i = 0; i < 2; i++) {
        Dictionary<Type, List<IRecipient>> dictionary = i == 0 ? this.derivedRecipients : this.recipients;
        List<IRecipient> list;
        if(!dictionary.TryGetValue(key, out list)) {
          continue;
        }

        foreach(IRecipient item in list) {
          if(item.IsAlive) {
            if(item.Owner != recipient) {
              continue;
            }

            if(token != null && !token.Equals(item.Token)) {
              continue;
            }

            if(action != null && item.Action != (Delegate)action) {
              continue;
            }

            /* Register recipient for deletion. The Gc funtions will remove the item from the list. */
            item.MarkForDeletion();
          }
        }

        Cleanup(dictionary, key);
      }
    }

    /// <summary>Sends a message to registered recipients. The message will reach only recipients that registered for this message type using one of 
    /// the Register methods.</summary>
    /// <typeparam name="TMessage">The type of message that will be sent.</typeparam>
    /// <param name="message">The message to send to registered recipients.</param>
    public void Send<TMessage>(TMessage message) where TMessage : IMessage {
      this.Send<TMessage>(message, null);
    }

    /// <summary>Sends a message to registered recipients. The message will reach only recipients that registered for this message type using one of 
    /// the Register methods.</summary>
    /// <typeparam name="TMessage">The type of message that will be sent.</typeparam>
    /// <param name="message">The message to send to registered recipients.</param>
    /// <param name="token">A token for a messaging channel. If a recipient registers using a token, and a sender sends a message using the same 
    /// token, then this message will be delivered to the recipient. Other recipients who did not use a token when registering (or who used a 
    /// different token) will not get the message. Similarly, messages sent without any token, or with a different token, will not be delivered to 
    /// that recipient.</param>
    public void Send<TMessage>(TMessage message, object token) where TMessage : IMessage {
      if(message == null) {
        throw new ArgumentNullException("message");
      }

      Type type = message.GetType();
      List<IRecipient> list;

      List<Type> keys = this.derivedRecipients.Keys.Where(key => key.IsAssignableFrom(type)).ToList();
      foreach(Type key in keys) {
        lock(this.derivedRecipients) {
          if(!this.derivedRecipients.TryGetValue(key, out list)) {
            continue;
          }

          if(list.Count <= 0) {
            continue;
          }

          /* Create copy before releasing lock */
          list = list.ToList();
        }

        foreach(IRecipient recipient in list) {
          if(recipient.IsAlive && (token == null || token.Equals(recipient.Token))) {
            recipient.Process(message);
          }
        }
      }

      lock(this.recipients) {
        if(!this.recipients.TryGetValue(type, out list)) {
          return;
        }

        /* Create copy before releasing lock */
        list = list.ToList();
      }

      foreach(IRecipient recipient in list) {
        if(recipient.IsAlive && (token == null || token.Equals(recipient.Token))) {
          recipient.Process(message);
        }
      }
    }

    /// <summary>Collects the dead references registered by the messenger and removes them from the recipients list. This method will be called 
    /// automatically if <see cref="CleanupInterval"/> is greater then 0.</summary>
    public void Cleanup() {
      Cleanup(this.recipients);
      Cleanup(this.derivedRecipients);
    }

    /// <summary>Disposes any resources held by this instance.</summary>
    public void Dispose() {
      this.DisposeManagedResources();
    }
    #endregion

    #region Protected methods
    /// <summary>Disposes all the managed resources that are held by this instance.</summary>
    protected virtual void DisposeManagedResources() {
      if(this.cleanupTimer != null) {
        this.cleanupTimer.Dispose();
        this.cleanupTimer = null;
      }
    }
    #endregion

    #region Private static methods
    /// <summary>Collects and removes the dead reference inside a dictionary.</summary>
    /// <param name="dictionary">The dictionary that must be analyzed.</param>
    /// <param name="key">The key that must be analyzed (optional).</param>
    private static void Cleanup(Dictionary<Type, List<IRecipient>> dictionary, Type key = null) {
      if(key == null) {
        lock(dictionary) {
          dictionary.Keys.ToList().ForEach(item => Cleanup(dictionary, item));
        }
      }
      else {
        lock(dictionary) {
          List<IRecipient> list = dictionary[key].Where(item => item.IsAlive).ToList();
          if(list.Count > 0) {
            dictionary[key] = list;
          }
          else {
            dictionary.Remove(key);
          }
        }
      }
    }
    #endregion

    #region Private classes
    /// <summary>Private class for storing recipient settings.</summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    private class Recipient<TMessage> : IRecipient {
      #region Instance variables
      /// <summary>The action that must be executed when a message is received.</summary>
      private Action<TMessage> action;

      /// <summary>The actual reference to the recipient.</summary>
      private WeakReference reference;
      #endregion

      #region Constructors
      /// <summary>Initializes a new instance of the <see cref="Recipient{TMessage}"/> class.</summary>
      /// <param name="owner">The owner or actual recipient.</param>
      /// <param name="action">The action that must be executed when a message is received.</param>
      /// <param name="token">The token that was used while sending the message.</param>
      public Recipient(object owner, Action<TMessage> action, object token = null) {
        this.reference = new WeakReference(owner);
        this.action = action;
        this.Token = token;
      }
      #endregion

      #region Properties
      /// <summary>Gets a value indicating whether the owner is still alive.</summary>
      public bool IsAlive {
        get { return this.reference != null && this.reference.IsAlive; }
      }

      /// <summary>Gets the owner (recipient).</summary>
      public object Owner {
        get { return this.IsAlive ? this.reference.Target : null; }
      }

      /// <summary>Gets the token for additinal message filtering.</summary>
      public object Token { get; private set; }

      /// <summary>Gets the action called when a message is send.</summary>
      public Delegate Action {
        get { return this.action; }
      }
      #endregion

      #region Public methods
      /// <summary>Processes the specified message.</summary>
      /// <param name="message">The message.</param>
      public void Process(object message) {
        if(message is TMessage) {
          this.action((TMessage)message);
        }
      }

      /// <summary>Marks this instance for deletion.</summary>
      public void MarkForDeletion() {
        this.reference = null;
      }
      #endregion
    }
    #endregion
  }
}
