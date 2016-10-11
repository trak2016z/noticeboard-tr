using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NoticeBoard.Models
{
    public class Category
    {
        public Category()
        {
            this.AdvertisementCategory = new HashSet<AdvertisementCategory>();
        }

        [Key]
        [Display(Name = "Id kategorii")]
        public int Id { get; set; }

        [Display(Name = "Nazwa kategorii")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Id rodzica")]
        [Required]
        public int ParentId { get; set; }

        public ICollection<AdvertisementCategory> AdvertisementCategory { get; set; }


    }
}