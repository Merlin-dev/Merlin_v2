using Merlin.Concurrent;

namespace Merlin.Hook
{
    /// <summary>
    /// Entry class for injector, responsible for starting all services etc...
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point for injector.
        /// </summary>
        public static void Run()
        {
            ConcurrentTaskManager.Initialize();
            //Initialize communication with server/GUI
            Main();
        }

        private static void Main()
        {
            //Initialize some debugging stuff, like thread info packets etc...
        }
    }
}