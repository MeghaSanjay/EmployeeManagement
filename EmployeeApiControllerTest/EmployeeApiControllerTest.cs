using EmployeeManagement.API.Controllers;
using EmployeeManagement.API.Models;
using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using EmployeeManagement.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EmployeeApiControllerTest
{
    [TestClass]
    public class EmployeeApiControllerTest
    {
        #region PRIVATE_FIELDS

        private EmployeeApiController _employeeApiController;
        private Mock<IEmployeeService> _mockEmployeeService;

        #endregion

        #region TEST_INITILIZE
        [TestInitialize]
        public void TestInitialize()
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            _employeeApiController = new EmployeeApiController(_mockEmployeeService.Object);
        }

        #endregion


        #region PUBLIC_FIELD
        [TestMethod]
        public void GetEmployeesById_ReturnSucess()
        {
            //Arrange

            _mockEmployeeService.Setup(m => m.GetEmployeeById(7)).Returns(GetDummyEmployeeSucess());
            /*{
                Assert.AreEqual(result.StatusCode, StatusCodes.Status200OK);
            }));*/
            // ;
            //Act
            var result = _employeeApiController.GetEmployeesById(7) as OkObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);
            Assert.AreEqual(result.StatusCode, StatusCodes.Status200OK);
            //Verify
            _mockEmployeeService.Verify(m => m.GetEmployeeById(7), Times.Once);
        }
        [TestMethod]
        public void GetEmployeeById_RetursNull()
        {
            //Arrange
            _mockEmployeeService.Setup(m => m.GetEmployeeById(1)).Returns(GetDummyEmployeeNull());
            //Act
            var result = _employeeApiController.GetEmployeesById(1) as ObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status500InternalServerError);
            Assert.AreEqual(result.StatusCode, StatusCodes.Status500InternalServerError);
        }
        [TestMethod]
        public void GetEmployeeById_RetursInvalidId()
        {
            //Arrange
          //  _mockEmployeeService.Setup(m => m.GetEmployeeById(-1)).Returns(GetDummyEmployeeNull());
            //Act
            var result = _employeeApiController.GetEmployeesById(-1) as ObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status400BadRequest);

        }

        [TestMethod]
        public void GetEmployees_ReturnSucess()
        {
            //Arrange
             _mockEmployeeService.Setup(m => m.GetEmployees()).Returns( new List <EmployeeDto>());
            //Act
            var result = _employeeApiController.GetEmployee() as OkObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);
            //Verify
            _mockEmployeeService.Verify(m => m.GetEmployees(),Times.Once);
        }
        [TestMethod]
        public void GetEmployees_ReturnNull()
        {
            //Arrange
            _mockEmployeeService.Setup(m => m.GetEmployees()).Returns(()=>null);
            //Act
            var result = _employeeApiController.GetEmployee() as ObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status500InternalServerError);
        }

        [TestMethod]
        public void InsertEmployee_ReturSucess()
        {
            //Arrange
            _mockEmployeeService.Setup(m => m.InsertEmployee(It.IsAny<EmployeeDto>())).Returns(true);
            //Act
            var result = _employeeApiController.InsertEmployees(new EmployeeDetailedViewModel()) as OkObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);
            //Verify
            _mockEmployeeService.Verify(m => m.InsertEmployee(It.IsAny<EmployeeDto>()),Times.Once);


        }

       [TestMethod]
        public void InsertEmployee_ReturnNull()
        {
            //Arrange
            _mockEmployeeService.Setup(m => m.InsertEmployee(It.IsAny<EmployeeDto>())).Returns(false);
            //Act
            var result = _employeeApiController.InsertEmployees(new EmployeeDetailedViewModel()) as ObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status500InternalServerError);
        }

        [TestMethod]
        public void UpdateEmployee_ReturnSucess()
        {
            //Arrange
            _mockEmployeeService.Setup(m => m.UpdateEmployee(It.IsAny<EmployeeDto>())).Returns(true);
            //Act
            var result = _employeeApiController.UpdateEmployees(new EmployeeDetailedViewModel()) as OkObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);
            //Verify
            _mockEmployeeService.Verify(m => m.UpdateEmployee(It.IsAny<EmployeeDto>()),Times.Once);
        }
        [TestMethod]
        public void UpdataEmployee_ReturnNull()
        {
            //Arrange
            _mockEmployeeService.Setup(m => m.UpdateEmployee(It.IsAny<EmployeeDto>())).Returns(false);
            //Act
            var result = _employeeApiController.UpdateEmployees(new EmployeeDetailedViewModel()) as ObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status500InternalServerError);
          
        }

        [TestMethod]
        public void DeleteEmployee_ReturnSucess()
        {
            //Arrange
            _mockEmployeeService.Setup(m => m.DeleteEmployee(5)).Returns(true);
            //Act
            var result = _employeeApiController.DeleteEmployees(5) as OkObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);
            //Verify
            _mockEmployeeService.Verify(m => m.DeleteEmployee(5),Times.Once);


        }
        [TestMethod]
        public void DeleteEmployee_ReturnNull()
        {
            //Arrange
            _mockEmployeeService.Setup(m => m.DeleteEmployee(5)).Returns(false);
            //Act
            var result = _employeeApiController.DeleteEmployees(5) as ObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status500InternalServerError);
            
        }

        #endregion


        #region PRIVATE_FIELD
        private EmployeeDto GetDummyEmployeeSucess()
        {
            return new EmployeeDto();
        }
        private EmployeeDto GetDummyEmployeeNull()
        {
            return null;
        }
        #endregion
    }
}