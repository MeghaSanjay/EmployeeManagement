using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Models.Graph
{
    public class EmployeeInsertMutationResponse
    {
        [JsonProperty("insert_EmployeeHasura_one")]
        public EmployeeDto Employee { get; set; }
    }
    public class EmployeeUpdateMutationResponse
    {
        [JsonProperty(" update_EmployeeHasura_by_pk")]
        public EmployeeDto Employee { get; set; }
    }
    public class EmployeDeleteMutationResponse
    {
        [JsonProperty("delete_EmployeeHasura")]
        public AffectedRowResponse Employee { get; set; }
    }
}
