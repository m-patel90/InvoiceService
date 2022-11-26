using FluentValidation;
using Invoice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Applicaion.Validations
{
    public class InvoiceInfoValidator : AbstractValidator<InvoiceInfo>
    {
        public InvoiceInfoValidator()
        {
            RuleFor(InvoiceInfo => InvoiceInfo.InvoiceNo).NotEmpty();
        }
    }
}
