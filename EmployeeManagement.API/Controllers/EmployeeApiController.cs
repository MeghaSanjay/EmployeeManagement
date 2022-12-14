using EmployeeManagement.API.Models;
using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
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
        [Route("getById/{id}")]
        public IActionResult GetEmployeesById([FromRoute] int Id)
        {
            try
            {
                /// get employee by calling GetEmployeeById() in IEmployeeService and store it in a variable and Map that variable to EmployeeDetailedViewModel.
                ValidateEmployee(Id); 
               var employeeDetailedViewModel = _employeeService.GetEmployeeById(Id);
                if (employeeDetailedViewModel == null) {
                    throw new Exception("Invalid id");
                }

                var employeDto = new EmployeeDto
                {
                    Id=employeeDetailedViewModel.Id,
                    Name=employeeDetailedViewModel.Name,
                    Department=employeeDetailedViewModel.Department,
                    Age=employeeDetailedViewModel.Age,
                    Address=employeeDetailedViewModel.Address 
                };
                return Ok(employeDto);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("employees")]
        public IActionResult GetEmployee()
        {
            /// get employees by calling GetEmployees() in IEmployeeService and store it in a variable and Map that variable to EmployeeDetailedViewModel. 
            try
            {
                var listOfEmployeeView = _employeeService.GetEmployees();
                if(listOfEmployeeView==null)
                {
                    throw new Exception("No data found");
                }
                var listOfEmployeView = new List<EmployeeDetailedViewModel>();
                foreach(var employeView in listOfEmployeeView)
                {
                    var employee = new EmployeeDetailedViewModel()
                    {
                        Id=employeView.Id,
                        Name=employeView.Name,
                        Department=employeView.Department

                    };
                    listOfEmployeView.Add(employee);
                }

                return Ok(listOfEmployeeView);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("insertemployees")]
        public IActionResult InsertEmployees([FromBody] EmployeeDetailedViewModel employeeDetailed)
        {
            try
            {
                var employeeDto = new EmployeeDto()
                {
                   // Id = employeeDetailed.Id,
                    Name = employeeDetailed.Name,
                    Department = employeeDetailed.Department,
                    Age = employeeDetailed.Age,
                    Address = employeeDetailed.Address
                };
                var employeeDetailedViewModel = _employeeService.InsertEmployee(employeeDto);
               if(!employeeDetailedViewModel)
                {
                    throw new Exception("Cannot insert");
                }
                return Ok(employeeDetailedViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        [Route("employees/{id}")]
        public IActionResult DeleteEmployees([FromRoute] int id)
        {
            try
            {
                ValidateEmployee(id);
                var employeeDetailedViewModel = _employeeService.DeleteEmployee(id);
                if (!employeeDetailedViewModel)
                {
                    throw new Exception("Invalid Employee");
                }
                return Ok(employeeDetailedViewModel);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
      
        [HttpPut]
        [Route("updateemployees")]
        public IActionResult UpdateEmployees([FromBody] EmployeeDetailedViewModel employeeDetailed)
        {
            try
            {
               
                var employeeDto = new EmployeeDto()
                {
                    Id= employeeDetailed.Id,
                    Name= employeeDetailed.Name,
                    Department = employeeDetailed.Department,
                    Age = employeeDetailed.Age,
                    Address = employeeDetailed.Address
                };
               
               var employeeDetailedViewModel = _employeeService.UpdateEmployee(employeeDto);
                if(!employeeDetailedViewModel)
                {
                    throw new Exception("Invalid Employee");
                }
                return Ok(employeeDetailedViewModel);
            }
            catch (Exception ex)
            {
               return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        private void ValidateEmployee(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Invalid id");
            }
        }
    }
}
