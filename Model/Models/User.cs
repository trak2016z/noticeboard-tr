using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Repo.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Advertisements = new HashSet<Advertisement>();
        }

        public string FirsName { get; set; }
        public string SecondName { get; set; }
        
        [NotMapped]
        [Display(Name = "Imie i Nazwisko: ")]
        public string FullName
        {
            get { return FirsName + " " + SecondName; }
        }
        
        public virtual ICollection<Advertisement> Advertisements { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

    }
}