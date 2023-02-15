using FluentValidation;
using Invoice.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Applicaion.Validations
{
    public class InvoiceInfoValidator : AbstractValidator<InvoiceInfo>
    {
        public InvoiceInfoValidator()
        {
            RuleFor(InvoiceInfo => InvoiceInfo.InvoiceNo).NotNull()
                .MinimumLength(3).WithMessage("Please enter InvoceNo!");
            RuleFor(InvoiceInfo =>  InvoiceInfo.InvoiceDate).NotEmpty();
            RuleFor(InvoiceInfo => InvoiceInfo.DueDate).NotEmpty().GreaterThan(InvoiceInfo => InvoiceInfo.InvoiceDate);
            RuleFor(InvoiceInfo => InvoiceInfo.invoiceDetails.Count).GreaterThan(0);
            RuleForEach(InvoiceInfo => InvoiceInfo.invoiceDetails).SetValidator(new InvoiceDetailsValidator()).When(x => x.invoiceDetails.Count > 0);

            //RuleFor(x => x.invoiceDetails).Custom((list, context) => {
            //    if (list.Count == 0)
            //    {
            //        context.AddFailure("Invoice list must contains items");
            //    }   
            //});

            //RuleFor(x => x.invoiceDetails).ListMustContainThan(1);
        }
    }
}
