using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public EmployeeDto GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            return null;
        }

        public IEnumerable<EmployeeDto> GetEmployees()
        {
            //Get data from Repository
            var employee = _employeeRepository.GetEmployees();
            return null;
        }
        public EmployeeDto InsertEmployee(EmployeeData employee)
        {
            var employe = _employeeRepository.InsertEmployee(employee);
            return null;
        }
        public EmployeeDto DeleteEmployee(EmployeeData employee)
        {
            var employe = _employeeRepository.DeleteEmployee(employee);
            return null;
        }
       public EmployeeDto  UpdateEmployee(EmployeeData employee)
        {
            var employe = _employeeRepository.UpdateEmployee(employee);
            return null;
        }

    }
}
