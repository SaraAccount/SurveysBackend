using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.LogicQuastions
{
    public static class SelectAndRadioInput
    {
        public static Dictionary<string, int> CalculatePI(string[] data, string[] options)
        {
            int total = data.Length;

            if (total == 0)
            {
                return options.ToDictionary(opt => opt, opt => 0);
            }

            var optionCounts = options.ToDictionary(opt => opt, opt => 0);

            foreach (var answer in data)
            {
                if (optionCounts.ContainsKey(answer))
                {
                    optionCounts[answer]++;
                }
            }

            //convert to percent
            var result = optionCounts.ToDictionary(
                kvp => kvp.Key,
                kvp => (int)Math.Round((kvp.Value * 100.0) / total)
            );

            return result;
        }


    }
}
