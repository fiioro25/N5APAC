using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PAC.IBusinessLogic;
using PAC.Domain;
using Microsoft.AspNetCore.Mvc;
using PAC.WebAPI;

namespace PAC.Tests.WebApi
{
    [TestClass]
    public class StudentControllerTest
    {
        private StudentController _controller;
        private Mock<IStudentLogic> _studentLogicMock;

        [TestInitialize]
        public void Initialize()
        {
            _studentLogicMock = new Mock<IStudentLogic>();
            _controller = new StudentController(_studentLogicMock.Object);
        }

        [TestMethod]
        public void PostStudentOk()
        {
            // Arrange
            var student = new Student { Id = 1, Name = "John Doe" };

            // Act
            var result = _controller.Post(student);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            _studentLogicMock.Verify(x => x.InsertStudents(student), Times.Once);
        }

        [TestMethod]
        public void PostStudentFail()
        {
            // Arrange
            Student? invalidStudent = null;

            // Act
            var result = _controller.Post(invalidStudent);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            _studentLogicMock.Verify(x => x.InsertStudents(It.IsAny<Student>()), Times.Never);
        }
    }
}
