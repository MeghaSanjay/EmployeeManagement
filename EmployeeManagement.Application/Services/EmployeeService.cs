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
            var employeeData = _employeeRepository.GetEmployeeById(id);
            var employeeDto = new EmployeeDto()
            {
                Id = employeeData.Id,
                Name = employeeData.Name,
                Department = employeeData.Department,
                Age = employeeData.Age,
                Address = employeeData.Address
            };
            return employeeDto;
        }

        public IEnumerable<EmployeeDto> GetEmployees()
        {
            //Get data from Repository

            var listOfEmployeeData = _employeeRepository.GetEmployees();
            var listOfEmployeeDto = new List<EmployeeDto>();
            foreach (var employeeData in listOfEmployeeData)
            {
                var employee = new EmployeeDto()
                {
                    Id = employeeData.Id,
                    Name = employeeData.Name,
                    Department = employeeData.Department,
                    Age = employeeData.Age,
                    Address = employeeData.Address
                };
                listOfEmployeeDto.Add(employee);
            }
            return listOfEmployeeDto;       
        }
        public bool InsertEmployee(EmployeeDto employees)
        {
            var employeeData = new EmployeeData()
            {
              
                Name = employees.Name,
                Department = employees.Department,
                Age = employees.Age,
                Address = employees.Address
            };

            _employeeRepository.InsertEmployee(employeeData);
            return true;
        }
        public bool DeleteEmployee(int id)
        {
            /*var employeData = new EmployeeData()
            {
                Id=id

            };*/
            _employeeRepository.DeleteEmployee(id);
            return true;
        }
       public bool UpdateEmployee(EmployeeDto employees)
        {
            var employeData = new EmployeeData()
            {
                Id=employees.Id,
                Name = employees.Name,
                Department = employees.Department,
                Age = employees.Age,
                Address = employees.Address

            };
            _employeeRepository.UpdateEmployee(employeData);
            return true;
        }

        
    }
}
