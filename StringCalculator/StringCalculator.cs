namespace StringCalculator;

public class StringCalculator
{
    public int Add(string numbersSeparatedByDelimiters)
    {
        if (string.IsNullOrEmpty(numbersSeparatedByDelimiters))
            return 0;

        var delimiters = new List<string> { "\n", "," };
        var numbers = numbersSeparatedByDelimiters
            .Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        return numbers.Sum();
    }
}
