using System;
using Shouldly;
using Xunit;

namespace Calabonga.Microservices.Core.Tests
{
    public class EnumHelperTests
    {

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_be_under_testing()
        {
            // arrange
            
            // act
            var sut = EnumHelper<TestType>.Parse("Value");
            
            // assert
            sut.ShouldBe(TestType.Value);
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_be_parsed_but_not_equals_to_Value()
        {
            // arrange
            
            // act
            var sut = EnumHelper<TestType>.Parse("Multiple");
            
            // assert
            sut.ShouldNotBe(TestType.Value);
        }
        
        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_not_be_parsed()
        {
            // arrange
            
            // act
            
            // assert
            Assert.Throws<ArgumentException>(() => EnumHelper<TestType>.Parse("NOT_FOUND"));
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_parse_DisplayAttribute()
        {
            // arrange
            
            // act
            var sut = EnumHelper<TestType>.Parse("Значение");
            
            // assert
            sut.ShouldBe(TestType.Value);
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_parse_None()
        {
            // arrange
            
            // act
            var sut = EnumHelper<TestType>.Parse("None");
            
            // assert
            sut.ShouldBe(TestType.None);
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_parse_DisplayAttribute_None()
        {
            // arrange
            
            // act
            var sut = EnumHelper<TestType>.Parse("Не определено");
            
            // assert
            sut.ShouldBe(TestType.None);
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_parse_DisplayAttribute_Multiple()
        {
            // arrange
            
            // act
            var sut = EnumHelper<TestType>.Parse("Значение 2");
            
            // assert
            sut.ShouldBe(TestType.Value);
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_parse_Simple_as_string()
        {
            // arrange
            
            // act
            var sut = EnumHelper<TestType>.Parse("Simple");
            
            // assert
            sut.ShouldBe(TestType.Simple);
        }

        [Theory]
        [Trait("EnumHelper", "Parsing")]
        [InlineData(TestType.Value, "Значение")]
        [InlineData(TestType.Simple, "Простой")]
        [InlineData(TestType.Simple, "Простой1")]
        [InlineData(TestType.Simple, "Простой2")]
        [InlineData(TestType.Simple, "Простой3")]
        public void ItShould_parse_DisplayAttribute_Simple_as_string(TestType expected, string actual)
        {
            // arrange
            
            // act
            var sut = EnumHelper<TestType>.Parse(actual);
            
            // assert
            sut.ShouldBe(expected);
        }

        
        [Trait("EnumHelper", "Parsing")]
        [Theory]
        [InlineData("Простой1")]
        [InlineData("Простой2")]
        [InlineData("Простой3")]
        public void ItShould_parse_DisplayNamesAttribute_Simple(string actual)
        {
            // arrange
            
            // act
            var sut = EnumHelper<TestType>.Parse(actual);
            
            // assert
            sut.ShouldBe(TestType.Simple);
        }
    }
}