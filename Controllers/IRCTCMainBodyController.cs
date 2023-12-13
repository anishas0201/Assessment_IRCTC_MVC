using Assessment_IRCTC_Revervation.DAL;
using Assessment_IRCTC_Revervation.Interface;
using Assessment_IRCTC_Revervation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Assessment_IRCTC_Revervation.Controllers
{
    public class IRCTCMainBodyController : Controller
    {
        MainBodyInterface IBody = new MainBodyClass();
        RegisterInterface IRegister = new RegisterClass();
        DropDownClass ddcls = new DropDownClass();
        public IActionResult Dashboard()
        {
            ViewBag.sess = HttpContext.Session.GetString("text");
            return View();
        }
        public IActionResult TicketBooking(int Id)
        {
            var train = ddcls.GetTrainList();
            ViewBag.train = new SelectList(train, "Id", "trainName");
            var trainNumbers = ddcls.GetTrainNumbers(Id);
            ViewBag.trainNumbers = new SelectList(trainNumbers, "Id", "trainNumber");
            var mail = ddcls.GetMailList();
            ViewBag.mail = new SelectList(mail, "emailId", "emailId");
            return View();
        }

        public JsonResult GetTrainNumbers(int Id)
        {
            var trainNumbers = ddcls.GetTrainNumbers(Id);
            return Json(trainNumbers);
        }
        public IActionResult TrainMaster()
        {
            return View();
        }
        public IActionResult TrainList()
        {
            List<TrainMaster> result = IBody.GetTrainList();
            return View(result);
        }

        public JsonResult SaveTrainDetails(TrainMaster roleuser)
        {
            return Json(IBody.SaveTrainDetails(roleuser));
        }


        public JsonResult DeleteTrain(int Id)
        {
            return Json(IBody.DeleteTrain(Id));
        }

        public ActionResult GetTrainbyId(int Id)
        {
            TrainMaster result = IBody.GetTrainbyId(Id);
            return View(result);
        }

        public JsonResult UpdateTrainDetails(TrainMaster tmaster)
        {
            return Json(IBody.UpdateTrainDetails(tmaster));
        }




        public JsonResult SaveTicket(TicketBookingUser form)
        {
            return Json(IBody.SaveTicket(form));
        }

        public IActionResult TicketList()
        {
            List<TicketBookingUser> result = IBody.GetTicketList();
            return View(result);
            // return View();
        }



    }
}
