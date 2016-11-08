using Repo.Models;
using Repo.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Diagnostics;

namespace Repo.Advertisements
{
    public class AdvertisementRepo : IAdvertisementRepo
    {
        private readonly IAdvertisementContext _db;
        public AdvertisementRepo(IAdvertisementContext db)
        {
            _db = db;
        }
        
        public IQueryable<Advertisement> GetAdvetisements()
        {
            var advs = _db.Advertisement.AsNoTracking();
            return advs;
        }
    }
}
