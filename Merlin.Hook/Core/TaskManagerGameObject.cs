using Merlin.Concurrent;

namespace Merlin.Hook.Core
{
    public class TaskManagerGameObject //: MonoBehaviour
    {
        public static void Create()
        {
            //Craete null object
            //Dont destroy on load
        }

        private void OnApplicationQuit()
        {
            //Clear crashreport (removes logs, and every trace of us in logs)
        }

        private void OnDisable()
        {
            //Clear crashreport (removes logs, and every trace of us in logs)
        }

        private void Start()
        {
        }

        private void OnEnable()
        {
        }

        private void Update()
        {
            ConcurrentTaskManager.Update();
        }

        private void OnDestroy()
        {
            //If object was deleted by mistake (for some reason) reinitialize it
            //Kill game (not sure about that one)
        }
    }
}