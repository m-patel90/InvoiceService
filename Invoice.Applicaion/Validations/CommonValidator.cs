using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Applicaion.Validations
{
    public static class CommonValidator
    {
        public static IRuleBuilderOptionsConditions<T, IList<TElement>> ListMustContainThan<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder, int num)
        {

            return ruleBuilder.Custom((list, context) => {
                if (list.Count == num)
                {
                    context.AddFailure("The list must contain atleast one item");
                }
            });
        }
    }
}
