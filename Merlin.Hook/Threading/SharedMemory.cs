using Merlin.Concurrent;

namespace Merlin.Threading
{
    /// <summary>
    /// Class used as shared memory space between threads
    /// </summary>
    /// <remarks>
    /// It's plain <see cref="ConcurrentDictionary{TKey, TValue}"/> with nice wrapping around it
    /// </remarks>
    public static class SharedMemory
    {
        private static ConcurrentDictionary<string, object> _memory = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// Writes data to specific path in shared memory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path.</param>
        /// <param name="data">The data.</param>
        public static void Write<T>(string path, T data)
        {
            _memory[path] = data;
        }

        /// <summary>
        /// Reads the data from specified path in shared memory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static T Read<T>(string path)
        {
            return (T)_memory[path];
        }

        /// <summary>
        /// Deletes the data in specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static bool Delete(string path)
        {
            return _memory.Remove(path);
        }

        /// <summary>
        /// Checks if some data exists in shared memory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        ///   <c>true</c> if the specified path contains data; otherwise, <c>false</c>.
        /// </returns>
        public static bool Exists(string path)
        {
            return _memory.ContainsKey(path);
        }
    }
}