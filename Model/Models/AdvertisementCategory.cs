using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Repo.Models
{
    public class AdvertisementCategory
    {
        public AdvertisementCategory()
        {

        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int AdvertisementId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Advertisement Advertisement { get; set; }
    }
}