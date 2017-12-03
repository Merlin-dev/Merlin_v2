using Merlin.Concurrent;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

namespace Merlin.Script
{
    /// <summary>
    /// Class, responsible for managing scripts, also supports on demand/remote compilation
    /// </summary>
    public class ScriptManager
    {
        private static ConcurrentList<IScript> _scripts = new ConcurrentList<IScript>();

        /// <summary>
        /// Gets the scripts.
        /// </summary>
        /// <value>
        /// The scripts.
        /// </value>
        public static IScript[] Scripts => _scripts.ToArray();

        /// <summary>
        /// Clears the scripts.
        /// </summary>
        public static void ClearScripts()
        {
            foreach (IScript script in _scripts)
            {
                script.OnDestroy();
            }
            _scripts.Clear();
        }

        /// <summary>
        /// Compiles the script.
        /// </summary>
        /// <param name="outputPath">The output path.</param>
        /// <param name="referencedAssemblies">The referenced assemblies.</param>
        /// <param name="sources">The sources.</param>
        public static void CompileScript(string outputPath, string[] referencedAssemblies, string[] sources)
        {
            //Create C# compiler and compile to .NET 3.5
            CodeDomProvider compiler = new CSharpCodeProvider(new Dictionary<string, string>
            {
                {"CompilerVersion", "v3.5" }
            });

            CompilerParameters options = new CompilerParameters(referencedAssemblies, outputPath);
            options.GenerateExecutable = true;
            options.GenerateInMemory = false;

            compiler.CompileAssemblyFromSource(options, sources);

            //TODO: Probably send compilation result(s) back to GUI...
        }

        /// <summary>
        /// Updates the scripts.
        /// </summary>
        public static void Update()
        {
            //NOTE: maybe execute in parallel
            foreach (IScript script in _scripts)
            {
                try
                {
                    script.OnUpdate();
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }

        /// <summary>
        /// Executes the script.
        /// </summary>
        public static void ExecuteScript(/* Script info packet here */)
        {
            //script.OnStart();
            //_scripts.Add(script);
            //TODO: impl.
        }
    }
}