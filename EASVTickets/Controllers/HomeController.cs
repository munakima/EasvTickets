using BLL.WebApi.Gateway.Common;
using EASVTickets.Models;
using Elements.DTO;
using PDFManager.Common;
using System.Web.Mvc;

namespace EASVTickets.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var gateway = BLLGatewayFactory.CreateOrGet();
            var model = new EventListModel()
            {
                Events = gateway.GetFutureEvents()
            };
            return View(model);
        }

        public ActionResult SingleEvent(int id)
        {
            var gateway = BLLGatewayFactory.CreateOrGet();
            var model = gateway.GetEvent(id);
            return View(model);
        }

        public ActionResult SingleTicket(int id)
        {
            var gateway = BLLGatewayFactory.CreateOrGet();
            var model = gateway.GetEvent(id);
            return View(model);
        }
        public ActionResult pdfTest(int id)
        {
            var gateway = BLLGatewayFactory.CreateOrGet();
            var model = gateway.GetEvent(id);
            PDFGenerator p = new PDFGenerator();
            p.formatePDF(model);
            ViewBag.Message = "Your application description page.";
            return View();
        }
    }
}