using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Survey
    {
        [Key]
        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime DateClose { get; set; }
        public int MaxPeople { get; set; } = int.MaxValue;
        public int SurveyorId { get; set; }

        [ForeignKey("SurveyorId")]
        public User Surveyor { get; set; }

        public virtual ICollection<User> Respondents { get; set; } = new List<User>();

        public List<Question> Questions { get; set; }

    }
}
