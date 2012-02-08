using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sogeti.Pattern.DomainEvents;

namespace Sogeti.Pattern.Mvc3.Filters
{
    /// <summary>
    /// Catches errors in MVC applications and displays a proper error page.
    /// </summary>
    /// <remarks>
    /// <para>Looks for error pages in the "Views\Errors" folder. The view name should correspond to the names in <see cref="HttpStatusCode"/> enum.
    /// Will also look in "Views\Shared" if the view is not found in "Views\Errors". Will look for a view named "General" if a specific one is
    /// not found.</para>
    /// <para>
    /// Publishes the domain event <see cref="UncaughtException"/> if the error is for an exception.
    /// </para>
    /// <para>The views should use the standard <see cref="HandleErrorInfo"/> view model that the default implementation uses.</para>
    /// <para>Remember to add the filter in global.asax (replace the current error filter)</para>
    /// </remarks>
    public class ErrorHandlerFilter : FilterAttribute, IExceptionFilter
    {
        #region IExceptionFilter Members

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
                return;

            var statusCode = (int) HttpStatusCode.InternalServerError;
            if (filterContext.Exception is HttpException)
            {
                statusCode = ((HttpException) filterContext.Exception).GetHttpCode();
            }
            /*
            else if (filterContext.Exception is UnauthorizedAccessException)
            {
                //to prevent login prompt in IIS
                // which will appear when returning 401.
                statusCode = (int)HttpStatusCode.Forbidden;
            }
            */

            if (filterContext.Exception != null)
                DomainEventDispatcher.Current.Dispatch(new UncaughtException(filterContext.Exception));

            var result = CreateActionResult(filterContext, statusCode);
            filterContext.Result = result;

            // Prepare the response code.
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = statusCode;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }

        #endregion

        /// <summary>
        /// Creates the action result that are returned.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <param name="statusCode">The status code.</param>
        /// <returns></returns>
        protected virtual ActionResult CreateActionResult(ExceptionContext filterContext, int statusCode)
        {
            var ctx = new ControllerContext(filterContext.RequestContext, filterContext.Controller);
            var statusCodeName = ((HttpStatusCode) statusCode).ToString();

            var viewName = SelectFirstView(ctx,
                                           string.Format("~/Views/Error/{0}.cshtml", statusCodeName),
                                           "~/Views/Error/General.cshtml",
                                           statusCodeName,
                                           "Error");

            var controllerName = (string) filterContext.RouteData.Values["controller"];
            var actionName = (string) filterContext.RouteData.Values["action"];
            var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
            var result = new ViewResult
                             {
                                 ViewName = viewName,
                                 ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                             };
            result.ViewBag.StatusCode = statusCode;
            return result;
        }

        protected string SelectFirstView(ControllerContext ctx, params string[] viewNames)
        {
            return viewNames.First(view => ViewExists(ctx, view));
        }

        protected bool ViewExists(ControllerContext ctx, string name)
        {
            var result = ViewEngines.Engines.FindView(ctx, name, null);
            return result.View != null;
        }
    }
}