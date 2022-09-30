using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Models.Provider;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmployeeManagement.UI.Providers.Contracts
{
    public interface IEmployeeApiClient
    {
        IEnumerable<EmployeeViewModel> GetEmployees();
        EmployeeDetailedViewModel GetEmployeeById(int id);
        bool InsertEmployee(EmployeeDetailedViewModel employeeDetailedViewModel);

        bool DeleteEmployee(int id);

        bool UpdateEmployee(EmployeeDetailedViewModel employeeDetailed);
        
    }
}
