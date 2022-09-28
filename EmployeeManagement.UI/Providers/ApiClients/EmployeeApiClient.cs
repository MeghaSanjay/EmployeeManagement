using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Models.Provider;
using EmployeeManagement.UI.Providers.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace EmployeeManagement.UI.Providers.ApiClients
{
    public class EmployeeApiClient : IEmployeeApiClient
    {
        private readonly HttpClient _httpClient;

        public EmployeeApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

       /* public EmployeeData DeleteEmployee(EmployeeDetailedViewModel employee)
        {
            throw new System.NotImplementedException();
        }*/

        public IEnumerable<EmployeeData> GetEmployees()
        {
            //Consume /employee endpoint in the EmployeeManagementApi using _httpClient
            using (var response = _httpClient.GetAsync("").Result)
            {
                var employee = JsonConvert.DeserializeObject<IEnumerable<EmployeeData>>(response.Content.ReadAsStringAsync().Result);
                return employee;
            }   
        }

       public EmployeeData GetEmployeeById(int id)
        {

        }
    }
}
