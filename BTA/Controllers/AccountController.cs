using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using BTA.Models;
using System.Web.Security;
using System.Diagnostics;

using System.Web.Helpers;
using System.Globalization;

namespace BTA.Controllers
{

    public class AccountController : Controller
    {


        public AccountController()
        {
            db = new Entities();
        }
        private void PopuniDrzave()
        {
            SelectList items = Admin.PopuniImeDrzava();
            if (items != null)
            {
                ViewBag.drzave = items;
            }
        }
        private void PopuniGradove()
        {
            SelectList items = Admin.PopuniImeGradova();
            if (items != null)
            {
                ViewBag.gradove = items;
            }
        }
        private void PopuniTipPrevoza()
        {
            SelectList items = Admin.PopuniTipovePrevoza();
            if (items != null)
            {
                ViewBag.tipprevoza = items;
            }
        }
        private void PopuniTipSmestaja()
        {
            SelectList items = Admin.PopuniTipoveSmestaja();
            if (items != null)
            {
                ViewBag.tipsmestaja = items;
            }
        }
        private void PopuniProvajdere()
        {
            SelectList items = Admin.PopuniProvajdere();
            if (items != null)
            {
                ViewBag.provajderi = items;
            }
        }
        private void PopuniSmestaje()
        {
            SelectList items = Admin.PopuniSmestaje();
            if (items != null)
            {
                ViewBag.smestaji = items;
            }
        }
        private void PopuniSifreGradova1()
        {
            SelectList items = Admin.PopuniSifreGradova();
            if (items != null)
            {
                ViewBag.sifragradova = items;
            }
        }
        private void PopuniSifreKategorija()
        {
            SelectList items = Admin.PopuniSifreGradskeKategorije();
            if (items != null)
            {
                ViewBag.sifrekategorije = items;
            }
        }
        private void PopuniSifreKorisnika()
        {
            SelectList items = Admin.PopuniSifreUsera();
            if (items != null)
            {
                ViewBag.sifreusera = items;
            }
        }
        private Entities db = new Entities();
        // GET: Login

        public ActionResult StartPage()
        {

            return View();
        }
        public ActionResult LogInView(LogInViewModel korisnik)
        {

            if (ModelState.IsValid)
            {

                {
                    var obj = db.Users.Where(a => a.UserName.Equals(korisnik.UserName) && a.Password.Equals(korisnik.Password)).FirstOrDefault();//kad ne nadje firstordefault vraca null
                    if (obj == null)
                    {
                        korisnik.LoginError = "Please enter correct data ";
                        return View("StartPage", korisnik);
                    }
                    else
                    {
                        Session["iduser-a"] = obj.UserID;
                        Session["korisnik"] = obj.UserName;
                        Session["rola"] = obj.RoleID;
                        return RedirectToAction("UserPage");

                    }


                }

            }
            return View();
        }
        public ActionResult RegisterPage()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]


        public ActionResult RegisterPage(User korisnik)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(x => x.UserName == korisnik.UserName || x.Email == korisnik.Email))//lamda zapis koji proverava da li postoji vec username ili email
                {
                    ViewBag.DuplicateMessage = "User name or e-mail are already taken";
                    return View();
                }
                else
                {
                    korisnik.RoleID = 2;
                    Random r = new Random();//klasa koja generise random broj
                    korisnik.UserID = r.Next();//tako generisemo id korisnika


                    db.Users.Add(korisnik);
                    db.SaveChanges();
                    ViewBag.SuccessMessage = "Registration was succsessfull. ";
                    return View("RegisterPage");
                }

            }
            return View();
        }
        public ActionResult UserPage()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("StartPage", "Account");
        }
        public ActionResult LifeInDiffrentCountries()
        {
            PopuniSifreKategorija();
            return View();
        }
        public ActionResult AddInformationAboutLifeInTheCity()
        {
            PopuniSifreGradova1();
            PopuniSifreKategorija();
            PopuniGradove();
            return View();
        }
        [HttpPost]

        public ActionResult AddInformationAboutLifeInTheCity(FormCollection info)
        {
            PopuniSifreKategorija();
            PopuniSifreGradova1();
            PopuniGradove();
            if (ModelState.IsValid)
            {
                CityInfoByCategory i = new CityInfoByCategory();
                i.CityID = int.Parse(info["ListaGradova"]);
                i.CityCategoryID = int.Parse(info["kategorije"]);
                i.Info = info["Info"];
                i.Date = DateTime.Today;
                i.UserID = int.Parse(Session["iduser-a"].ToString());
                Admin.DodajCityCategoryInf(i);
                return View();
            }
            return View();
        }
        public JsonResult GetSearchValue(string search)
        {
            Entities db = new Entities();
            List<CityModel> allsearch = db.Cities.Where(x => x.CityName.Contains(search)).Select(x => new CityModel
            {
                CityID = x.CityID,
                CityName = x.CityName
            }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetSearchValue1(string search)
        {
            Entities db = new Entities();
            List<AccommodationModel> allsearch = db.Accommodations.Where(x => x.Name.Contains(search)).Select(x => new AccommodationModel
            {
                AccommodationID = x.AccommodationID,
                Name = x.Name
            }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public ActionResult GetCityInfo(CityModel cityModel)
        {
            if (db.Cities.Any(x => x.CityName == cityModel.CityName && x.Lat == cityModel.Lat && x.Lng == cityModel.Lng))
            {

                return View("LifeInDiffrentCountries");
            }
            else
            {
                if (db.Countries.Any(x => x.CountryName == cityModel.CountryName))
                {
                    City a = new City();
                    a.CityName = cityModel.CityName;
                    a.Lat = cityModel.Lat;
                    a.Lng = cityModel.Lng;
                    a.CountryID = (from b in db.Countries where b.CountryName == cityModel.CountryName select b.CountryID).FirstOrDefault();
                    Random R = new Random();
                    a.CityID = R.Next();
                    Admin.DodajGrad(a);
                }
                else
                {

                    Country c = new Country();
                    c.CountryName = cityModel.CountryName;
                    Random R = new Random();
                    c.CountryID = R.Next();
                    Admin.DodajDrzavu(c);
                    Debug.WriteLine(cityModel.CityName);
                    City d = new City();
                    d.CityName = cityModel.CityName;
                    d.Lat = cityModel.Lat;
                    d.Lng = cityModel.Lng;

                    d.CityID = R.Next();
                    d.CountryID = c.CountryID;
                    Admin.DodajGrad(d);
                }
            }



            return View();
        }

        public ActionResult ChooseThebestAccomodation()

        {
            PopuniSifreGradova1();
            PopuniDrzave();
            PopuniGradove();
            PopuniProvajdere();
            PopuniTipPrevoza();
            PopuniTipSmestaja();
            PopuniSmestaje();
            return View();
        }
        public ActionResult Accommodations()
        {
            PopuniTipSmestaja();
            PopuniSifreGradova1();
            PopuniDrzave();
            PopuniGradove();
            PopuniProvajdere();
            PopuniTipPrevoza();
            PopuniSmestaje();
            return View();
        }
        [HttpPost]
        public ActionResult Accomodations(FormCollection smestaj)
        {
            PopuniTipSmestaja();
            PopuniSifreGradova1();
            PopuniDrzave();
            PopuniGradove();
            PopuniProvajdere();
            PopuniTipPrevoza();
            PopuniSmestaje();
            if (ModelState.IsValid)
            {
                Accommodation nov = new Accommodation();
                Random r = new Random();
                nov.AccommodationID = r.Next();
                nov.Name = smestaj["Name"];
                nov.Address = smestaj["Address"];
                nov.Email = smestaj["Email"];
                nov.Phone = smestaj["Phone"];
                nov.CityID = int.Parse(smestaj["ListaGradova"]);
                nov.AccCategoryID = int.Parse(smestaj["ListaTipovaSmestaja"]);
                nov.Description = smestaj["Description"];


                Admin.DodajAccomodation(nov);
                TempData["Naziv"] = nov.Name;

            }
            return View("ChooseTheBestAccomodation");
        }
        [HttpPost]
        public ActionResult AccomodationDetails(FormCollection accomodationdetail)
        {
            AccommodationModel prikazi = new AccommodationModel();
            prikazi = Admin.PrikaziAccomodationPoSifri(int.Parse(accomodationdetail["listaSmestaja"]));
            using (Entities db = new Entities())
            {

                prikazi.gradovi = (from a in db.Cities select a).ToList();
                prikazi.tipsmestaja = (from s in db.AccCategories select s).ToList();
                return View("AccommodationShowing", prikazi);
            }
        }
        [HttpPost]
        public ActionResult AccommodationShowing(Accommodation smestaj)
        {

            if (ModelState.IsValid)
            {
                Admin.IzmeniAccomodation(smestaj);
                TempData["Izmena"] = smestaj.Name;

                return RedirectToAction("ChooseThebestAccomodation", "Account");
            }
            return View(smestaj);

        }


        public ActionResult EnjoyYourTravel()
        {
            PopuniTipSmestaja();
            PopuniSifreGradova1();
            PopuniDrzave();
            PopuniGradove();
            PopuniProvajdere();
            PopuniTipPrevoza();
            PopuniSmestaje();
            return View();
        }
        [HttpPost]
        public ActionResult EnjoyYourTravel(FormCollection transportaiton)
        {
            PopuniTipSmestaja();
            PopuniSifreGradova1();
            PopuniDrzave();
            PopuniGradove();
            PopuniProvajdere();
            PopuniTipPrevoza();
            PopuniSmestaje();
            var cityfrom = transportaiton[0];
            var citfroomid = (from b in db.Cities where b.CityName == cityfrom select b.CityID).FirstOrDefault();
            var cityto = transportaiton[1];
            var citytoid = (from b in db.Cities where b.CityName == cityto select b.CityID).FirstOrDefault();
            var typecategory = int.Parse(transportaiton[2]);
            var provider = int.Parse(transportaiton[3]);
            var transportaiton1 = db.Transportaitons.Where(city => city.CityFromID == citfroomid && city.CityToID == citytoid && city.TransportaionTypeID == typecategory && city.TransportaitonProviderID == provider).SingleOrDefault();
            if (transportaiton1 != null)
            {
                TransportaitonModel prikazi = new TransportaitonModel();
                prikazi = Admin.TransportaitonPoSifri(transportaiton1.TransportaitonID);


                TempData["transport"] = 1;
                return View("InformationAboutTransportaiton", prikazi);
            }
            else
            {
                TempData["nematransporta"] = 1;
                TransportaitonModel t = new TransportaitonModel();
                t.CityFromID = citfroomid;
                t.CityName = (from a in db.Cities where a.CityID == citfroomid select a.CityName).FirstOrDefault();
                t.CityToID = citytoid;
                t.CityName1 = (from b in db.Cities where b.CityID == citytoid select b.CityName).FirstOrDefault();

                return View("InformationAboutTransportaiton", t);
            }
        }
        public ActionResult Feedbacks()
        {
            PopuniSifreKorisnika();
            FeedbackUser u = new FeedbackUser();
            u.UserID = int.Parse(Session["iduser-a"].ToString());

            return View(u);
        }
        [HttpPost]
        public ActionResult Feedbacks(FormCollection user)
        {
            PopuniSifreKorisnika();
            FeedbackUser u = new FeedbackUser();
            u.UserID = int.Parse(user[0]);
            return View("KomentariKorisnika", u);
        }
        public ActionResult Providers()
        {
            PopuniSifreGradova1();
            PopuniDrzave();
            PopuniGradove();
            PopuniProvajdere();
            PopuniTipPrevoza();
            return View();
        }
        [HttpPost]
        public ActionResult Providers(FormCollection provajder)
        {
            PopuniSifreGradova1();
            PopuniDrzave();
            PopuniGradove();
            PopuniProvajdere();
            PopuniTipPrevoza();
            if (ModelState.IsValid)
            {
                TransportaitonProvider nov = new TransportaitonProvider();
                Random r = new Random();
                nov.TransportationProviderID = r.Next();
                nov.TransportationProviderName = provajder["TransportationProviderName"];
                nov.StreetName = provajder["StreetName"];
                nov.StreetNumber = provajder["StreetNumber"];
                nov.Phone = provajder["Phone"];
                nov.CityID = int.Parse(provajder["ListaGradova"]);

                nov.CountryID = Admin.VratiSifruDrzave(nov.CityID);
                nov.TransportaitonTypeID = int.Parse(provajder["ListaTipovaPrevoza"]);
                Admin.DodajProvider(nov);
                TempData["Naziv"] = nov.TransportationProviderName;

            }
            return View("Providers");
        }
        [HttpPost]
        public ActionResult ProvidersDetails(FormCollection providerdetail)
        {
            TransportationProviderViewModel prikazi = new TransportationProviderViewModel();
            prikazi = Admin.PrikaziProvajderPoSifri(int.Parse(providerdetail["listaProvajdera"]));
            using (Entities db = new Entities())
            {
                prikazi.provajderi = (from p in db.TransportaitonProviders select p).ToList();
                prikazi.gradovi = (from a in db.Cities select a).ToList();
                prikazi.drzave = (from b in db.Countries select b).ToList();
                prikazi.tipovitransporta = (from c in db.TransportaitonTypes select c).ToList();
                return View("ProvidersShowing", prikazi);
            }
        }
        [HttpPost]
        public ActionResult ProvidersShowing(TransportaitonProvider provider)
        {

            if (ModelState.IsValid)
            {
                Admin.IzmeniProvajdera(provider);
                TempData["Izmena"] = provider.TransportationProviderName;

                return RedirectToAction("Providers", "Account");
            }
            return View(provider);

        }


       
        [HttpPost]
        public ActionResult PrikaziInfoOGradu(FormCollection info)

        {

           
            Debug.WriteLine(info["CityName"]);
            var cityName = info["CityName"];
            var cityFromDb = db.Cities.Where(city => city.CityName == cityName).SingleOrDefault();
            

            CityInfoByCategoryViewModel prikazi = Admin.InfoAboutCity(cityFromDb.CityID, int.Parse(info["kategorije"]));

            if (prikazi != null)
            {
                TempData["provera"] = cityFromDb;
                
                return View(prikazi);


            }
            else
            {
                CityInfoByCategoryViewModel r = new CityInfoByCategoryViewModel();
                r.CityCategoryID = int.Parse(info["kategorije"]);
                r.CityID = cityFromDb.CityID;
                r.UserID = int.Parse(Session["iduser-a"].ToString());
                r.CityName = cityName;
                TempData["nema"] = cityName;
                return View(r);
            }


        }
        [HttpPost]
        public ActionResult DodajKomentar(FormCollection komentar)
        {
            if (ModelState.IsValid)
            {
                CityComment cityComment = new CityComment();
                cityComment.CategoryID = int.Parse(komentar[1]);
                cityComment.Date = DateTime.Today;
                Random r = new Random();
                cityComment.CommentID = r.Next();
                cityComment.UserID = int.Parse(Session["iduser-a"].ToString());
                cityComment.Comment = komentar[0];
                cityComment.CityID = int.Parse(komentar[3]);
                Admin.DodajKomentar1(cityComment);
                CityInfoByCategoryViewModel p = new CityInfoByCategoryViewModel();
                p.Info = komentar[6];
                if (p.Info != "")
                {
                    TempData["provera"] = 1;
                }
                else
                {
                    TempData["nema"] = 1;
                }



                p.CityName = komentar[4];
                p.CityCategoryName = komentar[5];

                p.Date = DateTime.Parse(komentar[7]);
                p.UserName = komentar[8];
                p.CityCategoryID = cityComment.CategoryID;
                p.CityID = cityComment.CityID;
                p.UserID = cityComment.UserID;


                return View("PrikaziInfoOGradu", p);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult DeleteComment(FormCollection forma)
        {

            var komentarzabrisanje = int.Parse(forma[0]);

            Admin.IzbrisiKomentar(komentarzabrisanje);

            CityInfoByCategoryViewModel p = new CityInfoByCategoryViewModel();
            p.CityName = forma[6];
            p.CityCategoryName = forma[7];
            p.Info = forma[8];
            if (p.Info != "")
            {
                TempData["provera"] = 1;
            }
            else
            {
                TempData["nema"] = 1;
            }
            p.Date = DateTime.Parse(forma[10]);
            p.UserName = forma[9];
            p.CityCategoryID = int.Parse(forma[11]);
            p.CityID = int.Parse(forma[13]);
            p.UserID = int.Parse(forma[12]);
            return View("PrikaziInfoOGradu", p);
        }


        [HttpPost]
        public ActionResult PrikaziInfoOAcc(FormCollection info)

        {

            
            Debug.WriteLine(info["Name"]);
            var AccName = info["Name"];
            var Acc = db.Accommodations.Where(city => city.Name == AccName).SingleOrDefault();
            

            AccommodationModel prikazi = Admin.InfoAboutAcc(Acc.AccommodationID);


            if (prikazi != null)
            {
                TempData["provera"] = Acc;
                
                return View(prikazi);


            }
            else
            {
                TempData["nema"] = AccName;
                return View(prikazi);
            }


        }
        [HttpPost]
        public ActionResult DodajKomentar1(FormCollection komentar)
        {
            if (ModelState.IsValid)
            {
                AccFeedback accComment = new AccFeedback();

                accComment.DateTimeOfFeedback = DateTime.Now;
                Random r = new Random();
                accComment.AccFeedbackID = r.Next();
                accComment.UserID = int.Parse(Session["iduser-a"].ToString());
                accComment.Comment = komentar[0];
                accComment.StarRating = int.Parse(komentar[1]);
                accComment.AccommodationID = int.Parse(komentar[2]);

                Admin.DodajAccFeedback(accComment);
                AccommodationModel p = new AccommodationModel();
                p.prosek = Admin.ProsekOcena(accComment.AccommodationID);


                TempData["provera"] = 1;


                p.AccommodationID = int.Parse(komentar[2]);
                p.Name = komentar[3];
                p.Name1 = komentar[4];
                p.CityName = komentar[5];
                p.Address = komentar[10];
                p.Phone = komentar[11];
                p.Email = komentar[12];
                p.Description = komentar[13];


                return View("PrikaziInfoOAcc", p);

            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult DeleteComment1(FormCollection forma)
        {

            var komentarzabrisanje = int.Parse(forma[0]);

            Admin.IzbrisiFeedback(komentarzabrisanje);
            TempData["provera"] = 1;
            AccommodationModel p = new AccommodationModel();
            p.CityName = forma[6];
            p.Name = forma[5];

            p.DateTimeOfFeedback = DateTime.Parse(forma[4]);
            p.UserName = forma[7];

            p.CityID = int.Parse(forma[8]);
            p.UserID = int.Parse(forma[9]);
            p.Name1 = forma[10];

            p.AccommodationID = int.Parse(forma[12]);
            p.prosek = Admin.ProsekOcena(p.AccommodationID);
            return View("PrikaziInfoOAcc", p);
        }
        [HttpPost]
        public ActionResult DodajKomentar2(FormCollection komentar)
        {
            if (ModelState.IsValid)
            {
                TransportationFeedback tfeedback = new TransportationFeedback();
                Random r = new Random();
                tfeedback.FeedBackID = r.Next();
                tfeedback.CommentID = r.Next();
                tfeedback.RatingID = r.Next();
                tfeedback.UserID = int.Parse(Session["iduser-a"].ToString());
                tfeedback.TransportationProviderID = int.Parse(komentar[11]);
                TransportatitonComment tcomment = new TransportatitonComment();
                tcomment.CommentID = tfeedback.CommentID.GetValueOrDefault();
                tcomment.UserID = tfeedback.UserID;
                tcomment.Comment = komentar[0];
                tcomment.Date = DateTime.Now;
                tcomment.TransportationProviderID = tfeedback.TransportationProviderID;
                TransportatitonRating trating = new TransportatitonRating();
                trating.RatingID = tfeedback.RatingID.GetValueOrDefault();
                trating.UserID = tfeedback.UserID;
                trating.StarRating = int.Parse(komentar[1]);
                trating.Date = DateTime.Now;
                trating.TrasportationProviderID = tfeedback.TransportationProviderID;

                Admin.Addtcomment(tcomment);
                Admin.Addtrating(trating);
                Admin.AddTfeedback(tfeedback);
                TransportaitonModel tm = new TransportaitonModel();
                tm.TransportaitonProviderID = int.Parse(komentar[11]);
                tm.prosek = Admin.ProsekOcenaTransporta(tm.TransportaitonProviderID);
                tm.CityName = komentar[3];
                tm.CityName1 = komentar[4];
                tm.TransportaitonTypeName = komentar[5];
                tm.TransportationProviderName = komentar[6];
                tm.TransportaionTypeID = int.Parse(komentar[2]);

                TempData["transport"] = 1;




                return View("InformationAboutTransportaiton", tm);
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult DeleteComment2(FormCollection forma)
        {
            var tkomentarzabrisanje = int.Parse(forma[1]);
            var tratingzabrisanje = int.Parse(forma[2]);

            var komentarzabrisanje = int.Parse(forma[0]);

            Admin.IzbrisiTRating(tratingzabrisanje);
            Admin.IzbrisiTKomentar(tkomentarzabrisanje);
            Admin.IzbrisiTFeedback(komentarzabrisanje);
            TempData["transport"] = 1;
            TransportaitonModel p = new TransportaitonModel();
            p.TransportaitonProviderID = int.Parse(forma[12]);
            p.prosek = Admin.ProsekOcenaTransporta(p.TransportaitonProviderID);
            p.CityName = forma[7];
            p.CityName1 = forma[13];
            p.TransportationProviderName = forma[14];
            p.TransportaionTypeID = int.Parse(forma[15]);


            p.UserName = forma[8];

            p.CityID = int.Parse(forma[10]);
            p.UserID = int.Parse(forma[3]);
            p.TransportaitonTypeName = forma[16];


            return View("InformationAboutTransportaiton", p);
        }
        public ActionResult DeleteComment3(FormCollection forma)
        {
            var tkomentarzabrisanje = int.Parse(forma[1]);
            var tratingzabrisanje = int.Parse(forma[2]);

            var komentarzabrisanje = int.Parse(forma[0]);

            Admin.IzbrisiTRating(tratingzabrisanje);
            Admin.IzbrisiTKomentar(tkomentarzabrisanje);
            Admin.IzbrisiTFeedback(komentarzabrisanje);
            FeedbackUser u = new FeedbackUser();

            u.UserID = int.Parse(forma[3]);


            return View("KomentariKorisnika", u);
        }
        public ActionResult DeleteComment4(FormCollection forma)
        {

            var komentarzabrisanje = int.Parse(forma[0]);

            Admin.IzbrisiFeedback(komentarzabrisanje);

            FeedbackUser u = new FeedbackUser();

            u.UserID = int.Parse(forma[2]);


            return View("KomentariKorisnika", u);
        }
        public ActionResult DeleteComment5(FormCollection forma)
        {
            var komentarzabrisanje = int.Parse(forma[0]);

            Admin.IzbrisiKomentar(komentarzabrisanje);
            FeedbackUser u = new FeedbackUser();

            u.UserID = int.Parse(forma[2]);


            return View("KomentariKorisnika", u);


        }
    }
}

