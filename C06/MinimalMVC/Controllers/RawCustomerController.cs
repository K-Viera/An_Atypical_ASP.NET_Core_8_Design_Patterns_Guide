using Microsoft.AspNetCore.Mvc;
using MinimalMVC.Data;
using MinimalMVC.Models;

namespace MinimalMVC.Controllers
{
    public class CustomerController : ControllerBase
    {
        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> PutAsync(
            int id,
            [FromBody] Customer value,
            ICustomerRepository customerRepository)
        {
            var customer = await customerRepository.UpdateAsync(
                value,
                HttpContext.RequestAborted);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }
    }
}
