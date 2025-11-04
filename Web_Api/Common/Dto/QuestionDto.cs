using Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public eTypeTag TypeTag { get; set; }
        public bool IsRequired { get; set; }
        public string Options { get; set; }


        //// Add
        //public int SurveyId { get; set; }

        //[ForeignKey("SurveyId")]
        //public Survey Survey { get; set; }
    }
}
