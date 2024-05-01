using System.Text.RegularExpressions;

namespace StringCalculator;

public class StringCalculator
{
    public int Add(string numbersSeparatedByDelimiters)
    {
        if (IsEmptyString(numbersSeparatedByDelimiters))
            return 0;

        var numbers = GetNumbers(numbersSeparatedByDelimiters);
        ValidateNumbers(numbers);
        return numbers.Sum();
    }

    private static bool IsEmptyString(string numbersSeparatedByDelimiters)
    {
        return string.IsNullOrEmpty(numbersSeparatedByDelimiters);
    }

    private List<int> GetNumbers(string stringWithNumbers)
    {
        var delimiters = GetDelimiters(ref stringWithNumbers);
        var onlyNumbers = GetNumbersWithoutDelimitersSpecification(stringWithNumbers);
        onlyNumbers = NumbersWithOneDelimiter(delimiters, delimiters.First(), onlyNumbers);
        var numbers = onlyNumbers
            .Split(delimiters.First())
            .Select(int.Parse)
            .ToList();

        return GetNumbersLessThanThousand(numbers);
    }

    private static string NumbersWithOneDelimiter(List<string> delimiters, string newDelimiter, string stringWithoutSpecification)
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
            var firstNumberIndex = numbers.LastIndexOf(']') + 1;
            return numbers.Substring(firstNumberIndex);
        }
        else
        {
            var firstNumberIndex = numbers.LastIndexOf('\n') + 1;
            return numbers.Substring(firstNumberIndex);
        }
    }
    
    private static List<string> GetDelimiters(ref string numbers)
    {
        var delimiters = new List<string>() { ",", "\n" };
        if (numbers.Contains("//"))
        {
            if (numbers.Contains("[") && numbers.Contains("]"))
            {
                Regex regex = new Regex(@"\[(.+?)\]");
                var delimits = regex.Matches(numbers);
                delimiters.AddRange(delimits.Select(delimit => delimit.Groups[1].Value));
                numbers = numbers.Substring(numbers.IndexOf("\n", StringComparison.Ordinal) + 1);
            }
            else
            {
                delimiters.Add(numbers[2].ToString());
                numbers = numbers.Substring(4);
            }
        }
        return delimiters;
    }
}
