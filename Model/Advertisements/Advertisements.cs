using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Advertisements
{
    public class Advertisements
    {
        private NoticeBoardContext db = new NoticeBoardContext();
           
        public IQueryable<Advertisement> GetAdvertisements()
        {
            var advs = db.Advertisements.AsNoTracking();
            return advs;
        }
    }
}
