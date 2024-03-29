﻿using BlazorCrm.Server.Data;
using BlazorCrm.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor.Kanban.Internal;

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
        public class EmployeesWithUpcomingEndDateResponse
        {
            public List<string> Names { get; set; }
        }

        [HttpGet("employeeswithupcomingenddate")]
        public async Task<ActionResult<EmployeesWithUpcomingEndDateResponse>> GetEmployeesWithUpcomingEndDate()
        {
            var today = DateTime.Today;
            var endDateThreshold = today.AddDays(10);

            var employeesWithUpcomingEndDate = await _context.Employees
                .Where(e => !e.IsDeleted && e.DateEnd > today && e.DateEnd <= endDateThreshold)
                .Select(e => e.FirstName + " " + e.LastName)
                .ToListAsync();

            var response = new EmployeesWithUpcomingEndDateResponse
            {
                Names = employeesWithUpcomingEndDate
            };

            return response;
        }


        [HttpGet("totalsalary")]
        public async Task<ActionResult<decimal>> GetTotalSalaryOfActiveEmployees()
        {
            var totalSalary = await _context.Employees.Where(e => !e.IsDeleted).SumAsync(e => e.Salary);
            return Convert.ToDecimal(totalSalary);
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetActiveEmployeesCount()
        {
            var count = await _context.Employees.Where(e => !e.IsDeleted).CountAsync();
            return count;
        }

        [HttpGet("inactivecount")]
        public async Task<ActionResult<int>> GetInActiveEmployeesCount()
        {
            var count = await _context.Employees.Where(e => e.IsDeleted).CountAsync();
            return count;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            return await _context.Employees
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
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
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            var dbEmployee = await _context.Employees.FindAsync(id);
            if (dbEmployee is null)
            {
                return NotFound("Contact not found.");
            }

            dbEmployee.FirstName = employee.FirstName;
            dbEmployee.LastName = employee.LastName;
            
            dbEmployee.Email = employee.Email;
            dbEmployee.Phone = employee.Phone;
            dbEmployee.NickName = employee.NickName;
            dbEmployee.Place = employee.Place;
            dbEmployee.Salary = employee.Salary;
            dbEmployee.DateOfBirth = employee.DateOfBirth;
            
            dbEmployee.DateUpdated = DateTime.Now;
            dbEmployee.DateStart = employee.DateStart;
            dbEmployee.DateEnd  = employee.DateEnd;
          
            await _context.SaveChangesAsync();




            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var dbEmployee = await _context.Employees.FindAsync(id);
            if (dbEmployee is null)
            {
                return NotFound("Contact not found.");
            }
            dbEmployee.IsDeleted = true;
            dbEmployee.DateDeleted = DateTime.Now;
            await _context.SaveChangesAsync();
            return await GetAllEmployees();
        }
    }

   
}
