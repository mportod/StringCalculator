namespace StringCalculator;

public class StringCalculator
{
    public int Add(string numbersSeparatedByDelimiters)
    {
        if (string.IsNullOrEmpty(numbersSeparatedByDelimiters))
            return 0;

        if (numbersSeparatedByDelimiters.StartsWith("//"))
        {
            var delimiter = numbersSeparatedByDelimiters[2];
            var numbers = numbersSeparatedByDelimiters
                .Substring(4)
                .Split(delimiter)
                .Select(int.Parse)
                .ToList();
            
            return numbers.Sum();
        }
        else
        {
            var delimiters = new List<string> { "\n", "," };
            var numbers = numbersSeparatedByDelimiters
                .Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            return numbers.Sum();
        }
    }
}
