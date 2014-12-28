using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace Zxl.MvcPro.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            System.Web.Hosting.AppDomainFactory fac;
            System.Web.Hosting.AppDomainInfo adi;
            System.Web.Hosting.ApplicationHost ah;
            System.Web.Hosting.ApplicationInfo ai;
            System.Web.Hosting.ApplicationManager am;
            System.Web.Hosting.AppManagerAppDomainFactory amadf;
            System.Web.Hosting.HostingEnvironment he;
            System.Web.Hosting.ISAPIRuntime isapiR;
            //System.Web.Hosting.ISAPIWorkerRequest isapiWr; // internal abstract class
            System.Web.HttpWorkerRequest httpWr;

            System.Web.HttpRuntime hr;

            System.Web.HttpContextBase hcb;
            System.Web.HttpRequestBase hreqb;
            System.Web.HttpResponseBase hresb;
            System.Web.HttpApplicationStateBase hasb;
            System.Web.HttpContext hc;
            System.Web.HttpRequest hreq;
            System.Web.HttpResponse hres;
            System.Web.HttpCookie httpcookie;
            System.Web.HttpCookieCollection hcc;

            


            //System.Web.HttpApplicationFactory; // internal class
            System.Web.HttpApplication ha;
            System.Web.HttpApplicationState has;
            System.Web.SessionState.HttpSessionState hss;
            System.Web.HttpUtility hu;
            System.Web.HttpServerUtility hsu;

            



            ViewBag.Message = "Welcome to ASP.NET MVC!";
            //using (PmsSvc.PmsServiceClient svc = new PmsSvc.PmsServiceClient())

            return View();
        }
        
        public ActionResult About()
        {
            return View();
        }
    }

    public enum PMS_eDB_Roles
    { 
        AdminRole = 1, // 系统管理员
        SupportRole = 3, // 运营和技术支持人员
        AsgRole = 4, // 客户服务人员
        PlanerRole = 5, //销售部门策划人员
        SolutionRole = 6,// Solution人员
        TestRole = 7 // 测试角色
    }
}
