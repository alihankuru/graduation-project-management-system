using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Deneme.Models;

namespace Deneme.Controllers
{
    public class JuryController : Controller
    {

        GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities();
        int dayint;
        public JsonResult Javascript(string Day)
        {


            int.TryParse(Day, out dayint);

            ViewData["day"] = dayint;
            var jury = db.DaysTable.Where(x => x.JuryID == 3 && x.DayNumber == dayint).FirstOrDefault();
            var presentaable = db.PresentationsTbl.Where(x => x.PresentationDate == dayint);
            return Json(new
            {
                result = jury.AvaibilityInfo
            }
            );
        }

        public ActionResult Filter(List<string> nn)
        {
            int.TryParse(nn[0], out dayint);
            var prsentation = db.PresentationsTbl.Where(x => x.PresentationDate == dayint).ToList();
            string[] cars = { "09:00-09:30", "09:30-10:00", "10:00-10:30", "10:30-11:00","11:00-11:30",
                              "11:30-12:00","12:00-12:30","12:30-13:00","13:00-13:30",
                              "13:30-14:00","14:00-14:30","14:30-15:00","15:00-15:30","15:30-16:00",
                              "16:00-16:30","16:30-17:00","17:00-17:30","17:30-18:00"};
            ViewBag.Cars = cars;
            return PartialView("Presentation", prsentation);
        }

        [HttpGet]
        public ActionResult Presentation()
        {
            string[] cars = { "09:00-09:30", "09:30-10:00", "10:00-10:30", "10:30-11:00","11:00-11:30",
                              "11:30-12:00","12:00-12:30","12:30-13:00","13:00-13:30",
                              "13:30-14:00","14:00-14:30","14:30-15:00","15:00-15:30","15:30-16:00",
                              "16:00-16:30","16:30-17:00","17:00-17:30","17:30-18:00"};
            ViewBag.Cars = cars;
            var prsentation = db.PresentationsTbl.ToList();
            return PartialView("Presentation", prsentation.ToList());
        }

        public ActionResult List(int? a)
        {
            var y = from d in db.PresentationsTbl select d;

            if (a != null)
            {
                y = y.Where(n => n.PresentationID == a);
            }
            return RedirectToAction("Jury", y.ToList());
        }

        public ActionResult Landing()
        {

            return View();

        }
        ///
        [HttpPost]
        public ActionResult Avability(List<string> Cars, List<string> day)
        {

            int value;

            bool isNumber = int.TryParse(day[0], out value);
            var jury = db.DaysTable.Where(x => x.JuryID == 1 && x.DayNumber == value).FirstOrDefault();
            string temp = String.Empty;
            if (isNumber)
            {

                if (Cars[4] == "2")
                {
                    temp += "11";
                    for (int i = 4; Cars.Count > i; i++)
                    {
                        if (i + 1 == Cars.Count)
                        {
                            temp += "0";
                            break;
                        }
                        if (Cars[i + 1] == "1")
                        {
                            temp += "1";
                            i++;
                        }
                        else
                        {
                            temp += 0;
                        }
                    }
                }///Cars[4] end

                if (Cars[3] == "2")
                {

                    string val = Cars[0] + Cars[1] + Cars[2];
                    if (val == "011")
                    {
                        temp += "01";
                    }
                    for (int i = 3; Cars.Count > i; i++)
                    {
                        if (i + 1 == Cars.Count)
                        {
                            temp += "0";
                            break;
                        }

                        if (Cars[i + 1] == "1")
                        {
                            temp += "1";
                            i++;
                        }
                        else
                        {
                            temp += 0;
                        }
                    }
                }///Cars[3] end

                if (Cars[2] == "2")
                {
                    temp += "00";
                    for (int i = 2; Cars.Count > i; i++)
                    {
                        if (i + 1 == Cars.Count)
                        {
                            temp += "0";
                            break;
                        }
                        if (Cars[i + 1] == "1")
                        {
                            temp += "1";
                            i++;
                        }
                        else
                        {
                            temp += 0;
                        }
                    }
                }///Cars[2] end

            } //if number end

            jury.AvaibilityInfo = temp;
            jury.DayNumber = value;
            db.SaveChanges();

            return RedirectToAction("Jury");

        }




        public ActionResult Jury()
        {
            string[] cars = { "09:00-09:30", "09:30-10:00", "10:00-10:30", "10:30-11:00","11:00-11:30",
                              "11:30-12:00","12:00-12:30","12:30-13:00","13:00-13:30",
                              "13:30-14:00","14:00-14:30","14:30-15:00","15:00-15:30","15:30-16:00",
                              "16:00-16:30","16:30-17:00","17:00-17:30","17:30-18:00"};



            if (dayint != 0)
            {

                var juryday = db.DaysTable.Where(x => x.JuryID == 3 && x.DayNumber == dayint).FirstOrDefault();
                ViewBag.Bit = juryday.AvaibilityInfo;
                ViewBag.Cars = cars;
                var presentation = db.PresentationsTbl.ToList();

                return View(presentation);
            }
            else
            {
                var jury = db.DaysTable.Where(x => x.JuryID == 3).FirstOrDefault();
                ViewBag.Bit = jury.AvaibilityInfo;
                ViewBag.Cars = cars;
                var presentation = db.PresentationsTbl.ToList();

                return View(presentation);
            }


        }



        public ActionResult Student()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}