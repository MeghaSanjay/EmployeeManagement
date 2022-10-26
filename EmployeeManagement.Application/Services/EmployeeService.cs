using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using System;
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
            try
            {
                ValidateEmployee(id);
                var employeeData = _employeeRepository.GetEmployeeById(id);
                if(employeeData==null)
                {
                    throw new Exception("Invalid id");
                }

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
            catch (Exception )
            {
                throw;
            }


        }

        public IEnumerable<EmployeeDto> GetEmployees()
        {
           
                //Get data from Repository
                var listOfEmployeeData = _employeeRepository.GetEmployees();
                if (listOfEmployeeData == null)
                {
                    throw new Exception("No data found");
                }
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
           var employee= _employeeRepository.InsertEmployee(employeeData);
            if(!employee)
            {
                throw new Exception("Cannot insert");
            }
            return true;
        }
        public bool DeleteEmployee(int id)
        {
           
            ValidateEmployee(id);
           var employeeData = _employeeRepository.DeleteEmployee(id);
            if(!employeeData)
            {
                throw new Exception("Invalid id");
            }
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
          var employee= _employeeRepository.UpdateEmployee(employeData);
            if(!employee)
            {
                throw new Exception("Invalid employee");
            }
            return true;
        }
        private  void ValidateEmployee(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Invalid id");
            }
        }
       


    }
}
