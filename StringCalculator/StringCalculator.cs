using System.Reflection.Metadata.Ecma335;
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
        var numbers = new List<int>();
        if (HasSpecificDelimiter(stringWithNumbers))
        {
            char delimiter = GetSpecificDelimiter(stringWithNumbers);
            var stringWithoutSpecification= GetNumbersWithoutSpecification(stringWithNumbers);
            numbers = stringWithoutSpecification
                .Split(delimiter)
                .Select(int.Parse)
                .ToList();
        }
        else
        {
            var delimiters = new List<string> { "\n", "," };
            numbers = stringWithNumbers
                .Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }
        
        return GetNumbersLessThanThousand(numbers);
    }

    private static List<int> GetNumbersLessThanThousand(List<int> numbers)
    {
        return numbers.Where(n => n <= 1000).ToList();
    }

    private static bool HasSpecificDelimiter(string numbersSeparatedByDelimiters)
    {
        return numbersSeparatedByDelimiters.StartsWith("//");
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

    private static string GetNumbersOnStringWithDelimiter(List<int> numbers, string delimiter)
    {
        return String.Join(delimiter, numbers.ToArray());
    }

    private static List<int> GetNegativeNumbers(List<int> numbers)
    {
        return numbers.Where(n => n < 0).ToList();
    }

    private static bool ContainsNegativeNumbers(List<int> numbers)
    {
        return numbers.Any(n => n < 0);
    }

    private char GetSpecificDelimiter(string stringWithNumbers)
    {
        if (stringWithNumbers.Contains("["))
            return stringWithNumbers[stringWithNumbers.IndexOf('[') + 1];
     
        return stringWithNumbers[2];
    }

    private string GetNumbersWithoutSpecification(string stringWithNumbers)
    {
        if (stringWithNumbers.Contains("["))
        {
            var delimiter = GetSpecificDelimiter(stringWithNumbers);
            var stringWithoutSpecification = stringWithNumbers.Substring(stringWithNumbers.IndexOf('\n') + 1);
            return GetNumbersWithOneDelimiterOcurrence(stringWithoutSpecification, delimiter);
        }

        return stringWithNumbers.Substring(4);
    }

    private string GetNumbersWithOneDelimiterOcurrence(string stringWithNumbers, char delimiter){
        string pattern = $"{Regex.Escape(delimiter.ToString())}{{2,}}";
        return Regex.Replace(stringWithNumbers, pattern, delimiter.ToString());
    }
}
