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

    private List<int> GetNumbers(string numbersSeparatedByDelimiters)
    {
        var numbers = new List<int>();
        if (HasSpecificDelimiter(numbersSeparatedByDelimiters))
        {
            var delimiter = numbersSeparatedByDelimiters[2];
            numbers = numbersSeparatedByDelimiters
                .Substring(4)
                .Split(delimiter)
                .Select(int.Parse)
                .ToList();
        }
        else
        {
            var delimiters = new List<string> { "\n", "," };
            numbers = numbersSeparatedByDelimiters
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

  
}
