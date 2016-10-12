using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repo.Models
{
    public class AdvertisementImage
    {
        public AdvertisementImage()
        {
            
        }

        public int Id { get; set; }
        public int AdvertidementId { get; set; }
        public byte[] Image { get; set; }
        
    }
}