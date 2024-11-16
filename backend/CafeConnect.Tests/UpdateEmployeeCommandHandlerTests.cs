using CafeConnect.Application.Features.Employee.Commands;
using CafeConnect.Domain.Enums;
using CafeConnect.Domain.Repositories;
using Moq;

namespace CafeConnect.Tests
{
    public class UpdateEmployeeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldUpdateEmployeeDetails_WhenValidRequestIsProvided()
        {
            // Arrange
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            var handler = new UpdateEmployeeCommandHandler(mockEmployeeRepository.Object);

            var command = new UpdateEmployeeCommand
            {
                EmployeeId = "UI1234567",
                Name = "Updated Name",
                EmailAddress = "updated.email@example.com",
                PhoneNumber = 9876543,
                Gender = GenderType.Male,
                CafeId = Guid.NewGuid()
            };

            var updatedEmployee = new Domain.Entities.Employee
            {
                Id = command.EmployeeId,
                Name = command.Name,
                EmailAddress = command.EmailAddress,
                PhoneNumber = command.PhoneNumber,
                Gender = command.Gender,
                CafeId = command.CafeId
            };

            mockEmployeeRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Employee>()))
                .Returns(Task.CompletedTask);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockEmployeeRepository.Verify(repo => repo.UpdateAsync(It.Is<Domain.Entities.Employee>(e =>
                e.Id == updatedEmployee.Id &&
                e.Name == updatedEmployee.Name &&
                e.EmailAddress == updatedEmployee.EmailAddress &&
                e.PhoneNumber == updatedEmployee.PhoneNumber &&
                e.Gender == updatedEmployee.Gender &&
                e.CafeId == updatedEmployee.CafeId)), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldSetCafeIdToNull_WhenCafeIdIsEmptyGuid()
        {
            // Arrange
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            var handler = new UpdateEmployeeCommandHandler(mockEmployeeRepository.Object);

            var command = new UpdateEmployeeCommand
            {
                EmployeeId = "UI1234567",
                Name = "Test Name",
                EmailAddress = "test.email@example.com",
                PhoneNumber = 1234567890,
                Gender = GenderType.Female,
                CafeId = Guid.Empty
            };

            var updatedEmployee = new Domain.Entities.Employee
            {
                Id = command.EmployeeId,
                Name = command.Name,
                EmailAddress = command.EmailAddress,
                PhoneNumber = command.PhoneNumber,
                Gender = command.Gender,
                CafeId = null
            };

            mockEmployeeRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Employee>()))
                .Returns(Task.CompletedTask);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockEmployeeRepository.Verify(repo => repo.UpdateAsync(It.Is<Domain.Entities.Employee>(e =>
                e.Id == updatedEmployee.Id &&
                e.CafeId == null)), Times.Once);
        }
    }
}