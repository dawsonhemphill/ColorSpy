using System;

namespace ColorSpy.Mediator
{
    public enum Message
    {
        MouseEnteredWindow,

        MouseLeftWindow,
    }

    /// <summary>
    /// Mediator Class to enable communication between view models.
    /// </summary>
    public sealed class Mediator
    {
        private readonly MultiDictionary<Message, Action<object>> internalList
            = new MultiDictionary<Message, Action<object>>();

        /// <summary>
        /// Gets public Instance of the mediator.
        /// </summary>
        public static Mediator Instance { get; } = new Mediator();

        /// <summary>
        /// Method for a view model to register to a message.
        /// </summary>
        /// <param name="callback">The Action to do on the message.</param>
        /// <param name="message">The message to perform an action on.</param>
        public void Register(Action<object> callback, Message message)
        {
            internalList.AddValue(message, callback);
        }

        /// <summary>
        /// Notify all listeners of a message of a new callback.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="args"> The object to send in the message.</param>
        public void NotifyColleagues(Message message, object args)
        {
            if (internalList.ContainsKey(message))
            {
                foreach (Action<object> callback in internalList[message])
                {
                    callback(args);
                }
            }
        }
    }
}