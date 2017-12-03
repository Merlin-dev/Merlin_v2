using Merlin.Communication;
using Merlin.Concurrent;
using Merlin.Threading;
using System.Linq;

/// <summary>
///
/// </summary>
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
            TaskManager.Initialize();
            TaskManager.RunAsync(() =>
            {
                CommunicationService.Initialize();
                //CommunicationService.Client.Send(null) //Say hi to GUI, or send it to hell, or what ever
                Main();
            });
        }

        private static void Main()
        {
            //Send thread status every second to GUI
            TaskManager.RunPeriodic(() =>
            {
                ThreadManager.UpdateThreadStates();
                CommunicationService.Client.Send(new RunningThreadInfoCollection
                {
                    ThreadInfo = ThreadManager.RunningThreads.ToArray()
                });
            }, "Thread Info", 1000);

            TaskManager.RunPeriodic(() =>
            {
                Debug.Log("Log");
                Debug.LogAssert("Assert");
                Debug.LogError("Error");
                Debug.LogException(new System.Exception());
                Debug.LogWarning("Warning");
                Debug.LogFormat("Test {0} formatting", "asd");
            }, "Debug testing", 5000);
            //TODO: More debugging states like: IsMounting etc... (generally character info, probably)
        }
    }
}