using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTA.Models
{
    public class Admin
    {
        public static void DodajKomentar1(CityComment comment)
        {
            using (Entities db = new Entities())
            {
                try
                {
                    db.CityComments.Add(comment);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
        }
        public static void AddTfeedback(TransportationFeedback transportationFeedback)
        {
            using (Entities db = new Entities())
            {
                try
                {
                    db.TransportationFeedbacks.Add(transportationFeedback);
                    db.SaveChanges();
                }
                catch(Exception)
                {

                }
            }
        }
        public static void Addtcomment(TransportatitonComment transportationComment)
        {
            using (Entities db = new Entities())
            {
                try
                {
                    db.TransportatitonComments.Add(transportationComment);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
        }
        public static void Addtrating(TransportatitonRating transportationRating)
        {
            using (Entities db = new Entities())
            {
                try
                {
                    db.TransportatitonRatings.Add(transportationRating);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
        }
        public static void DodajAccFeedback(AccFeedback feedback)
        {
            using (Entities db = new Entities())
            {
                try
                {
                    db.AccFeedbacks.Add(feedback);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
        }
        public static void DodajCityCategoryInf(CityInfoByCategory category)
        {
            using (Entities db = new Entities())
            {
                try
                {
                    db.CityInfoByCategories.Add(category);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
        }
        public static void DodajProvider(TransportaitonProvider provider)
        {
            using (Entities db = new Entities())
            {
                try
                {
                    db.TransportaitonProviders.Add(provider);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
        }
        public static void DodajAccomodation(Accommodation smestaj)
        {
            using (Entities db = new Entities())
            {
                try
                {
                    db.Accommodations.Add(smestaj);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
        }
        public static int VratiSifruDrzave(int sifra)
        {
            using (Entities db = new Entities())
            {

                var a = (from s in db.Cities where s.CityID == sifra select s.CountryID).FirstOrDefault();
                return a;

            }

        }
        public static TransportationProviderViewModel PrikaziProvajderPoSifri(int sifra)
        {
            using (Entities dbEntiti = new Entities())
            {

                TransportationProviderViewModel provajder = (from a in dbEntiti.TransportaitonProviders
                                                             where a.TransportationProviderID == sifra
                                                             select new TransportationProviderViewModel { TransportationProviderID = a.TransportationProviderID, TransportationProviderName = a.TransportationProviderName, CityID = a.CityID, CityName = a.City.CityName, TransportaitonTypeID = a.TransportaitonTypeID, TransportaitonTypeName = a.TransportaitonType.TransportaitonTypeName, CountryID = a.CountryID, CountryName = a.Country.CountryName, Phone = a.Phone, StreetName = a.StreetName, StreetNumber = a.StreetNumber }).Single();


                return provajder;

            }
        }
        public static TransportaitonModel TransportaitonPoSifri(int sifra)
        {
            using (Entities dbEntiti = new Entities())
            {

               TransportaitonModel transport = (from a in dbEntiti.Transportaitons where a.TransportaitonID == sifra select new TransportaitonModel { TransportaitonID = a.TransportaitonID, CityFromID = a.CityFromID, CityToID = a.CityToID, CityName = a.City.CityName, CityName1 = a.City1.CityName, TransportaionTypeID = a.TransportaionTypeID, TransportaitonProviderID = a.TransportaitonProviderID, TransportaitonTypeName = a.TransportaitonType.TransportaitonTypeName, TransportationProviderName = a.TransportaitonProvider.TransportationProviderName,prosek=null, }).Single();
                int? proveridaliimastarrating = (from b in dbEntiti.TransportatitonRatings where b.TrasportationProviderID == transport.TransportaitonProviderID select b.StarRating).FirstOrDefault();
                if (proveridaliimastarrating != 0)
                {
                    transport.prosek = Admin.ProsekOcenaTransporta(transport.TransportaitonProviderID);
                }
                return transport;
            }
        }
        public static AccommodationModel PrikaziAccomodationPoSifri(int sifra)
        {
            using (Entities dbEntiti = new Entities())
            {

                AccommodationModel smestaj = (from a in dbEntiti.Accommodations
                                                             where a.AccommodationID == sifra
                                                             select new AccommodationModel { AccommodationID = a.AccommodationID, Name=a.Name,AccCategoryID=a.AccCategoryID,Name1=a.AccCategory.Name,CityID = a.CityID, CityName = a.City.CityName,Address=a.Address,Phone = a.Phone,Email=a.Email,Description=a.Description }).Single();

            
                return smestaj;

            }
        }
        public static void DodajGrad(City grad)
        {
            using (Entities db = new Entities())
            {
                try
                {
                    db.Cities.Add(grad);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }
        }
        public static void DodajDrzavu(Country drzava)
        {
            using (Entities db = new Entities())
            {
                try
                {
                    db.Countries.Add(drzava);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }
        }
        public static void IzmeniProvajdera(TransportaitonProvider provider)
        {
            using (Entities dbEntiti = new Entities())
            {
                TransportaitonProvider izmena = (from a in dbEntiti.TransportaitonProviders
                                                 where a.TransportationProviderID == provider.TransportationProviderID
                                                 select a).Single();
                izmena.TransportationProviderName = provider.TransportationProviderName;
                izmena.TransportaitonTypeID = provider.TransportaitonTypeID;
                izmena.CityID = provider.CityID;
                izmena.CountryID = provider.CountryID;
                izmena.StreetName = provider.StreetName;
                izmena.StreetNumber = provider.StreetNumber;
                izmena.Phone = provider.Phone;
                try
                {
                    dbEntiti.SaveChanges();
                }
                catch
                {

                }
            }
        }
        public static void IzmeniAccomodation(Accommodation smestaj)
        {
            using (Entities dbEntiti = new Entities())
            {
                Accommodation izmena = (from a in dbEntiti.Accommodations
                                                 where a.AccommodationID == smestaj.AccommodationID
                                                 select a).Single();
                izmena.Name = smestaj.Name;
                izmena.AccommodationID = smestaj.AccommodationID;
                izmena.CityID = smestaj.CityID;
                izmena.Address = smestaj.Address;
                izmena.Phone = smestaj.Phone;
                izmena.Email = smestaj.Email;
                izmena.Description = smestaj.Description;
                izmena.AccCategoryID = smestaj.AccCategoryID;
                try
                {
                    dbEntiti.SaveChanges();
                }
                catch
                {

                }
            }
        }
        public static CityInfoByCategoryViewModel InfoAboutCity(int id, int id1)
        {
            using (Entities db = new Entities())
            {

                CityInfoByCategoryViewModel info = (from a in db.CityInfoByCategories
                                                    where (a.CityID == id && a.CityCategoryID == id1)
                                                    select new CityInfoByCategoryViewModel { CityCategoryID = a.CityCategoryID, CityID = a.CityID, CityName = a.City.CityName, Date = a.Date, Info = a.Info, UserID = a.UserID, UserName = a.User.UserName, CityCategoryName = a.CityCategory.CityCategoryName }).SingleOrDefault();


                return info;

            }
        }
        public static AccommodationModel InfoAboutAcc(int id)
        {
            using (Entities db = new Entities())
            {

                AccommodationModel info = (from a in db.Accommodations
                                           where (a.AccommodationID == id)
                                           select new AccommodationModel { AccommodationID = a.AccommodationID, Name = a.Name, AccCategoryID = a.AccCategoryID, Address = a.Address, CityID = a.CityID, CityName = a.City.CityName, Description = a.Description, Email = a.Email, Phone = a.Phone, Name1 = a.AccCategory.Name,prosek=null}).SingleOrDefault();
                int? proveridaliimastarrating = (from b in db.AccFeedbacks where b.AccommodationID == id select b.StarRating).FirstOrDefault();
                if (proveridaliimastarrating !=0)
                {
                    info.prosek = Admin.ProsekOcena(id);
                }
                

                return info;

            }
        }
        public static void IzbrisiKomentar(int id)
        {
            using (Entities db = new Entities())

            {
                CityComment komentarzabrisanje = (from a in db.CityComments where a.CommentID == id select a).Single();
                try
                {

                    db.CityComments.Remove(komentarzabrisanje);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
        }
        public static void IzbrisiFeedback(int id)
        {
            using (Entities db = new Entities())

            {
                AccFeedback komentarzabrisanje = (from a in db.AccFeedbacks where a.AccFeedbackID == id select a).Single();
                try
                {

                    db.AccFeedbacks.Remove(komentarzabrisanje);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
        }
        public static void IzbrisiTFeedback(int id)
        {
            using (Entities db = new Entities())

            {
                TransportationFeedback komentarzabrisanje = (from a in db.TransportationFeedbacks where a.FeedBackID == id select a).Single();
                try
                {

                    db.TransportationFeedbacks.Remove(komentarzabrisanje);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
        }
        public static void IzbrisiTKomentar(int id)
        {
            using (Entities db = new Entities())

            {
                TransportatitonComment komentarzabrisanje = (from a in db.TransportatitonComments where a.CommentID == id select a).Single();
                try
                {

                    db.TransportatitonComments.Remove(komentarzabrisanje);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
        }
        public static void IzbrisiTRating(int id)
        {
            using (Entities db = new Entities())

            {
               TransportatitonRating komentarzabrisanje = (from a in db.TransportatitonRatings where a.RatingID == id select a).Single();
                try
                {

                    db.TransportatitonRatings.Remove(komentarzabrisanje);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
        }
        public static List<CityInfoByCategoryViewModel> CityCommentPoSifri(int id, int id1)
        {
            using (Entities db = new Entities())
            {
                List<CityInfoByCategoryViewModel> komentari = (from a in db.CityComments where (a.CityID == id && a.CategoryID == id1) select new CityInfoByCategoryViewModel { CityID = a.CityID, CityName = a.City.CityName, CityCategoryName = a.CityCategory.CityCategoryName, UserName = a.User.UserName, CityCategoryID = a.CategoryID, UserID = a.UserID, Date = a.Date, Comment = a.Comment, CommentID = a.CommentID, CategoryID = a.CategoryID }).OrderByDescending(item => item.Date).ToList();
                return komentari;
            }
        }
        public static List<CityInfoByCategoryViewModel> CityCommentPoSifriUsera(int id)
        {
            using (Entities db = new Entities())
            {
                List<CityInfoByCategoryViewModel> komentari = (from a in db.CityComments where (a.UserID == id) select new CityInfoByCategoryViewModel { CityID = a.CityID, CityName = a.City.CityName, CityCategoryName = a.CityCategory.CityCategoryName, UserName = a.User.UserName, CityCategoryID = a.CategoryID, UserID = a.UserID, Date = a.Date, Comment = a.Comment, CommentID = a.CommentID, CategoryID = a.CategoryID }).OrderByDescending(item => item.Date).ToList();
                return komentari;
            }
        }
        public static List<AccommodationModel> AccomodationFeedbackPoSifri(int id)
        {
            using (Entities db = new Entities())
            {
                List<AccommodationModel> komentar = (from a in db.AccFeedbacks where (a.AccommodationID == id) select new AccommodationModel { StarRating1 = a.StarRating, Comment = a.Comment, DateTimeOfFeedback = a.DateTimeOfFeedback, Name = a.Accommodation.Name, Name1 = a.Accommodation.AccCategory.Name,UserName=a.User.UserName,AccFeedbackID=a.AccFeedbackID}).OrderByDescending(item => item.DateTimeOfFeedback).ToList();
                return komentar;
            }
        }
        public static List<AccommodationModel> AccomodationFeedbackPoSifriUsera(int id)
        {
            using (Entities db = new Entities())
            {
                List<AccommodationModel> komentar = (from a in db.AccFeedbacks where (a.UserID == id) select new AccommodationModel { StarRating1 = a.StarRating, Comment = a.Comment, DateTimeOfFeedback = a.DateTimeOfFeedback, Name = a.Accommodation.Name, Name1 = a.Accommodation.AccCategory.Name, UserName = a.User.UserName, AccFeedbackID = a.AccFeedbackID }).OrderByDescending(item => item.DateTimeOfFeedback).ToList();
                return komentar;
            }
        }
        public static List<TransportaitonModel> TransportationFeedbackPoSifri(int id)
        {
            using (Entities db = new Entities())
            {
                List<TransportaitonModel> komentar = (from a in db.TransportationFeedbacks where (a.TransportationProviderID == id) select new TransportaitonModel { Comment = a.TransportatitonComment.Comment, Date = a.TransportatitonComment.Date, StarRating = a.TransportatitonRating.StarRating, Date1 = a.TransportatitonRating.Date, UserName = a.User.UserName, CommentID = a.TransportatitonComment.CommentID, RatingID = a.TransportatitonRating.RatingID,FeedBackID=a.FeedBackID,UserID=a.UserID  }).OrderByDescending(item => item.Date).ToList();
                return komentar;
            }
        }
        public static List<TransportaitonModel> TransportationFeedbackPoUseru(int id)
        {
            using (Entities db = new Entities())
            {
                List<TransportaitonModel> komentar = (from a in db.TransportationFeedbacks where (a.UserID == id) select new TransportaitonModel { Comment = a.TransportatitonComment.Comment, Date = a.TransportatitonComment.Date, StarRating = a.TransportatitonRating.StarRating, Date1 = a.TransportatitonRating.Date, UserName = a.User.UserName, CommentID = a.TransportatitonComment.CommentID, RatingID = a.TransportatitonRating.RatingID, FeedBackID = a.FeedBackID, UserID = a.UserID }).OrderByDescending(item => item.Date).ToList();
                return komentar;
            }
        }


        public static double ProsekOcena(int id)
        {
            using (Entities db = new Entities())
            {
                double prosek=0;
                double? suma = (from a in db.AccFeedbacks where (a.AccommodationID == id) select a.StarRating).Sum();
                double? broj = (from a in db.AccFeedbacks where (a.AccommodationID == id) select a).Count();
                if (suma == null || broj == null || broj == 0) 
                {
                    return prosek;
                }
                else
                {


                    prosek =  (suma / broj).GetValueOrDefault();

                    return prosek;
                }            
            }
        }
        public static double ProsekOcenaTransporta(int id)
        {
            using (Entities db = new Entities())
            {
                double prosek = 0;
                double? suma = (from a in db.TransportatitonRatings where (a.TrasportationProviderID == id) select a.StarRating).Sum();
                double? broj = (from a in db.TransportatitonRatings where (a.TrasportationProviderID == id) select a).Count();
                if (suma == null || broj == null || broj == 0)
                {
                    return prosek;
                }
                else
                {


                    prosek = (suma / broj).GetValueOrDefault();

                    return prosek;
                }
            }
        }

        public static int Sifra { get; set; }

        public static SelectList PopuniImeDrzava()
        {
            Entities db = new Entities();
            IEnumerable<SelectListItem> imenadrzava = (from s in db.Countries select s).AsEnumerable().Select(s => new SelectListItem { Text = s.CountryName, Value = s.CountryID.ToString() });
            return new SelectList(imenadrzava, "Value", "Text", Sifra);
        }
        public static SelectList PopuniImeGradova()
        {
            Entities db = new Entities();
            IEnumerable<SelectListItem> imenagradova = (from s in db.Cities select s).AsEnumerable().Select(s => new SelectListItem { Text = s.CityName+","+s.Country.CountryName, Value = s.CityID.ToString() });
            return new SelectList(imenagradova, "Value", "Text", Sifra);
        }
        public static SelectList PopuniTipovePrevoza()
        {
            Entities db = new Entities();
            IEnumerable<SelectListItem> tipoviprevoza = (from s in db.TransportaitonTypes select s).AsEnumerable().Select(s => new SelectListItem { Text = s.TransportaitonTypeName, Value = s.TransportaitonTypeID.ToString() });
            
            
            return new SelectList(tipoviprevoza, "Value", "Text", Sifra);

        }
        public static SelectList PopuniTipoveSmestaja()
        {
            Entities db = new Entities();
            IEnumerable<SelectListItem> tipovismestaja = (from s in db.AccCategories select s).AsEnumerable().Select(s => new SelectListItem { Text = s.Name, Value = s.AccCategoryID.ToString() });


            return new SelectList(tipovismestaja, "Value", "Text", Sifra);

        }
        public static SelectList PopuniProvajdere()
        {
            Entities db = new Entities();
            IEnumerable<SelectListItem> imenaprovajdera = (from s in db.TransportaitonProviders select s).AsEnumerable().Select(s => new SelectListItem { Text = s.TransportationProviderName, Value = s.TransportationProviderID.ToString() });
            return new SelectList(imenaprovajdera, "Value", "Text", Sifra);
        }
        public static SelectList PopuniSmestaje()
        {
            Entities db = new Entities();
            IEnumerable<SelectListItem> imenasmestaja = (from s in db.Accommodations select s).AsEnumerable().Select(s => new SelectListItem { Text = s.Name, Value = s.AccommodationID.ToString() });
            return new SelectList(imenasmestaja, "Value", "Text", Sifra);
        }

        public static SelectList PopuniSifreGradova()
        {
            Entities db = new Entities();
            IEnumerable<SelectListItem> sifragradova = (from s in db.TransportaitonProviders select s).AsEnumerable().Select(s => new SelectListItem { Text = s.City.CityName, Value = s.CityID.ToString() });
            return new SelectList(sifragradova, "Value", "Text", Sifra);
        }
        public static  SelectList PopuniSifreGradskeKategorije()
        {
            Entities db = new Entities();
            IEnumerable<SelectListItem> sifrekategorije = (from s in db.CityCategories select s).AsEnumerable().Select(s => new SelectListItem { Text = s.CityCategoryName, Value = s.CityCategoryID.ToString() });
            return new SelectList(sifrekategorije, "Value", "Text", Sifra);
        }

        public static SelectList PopuniSifreUsera()
        {
            Entities db = new Entities();
            IEnumerable<SelectListItem> sifreusera = (from s in db.Users select s).AsEnumerable().Select(s => new SelectListItem { Text = s.UserName, Value = s.UserID.ToString() });
            return new SelectList(sifreusera, "Value", "Text", Sifra);
        }




    }

}