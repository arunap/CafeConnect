using CafeConnect.Application.Features.Employee.Commands;
using CafeConnect.Domain.Enums;
using CafeConnect.Domain.Repositories;
using Moq;

namespace CafeConnect.Tests
{
    public class CreateEmployeeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnEmployeeId_WhenEmployeeIsSuccessfullyCreated()
        {
            // Arrange
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            var handler = new CreateEmployeeCommandHandler(mockEmployeeRepository.Object);

            var command = new CreateEmployeeCommand
            {
                Name = "John Doe",
                EmailAddress = "john.doe@example.com",
                PhoneNumber = 1234567890,
                Gender = GenderType.Male,
                CafeId = Guid.NewGuid()
            };

            var generatedEmployeeId = "UI1234567";
            mockEmployeeRepository
                .SetupSequence(repo => repo.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((Domain.Entities.Employee)null);

            mockEmployeeRepository
                .Setup(repo => repo.InsertAsync(It.IsAny<Domain.Entities.Employee>()))
                .ReturnsAsync(generatedEmployeeId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(generatedEmployeeId, result);
            mockEmployeeRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<string>()), Times.AtLeastOnce);
            mockEmployeeRepository.Verify(repo => repo.InsertAsync(It.Is<Domain.Entities.Employee>(e =>
                e.Name == command.Name &&
                e.EmailAddress == command.EmailAddress &&
                e.PhoneNumber == command.PhoneNumber &&
                e.Gender == command.Gender &&
                e.CafeId == command.CafeId)), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldRegenerateEmployeeId_WhenGeneratedIdAlreadyExists()
        {
            // Arrange
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            var handler = new CreateEmployeeCommandHandler(mockEmployeeRepository.Object);

            var command = new CreateEmployeeCommand
            {
                Name = "Jane Smith",
                EmailAddress = "jane.smith@example.com",
                PhoneNumber = 98765432,
                Gender = GenderType.Female
            };

            var existingEmployeeId = "UI1234567";
            var newEmployeeId = "UI7654321";

            mockEmployeeRepository
                .SetupSequence(repo => repo.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new Domain.Entities.Employee { Id = existingEmployeeId })
                .ReturnsAsync((Domain.Entities.Employee)null);

            mockEmployeeRepository
                .Setup(repo => repo.InsertAsync(It.IsAny<Domain.Entities.Employee>()))
                .ReturnsAsync(newEmployeeId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(newEmployeeId, result);
            mockEmployeeRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<string>()), Times.AtLeast(2));
            mockEmployeeRepository.Verify(repo => repo.InsertAsync(It.Is<Domain.Entities.Employee>(e =>
                e.Name == command.Name &&
                e.EmailAddress == command.EmailAddress &&
                e.PhoneNumber == command.PhoneNumber &&
                e.Gender == command.Gender &&
                e.CafeId == null)), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldSetCafeIdToNull_WhenCafeIdIsEmptyGuid()
        {
            // Arrange
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            var handler = new CreateEmployeeCommandHandler(mockEmployeeRepository.Object);

            var command = new CreateEmployeeCommand
            {
                Name = "Alice Johnson",
                EmailAddress = "alice.johnson@example.com",
                PhoneNumber = 5678901,
                Gender = GenderType.Female,
                CafeId = Guid.Empty
            };

            var generatedEmployeeId = "UI3456789";

            mockEmployeeRepository
                .Setup(repo => repo.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((Domain.Entities.Employee)null);

            mockEmployeeRepository
                .Setup(repo => repo.InsertAsync(It.IsAny<Domain.Entities.Employee>()))
                .ReturnsAsync(generatedEmployeeId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(generatedEmployeeId, result);
            mockEmployeeRepository.Verify(repo => repo.InsertAsync(It.Is<Domain.Entities.Employee>(e =>
                e.CafeId == null)), Times.Once);
        }
    }
}