using System;
using System.Linq;
using System.Reflection;

namespace MVVMExample.Extensions
{
    public static class ObjectExtensions
    {
        #region Static members

        /// <summary>
        ///     Calls internal method from different assembly using reflection.
        /// </summary>
        /// <param name="obj">Object instance.</param>
        /// <param name="methodName">Internal method name.</param>
        /// <param name="args">Internal method arguments.</param>
        /// <returns>Internal method return.</returns>
        public static object InternalMethodCall<T>(this T obj, string methodName, params object[] args)
            where T : class
        {
            return obj.InternalMethodCall(typeof(T), methodName, args);
        }

        /// <summary>
        ///     Calls internal method from different assembly using reflection.
        /// </summary>
        /// <param name="obj">Object instance.</param>
        /// <param name="type">Required reflection type.</param>
        /// <param name="methodName">Internal method name.</param>
        /// <param name="args">Internal method arguments.</param>
        /// <returns>Internal method return.</returns>
        public static object InternalMethodCall(this object obj, Type type, string methodName, params object[] args)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            MethodInfo dynamicMethod;
            if (args.Any(a => a == null))
            {
                dynamicMethod = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            }
            else
            {
                dynamicMethod = type.GetMethod(methodName,
                                               BindingFlags.NonPublic | BindingFlags.Instance,
                                               null,
                                               args.Select(a => a.GetType()).ToArray(),
                                               null);
            }

            try
            {
                return dynamicMethod?.Invoke(obj, args);
            }
            catch (TargetInvocationException e)
            {
                // ReSharper disable once PossibleNullReferenceException
                throw e.InnerException;
            }
        }

        #endregion
    }
}