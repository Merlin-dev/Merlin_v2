using Merlin.Communication;
using Merlin.Concurrent;
using System.Linq;
using System.Threading;

namespace Merlin.Threading
{
    /// <summary>
    /// Class responsible for managing all threads created for use of parallel execution
    /// </summary>
    public static class ThreadManager
    {
        /// <summary>x
        /// The list of currently running threads
        /// </summary>
        public static ConcurrentList<ThreadInfo> RunningThreads = new ConcurrentList<ThreadInfo>();

        /// <summary>
        /// List of threads created by the hook with their IDs as keys
        /// </summary>
        public static ConcurrentDictionary<int, Thread> ThreadMap = new ConcurrentDictionary<int, Thread>();

        /// <summary>
        /// Aborts the thread if specific ID.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public static void Abort(int id)
        {
            if (!ThreadMap.ContainsKey(id))
                return;

            ThreadMap[id].Abort();
        }

        /// <summary>
        /// Aborts all threads.
        /// </summary>
        public static void AbortAll()
        {
            int[] ids = ThreadMap.Keys;
            for (int i = 0; i < ids.Length; i++)
            {
                Abort(ids[i]);
            }
        }

        /// <summary>
        /// Executes the command on specified thread.
        /// </summary>
        /// <param name="command">The command.</param>
        public static void ExecuteThreadCommand(ThreadCommand command)
        {
            if (command.Command == "Abort")
                Abort(command.ID);
        }

        /// <summary>
        /// Registers the thread.
        /// </summary>
        /// <param name="thread">The thread.</param>
        /// <param name="parent">The parent thread.</param>
        /// <param name="name">The name of thread.</param>
        public static void RegisterThread(Thread thread, Thread parent, string name)
        {
            //Check if thread has no parent or has parent managed by us
            if (parent == null || RunningThreads.Any((ThreadInfo i) => i.ManagedThreadId == parent.ManagedThreadId))
            {
                RunningThreads.Add(new ThreadInfo
                {
                    ManagedThreadId = thread.ManagedThreadId,
                    ParentManagedThreadId = parent == null ? -1 : parent.ManagedThreadId,
                    Name = name
                });

                ThreadMap[thread.ManagedThreadId] = thread;
            }
        }

        /// <summary>
        /// Updates the thread states.
        /// </summary>
        public static void UpdateThreadStates()
        {
            //Update state of threads sent to GUI
            foreach (ThreadInfo info in RunningThreads)
            {
                if (ThreadMap[info.ManagedThreadId].IsAlive)
                {
                    info.State = "Running";
                }
                else
                {
                    info.State = "Stopped";
                }
            }

            //Remove all stopped threads from Running list
            RunningThreads.RemoveAll((ThreadInfo i) => !ThreadOrChildIsAlive(i.ManagedThreadId));
        }

        /// <summary>
        /// Checks if the thread or child is alive.
        /// </summary>
        /// <param name="id">ID of thread.</param>
        private static bool ThreadOrChildIsAlive(int id)
        {
            return ThreadMap[id].IsAlive ? true : (from i in RunningThreads where i.ParentManagedThreadId == id select i).Any((ThreadInfo i) => ThreadOrChildIsAlive(i.ManagedThreadId));
        }
    }
}