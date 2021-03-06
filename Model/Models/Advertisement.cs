﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repo.Models
{
    public class Advertisement
    {
        public Advertisement()
        {
            this.AdvertisementCategory = new HashSet<AdvertisementCategory>();
            this.AdvertisementImage = new HashSet<AdvertisementImage>();
        }

        [Display(Name = "Id:")]
        public int Id { get; set; }

        [Display(Name = "Treść ogłoszenia:")]
        [MaxLength(500)]
        [Required(ErrorMessage = "Treść ogłoszenia jest wymagana")]
        public string Description { get; set; }

        [Display(Name = "Tytuł ogłoszenia")]
        [MaxLength(250)]
        [Required(ErrorMessage = "Tytuł jest wymagany")]
        public string Title { get; set; }

        [Display(Name = "Data dodania")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Cena")]
        [DataType(DataType.Currency)]
        //[DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Cena jest wymagana")]
        public double Price { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<AdvertisementCategory> AdvertisementCategory { get; set; }
        public virtual ICollection<AdvertisementImage> AdvertisementImage { get; set; }
        public virtual User User { get; set; }
    }
}