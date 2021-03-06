﻿using Repo.Models;
using Repo.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Diagnostics;
using System.Data.Entity;

namespace Repo.Advertisements
{
    public class AdvertisementRepo : IAdvertisementRepo
    {
        private readonly IAdvertisementContext _db;
        public AdvertisementRepo(IAdvertisementContext db)
        {
            _db = db;
        }

        public void AddAdvetisement(Advertisement adv)
        {
            _db.Advertisement.Add(adv);
        }
        public void AddImageToAdvertisement(AdvertisementImage image)
        {
            _db.AdvertisementImage.Add(image);
        }
        public bool DeleteAllImageFromAdvertisement(int id)
        {
            var advImgs = _db.AdvertisementImage.Find(id);
            _db.AdvertisementImage.Remove(advImgs);
            //advImgs.RemoveAll(x => x.AdvertisementId == id);
            //var advImg = _db.AdvertisementImage;
            //Advertisement adv = _db.Advertisement.Find(id);
            //var advImg = adv.AdvertisementImage;
            //foreach (var item in advImg)
            //{
            //    _db.AdvertisementImage.Remove(item);
            //}

            //_db.AdvertisementImage.Remove(advImg);
            
            //foreach (var item in advImg)
            //{
            //    if (item.AdvertisementId == id)
            //    {
            //        //_db.AdvertisementImage.Remove(advImg);
            //    }

            //}

            try
            {
                //_db.AdvertisementImage.Remove();
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool DeleteAdvertisement(int id)
        {
            Advertisement adv = _db.Advertisement.Find(id);
            _db.Advertisement.Remove(adv);
            try
            {
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Advertisement GetAdvertisementById(int id)
        {
            Advertisement adv = _db.Advertisement.Find(id);
            return adv;
        }

        public IQueryable<Advertisement> GetAdvetisements()
        {
            var advs = _db.Advertisement.AsNoTracking();
            return advs;
        }
        public AdvertisementImage GetAdvertisementImage(int? id)
        {
            var advImage = _db.AdvertisementImage.First(a => a.Id == 5);
            return advImage;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void UpdateAdvertisement(Advertisement adv)
        {
            _db.Entry(adv).State = EntityState.Modified;
        }
        public IQueryable<Advertisement> GetPage(int? page = 1, int? pageSize = 10)
        {
            //sortowanie malejaco - czyli od najnowszych
            //skip - opuszczanie wybranej liczby elementów
            //take - ile elem trzeba pobrać
            var advs = _db.Advertisement.OrderByDescending(a => a.Date).Skip((page.Value - 1) * 
                pageSize.Value).Take(pageSize.Value);
           
            return advs;
        }
    }
}
