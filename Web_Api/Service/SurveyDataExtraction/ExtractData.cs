using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SurveyDataExtraction
{
    public static class ExtractData
    {
        public static double[] ExtractNumberAnswer(Survey survey, int questionId)
        {
            var question = survey.Questions
                .FirstOrDefault(q => q.Id == questionId && q.TypeTag == eTypeTag.INPUT_NUMBER);

            if (question == null || question.Answers == null)
                return Array.Empty<double>();

            var numericAnswers = new List<double>();

            foreach (var answer in question.Answers)
            {
                if (answer.IsAnswered && double.TryParse(answer.AnswerValue, out double value))
                {
                    numericAnswers.Add(value);
                }
            }

            return numericAnswers.ToArray();
        }

        public static string[][] ExtractCheckboxAnswer(Survey survey, int questionId)
        {
            var question = survey.Questions
                .FirstOrDefault(q => q.Id == questionId && q.TypeTag == eTypeTag.CHECKBOX);

            if (question == null || question.Answers == null)
                return Array.Empty<string[]>();

            var allSelections = new List<string[]>();

            foreach (var answer in question.Answers)
            {
                if (!answer.IsAnswered || string.IsNullOrWhiteSpace(answer.AnswerValue))
                    continue;

                var selectedOptions = ParseAnswerValue(answer.AnswerValue);
                if (selectedOptions.Length > 0)
                    allSelections.Add(selectedOptions);
            }

            return allSelections.ToArray();
        }

        public static string[] ParseAnswerValue(string raw)
        {
            return raw
                .Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(opt => opt.Trim())
                .ToArray();
        }
        
        public static string[] ExtractSelectAndRadioAnswer(Survey survey, int questionId)
        {
            var question = survey.Questions
                .FirstOrDefault(q =>
                    q.Id == questionId &&
                    (q.TypeTag == eTypeTag.SELECT || q.TypeTag == eTypeTag.RADIO));

            if (question == null || question.Answers == null)
                return Array.Empty<string>();

            var selections = new List<string>();

            foreach (var answer in question.Answers)
            {
                if (!answer.IsAnswered || string.IsNullOrWhiteSpace(answer.AnswerValue))
                    continue;

                selections.Add(answer.AnswerValue.Trim());
            }

            return selections.ToArray();
        }

        public static string[] ExtractTextAnswer(Survey survey, int questionId)
        {
            var question = survey.Questions
                .FirstOrDefault(q => q.Id == questionId && q.TypeTag == eTypeTag.INPUT_TEXT);

            if (question == null || question.Answers == null)
                return Array.Empty<string>();

            List<string> textAnswers = new List<string>();

            foreach (var answer in question.Answers)
            {
                if (answer.IsAnswered && !string.IsNullOrWhiteSpace(answer.AnswerValue))
                {
                    textAnswers.Add(answer.AnswerValue);
                }
            }

            return textAnswers.ToArray();
        }


    }
}


