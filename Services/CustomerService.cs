using CRMAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CRMAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private const string DATA_LOCATION = @"Data/customers.json";
        private const string INVALID_DATA_LOCATION = "Invalid Data Location Specified!";

        private IEnumerable<Customer> customersList = default(IEnumerable<Customer>);

        public CustomerService()
        {
            var status = !string.IsNullOrEmpty(DATA_LOCATION) &&
                File.Exists(DATA_LOCATION);

            if (!status)
            {
                throw new ApplicationException(INVALID_DATA_LOCATION);
            }

            var contents = File.ReadAllText(DATA_LOCATION);

            this.customersList = JsonConvert.DeserializeObject<IEnumerable<Customer>>(contents);
        }

        public IEnumerable<Customer> GetCustomers(string customerName = null)
        {
            if (string.IsNullOrEmpty(customerName))
                return this.customersList;

            var filteredCustomersList =
                customersList
                    .Where(customer => customer.Name.Contains(customerName, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            return filteredCustomersList;
        }
    }
}
