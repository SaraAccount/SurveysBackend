using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.LogicQuastions
{
    public static class NumberInput
    {
        public static double CalculateAverage(double[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return 0;

            double sum = 0;
            foreach (double num in numbers)
            {
                sum += num;
            }

            return sum / numbers.Length;
        }

        public static double CalculateMedian(double[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return 0;

            var sorted = numbers.OrderBy(n => n).ToArray();
            int mid = sorted.Length / 2;

            if (sorted.Length % 2 == 0)
            {
                return (sorted[mid - 1] + sorted[mid]) / 2.0;
            }
            else
            {
                return sorted[mid];
            }
        }

        public static double CalculateFirstMode(double[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return 0;

            Dictionary<double, int> frequencyMap = new Dictionary<double, int>();

            foreach (double num in numbers)
            {
                if (frequencyMap.ContainsKey(num))
                    frequencyMap[num]++;
                else
                    frequencyMap[num] = 1;
            }

            int maxFrequency = frequencyMap.Values.Max();
            var modes = frequencyMap
                .Where(pair => pair.Value == maxFrequency)
                .Select(pair => pair.Key)
                .ToList();

            return modes.First();
        }

        public static List<double> CalculateModes(double[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return new List<double>();

            Dictionary<double, int> frequencyMap = new Dictionary<double, int>();

            foreach (double num in numbers)
            {
                if (frequencyMap.ContainsKey(num))
                    frequencyMap[num]++;
                else
                    frequencyMap[num] = 1;
            }

            int maxFrequency = frequencyMap.Values.Max();

            var modes = frequencyMap
                .Where(pair => pair.Value == maxFrequency)
                .Select(pair => pair.Key)
                .ToList();

            return modes;
        }

        public static double CalculateStandardDeviation(double[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return 0;

            double average = CalculateAverage(numbers);

            double sumOfSquares = 0;
            foreach (var num in numbers)
            {
                sumOfSquares += Math.Pow(num - average, 2);
            }

            double variance = sumOfSquares / numbers.Length;
            return Math.Sqrt(variance);
        }

    }
}


