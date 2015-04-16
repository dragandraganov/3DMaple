using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _3DMapleSystem.Web.Infrastructure.Helpers
{
    public class HyphenatedRouteHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            requestContext.RouteData.Values["subCategoryName"] = requestContext.RouteData.Values["subCategoryName"].ToString().Replace("&", "-").Replace("%", "-");
            return base.GetHttpHandler(requestContext);
        }
    }
}
