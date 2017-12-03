using Merlin.Concurrent;
using Microsoft.CSharp;
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
        private static ConcurrentList<Script> _scripts = new ConcurrentList<Script>();

        /// <summary>
        /// Gets the scripts.
        /// </summary>
        /// <value>
        /// The scripts.
        /// </value>
        public static Script[] Scripts => _scripts.ToArray();

        /// <summary>
        /// Clears the scripts.
        /// </summary>
        public static void ClearScripts()
        {
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

            //TODO: Check if param. nr.1 is referenced assemblies, i not use commended line bellow

            CompilerParameters options = new CompilerParameters(referencedAssemblies, outputPath);
            //cp.ReferencedAssemblies.AddRange(referencedAssemblies);
            options.GenerateExecutable = true;
            options.GenerateInMemory = false;

            compiler.CompileAssemblyFromSource(options, sources);

            //TODO: Probably send compilation result(s) back to GUI...
        }

        /// <summary>
        /// Executes the script.
        /// </summary>
        public static void ExecuteScript(/* Script info packet here */)
        {
            //TODO: impl.
        }
    }
}