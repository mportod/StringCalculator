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
      
        [TestCase("2,3", 5)]
        [TestCase("2,4", 6)]
        public void return_sum_when_numbers_contains_two_numbers(string numbersSeparatedByComma, int sum)
        {
            var result = sut.Add(numbersSeparatedByComma);

            result.Should().Be(sum);
        }

        [Test]
        public void return_ten_when_numbers_contains_the_numbers_two_five_and_three()
        {
            var result = sut.Add("2,3,5");

            result.Should().Be(10);
        }
    }
}
