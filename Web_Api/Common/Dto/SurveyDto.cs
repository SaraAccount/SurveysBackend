using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class SurveyDto
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime DateClose { get; set; }
        public int MaxPeople { get; set; }
        public int SurveyorId { get; set; }

        public List<QuestionDto> Questions { get; set; }
        public List<int> RespondentId { get; set; }

    }
}
