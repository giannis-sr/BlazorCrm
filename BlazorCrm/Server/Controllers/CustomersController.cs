using BlazorCrm.Server.Data;
using BlazorCrm.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor.Kanban.Internal;

namespace BlazorCrm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
       
            private readonly DataContext _context;
            public CustomersController(DataContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<ActionResult<List<Customer>>> GetAllCustomers()
            {
                return await _context.Customers
                    .Where(c => !c.IsDeleted)
                    .ToListAsync();
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Customer>> GetCustomerById(int id)
            {
                var result = await _context.Customers.FindAsync(id);
                if (result is null)
                {
                    return NotFound("Contact not found.");
                }

                return result;
            }

            [HttpPost]
            public async Task<ActionResult<List<Customer>>> CreateCustomers(Customer customer)
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return Ok();
            }

            [HttpPut("{id}")]
            public async Task<ActionResult<Customer>> UpdateCustomer(int id, Customer customer)
            {
                var dbCustomer = await _context.Customers.FindAsync(id);
                if (dbCustomer is null)
                {
                    return NotFound("Contact not found.");
                }

                dbCustomer.FirstName = customer.FirstName;
                dbCustomer.LastName = customer.LastName;

                dbCustomer.Email = customer.Email;
                dbCustomer.Phone = customer.Phone;
                dbCustomer.NickName = customer.NickName;
                dbCustomer.Place = customer.Place;
                dbCustomer.Salary = customer.Salary;
                dbCustomer.DateOfBirth = customer.DateOfBirth;

                dbCustomer.DateUpdated = DateTime.Now;
                dbCustomer.DateStart = customer.DateStart;
                dbCustomer.DateEnd = customer.DateEnd;

                await _context.SaveChangesAsync();

                return Ok(customer);
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult<List<Customer>>> DeleteCustomer(int id)
            {
                var dbCustomer = await _context.Customers.FindAsync(id);
                if (dbCustomer is null)
                {
                    return NotFound("Contact not found.");
                }
                dbCustomer.IsDeleted = true;
                dbCustomer.DateDeleted = DateTime.Now;
                await _context.SaveChangesAsync();
                return await GetAllCustomers();
            }
        }
    }
