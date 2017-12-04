using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merlin.Hook.Helpers
{
    public static class CollectionsExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> instance, Action<T> action)
        {
            foreach (T obj in instance)
            {
                action(obj);
            }
        }
    }
}
