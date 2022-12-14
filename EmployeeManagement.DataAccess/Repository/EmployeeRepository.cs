using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using Microsoft.Data.SqlClient;
//using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.DataAccess.Repository
{
    /// <summary>
    /// Connect To Database and Perforum CRUD operations
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        
        private SqlConnection _sqlConnection;
       
        public EmployeeRepository()
            {
            _sqlConnection = new SqlConnection("data source=(localdb)\\mssqllocaldb;database=TRAINING;");

            }

        public EmployeeData GetEmployeeById(int id)
        {
            //Take data from Table By Id
            try
            {
                _sqlConnection.Open();
                var sqlcommand = new SqlCommand(cmdText: "select * from EMPLOYEE where Id = @id", _sqlConnection);
                sqlcommand.Parameters.AddWithValue("Id", id);
                var sqlDataReader = sqlcommand.ExecuteReader();
                var employee = new EmployeeData();  

                while (sqlDataReader.Read())
                {
                    employee = new EmployeeData();
                    employee.Id = (int)sqlDataReader["Id"];
                    employee.Name = (string)sqlDataReader["Name"];
                    employee.Department = (string)sqlDataReader["Department"];
                    employee.Age = (int)sqlDataReader["Age"];
                    employee.Address = (string)sqlDataReader["Address"];

                }
                return employee;
            }
            catch 
            {
                throw;

            }
            finally
            {
                _sqlConnection.Close();
            }
        }
        public IEnumerable<EmployeeData> GetEmployees()
        {
            //Take data from Table

            try
            {
                _sqlConnection.Open();
                var sqlcommand = new SqlCommand(cmdText: "SELECT Id,Name,Department FROM EMPLOYEE", _sqlConnection);
                var sqlDataReader = sqlcommand.ExecuteReader();
                var listOfEmployee = new List<EmployeeData>();

                while (sqlDataReader.Read())
                {
                    listOfEmployee.Add(new EmployeeData()
                    {
                        Id = (int)sqlDataReader["Id"],
                        Name = (string)sqlDataReader["Name"],
                        Department = (string)sqlDataReader["Department"]      
                    });
                }
                return listOfEmployee;
            }
            catch 
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }     
        }
        public bool InsertEmployee(EmployeeData employee)
        {
            try
            {
                _sqlConnection.Open();
                var sqlcommand = new SqlCommand(cmdText: "INSERT INTO EMPLOYEE(Name, Department, Age, Address)values(@name,@dept, @age, @address)", _sqlConnection);
                sqlcommand.Parameters.AddWithValue("name", employee.Name);
                sqlcommand.Parameters.AddWithValue("dept", employee.Department);
                sqlcommand.Parameters.AddWithValue("age", employee.Age);
                sqlcommand.Parameters.AddWithValue("address", employee.Address);
                sqlcommand.ExecuteNonQuery();
                return true;
            }
            catch 
            {
                
                throw;
            }
            finally
            {
                _sqlConnection.Close();

            }

        }
        public bool DeleteEmployee(int id)
        {
            try
            {
                _sqlConnection.Open();
                var sqlcommand = new SqlCommand(cmdText: "delete from EMPLOYEE where Id=@id", _sqlConnection);
                sqlcommand.Parameters.AddWithValue("id",id);
                sqlcommand.ExecuteNonQuery();
                return true;
            }
            catch 
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();

            }
        }
       public bool UpdateEmployee(EmployeeData employee)
        {
            try
            {
                _sqlConnection.Open();
                var sqlcommand = new SqlCommand(cmdText: "update EMPLOYEE set Name=@name, Department=@deptartment,Age=@age,Address=@address where Id=@id", _sqlConnection);
                sqlcommand.Parameters.AddWithValue("id", employee.Id);
                sqlcommand.Parameters.AddWithValue("name", employee.Name);
                sqlcommand.Parameters.AddWithValue("deptartment", employee.Department);
                sqlcommand.Parameters.AddWithValue("age", employee.Age);
                sqlcommand.Parameters.AddWithValue("address", employee.Address);
                
                sqlcommand.ExecuteNonQuery();
                return true;
            }
            catch 
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
    }
}
