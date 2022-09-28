using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Models.Provider;
using System.Collections.Generic;

namespace EmployeeManagement.UI.Providers.Contracts
{
    public interface IEmployeeApiClient
    {
        IEnumerable<EmployeeData> GetEmployees();
        EmployeeData GetEmployeeById(int id);

        EmployeeData DeleteEmployee(EmployeeDetailedViewModel employee);

        EmployeeData UpdateEmploye(EmployeeDetailedViewModel employee);
    }
}
