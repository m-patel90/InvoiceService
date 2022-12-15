using Invoice.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Applicaion.CQRS.Notifications
{
    public class ProductAddedNotification : INotification
    {
        public Product product { get; set; }
    }
}
