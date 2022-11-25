using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using EmployeeManagement.Application.Services;
using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace EmployeeServiceTest
{
    [TestClass]
    public class EmployeeServiceTest
    {
        private EmployeeService _employeeService;
        private Mock<IEmployeeRepository> _mockEmployeeRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_mockEmployeeRepository.Object,null);

        }
        [TestMethod]
        public void GetEmployeesById_ReturnSucess()
        {
            //Arrange
            _mockEmployeeRepository.Setup(m => m.GetEmployeeById(7)).Returns(GetDummyEmployeeSucess());
            //Act
            var result = _employeeService.GetEmployeeById(7);
            //Assert
            Assert.IsNotNull(result);
            //Verify
            _mockEmployeeRepository.Verify(m => m.GetEmployeeById(7),Times.Once);

        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetEmployeeById_RetursNull()
        {
            //Arrange
            _mockEmployeeRepository.Setup(m => m.GetEmployeeById(1)).Returns(GetDummyEmployeeNull());
            //Act
            var result = _employeeService.GetEmployeeById(1);
            //Assert 
            Assert.IsNotNull(result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetEmployeeById_RetursInvalidId()
        {
            //Arrange
            // _mockEmployeeRepository.Setup(m => m.GetEmployeeById(-1)).Returns(GetDummyEmployeeNull());
            //Act
            var result = _employeeService.GetEmployeeById(-1);
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetEmployees_ReturnSucess()
        {
            //Arrange
            _mockEmployeeRepository.Setup(m => m.GetEmployees()).Returns(new List<EmployeeData>());
            //Act
            var result = _employeeService.GetEmployees();
            //Assert
            Assert.IsNotNull(result);
            //Verify
            _mockEmployeeRepository.Verify(m => m.GetEmployees(),Times.Once);

        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetEmployees_ReturnNull()
        {
            //Arrange
            _mockEmployeeRepository.Setup(m => m.GetEmployees()).Returns(() => null);
            //Act
            var result = _employeeService.GetEmployees();
            //Assert
            Assert.IsNotNull(result); 
        }

        [TestMethod]
        public void InsertEmployee_ReturSucess()
        {
            //Arrange
            _mockEmployeeRepository.Setup(m => m.InsertEmployee(It.IsAny<EmployeeData>())).Returns(true);
            //Act
            var result = _employeeService.InsertEmployee(new EmployeeDto()); ;
            //Assert
            Assert.IsNotNull(result);
            //Verify
            _mockEmployeeRepository.Verify(m => m.InsertEmployee(It.IsAny<EmployeeData>()),Times.Once);
        }

         [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void InsertEmployee_ReturnNull()
         {
             //Arrange
             _mockEmployeeRepository.Setup(m => m.InsertEmployee(It.IsAny<EmployeeData>())).Returns(false);
             //Act
             var result = _employeeService.InsertEmployee(new EmployeeDto());
             //Assert
             Assert.IsNotNull(result);  
         }

        [TestMethod]
        public void UpdateEmployee_ReturnSucess()
        {
            //Arrange
            _mockEmployeeRepository.Setup(m => m.UpdateEmployee(It.IsAny<EmployeeData>())).Returns(true);
            //Act
            var result = _employeeService.UpdateEmployee(new EmployeeDto());
            //Assert
            Assert.IsNotNull(result);
            //Verify
            _mockEmployeeRepository.Verify(m => m.UpdateEmployee(It.IsAny<EmployeeData>()),Times.Once);
        }
         [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdataEmployee_ReturnNull()
         {
             //Arrange
             _mockEmployeeRepository.Setup(m => m.UpdateEmployee(It.IsAny<EmployeeData>())).Returns(false);
             //Act
             var result = _employeeService.UpdateEmployee(new EmployeeDto());
             //Assert
             Assert.IsNotNull(result);
            // Assert.IsTrue(result.StatusCode == StatusCodes.Status500InternalServerError);

       }

        [TestMethod]
        public void DeleteEmployee_ReturnSucess()
        {
            //Arrange
            _mockEmployeeRepository.Setup(m => m.DeleteEmployee(5)).Returns(true);
            //Act
            var result = _employeeService.DeleteEmployee(5);
            //Assert
            Assert.IsNotNull(result);
            //verify
            _mockEmployeeRepository.Verify(m => m.DeleteEmployee(5),Times.Once);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteEmployee_ReturnNull()
        {
            //Arrange
            _mockEmployeeRepository.Setup(m => m.DeleteEmployee(-1)).Returns(false);
            //Act
            var result = _employeeService.DeleteEmployee(-1);
          
        }
        #region PRIVATE_FIELD
        private EmployeeData GetDummyEmployeeSucess()
        {
            return new EmployeeData();
        }
        private EmployeeData GetDummyEmployeeNull()
        {
            return null;
        }
        
        #endregion
    }
}
