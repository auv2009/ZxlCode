using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace Zxl.MvcPro.Controllers
{
    public class DemoController : Controller
    {
        ILog _logger = LogManager.GetLogger("HomeController");

        public ActionResult Log4net()
        {
            _logger.Debug("Debug");
            _logger.Info("Info");
            _logger.Warn("Warn");
            _logger.Error("Error");
            _logger.Fatal("Fatal");

            return View();
        }
        
        public ActionResult FusionCharts()
        {
            return View();
        }
        public ActionResult GoogleChart()
        {
            return View();
        }

        public ActionResult My97DatePicker()
        {
            return View();
        }

        public ActionResult JQueryUITab()
        {
            return View();
        }
        public ActionResult JQueryDialog()
        {
            return View();
        }

        public ActionResult JQueryDataTable()
        {
            return View();
        }
        
    }
}
