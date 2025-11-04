using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.LogicQuastions
{
    public static class CheckboxInput
    {
        public static Dictionary<string, int> CalculatePI(string[][] data, string[] options)
        {
            var totalSelections = data.Sum(arr => arr.Length);

            if (totalSelections == 0)
            {
                return options.ToDictionary(opt => opt, opt => 0);
            }

            var optionCount = options.ToDictionary(opt => opt, opt => 0);

            foreach (var userSelections in data)
            {
                foreach (var selection in userSelections)
                {
                    if (optionCount.ContainsKey(selection))
                        optionCount[selection]++;
                }
            }

            // convert to percent
            var result = optionCount.ToDictionary(
                kvp => kvp.Key,
                kvp => (int)Math.Round((kvp.Value * 100.0) / totalSelections)
            );

            return result;
        }
    }
}
