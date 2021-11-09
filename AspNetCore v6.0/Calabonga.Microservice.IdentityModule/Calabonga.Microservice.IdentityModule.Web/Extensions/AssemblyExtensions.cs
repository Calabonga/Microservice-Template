using System;
using System.Collections.Generic;
using System.Reflection;

namespace Calabonga.Microservice.IdentityModule.Web.Extensions
{
    /// <summary>
    /// Assembly helpers
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Returns types
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static List<Type> GetTypesAssignableFrom<T>(this Assembly assembly) => assembly.GetTypesAssignableFrom(typeof(T));

        private static List<Type> GetTypesAssignableFrom(this Assembly assembly, Type compareType)
        {
            var ret = new List<Type>();
            foreach (var type in assembly.DefinedTypes)
            {
                if (compareType.IsAssignableFrom(type) && compareType != type && !type.IsAbstract)
                {
                    ret.Add(type);
                }
            }
            return ret;
        }
    }
}
