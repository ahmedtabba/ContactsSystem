﻿using ContactsSystem.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ContactsSystem.Controllers
{
    public class ProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profiles_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Profile> profiles = db.Profiles;
            DataSourceResult result = profiles.ToDataSourceResult(request, profile => new
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = profile.Email,
                Phone = profile.Phone,
                Address = profile.Address
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Profiles_Create([DataSourceRequest]DataSourceRequest request, Profile profile)
        {
            if (ModelState.IsValid)
            {

                var entity = new Profile
                {
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Email = profile.Email,
                    Phone = profile.Phone,
                    Address = profile.Address
                };

                db.Profiles.Add(entity);
                db.SaveChanges();
                profile.Id = entity.Id;
            }

            return Json(new[] { profile }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Profiles_Update([DataSourceRequest]DataSourceRequest request, Profile profile)
        {
            if (ModelState.IsValid)
            {
                var entity = new Profile
                {
                    Id = profile.Id,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Email = profile.Email,
                    Phone = profile.Phone,
                    Address = profile.Address
                };

                db.Profiles.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { profile }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Profiles_Destroy([DataSourceRequest]DataSourceRequest request, Profile profile)
        {
            if (ModelState.IsValid)
            {
                var entity = new Profile
                {
                    Id = profile.Id,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Email = profile.Email,
                    Phone = profile.Phone,
                    Address = profile.Address
                };

                db.Profiles.Attach(entity);
                db.Profiles.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { profile }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
