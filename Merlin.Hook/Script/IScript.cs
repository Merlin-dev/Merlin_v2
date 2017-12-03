namespace Merlin.Script
{
    /// <summary>
    /// The base class for script, simmilar to <see cref="UnityEngine.MonoBehaviour"/>
    /// </summary>
    public interface IScript
    {
        /// <summary>
        /// Called when script is loaded, called before first update.
        /// </summary>
        void OnStart();
        /// <summary>
        /// Called every Unity update cycle.
        /// </summary>
        void OnUpdate();
        /// <summary>
        /// Called when script instance is destroyed on unloaded.
        /// </summary>
        void OnDestroy();
    }
}