using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Models.Provider;
using EmployeeManagement.UI.Providers.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EmployeeManagement.UI.Providers.ApiClients
{
    public class EmployeeApiClient : IEmployeeApiClient
    {
        private readonly HttpClient _httpClient;

        public EmployeeApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IEnumerable<EmployeeViewModel> GetEmployees()
        {
            //Consume /employee endpoint in the EmployeeManagementApi using _httpClient
            using (var response = _httpClient.GetAsync("https://localhost:5001/api/employees").Result)
            {
                var employee = JsonConvert.DeserializeObject<IEnumerable<EmployeeViewModel>>(response.Content.ReadAsStringAsync().Result);
                return employee;
            }
        }

        public EmployeeDetailedViewModel GetEmployeeById(int id)
        {
            using (var respose = _httpClient.GetAsync("https://localhost:5001/api/getById/" + id).Result)
            {
                var employee = JsonConvert.DeserializeObject<EmployeeDetailedViewModel>(respose.Content.ReadAsStringAsync().Result);
                return employee;
            }

        }
        public bool InsertEmployee(EmployeeDetailedViewModel employee)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(employee),Encoding.UTF8,"application/json");
            using (var response = _httpClient.PostAsync("https://localhost:5001/api/insertemployees", stringContent).Result)
            {

                return true;
            }
        }
        public bool DeleteEmployee(int id)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(id));
            using (var response=_httpClient.DeleteAsync("https://localhost:5001/api/employees/"+ id).Result)
            {
                return true;
            }
            
        }
        public bool UpdateEmployee(EmployeeDetailedViewModel employeeDetailed)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(employeeDetailed),Encoding.UTF8, "application/json");
            using (var response = _httpClient.PutAsync("https://localhost:5001/api/updateemployees",stringContent).Result)
                return true;
        }   
    }
}
    

