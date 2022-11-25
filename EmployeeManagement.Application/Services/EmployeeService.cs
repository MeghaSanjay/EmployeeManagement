using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using EmployeeManagement.Application.Models.Graph;
using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using EmployeeManagement.DataAccess.Repository;
using GraphQL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Services
{
    public class EmployeeService : GraphqlClientBase, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly HasuraConfiguration _hasuraConfiguration;


        public EmployeeService(IEmployeeRepository employeeRepository, IOptions<HasuraConfiguration> options)
        {
            _employeeRepository = employeeRepository;
            _hasuraConfiguration = options.Value;
          
        }

        public EmployeeDto GetEmployeeById(int id)
        {
                ValidateEmployee(id);

                var emp = _hasuraConfiguration.saveToDb;
                if (emp == true)
                {
                    var employeeData = _employeeRepository.GetEmployeeById(id);
                    if (employeeData == null)
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
            
            else
            {
                var employee = GetEmployeeByIdSaveToHasura(id).Result;
                return employee;
            }
        }
        public async Task<EmployeeDto>GetEmployeeByIdSaveToHasura(int id)
        {
            var getEmployeeId = @"query MyQuery2($id: Int!) {
                               EmployeeHasura_by_pk(id: $id) {
                                   id
                                   name
                                   age
                                   department
                                   address
                                   }
                                  }";
            var graphQlRequest = new GraphQLRequest
            {
                Query = getEmployeeId,
                Variables = new
                {
                    id
                }
            };
            var response = await _graphQLHttpClient.SendQueryAsync<GetEmployeeByIdQueryResponse>(graphQlRequest);
            ValidateGraphResponse(response);
            return response.Data.Employee;
        }

        public IEnumerable<EmployeeDto> GetEmployees()
        {
            //Get data from Repository
             var emp =  _hasuraConfiguration.saveToDb;
            var listOfEmployeeDto = new List<EmployeeDto>();
            if (emp == true)
            {
                var listOfEmployeeData = _employeeRepository.GetEmployees();

                if (listOfEmployeeData == null)
                {
                    throw new Exception("No data found");
                }
               
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
                
            }
            else
            {
                 listOfEmployeeDto = GetEmployeeSaveToHasura().Result.ToList();
            }
            return listOfEmployeeDto;
            
        }
        public async Task<IEnumerable<EmployeeDto>> GetEmployeeSaveToHasura()
        {
            var getEmployee = @"query MyQuery {
                                         EmployeeHasura {
                                         id
                                         name
                                         age
                                         department
                                         address
                                       }
                                    }";
            var graphQlRequest = new GraphQLRequest()
            {
                Query = getEmployee
            };
            var response = await _graphQLHttpClient.SendQueryAsync<GetEmployeeQueryResponse>(graphQlRequest);
            ValidateGraphResponse(response);
            return response.Data.Employees;

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
            var employeHasura = new EmployeeHasura()
            {
                Name=employees.Name,
                Department = employees.Department,
                Age = employees.Age,
                Address = employees.Address

            };
            var emp = _hasuraConfiguration.saveToDb;
            if (emp == true)
            {
                var employee = _employeeRepository.InsertEmployee(employeeData);
                if (!employee)
                {
                    throw new Exception("Cannot insert");
                }    
            }
            else
            {
              var employee=InsertEmployeeSaveToHasura(employeHasura);
               
            }

            return true;
        }
        public async Task<int> InsertEmployeeSaveToHasura(EmployeeHasura employeeHasura)
        {
            var insertEmployeeMutation = @"mutation MyMutation($employee: EmployeeHasura_insert_input!) {
                                                 insert_EmployeeHasura_one(object: $employee){
                                                  id
                                                 }
                                                }
                                                 ";

             var graphQlRequest = new GraphQLRequest
            {
                Query = insertEmployeeMutation,
                Variables = new
                {
                    employee = employeeHasura
                }
            };

            var response = await _graphQLHttpClient.SendMutationAsync<EmployeeInsertMutationResponse>(graphQlRequest);
            ValidateGraphResponse(response);

            return response.Data.Employee?.Id ?? 0;
        }
        public bool DeleteEmployee(int id)
        {
           
            ValidateEmployee(id);
            var emp = _hasuraConfiguration.saveToDb;
            if (emp == true)
            {
                var employeeData = _employeeRepository.DeleteEmployee(id);
                if (!employeeData)
                {
                    throw new Exception("Invalid id");
                }
            }
            else
            {
                var employee = DeleteEmployeeSaveToHasura(id);
            }
            return true;
        }
        public async Task<bool>DeleteEmployeeSaveToHasura(int id)
        {
            var deleteEmployeMutation = @"mutation deleteEmployee($employe: Int) {
                                      delete_EmployeeHasura(where: {id: {_eq: $employe}}) {
                                          affected_rows
                                         }
                                      }";
            var graphQlRequest = new GraphQLRequest
            {
                Query = deleteEmployeMutation,
                Variables = new
                {
                    employe = id
                }

            };
            var response = await _graphQLHttpClient.SendMutationAsync<EmployeDeleteMutationResponse>(graphQlRequest);
            ValidateGraphResponse(response);
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
            var emp = _hasuraConfiguration.saveToDb;
            if (emp == true)
            {
                var employee = _employeeRepository.UpdateEmployee(employeData);
                if (!employee)
                {
                    throw new Exception("Invalid employee");
                }
           }
            else
            {
                var employee = UpdateEmployeeSaveToHasura(employees);
            }
            return true;
        }
        public async Task<int> UpdateEmployeeSaveToHasura(EmployeeDto employeeDto)
        {
            var updateEmployeeMutation = @"mutation MyMutation($id: Int!, $employee: EmployeeHasura_set_input!) {
                                         update_EmployeeHasura_by_pk(pk_columns: {id: $id}, _set: $employee) {
                                            id
                                           }
                                         } ";
            var variables = new
            {
                id = employeeDto.Id,
                employee = new
                {
                   id=employeeDto.Id,
                   name=employeeDto.Name,
                   age=employeeDto.Age,
                   department=employeeDto.Department,
                   address=employeeDto.Address
                }
            };
            var graphQlRequest = new GraphQLRequest
            {
                Query = updateEmployeeMutation,
                Variables= variables          
            };
            var response = await _graphQLHttpClient.SendMutationAsync<EmployeeUpdateMutationResponse>(graphQlRequest);
            ValidateGraphResponse(response);
            return response.Data.Employee?.Id ?? 0;
        }

        private static void ValidateGraphResponse<T>(GraphQLResponse<T> response)
        {
            if (response.Errors != null && response.Errors.Any())
            {
                throw new Exception(string.Join(", ", response.Errors.Select(s => s.Message).ToList()));
            }
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
