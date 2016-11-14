using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repo.Models
{
    public class AdvertisementImage
    {
        public AdvertisementImage()
        {
            
        }

        [Key]
        public int Id { get; set; }
        public int AdvertidementId { get; set; }
        public byte[] Image { get; set; }

        public virtual Advertisement Advertisement { get; set; }
    }
}