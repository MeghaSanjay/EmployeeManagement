using EmployeeManagement.Application.Contracts;
using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Providers.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.UI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeApiClient _employeeApiClient;

        public EmployeeController(IEmployeeApiClient employeeApiClient)
        {
            this._employeeApiClient = employeeApiClient;
        }

        public IActionResult Index()
        {
            try
            { 
                //Dummy Data Need to Replace with employees object
                var employee = _employeeApiClient.GetEmployees();
             
                return View(employee);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IActionResult GetEmployeById(int id)
        {
            try
            {
                var employeeDetailedView = _employeeApiClient.GetEmployeeById(id);

                return View(employeeDetailedView);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public IActionResult InsertEmploye(EmployeeDetailedViewModel employeeDetailed)
        {
            try
            {
                var employeeDetailedview = _employeeApiClient.InsertEmployee(employeeDetailed);
                return View(employeeDetailedview);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public IActionResult DeleteEmployes(int id)
        {
            try
            {
                var employeDetailedView = _employeeApiClient.DeleteEmployee(id);
                return View(employeDetailedView);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public IActionResult UpdateEmployes(EmployeeDetailedViewModel employeeDetailed)
        {
            try
            {
                var employeDetailedView = _employeeApiClient.UpdateEmployee(employeeDetailed);
                return View(employeDetailedView);
            }
            catch(Exception)
            {
                throw;
            }
        }
        
    }
}
