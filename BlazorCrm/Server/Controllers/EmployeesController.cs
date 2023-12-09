using BlazorCrm.Server.Data;
using BlazorCrm.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DataContext _context;
        public EmployeesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllContacts()
        {
            return await _context.Employees
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetContactById(int id)
        {
            var result = await _context.Employees.FindAsync(id);
            if (result is null)
            {
                return NotFound("Contact not found.");
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateContact(int id, Employee employee)
        {
            var dbEmployee = await _context.Employees.FindAsync(id);
            if (dbEmployee is null)
            {
                return NotFound("Contact not found.");
            }

            dbEmployee.FirstName = employee.FirstName;
            dbEmployee.LastName = employee.LastName;
            dbEmployee.NickName = employee.NickName;
            dbEmployee.Email = employee.Email;
            dbEmployee.Phone = employee.Phone;
            dbEmployee.DateOfBirth  = employee.DateOfBirth;
            dbEmployee.Place = employee.Place;
            dbEmployee.DateUpdated = DateTime.Now;




            return Ok(employee);
        }
    }


}
