using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TestEMS.Filters
{
    public class SessionRequiredAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if the session variable is set
            var employeeId = context.HttpContext.Session.GetString("EmployeeId");

            // If the session variable is null or empty, redirect to the login page
            if (string.IsNullOrEmpty(employeeId))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
        }
    }
}
