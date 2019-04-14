using System.Collections.Generic;
using System.Web.Mvc;
using static System.Web.HttpContext;

namespace RAC.BusinessLogic
{
    public class PageHistoryAttribute : ActionFilterAttribute
    {
        /* An ActionFilter is a filter than can be applied to specific controller actions to be executed whenever said
         * action is executed. This allows code to be written that checks pages as they are being loaded by the user.
         * This ActionFilter adds pages the user has viewed to a history stack. This allows the users to navigate through
         * history within the RAC application.
         *
         * To assign an action this filter, add the `[PageHistory]` attribute to the method.
         *
         * See also:
         * HomeController.PreviousPage()
         * Global.asax.cs
         */

        public override void OnActionExecuting(ActionExecutingContext actionExecutingContext)
        {
            /*
             * Executes whenever an attributed action is executed. Reads the controller and action that was executed,
             * then adds it to the history stack. Handles if the stack is `null`, empty, or if the page being viewed is
             * already the newest page in the stack.
             *
             * See also:
             * HomeController.PreviousPage()
             * Global.asax.cs
             */

            // Gets the page viewed in this format: {ControllerName}/{ActionName}.
            // e.g. Account/Login
            string pageName =
                $"{actionExecutingContext.ActionDescriptor.ControllerDescriptor.ControllerName}/{actionExecutingContext.ActionDescriptor.ActionName}";

            // Handle ID's being passed within the URL
            if (actionExecutingContext.ActionParameters.Keys.Contains("Id"))
            {
                pageName += $"/{actionExecutingContext.ActionParameters["Id"]}";
            }

            // Check if the stack is `null` before attempting to push a page to the stack
            if ((Stack<string>) Current.Session["historyStack"] == null)
            {
                Current.Session["historyStack"] = new Stack<string>();

                // Adds the current page to the stack in case the user has navigated directly to a page with
                // a back button on it.
                ((Stack<string>) Current.Session["historyStack"]).Push(pageName);
            }
            else
            {
                var historyStack = (Stack<string>) Current.Session["historyStack"];

                // Checks if either the stack is empty, or the most recent page is the same as the current page.
                // The empty check executes first, preventing an exception from being thrown in the case of
                // peeking an empty stack. Empty stacks can occur if the user is mixing use of the internal
                // back button with the browsers back button.
                if (historyStack.Count == 0 || !historyStack.Peek().Equals(pageName))
                {
                    historyStack.Push(pageName);
                }
            }
        }

        public static bool HasHistory()
        {
            /*
             * Checks if the user has any history in their session. The history stack will always have 1, as the
             * page the user begins their session on is added to their history.
             *
             * See also:
             * _Layout.cshtml
             */

            return ((Stack<string>) Current.Session["historyStack"]).Count != 1;
        }
    }
}