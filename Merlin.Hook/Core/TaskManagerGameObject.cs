using Merlin.Concurrent;
using System.Diagnostics;
using UnityEngine;

namespace Merlin.Core
{
    /// <summary>
    /// GameObject responsible for relaying Update() to our code
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class TaskManagerGameObject : MonoBehaviour
    {
        private static bool UserQuit;
        public static void Create()
        {
            DontDestroyOnLoad(new GameObject().AddComponent<TaskManagerGameObject>());
        }

        private void OnApplicationQuit()
        {
            CrashReport.RemoveAll();
            UserQuit = true;
        }

        private void OnDisable()
        {
            CrashReport.RemoveAll();
            UserQuit = true;
        }

        private void Start()
        {
            CrashReport.RemoveAll();
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
            if (!UserQuit)
            {
                Create();
                return;
            }

            //If something killed us, then we kill it
            Process.GetCurrentProcess().Kill();
        }
    }
}