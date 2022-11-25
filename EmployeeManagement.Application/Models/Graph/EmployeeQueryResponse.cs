using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Models.Graph
{
    public class GetEmployeeQueryResponse
    {

        [JsonProperty("EmployeeHasura")]
        public IEnumerable<EmployeeDto> Employees { get; set; }
    }
        public class GetEmployeeByIdQueryResponse
        {

        [JsonProperty("EmployeeHasura_by_pk")]
        public EmployeeDto Employee { get; set; }
        }   
}
