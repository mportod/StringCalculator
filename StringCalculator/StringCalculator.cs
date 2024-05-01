namespace StringCalculator;

public class StringCalculator
{
    public int Add(string numbersSeparatedByDelimiter)
    {
        if (string.IsNullOrEmpty(numbersSeparatedByDelimiter))
            return 0;

        var numbers = numbersSeparatedByDelimiter
            .Replace("\n", ",")
            .Split(',')
            .Select(int.Parse)
            .ToList();

        return numbers.Sum();
    }
}
