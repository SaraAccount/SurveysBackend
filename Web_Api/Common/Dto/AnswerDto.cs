using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public bool IsAnswered { get; set; }
        public string AnswerValue { get; set; }

        public int QuestionId { get; set; }
        public int UserId { get; set; }
    }
}
