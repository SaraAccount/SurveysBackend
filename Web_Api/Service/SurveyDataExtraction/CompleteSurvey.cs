using Repository.Entities;
using Service.LogicQuastions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Service.SurveyDataExtraction
{
    public static class CompleteSurvey
    {
        public static Dictionary<int, Object> CompleteSurveyData(Survey survey)
        {
            Dictionary<int,Object> data = new Dictionary<int,Object>();

            foreach (Question question in survey.Questions) 
            {
                if(question.TypeTag == eTypeTag.INPUT_NUMBER)
                {
                    double[] arr = ExtractData.ExtractNumberAnswer(survey, question.Id);
                    double[] res = new double[4];
                    res[0] =  NumberInput.CalculateAverage(arr);
                    res[1] = NumberInput.CalculateMedian(arr);
                    res[2] = NumberInput.CalculateFirstMode(arr);
                    res[3] = NumberInput.CalculateStandardDeviation(arr);

                    data.Add(question.Id, res);

                }
                if (question.TypeTag == eTypeTag.INPUT_TEXT)
                {
                    string[] arr = ExtractData.ExtractTextAnswer(survey, question.Id);
                    data.Add(question.Id, arr);
                }
                if (question.TypeTag == eTypeTag.SELECT)
                {
                    string[] arr = ExtractData.ExtractSelectAndRadioAnswer(survey, question.Id);
                    string[] op = ExtractData.ParseAnswerValue(question.Options);
                    Dictionary<string, int> res = SelectAndRadioInput.CalculatePI(arr, op);
                    data.Add(question.Id, res);
                }
                if (question.TypeTag == eTypeTag.RADIO)
                {
                    string[] arr = ExtractData.ExtractSelectAndRadioAnswer(survey, question.Id);
                    string[] op = ExtractData.ParseAnswerValue(question.Options);
                    Dictionary<string, int> res = SelectAndRadioInput.CalculatePI(arr, op);
                    data.Add(question.Id, res);
                }
                if (question.TypeTag == eTypeTag.CHECKBOX)
                {
                    string[][] arr = ExtractData.ExtractCheckboxAnswer(survey, question.Id);
                    string[] op = ExtractData.ParseAnswerValue(question.Options);
                    Dictionary<string, int> res = CheckboxInput.CalculatePI(arr, op);
                    data.Add(question.Id, res);
                }

            }
            return data;
        }
    }
}
