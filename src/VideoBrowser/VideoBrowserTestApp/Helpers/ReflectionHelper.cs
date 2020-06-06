namespace VideoBrowserTestApp.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Defines the <see cref="ReflectionHelper" />.
    /// </summary>
    public static class ReflectionHelper
    {
        #region Methods

        /// <summary>
        /// The GetInstances.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <returns>The <see cref="IList{T}"/>.</returns>
        public static IList<T> GetInstances<T>()
        {
            return (from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.BaseType == (typeof(T)) && t.GetConstructor(Type.EmptyTypes) != null
                    select (T)Activator.CreateInstance(t)).ToList();
        }

        #endregion Methods
    }
}