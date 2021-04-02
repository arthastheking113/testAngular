using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace testAngular.Models
{
    public class Member
    {
        [Key]
        public long Id { get; set; }

        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string MiddleName { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
