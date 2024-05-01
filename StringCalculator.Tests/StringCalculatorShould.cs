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

        [Test]
        public void return_three_when_numbers_contains_only_three_number()
        {
            var result = sut.Add("3");

            result.Should().Be(3);
        }

        [Test]
        public void return_four_when_numbers_contains_only_four_number()
        {
            var result = sut.Add("4");

            result.Should().Be(4);
        }
    }
}