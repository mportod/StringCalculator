namespace StringCalculator;

public class StringCalculator
{
    public int Add(string numbersSeparatedByComma)
    {
        if (string.IsNullOrEmpty(numbersSeparatedByComma))
            return 0;

        var numbers = numbersSeparatedByComma
            .Split(',')
            .Select(int.Parse)
            .ToList();

        return numbers.Sum();
    }
}
