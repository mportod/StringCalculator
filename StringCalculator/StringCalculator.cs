namespace StringCalculator;

public class StringCalculator
{
    public int Add(string numbersSeparatedByDelimiters)
    {
        if (IsEmptyString(numbersSeparatedByDelimiters))
            return 0;

        if (HasSpecificDelimiter(numbersSeparatedByDelimiters))
        {
            return AddWithSpecificDelimiter(numbersSeparatedByDelimiters);
        }

        return AddWithDefaultDelimiters(numbersSeparatedByDelimiters);
    }

    private static bool IsEmptyString(string numbersSeparatedByDelimiters)
    {
        return string.IsNullOrEmpty(numbersSeparatedByDelimiters);
    }

    private static int AddWithDefaultDelimiters(string numbersSeparatedByDelimiters)
    {
        var delimiters = new List<string> { "\n", "," };
        var numbers = numbersSeparatedByDelimiters
            .Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        ValidateNumbers(numbers);
        return numbers.Sum();
    }

    private static int AddWithSpecificDelimiter(string numbersSeparatedByDelimiters)
    {
        var delimiter = numbersSeparatedByDelimiters[2];
        var numbers = numbersSeparatedByDelimiters
            .Substring(4)
            .Split(delimiter)
            .Select(int.Parse)
            .ToList();
       
        ValidateNumbers(numbers);
        return numbers.Sum();
    }

    private static bool HasSpecificDelimiter(string numbersSeparatedByDelimiters)
    {
        return numbersSeparatedByDelimiters.StartsWith("//");
    }

    private static void ValidateNumbers(List<int> numbers)
    {
        if (ContainsNegativeNumbers(numbers))
            throw new ArgumentException("negatives not allowed");
    }

    private static bool ContainsNegativeNumbers(List<int> numbers)
    {
        return numbers.Any(n => n < 0);
    }
}
