using Deneme.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Group.Models;

using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium;
using Proposal.Models;

namespace Deneme.Controllers
{
    public class LoginController : Controller
    {

        GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities();
        public ActionResult orionLogin()
        {


            Session.Abandon();
            FormsAuthentication.SignOut();
            return View();
        }





        [HttpPost]
        public ActionResult orionLogin(User user)
        {
            int number;
            ChromeDriver driver;

            try
            {

                var options = new ChromeOptions();
                options.AddArgument("--window-position=-32000,-32000");
                options.AddArgument("headless");
                ChromeDriverService service = ChromeDriverService.CreateDefaultService();


                driver = new ChromeDriver(service, options);

                driver.Navigate().GoToUrl("https://orion.iku.edu.tr/irj/servlet/prt/portal/prtroot/pcd!3aportal_content!2fkultur!2fKulturMobile!2fFiori?sap-config-mode=true");

                driver.FindElement(By.Id("logonuidfield")).SendKeys(user.Username);
                driver.FindElement(By.Id("logonpassfield")).SendKeys(user.Password);
                var Buttons = driver.FindElements(By.CssSelector("input[type='submit'][value='Giris / Log In']"));


                foreach (var link in Buttons)
                {
                    link.Click();
                }


                if (driver.PageSource.Contains("Home"))
                {


                    using (GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities())
                    {

                        if (db.User.Any(x => x.Username == user.Username))
                        {

                        }
                        else
                        {
                            bool success = int.TryParse(user.Username, out number);
                            if (success==true)
                            {
                                user.Password = Crypto.Hash(user.Password);


                                db.User.Add(user);
                                user.Role = "Student";
                                db.SaveChanges();
                            }
                            else
                            {
                                user.Password = Crypto.Hash(user.Password);


                                db.User.Add(user);
                                user.Role = "Professor";
                                db.SaveChanges();

                            }

                        }
                    }



                    FormsAuthentication.SetAuthCookie(user.Username, false);

                    Session["ID"] = user.Username;

                    driver.Quit();
                    return RedirectToAction("LoginSucces", "Login");
                }
                else
                {

                    var msg = "User Authentication Failed";
                    driver.Quit();
                    TempData["ErrorMessage"] = msg;

                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return RedirectToAction("orionLogin", "Login");


        }


        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("orionLogin", "Login");

        }


        public ActionResult LoginSucces()
        {
            return View();
        }










    }
}