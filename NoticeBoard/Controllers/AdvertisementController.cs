﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repo.Models;
using Repo.Advertisements;
using Repo.IRepo;
using Microsoft.AspNet.Identity;
using System.Diagnostics;
using PagedList;

namespace NoticeBoard.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementRepo _repo;

        public AdvertisementController(IAdvertisementRepo repo)
        {
            _repo = repo;
        }

        // GET: Advertisement
        public ActionResult Index(int? page, string sortOrder, string searchString)
        {
            var currentPage = page ?? 1;
            int onPage = 15;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSort = sortOrder == "DataDodania" ? "DataDodaniaAsc" : "DataDodania";
            ViewBag.TitleSort = sortOrder == "TytulAsc" ? "Tytul" : "TytulAsc";
            ViewBag.PriceSort = sortOrder == "Cena" ? "CenaAsc" : "Cena";

            var advertisement = _repo.GetAdvetisements();
            switch (sortOrder)
            {
                case "DataDodania":
                    advertisement = advertisement.OrderByDescending(a => a.Date);
                    break;
                case "DataDodaniaAsc":
                    advertisement = advertisement.OrderBy(a => a.Date);
                    break;
                case "Tytul":
                    advertisement = advertisement.OrderByDescending(a => a.Title);
                    break;
                case "TytulAsc":
                    advertisement = advertisement.OrderBy(a => a.Title);
                    break;
                case "Cena":
                    advertisement = advertisement.OrderByDescending(a => a.Price);
                    break;
                case "CenaAsc":
                    advertisement = advertisement.OrderBy(a => a.Price);
                    break;

                default:
                    advertisement = advertisement.OrderByDescending(a => a.Id);
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                advertisement = advertisement.Where(s => s.Title.Contains(searchString));
            }
            //advertisement = advertisement.OrderByDescending(adv => adv.Date);
            return View(advertisement.ToPagedList<Advertisement>(currentPage, onPage));

        }

        // GET: Advertisement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = _repo.GetAdvertisementById((int) id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        //// GET: Advertisement/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: Advertisement/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize] //tylko dla zalogowanych
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Description,Title,Price")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                advertisement.UserId = User.Identity.GetUserId();
                advertisement.Date = DateTime.Now;

                try
                {
                    _repo.AddAdvetisement(advertisement);
                    _repo.SaveChanges();
                    
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View(advertisement);
                }


            }

            return View(advertisement);
        }

        [Authorize]
        //// GET: Advertisement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = _repo.GetAdvertisementById((int)id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            else if(advertisement.UserId != User.Identity.GetUserId() && !(User.IsInRole("Admin") || User.IsInRole("Moderator"))) 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            return View(advertisement);
        }

        //// POST: Advertisement/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Title,Date,UserId,Price")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.UpdateAdvertisement(advertisement);
                    _repo.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.Error = true;
                    return View(advertisement);
                }
               // return RedirectToAction("Index");
            }
            ViewBag.Error = false;
            return View(advertisement);
        }

        [Authorize]
        //// GET: Advertisement/Delete/5
        public ActionResult Delete(int? id, bool? error)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = _repo.GetAdvertisementById((int)id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            else if(advertisement.UserId != User.Identity.GetUserId() && !User.IsInRole("Admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (error != null)
            {
                ViewBag.Error = true;
            }
            return View(advertisement);
        }

        //// POST: Advertisement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.DeleteAdvertisement(id);
            try
            {
                _repo.SaveChanges();
            }
            catch (Exception)
            {

                return RedirectToAction("Delete", new { id = id, error = true });
            }
            return RedirectToAction("Index");
        }

        //Get:/Advertisement
        //public ActionResult Partial()
        //{
        //    var adv = _repo.GetAdvetisements();
        //    return PartialView("Index", adv);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
