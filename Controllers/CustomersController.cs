using CRMAPI.Models;
using CRMAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("DefaultPolicy")]
    public class CustomersController : ControllerBase
    {
        private ICustomerService customerService = default(ICustomerService);

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet("{customerName?}")]
        public IActionResult GetCustomers(string customerName = default(string))
        {
            var filteredCustomersList = this.customerService.GetCustomers(customerName);

            if (filteredCustomersList == default(IEnumerable<Customer>))
                return new NoContentResult();

            return Ok(filteredCustomersList);
        }
    }
}
