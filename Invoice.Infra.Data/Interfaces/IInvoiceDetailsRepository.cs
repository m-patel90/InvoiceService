﻿using Invoice.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infra.Data.Interfaces
{
    public interface IInvoiceDetailsRepository : IGenericRepository<InvoiceDetails>
    {
        List<InvoiceDetails> GetByInvoiceId(int invoiceId);
    }
}
