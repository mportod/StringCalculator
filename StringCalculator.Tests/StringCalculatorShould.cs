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
        
        [Test]
        public void return_five_when_numbers_contains_numbers_two_and_three()
        {
            var result = sut.Add("2,3");

            result.Should().Be(5);
        }
        [Test]
        public void return_six_when_numbers_contains_numbers_two_and_four()
        {
            var result = sut.Add("2,4");

            result.Should().Be(6);
        }
    }
}