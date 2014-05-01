using System;

namespace Enkoni.Framework.UI.Mvvm {
  /// <summary>Interface to define a Messenger.</summary>
  public interface IMessenger {
    /// <summary>Registers a recipient for a type of <typeparamref name="TMessage"/>. The action parameter will be executed when a corresponding 
    /// message is sent.
    /// <para>Registering a recipient does not create a hard reference to it, so if this recipient is deleted, no memory leak is caused.</para>
    /// </summary>
    /// <typeparam name="TMessage">The type of message that the recipient registers for.</typeparam>
    /// <param name="recipient">The recipient that will receive the messages.</param>
    /// <param name="action">The action that will be executed when a <typeparamref name="TMessage"/> is sent.</param>
    void Register<TMessage>(object recipient, Action<TMessage> action) where TMessage : IMessage;

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
    void Register<TMessage>(object recipient, Action<TMessage> action, bool includeDerivedMessages) where TMessage : IMessage;

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
    void Register<TMessage>(object recipient, Action<TMessage> action, bool includeDerivedMessages, object token) where TMessage : IMessage;

    /// <summary>Deregisters a message recipient for a given type of messages. Other message types will still be transmitted to the recipient (if it 
    /// registered for them previously).</summary>
    /// <typeparam name="TMessage">The type of messages that the recipient wants to unregister from.</typeparam>
    /// <param name="recipient">The recipient that must be unregistered.</param>
    void Deregister<TMessage>(object recipient) where TMessage : IMessage;

    /// <summary>Deregisters a message recipient for a given type of messages, for a given action. Other message types will still be transmitted to 
    /// the recipient (if it registered for them previously).</summary>
    /// <typeparam name="TMessage">The type of messages that the recipient wants to unregister from.</typeparam>
    /// <param name="recipient">The recipient that must be unregistered.</param>
    /// <param name="action">The action that must be unregistered for the recipient and for the message type <typeparamref name="TMessage"/>.</param>
    void Deregister<TMessage>(object recipient, Action<TMessage> action) where TMessage : IMessage;

    /// <summary>Deregisters a message recipient for a given type of messages, for a given action and a given token. Other message types will still 
    /// be transmitted to the recipient (if it registered for them previously). Other actions that have been registered for the message type 
    /// <typeparamref name="TMessage"/>, for the given recipient and other tokens (if available) will also remain available.</summary>
    /// <typeparam name="TMessage">The type of messages that the recipient wants to unregister from.</typeparam>
    /// <param name="recipient">The recipient that must be unregistered.</param>
    /// <param name="action">The action that must be unregistered for the recipient and for the message type <typeparamref name="TMessage"/>.</param>
    /// <param name="token">The token for which the recipient must be unregistered.</param>
    void Deregister<TMessage>(object recipient, Action<TMessage> action, object token) where TMessage : IMessage;

    /// <summary>Sends a message to registered recipients. The message will reach only recipients that registered for this message type using one of 
    /// the Register methods.</summary>
    /// <typeparam name="TMessage">The type of message that will be sent.</typeparam>
    /// <param name="message">The message to send to registered recipients.</param>
    void Send<TMessage>(TMessage message) where TMessage : IMessage;

    /// <summary>Sends a message to registered recipients. The message will reach only recipients that registered for this message type using one of 
    /// the Register methods.</summary>
    /// <typeparam name="TMessage">The type of message that will be sent.</typeparam>
    /// <param name="message">The message to send to registered recipients.</param>
    /// <param name="token">A token for a messaging channel. If a recipient registers using a token, and a sender sends a message using the same 
    /// token, then this message will be delivered to the recipient. Other recipients who did not use a token when registering (or who used a 
    /// different token) will not get the message. Similarly, messages sent without any token, or with a different token, will not be delivered to 
    /// that recipient.</param>
    void Send<TMessage>(TMessage message, object token) where TMessage : IMessage;
  }
}
