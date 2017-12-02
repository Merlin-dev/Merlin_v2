using Merlin.Communication;
using Merlin.Concurrent;
using Merlin.Threading;
using System.Linq;

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
            ConcurrentTaskManager.RunAsync(() =>
            {
                CommunicationService.Initialize();
                //CommunicationService.Client.Send(null) //Say hi to GUI, or send it to hell, or what ever
                Main();
            });
        }

        private static void Main()
        {
            //Send thread status every second to GUI
            ConcurrentTaskManager.RunPeriodic(() =>
            {
                ThreadManager.UpdateThreadStates();
                CommunicationService.Client.Send(new RunningThreadInfoCollection
                {
                    ThreadInfo = ThreadManager.RunningThreads.ToArray()
                });
            }, "Thread Info", 1000);
            //TODO: More debugging states like: IsMounting etc... (generally character info, probably)
        }
    }
}