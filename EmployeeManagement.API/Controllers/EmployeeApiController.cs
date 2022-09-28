using EmployeeManagement.API.Models;
using EmployeeManagement.Application.Contracts;
using EmployeeManagement.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeeApiController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeApiController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        [HttpGet]
        [Route("employeeId/{Id}")]
        public IActionResult GetEmployeeById([FromRoute] int employeeId)
        {
            try
            {
                /// get employee by calling GetEmployeeById() in IEmployeeService and store it in a variable and Map that variable to EmployeeDetailedViewModel. 
                var employeeDetailedViewModel = _employeeService.GetEmployeeById(employeeId);
                return Ok(employeeDetailedViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("get-all")]
        public IActionResult GetEmployees()
        {
            /// get employees by calling GetEmployees() in IEmployeeService and store it in a variable and Map that variable to EmployeeDetailedViewModel. 
            try
            {
                var EmployeeViewModel = _employeeService.GetEmployees();

                return Ok(EmployeeViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("insertEmployee")]
        public IActionResult InsertEmployee([FromBody] EmployeeData employee)
        {
            try
            {
                var employeeDetailedViewModel = _employeeService.InsertEmployee(employee);
                return Ok(employeeDetailedViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        [Route("deleteEmployee")]
        public IActionResult DeleteEmployee([FromRoute] EmployeeData employee)
        {
            try
            {
                var employeeDetailedViewModel = _employeeService.DeleteEmployee(employee);
                return Ok(employeeDetailedViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [Route("updateEmployee")]
        public IActionResult UpdateEmployee([FromBody] EmployeeData employee)
        {
            try
            {
                var employeeDetailedViewModel = _employeeService.UpdateEmployee(employee);
                return Ok(employeeDetailedViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
    }
}
