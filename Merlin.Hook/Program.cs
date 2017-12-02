using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //Initialize task manager
            //Initialize communication with server/GUI
            Main();
        }

        private static void Main()
        {
            //Initialize some debugging stuff, like thread info packets etc...
        }
    }
}
