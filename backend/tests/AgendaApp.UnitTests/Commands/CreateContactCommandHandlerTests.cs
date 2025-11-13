using AutoMapper;
using Moq;
using Xunit;
using AgendaApp.Application.Commands.CreateContact;
using AgendaApp.Application.Exceptions;
using AgendaApp.Domain.Interfaces;
using AgendaApp.Domain.Entities;
using AgendaApp.Application.Mappings;


namespace AgendaApp.UnitTests.Commands
{
    public class CreateContactCommandHandlerTests
    {
        private readonly Mock<IContactRepository> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly CreateContactCommandHandler _handler;

        public CreateContactCommandHandlerTests()
        {
            _repositoryMock = new Mock<IContactRepository>();

            var config = new MapperConfiguration(cfg =>
                cfg.AddProfile<ContactMappingProfile>());
            _mapper = config.CreateMapper();

            _handler = new CreateContactCommandHandler(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsContactDto()
        {
            // Arrange
            var command = new CreateContactCommand
            {
                Nome = "João Silva",
                Email = "joao@email.com",
                Telefone = "(11) 98765-4321"
            };

            _repositoryMock.Setup(r => r.ExistsByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Nome, result.Nome);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Contact>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DuplicateEmail_ThrowsBusinessException()
        {
            // Arrange
            var command = new CreateContactCommand
            {
                Nome = "João Silva",
                Email = "joao@email.com",
                Telefone = "(11) 98765-4321"
            };

            _repositoryMock.Setup(r => r.ExistsByEmailAsync(command.Email))
                .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }
    }
}