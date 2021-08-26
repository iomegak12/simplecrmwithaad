using CRMAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMAPI.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomers(string customerName = default(string));
    }
}
