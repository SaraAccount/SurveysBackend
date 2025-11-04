using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }
        public eGender Gender { get; set; }
        public eSector Sector { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public eRole Role { get; set; }
    }
}
