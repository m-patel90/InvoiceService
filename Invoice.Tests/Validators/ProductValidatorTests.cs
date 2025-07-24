using FluentAssertions;
using Invoice.Applicaion.Validations;
using Invoice.Domain.Entity;
using System;
using Xunit;

namespace Invoice.Tests.Validators
{
    public class ProductValidatorTests
    {
        private readonly ProductValidator _validator;

        public ProductValidatorTests()
        {
            _validator = new ProductValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var product = new Product { Name = "" };
            var result = _validator.Validate(product);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == "Name");
        }

        [Fact]
        public void Should_Have_Error_When_ProductCode_Is_Empty()
        {
            var product = new Product { ProductCode = "" };
            var result = _validator.Validate(product);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == "ProductCode");
        }

        [Fact]
        public void Should_Have_Error_When_Price_Is_Zero_Or_Negative()
        {
            var product = new Product { Price = 0 };
            var result = _validator.Validate(product);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == "Price");
        }

        [Fact]
        public void Should_Have_Error_When_CGST_Is_Greater_Than_100()
        {
            var product = new Product { CGST = 101 };
            var result = _validator.Validate(product);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == "CGST");
        }

        [Fact]
        public void Should_Have_Error_When_HSNCode_Is_Invalid_Length()
        {
            var product = new Product { HSNCode = "12" };
            var result = _validator.Validate(product);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == "HSNCode");
        }

        [Fact]
        public void Should_Be_Valid_When_All_Properties_Are_Correct()
        {
            var product = new Product
            {
                Name = "Test Product",
                ProductCode = "TP001",
                Description = "Test Description",
                Price = 100.50m,
                Weight = 1.5m,
                CGST = 9,
                SGST = 9,
                HSNCode = "1234",
                UniqueId = Guid.NewGuid()
            };

            var result = _validator.Validate(product);
            result.IsValid.Should().BeTrue();
        }
    }
}
