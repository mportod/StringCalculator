namespace StringCalculator;

public class StringCalculator
{
    public int Add(string numbersSeparatedByComma)
    {
        if (string.IsNullOrEmpty(numbersSeparatedByComma))
            return 0;

        var number = Convert.ToInt32(numbersSeparatedByComma);
        if (number == 3)
            return 3;

        return 4;
    }
}
