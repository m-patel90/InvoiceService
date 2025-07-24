using FluentValidation;
using Invoice.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Applicaion.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(100);
            RuleFor(x => x.ProductCode).NotEmpty().MinimumLength(3).MaximumLength(20);
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Weight).GreaterThanOrEqualTo(0);
            RuleFor(x => x.CGST).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
            RuleFor(x => x.SGST).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
            RuleFor(x => x.HSNCode).NotEmpty().Length(4, 8);
            RuleFor(x => x.UniqueId).NotEmpty();
        }
    }
}
