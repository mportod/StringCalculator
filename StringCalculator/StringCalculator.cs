using System.Text.RegularExpressions;

namespace StringCalculator;

public class StringCalculator
{
    public int Add(string numbersAndDelimiters)
    {
        if (string.IsNullOrEmpty(numbersAndDelimiters))
            return 0;

        var numbers = GetNumbers(numbersAndDelimiters);
        ValidateNumbers(numbers);
        return numbers.Sum();
    }

    private List<int> GetNumbers(string numbersAndDelimiters)
    {
        var delimiters = GetDelimiters(numbersAndDelimiters);
        var onlyNumbers = GetNumbersWithoutDelimitersSpecification(numbersAndDelimiters);
        onlyNumbers = GetNumbersReplacedWithOneDelimiter(delimiters, delimiters.First(), onlyNumbers);
        var numbers = onlyNumbers
            .Split(delimiters.First())
            .Select(int.Parse)
            .Where(n => n <= 1000)
            .ToList();

        return numbers;
    }

    private static string GetNumbersReplacedWithOneDelimiter(List<string> delimiters, string newDelimiter, string stringWithoutSpecification)
    {
        delimiters.ForEach(d => stringWithoutSpecification = stringWithoutSpecification.Replace(d, newDelimiter));
        string pattern = $"{Regex.Escape(newDelimiter)}{{2,}}";
        stringWithoutSpecification = Regex.Replace(stringWithoutSpecification, pattern, newDelimiter);
        return stringWithoutSpecification;
    }

    private void ValidateNumbers(List<int> numbers)
    {
        if (numbers.Any(n => n < 0))
        {
            var negativeNumbers = numbers.Where(n => n < 0).ToList();
            var negativeNumbersAsString = string.Join(",", negativeNumbers);
            throw new ArgumentException($"negatives not allowed ({negativeNumbersAsString})");
        }
    }

    private string GetNumbersWithoutDelimitersSpecification(string numbers)
    {
        if (!numbers.StartsWith("//"))
            return numbers;

        var firstNumberIndex = numbers.LastIndexOf('\n') + 1;
        return numbers.Substring(firstNumberIndex);
    }

    private List<string> GetDelimiters(string numbers)
    {
        var delimiters = new List<string>() { ",", "\n" };
        if (numbers.Contains("//"))
        {
            if (numbers.Contains("[") && numbers.Contains("]"))
            {
                var regex = new Regex(@"\[(.+?)\]");
                var delimits = regex.Matches(numbers);
                delimiters.AddRange(delimits.Select(delimit => delimit.Groups[1].Value));
            }
            else
            {
                delimiters.Add(numbers[2].ToString());
            }
        }
        return delimiters;
    }
}
