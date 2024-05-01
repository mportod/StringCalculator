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

        [TestCase("2,4", 6, Description = "step 1")]
        [TestCase("2,3,5", 10, Description = "step 2")]
        [TestCase("1\n2,3", 6, Description = "step 3")]
        [TestCase("1\n2,3\n,4", 10, Description = "step 3")]
        [TestCase("//;\n1;3", 4, Description = "step 4")]
        [TestCase("2,1005,4500,3,1", 6, Description = "step 5")]
        [TestCase("//;\n1;1001;4", 5, Description = "step 5")]
        [TestCase("//[;;;]\n1;;;1001;;;4", 5, Description = "step 7")]
        [TestCase("//[*][%]\n1**2%3", 6, Description = "step 8")]
        [TestCase("//[**][%%%]\n1**2%%%3", 6, Description = "step 9")]
        [TestCase("//[**][%-]\n1**2%-3", 6)]
        public void return_sum_when_numbers_contains_numbers(string numbers, int sum)
        {
            var result = sut.Add(numbers);

            result.Should().Be(sum);
        }

        [TestCase("-2,3", "-2")]
        [TestCase("//;\n2;-3;-1", "-3,-1")]
        public void get_an_error_when_numbers_contains_negative_numbers(string numbers, string negativeNumbers)
        {
            var action = () => sut.Add(numbers);
            action.Should()
                .Throw<ArgumentException>().And.Message.Should().Be($"negatives not allowed ({negativeNumbers})");
        }
    }
}
