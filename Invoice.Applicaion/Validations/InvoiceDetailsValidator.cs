using FluentValidation;
using Invoice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Applicaion.Validations
{
    public class InvoiceDetailsValidator : AbstractValidator<InvoiceDetails>
    {
        public InvoiceDetailsValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty(); 
            RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
        }
    }
}
