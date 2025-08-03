// 代码生成时间: 2025-08-03 23:58:29
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationSystem
{
    /// <summary>
    /// A simple notification service that can be used to send messages to subscribers.
    /// </summary>
    public class MessageNotificationService
    {
        private readonly List<IMessageSubscriber> _subscribers = new List<IMessageSubscriber>();

        /// <summary>
        /// Adds a subscriber to the notification system.
        /// </summary>
        /// <param name="subscriber">The subscriber to add.</param>
        public void AddSubscriber(IMessageSubscriber subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));
            _subscribers.Add(subscriber);
        }

        /// <summary>
        /// Removes a subscriber from the notification system.
        /// </summary>
        /// <param name="subscriber">The subscriber to remove.</param>
        public void RemoveSubscriber(IMessageSubscriber subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));
            _subscribers.Remove(subscriber);
        }

        /// <summary>
        /// Notifies all subscribers with the given message.
        /// </summary>
        /// <param name="message">The message to send.</param>
        public async Task NotifySubscribersAsync(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException("Message cannot be null or whitespace.", nameof(message));

            foreach (var subscriber in _subscribers)
            {
                await subscriber.ReceiveMessageAsync(message); // Assuming async method
            }
        }
    }

    /// <summary>
    /// Interface for message subscribers.
    /// </summary>
    public interface IMessageSubscriber
    {
        Task ReceiveMessageAsync(string message);
    }
}
