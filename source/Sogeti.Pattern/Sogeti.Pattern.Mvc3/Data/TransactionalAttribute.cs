using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Sogeti.Pattern.Data;

namespace Sogeti.Pattern.Mvc3.Data
{
    /// <summary>
    /// Used to add transaction support to an extension method
    /// </summary>
    /// <remarks>
    /// <para>
    /// Requires that an <see cref="IUnitOfWork"/> implementation have been registered in the IoC container.
    /// </para>
    /// <para>Will commit the UoW if no exception have been thrown.</para>
    /// </remarks>
    public class TransactionalAttribute : ActionFilterAttribute
    {
        private IUnitOfWork _uow;

        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _uow = DependencyResolver.Current.GetService<IUnitOfWork>();
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Called by the ASP.NET MVC framework after the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
            {
                _uow.SaveChanges();
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
