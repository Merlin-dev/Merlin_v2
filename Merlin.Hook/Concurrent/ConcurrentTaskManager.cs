using Merlin.Threading;
using System;
using System.Threading;

namespace Merlin.Concurrent
{
    /// <summary>
    /// Task manager
    /// </summary>
    public static class ConcurrentTaskManager
    {
        public static ConcurrentQueue<ThreadedAction> PendingActions = new ConcurrentQueue<ThreadedAction>();

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize()
        {
            //Create game object and "hook" it
        }

        /// <summary>
        /// Runs the specified action in sync.
        /// </summary>
        /// <param name="action">The action.</param>
        public static void Run(Action action)
        {
            //Run in async
            RunAsync(action);

            //Now wait until the action is done
            while (PendingActions.Any((ThreadedAction a) => action == a.Action))
            {
                Thread.Sleep(1);
            }
        }

        /// <summary>
        /// Runs the action in async on game thread.
        /// </summary>
        /// <param name="action">The action.</param>
        public static void RunAsync(Action action)
        {
            PendingActions.Enqueue(new ThreadedAction
            {
                Action = action,
                ParentThread = Thread.CurrentThread
            });
        }

        /// <summary>
        /// Runs the action is async on separate thread.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="name">The thread name.</param>
        public static void RunAsync(ThreadStart action, string name)
        {
            Thread t = new Thread(action);
            ThreadManager.RegisterThread(t, Thread.CurrentThread, name);
            t.IsBackground = true;
            t.Start();
        }
        /// <summary>
        /// Runs action periodically on separate thread.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="name">The thread name.</param>
        /// <param name="timeout">The timeout.</param>
        public static void RunPeriodic(Action action, string name, int timeout)
        {
            RunAsync(delegate
            {
                while (true)
                {
                    try
                    {
                        action();
                    }
                    catch (ThreadAbortException)
                    {
                        throw;
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        Thread.Sleep(timeout);
                    }
                }
            }, name);
        }

        /// <summary>
        /// Executes any pending actions.
        /// </summary>
        public static void Update()
        {
            while (PendingActions.Any())
            {
                ThreadedAction ta = PendingActions.Peek();
                try
                {
                    ta.Action();
                }
                catch (ThreadAbortException)
                {
                    throw;
                }
                catch (Exception)
                {
                }
                finally
                {
                    PendingActions.Dequeue();
                }
            }
        }
        /// <summary>
        /// Action associated with specific thread
        /// </summary>
        public class ThreadedAction
        {
            /// <summary>
            /// The action to execute
            /// </summary>
            public Action Action { get; set; }

            /// <summary>
            /// The thread action will be executed on
            /// </summary>
            public Thread ParentThread { get; set; }
        }
    }
}