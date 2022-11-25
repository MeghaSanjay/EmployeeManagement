using EmployeeManagement.Application.Models;
using EmployeeManagement.UI.Controllers.InternalAPI;
using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Providers.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace EmployeeInternalApiControllerTest
{
    [TestClass]
    public class EmployeeInternalApiControllerTest
    {
        #region PRIVATE_FIELD

        private EmployeeInternalApiController _employeeInternalApiController;
        private Mock<IEmployeeApiClient> _mockEmployeeApiClient;

        #endregion

        #region INITIALIZE
        [TestInitialize]
        public void TestInitialize()
        {
            _mockEmployeeApiClient = new Mock<IEmployeeApiClient>();
            _employeeInternalApiController = new EmployeeInternalApiController(_mockEmployeeApiClient.Object);
        }

        #endregion

        #region PUBLIC_FIELD

        [TestMethod]
        public void GetEmployeeById_ReturnSucess()
        {
            //Arrange
            _mockEmployeeApiClient.Setup(m => m.GetEmployeeById(3)).Returns(GetDummyEmployeeSucess());
            //Act
            var result = _employeeInternalApiController.GetEmployesById(3) as OkObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);
            Assert.AreEqual(result.StatusCode, StatusCodes.Status200OK);
            //Verify
            _mockEmployeeApiClient.Verify(m => m.GetEmployeeById(3),Times.Once);

        }

        [TestMethod]
        public void GetEmployeeById_ReturnNull()
        {
            //Arrange
            _mockEmployeeApiClient.Setup(m => m.GetEmployeeById(4)).Returns(GetDummyEmployeeNull());
            //Act
            var result = _employeeInternalApiController.GetEmployesById(4) as ObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status500InternalServerError);
        }
        [TestMethod]
        public void GetEmployeeById_ReturnInvalidId()
        {
            //Arrange
            //_mockEmployeeApiClient.Setup(m => m.GetEmployeeById(4)).Returns(GetDummyEmployeeNull());
            //Act
            var result = _employeeInternalApiController.GetEmployesById(-1) as ObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status400BadRequest);
        }
        [TestMethod]
        public void InsertEmployee_ReturnSucess()
        {
            //Arrange 
            _mockEmployeeApiClient.Setup(m => m.InsertEmployee(It.IsAny<EmployeeDetailedViewModel>())).Returns(true);
            //Act
            var result = _employeeInternalApiController.InsertEmployes(new EmployeeDetailedViewModel()) as OkObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);
            //Verify
            _mockEmployeeApiClient.Verify(m => m.InsertEmployee(It.IsAny<EmployeeDetailedViewModel>()),Times.Once);

        }
        [TestMethod]
        public void InsertEmployee_ReturnNull()
        {
            //Arrange
             _mockEmployeeApiClient.Setup(m => m.InsertEmployee(It.IsAny<EmployeeDetailedViewModel>())).Returns(false);
            //Act
            var result = _employeeInternalApiController.InsertEmployes(new EmployeeDetailedViewModel()) as ObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode==StatusCodes.Status500InternalServerError);
        }
        [TestMethod]
        public void UpdateEmployee_ReturnSucess()
        {
            //Arrange
            _mockEmployeeApiClient.Setup(m => m.UpdateEmployee(It.IsAny<EmployeeDetailedViewModel>())).Returns(true);
            //Act
            var result = _employeeInternalApiController.UpdateEmploye(new EmployeeDetailedViewModel()) as OkObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);
        }
       [TestMethod]
        public void UpdateEmployee_ReturnNull()
        {
            //Arrange
            _mockEmployeeApiClient.Setup(m => m.UpdateEmployee(It.IsAny<EmployeeDetailedViewModel>())).Returns(false);
            //Act
            var result = _employeeInternalApiController.UpdateEmploye(new EmployeeDetailedViewModel()) as ObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status500InternalServerError);
        }
        [TestMethod]
        public void DeleteEmployee_ReturnSucess()
        {
            //Arrange
            _mockEmployeeApiClient.Setup(m => m.DeleteEmployee(3)).Returns(true);
            //Act
            var result = _employeeInternalApiController.DeleteEmploye(3) as ObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);
        }
        [TestMethod]
        public void DeleteEmployee_ReturnNull()
        {
            //Arrange
            _mockEmployeeApiClient.Setup(m => m.DeleteEmployee(-1)).Returns(false);
            //Act
            var result = _employeeInternalApiController.DeleteEmploye(-1) as ObjectResult;
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status400BadRequest);
            
        }

        #endregion

        private EmployeeDetailedViewModel GetDummyEmployeeNull()
        {
            return null;
        }

        private EmployeeDetailedViewModel GetDummyEmployeeSucess()
        {
            return new EmployeeDetailedViewModel();
        }
    }
}
