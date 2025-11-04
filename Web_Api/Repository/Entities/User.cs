using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public enum eGender
    {
        MALE,
        FEMALE,
        OTHER
    }

    public enum eSector
    {
        RELIGIOUS,
        HAREDI,
        SECULAR,
        TRADITIONAL,
        ARAB,
        CHRISTIAN,
        FORMER_HAREDI,
        OTHER
    }

    public enum eRole
    {
        ADMINISTRATOR,
        USER
    }
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate  { get; set; }
        public eGender Gender { get; set; }
        public eSector Sector { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public eRole Role { get; set; }
        public virtual ICollection<Survey> OwnSurveys { get; set; }
        public virtual ICollection<Survey> AnsweredSurveys { get; set; }
    }
}
