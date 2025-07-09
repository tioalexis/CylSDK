using System;
using System.Threading.Tasks;
using UnityEngine;

namespace CylSDK.Utils
{
    /// <summary>
    /// A collection of extension methods for Awaitable and Awaitable&lt;T&gt; types.
    /// </summary>
    public static class AwaitableExtensions
    {
        /// <summary>
        /// Converts an Awaitable to a Task.
        /// </summary>
        /// <param name="a">The Awaitable instance to convert.</param>
        public static async Task AsTask(this Awaitable a)
        {
            await a;
        }

        /// <summary>
        /// Converts an Awaitable&lt;T&gt; to a Task&lt;T&gt;.
        /// </summary>
        /// <param name="a">The Awaitable&lt;T&gt; instance to convert.</param>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <returns>A Task&lt;T&gt; that represents the asynchronous operation.</returns>
        public static async Task<T> AsTask<T>(this Awaitable<T> a)
        {
            return await a;
        }
        
        /// <summary>
        /// Allows an Awaitable to be executed without awaiting it, and provides optional success and error callbacks.
        /// This is useful for fire-and-forget scenarios where you do not need to wait for the operation to complete.
        /// Exceptions will be logged to the console if no error handler is provided.
        /// </summary>
        /// <param name="a">The Awaitable instance to fire and forget.</param>
        /// <param name="onSuccess">The action to execute on successful completion of the Awaitable.</param>
        /// <param name="onError">The action to execute if an exception occurs during the Awaitable's execution.</param>
        public static void FireAndForget(this Awaitable a, Action onSuccess = null, Action<Exception> onError = null)
        {
            a.AsTask().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (onError != null)
                    {
                        onError(task.Exception);
                    }
                    else
                    {
                        Debug.LogException(task.Exception);
                    }
                }
                else
                {
                    onSuccess?.Invoke();
                }
            });
        }
        
        /// <summary>
        /// Allows an Awaitable&lt;T&gt; to be executed without awaiting it, and provides optional success and error callbacks.
        /// This is useful for fire-and-forget scenarios where you do not need to wait for the operation to complete.
        /// Exceptions will be logged to the console if no error handler is provided.
        /// </summary>
        /// <param name="a">The Awaitable&lt;T&gt; instance to fire and forget.</param>
        /// <param name="onSuccess">The action to execute on successful completion of the Awaitable&lt;T&gt;.</param>
        /// <param name="onError">The action to execute if an exception occurs during the Awaitable&lt;T&gt;'s execution.</param>
        /// <typeparam name="T">The type of the result returned by the Awaitable&lt;T&gt;.</typeparam>
        public static void FireAndForget<T>(this Awaitable<T> a, Action<T> onSuccess = null, Action<Exception> onError = null)
        {
            a.AsTask().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (onError != null)
                    {
                        onError(task.Exception);
                    }
                    else
                    {
                        Debug.LogException(task.Exception);
                    }
                }
                else
                {
                    onSuccess?.Invoke(task.Result);
                }
            });
        }
    }
}