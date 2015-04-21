using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace _3DMapleSystem.Web.Infrastructure.Helpers
{
    //TODO Replace with 'Authorize' attribute in production mode
    public class AdminOnlyAttribute : AuthorizeAttribute
    {
        public string Message { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var result = new ViewResult();
            result.ViewName = "Login.cshtml";        
            result.MasterName = "_Layout.cshtml";    
            result.ViewBag.Message = this.Message;
            filterContext.Result = result;
        }
    }
}
