﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wardrobe.Models;

namespace wardrobe.Controllers
{
    public class OutfitsController : Controller
    {
        private WardrobeDBEntities db = new WardrobeDBEntities();

        // GET: Outfits
        public ActionResult Index()
        {
            var outfits = db.Outfits.Include(o => o.WardrobeItem).Include(o => o.WardrobeItem1).Include(o => o.WardrobeItem2);
            return View(outfits.ToList());
        }

        // GET: Outfits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outfit outfit = db.Outfits.Find(id);
            if (outfit == null)
            {
                return HttpNotFound();
            }
            return View(outfit);
        }

        // GET: Outfits/Create
        public ActionResult Create()
        {
            var possibleTops = from item in db.WardrobeItems
                               where item.Type.TypeName == "Top"
                               select item;
            var possibleBottoms = from item in db.WardrobeItems
                                  where item.Type.TypeName == "Bottom"
                                  select item;
            var possibleShoes = from item in db.WardrobeItems
                                where item.Type.TypeName == "Shoes"
                                select item;
            var possibleAccessories = from item in db.WardrobeItems
                                where item.Type.TypeName == "Accessories"
                                select item;

            ViewBag.TopID = new SelectList(possibleTops, "WardrobeItemID", "Name");
            ViewBag.BottomID = new SelectList(possibleBottoms, "WardrobeItemID", "Name");
            ViewBag.ShoeID = new SelectList(possibleShoes, "WardrobeItemID", "Name");
            ViewBag.Accessories = new SelectList(possibleAccessories, "WardrobeItemID", "Name");
            return View();
        }

        // POST: Outfits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OutfitID,TopID,BottomID,ShoeID")] Outfit outfit)
        {
            if (ModelState.IsValid)
            {
                db.Outfits.Add(outfit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TopID = new SelectList(db.WardrobeItems, "WardrobeItemID", "Name", outfit.TopID);
            ViewBag.BottomID = new SelectList(db.WardrobeItems, "WardrobeItemID", "Name", outfit.BottomID);
            ViewBag.ShoeID = new SelectList(db.WardrobeItems, "WardrobeItemID", "Name", outfit.ShoeID);
            ViewBag.Accessories = new SelectList(db.WardrobeItems, "WardrobeItemID", "Name", outfit.Accessories);
            return View(outfit);
        }

        // GET: Outfits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outfit outfit = db.Outfits.Find(id);
            if (outfit == null)
            {
                return HttpNotFound();
            }
            ViewBag.TopID = new SelectList(db.WardrobeItems, "WardrobeItemID", "Name", outfit.TopID);
            ViewBag.BottomID = new SelectList(db.WardrobeItems, "WardrobeItemID", "Name", outfit.BottomID);
            ViewBag.ShoeID = new SelectList(db.WardrobeItems, "WardrobeItemID", "Name", outfit.ShoeID);
            ViewBag.Accessories = new SelectList(db.WardrobeItems, "WardrobeItemID", "Name", outfit.Accessories);
            return View(outfit);
        }

        // POST: Outfits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OutfitID,TopID,BottomID,ShoeID")] Outfit outfit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(outfit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TopID = new SelectList(db.WardrobeItems, "WardrobeItemID", "Name", outfit.TopID);
            ViewBag.BottomID = new SelectList(db.WardrobeItems, "WardrobeItemID", "Name", outfit.BottomID);
            ViewBag.ShoeID = new SelectList(db.WardrobeItems, "WardrobeItemID", "Name", outfit.ShoeID);
            ViewBag.Accessories = new SelectList(db.WardrobeItems, "WardrobeItemID", "Name", outfit.Accessories);
            return View(outfit);
        }

        // GET: Outfits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outfit outfit = db.Outfits.Find(id);
            if (outfit == null)
            {
                return HttpNotFound();
            }
            return View(outfit);
        }

        // POST: Outfits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Outfit outfit = db.Outfits.Find(id);
            db.Outfits.Remove(outfit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
