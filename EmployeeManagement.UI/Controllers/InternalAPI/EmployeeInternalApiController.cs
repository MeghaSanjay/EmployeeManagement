using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Providers.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.UI.Controllers.InternalAPI
{
    [Route("api/internal/employees")]
    [ApiController]
    public class EmployeeInternalApiController : ControllerBase
    {
        private readonly IEmployeeApiClient _employeeApiClient;

        public EmployeeInternalApiController(IEmployeeApiClient employeeApiClient)
        {
            this._employeeApiClient = employeeApiClient;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetEmployesById([FromRoute] int id)
        {
            try
            {
                var employee = _employeeApiClient.GetEmployeeById(id);

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult InsertEmployes([FromBody] EmployeeDetailedViewModel employeeDetailed)
        {
            try
            {
                var employe = _employeeApiClient.InsertEmployee(employeeDetailed);
                return Ok(employe);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteEmploye([FromRoute] int id)
        {
            try
            {
                var employe = _employeeApiClient.DeleteEmployee(id);
                return Ok(employe);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        public IActionResult UpdateEmploye([FromBody] EmployeeDetailedViewModel employeeDetailed)
        {
            try
            {
                var employe = _employeeApiClient.UpdateEmployee(employeeDetailed);
                return Ok(employe);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}

