using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Practices.Unity;

namespace Sogeti.Pattern.Web.Mvc.Unity
{
    /// <summary>
    ///   Default scope for unity
    /// </summary>
    public class UnityHttpLifetimeScope : LifetimeManager
    {
        /// <summary>
        ///   Gets a value from the scope container
        /// </summary>
        /// <returns> Value if found; otherwise null </returns>
        public override object GetValue()
        {
            return HttpContext.Current.Items[this];
        }

        /// <summary>
        ///   Remove a value from the scope container
        /// </summary>
        public override void RemoveValue()
        {
            HttpContext.Current.Items.Remove(this);
        }

        /// <summary>
        ///   Set/update a value in the scope container
        /// </summary>
        /// <param name="newValue"> The new value </param>
        public override void SetValue(object newValue)
        {
            HttpContext.Current.Items[this] = newValue;
        }

        /// <summary>
        ///   Dipose life time maanger
        /// </summary>
        public void Dispose()
        {
            RemoveValue();
        }

        /// <summary>
        ///   Dispose all lifetime managers in the current Request context and all associated services.
        /// </summary>
        public static void DisposeAll()
        {
            var scopes = new List<UnityHttpLifetimeScope>();
            foreach (var key in HttpContext.Current.Items.Keys)
            {
                var scope = key as UnityHttpLifetimeScope;
                if (scope == null)
                    continue;

                scopes.Add(scope);
                var value = HttpContext.Current.Items[key];
                if (value is IDisposable)
                    ((IDisposable) value).Dispose();
            }

            foreach (var scope in scopes)
            {
                scope.Dispose();
            }
        }
    }
}