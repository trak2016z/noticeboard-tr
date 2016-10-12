using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Repo.Models
{
    public class Advertisement
    {
        public Advertisement()
        {
            this.AdvertisementCategory = new HashSet<AdvertisementCategory>();
        }

        [Display(Name = "Id:")]
        public int Id { get; set; }

        [Display(Name = "Treść ogłoszenia:")]
        [MaxLength(500)]
        public string Description { get; set; }

        [Display(Name = "Tytuł ogłoszenia")]
        [MaxLength(250)]
        public string Title { get; set; }

        [Display(Name = "Data dodania")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }

        [Display(Name = "Cena")]
        public double Price { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<AdvertisementCategory> AdvertisementCategory { get; set; }

        public virtual User User { get; set; }
    }
}