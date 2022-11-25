using EmployeeManagement.Application.Models;
using EmployeeManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Contracts
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetEmployees();
        EmployeeDto GetEmployeeById(int id);
        bool InsertEmployee(EmployeeDto employees);
        bool DeleteEmployee(int id);
        bool UpdateEmployee(EmployeeDto employees);
        Task<IEnumerable<EmployeeDto>> GetEmployeeSaveToHasura();
        Task<EmployeeDto> GetEmployeeByIdSaveToHasura(int id);
        Task<int> InsertEmployeeSaveToHasura(EmployeeHasura employeeHasura);
        Task<bool> DeleteEmployeeSaveToHasura(int id);



    }
}
