using FluentAssertions;
using NUnit.Framework;

namespace StringCalculator.Tests
{
    public class StringCalculatorShould
    {
        private StringCalculator sut = default!;

        [SetUp]
        public void Setup()
        {
            sut = new StringCalculator();
        }

        [Test]
        public void return_zero_when_numbers_is_empty()
        {
            var result = sut.Add("");

            result.Should().Be(0);
        }

        [TestCase(3)]
        [TestCase(4)]
        public void return_number_when_numbers_contains_only_one_number(int number)
        {
            var result = sut.Add(number.ToString());

            result.Should().Be(number);
        }
      
        [TestCase("2,4", 6)]
        [TestCase("2,3,5", 10)]
        public void return_sum_when_numbers_contains_numbers_separated_by_comma(string numbersSeparatedByComma, int sum)
        {
            var result = sut.Add(numbersSeparatedByComma);

            result.Should().Be(sum);
        }

        [TestCase("1\n2,3", 6)]
        [TestCase("1\n2,3\n,4", 10)]
        public void return_sum_when_numbers_contains_line_break_character(string numbersSeparatedByDelimiters, int sum)
        {
            var result = sut.Add(numbersSeparatedByDelimiters);

            result.Should().Be(sum);
        }

        [Test]
        public void return_sum_when_numbers_contains_specify_separator_and_numbers()
        {
            var result = sut.Add("//;\n1;3");

            result.Should().Be(4);
        }

        [TestCase("-2,3", "-2")]
        [TestCase("//;\n2;-3;-1", "-3,-1")]
        public void get_an_error_when_numbers_contains_negative_numbers(string numbers, string negativeNumbers)
        {
            var action = () => sut.Add(numbers);
            action.Should()
                .Throw<ArgumentException>().And.Message.Should().Be($"negatives not allowed ({negativeNumbers})");
        }

        [Test]
        public void return_sum_ignoring_numbers_greater_than_1000()
        {
            var result = sut.Add("//;\n1;1001;4");

            result.Should().Be(5);
        }
    }
}
