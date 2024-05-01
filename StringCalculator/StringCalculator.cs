using System.Text.RegularExpressions;

namespace StringCalculator;

public class StringCalculator
{
    public int Add(string numbersAndDelimiters)
    {
        if (IsEmptyString(numbersAndDelimiters))
            return 0;

        var numbers = GetNumbers(numbersAndDelimiters);
        ValidateNumbers(numbers);
        return numbers.Sum();
    }

    private static bool IsEmptyString(string numbersAndDelimiters)
    {
        return string.IsNullOrEmpty(numbersAndDelimiters);
    }

    private List<int> GetNumbers(string numbersAndDelimiters)
    {
        var delimiters = GetDelimiters(numbersAndDelimiters);
        var onlyNumbers = GetNumbersWithoutDelimitersSpecification(numbersAndDelimiters);
        onlyNumbers = GetNumbersReplacedWithOneDelimiter(delimiters, delimiters.First(), onlyNumbers);
        var numbers = onlyNumbers
            .Split(delimiters.First())
            .Select(int.Parse)
            .ToList();

        return GetNumbersLessThanThousand(numbers);
    }

    private static string GetNumbersReplacedWithOneDelimiter(List<string> delimiters, string newDelimiter, string stringWithoutSpecification)
    {
        delimiters.ForEach(d => stringWithoutSpecification = stringWithoutSpecification.Replace(d, newDelimiter));
        string pattern = $"{Regex.Escape(newDelimiter)}{{2,}}";
        stringWithoutSpecification = Regex.Replace(stringWithoutSpecification, pattern, newDelimiter);
        return stringWithoutSpecification;
    }

    private static List<int> GetNumbersLessThanThousand(List<int> numbers)
    {
        return numbers.Where(n => n <= 1000).ToList();
    }

    private static void ValidateNumbers(List<int> numbers)
    {
        if (ContainsNegativeNumbers(numbers))
        {
            var negativeNumbers = GetNegativeNumbers(numbers);
            var negativeNumbersAsString = GetNumbersOnStringWithDelimiter(negativeNumbers, ",");
            throw new ArgumentException($"negatives not allowed ({negativeNumbersAsString})");
        }
    }
    private static List<int> GetNegativeNumbers(List<int> numbers)
    {
        return numbers.Where(n => n < 0).ToList();
    }
    
    private static bool ContainsNegativeNumbers(List<int> numbers)
    {
        return numbers.Any(n => n < 0);
    }
    
    private static string GetNumbersOnStringWithDelimiter(List<int> numbers, string delimiter)
    {
        return String.Join(delimiter, numbers.ToArray());
    }

    private string GetNumbersWithoutDelimitersSpecification(string numbers)
    {
        if (!numbers.StartsWith("//"))
            return numbers;

        if (numbers.Contains("[") && numbers.Contains("]"))
        {
            var firstNumberIndex = numbers.LastIndexOf(']') + 2;
            return numbers.Substring(firstNumberIndex);
        }
        else
        {
            var firstNumberIndex = numbers.LastIndexOf('\n') + 1;
            return numbers.Substring(firstNumberIndex);
        }
    }
    
    private static List<string> GetDelimiters(string numbers)
    {
        var delimiters = new List<string>() { ",", "\n" };
        if (numbers.Contains("//"))
        {
            if (numbers.Contains("[") && numbers.Contains("]"))
            {
                Regex regex = new Regex(@"\[(.+?)\]");
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
