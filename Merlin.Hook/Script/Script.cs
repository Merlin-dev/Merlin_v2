namespace Merlin.Script
{
    /// <summary>
    /// The script itself
    /// </summary>
    public class Script
    {
        /*
         * Ideas
         *
         * One "EntryPoint" ex. Main() will be found through reflection or by searching for classes based on some interface, and started when script is loaded
         * no other methods will be required to be executed at run...
         *
         * or
         *
         * Unity like script (simmilar to v1)
         *
         * will have methods, which will use reflection to be found or just plain virtual methods..., (so we don't need to specify blank methods)
         * Start(); Update(); Destroy();
         * The reflection will search only in classes based on IScript for example
         *
         *
         * nr.1
         * - pros
         *  Faster to implement
         *  Less scalable...
         *  Will use TaskManager Heavily
         *  will run in parallel as periodic script / service
         *
         * - cons
         *  too much different from v1
         *  less scalable...
         *  not so intuitive
         *  would have fixed refresh rate, based on settings, when created (RunPeriodic())
         *
         * nr.2
         * - pros
         *  Harder to implement
         *  Good scaling
         *  Intuitive
         *  Could use TaskManager for long running tasks etc...
         *  nr.1 could be implemented in Start() method, so scripts could be written both as nr.1 and nr.2
         *
         * - cons
         *  Task manager will be "pointless" (for internal use only)
         *  harder to run "realtiime" with more laggy scripts
         *
         *
         *
         *  nr.2 would require us to implement something like RunPeriodic, but run only once, since async isn't 100%....
         *  it runs async in terms of current script, not unrelated to GameLoop....
         */
    }
}