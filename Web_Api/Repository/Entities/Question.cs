using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace Repository.Entities
{
    public enum eTypeTag
    {
        INPUT_TEXT,
        INPUT_NUMBER,
        CHECKBOX,
        SELECT,
        RADIO
    }
    public class Question
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string Label { get; set; }
        public eTypeTag TypeTag { get; set; }
        public bool IsRequired { get; set; } //must 
        public string Options { get; set; }
        public virtual ICollection<Answer>? Answers { get; set; }
        
        public string RenderHtml()
        {
            string requiredAttr = IsRequired ? "required" : "";

            switch (TypeTag)
            {
                case eTypeTag.INPUT_TEXT:
                    return $"<label for='{Id}'>{Label}</label><input type='text' id='{Id}' name='{Id}' {requiredAttr}>";

                case eTypeTag.INPUT_NUMBER:
                    return $"<label for='{Id}'>{Label}</label><input type='number' id='{Id}' name='{Id}' {requiredAttr}>";

                case eTypeTag.CHECKBOX:
                    return $"<label for='{Id}'>{Label}</label><input type='checkbox' id='{Id}' name='{Id}' {requiredAttr}>";

                case eTypeTag.RADIO:
                    if (string.IsNullOrEmpty(Options)) return "";
                    var radioOptions = Options.Split(',');
                    return $"<label>{Label}</label>" + string.Join("", radioOptions.Select(opt =>
                        $"<input type='radio' name='{Id}' value='{opt.Trim()}' {requiredAttr}> {opt.Trim()}"));

                case eTypeTag.SELECT:
                    if (string.IsNullOrEmpty(Options)) return "";
                    var selectOptions = Options.Split(',');
                    return $"<label for='{Id}'>{Label}</label><select id='{Id}' name='{Id}' {requiredAttr}>" +
                           string.Join("", selectOptions.Select(opt => $"<option value='{opt.Trim()}'>{opt.Trim()}</option>")) + "</select>";

                default:
                    return $"<p>Unsupported question type: {TypeTag}</p>";
            }
        }





    }
}
