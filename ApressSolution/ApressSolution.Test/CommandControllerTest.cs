using ApressSolution.Controllers;
using ApressSolution.Data;
using ApressSolution.Dtos;
using ApressSolution.Models;
using ApressSolution.Profiles;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ApressSolution.Test
{
    public class CommandControllerTest : IDisposable
    {
        Mock<ICommandAPIRepo> _mockRepo;
        CommandProfile _realProfile;
        MapperConfiguration _mapperConfiguration;
        IMapper _mapper;
        public CommandControllerTest()
        {
            _mockRepo = new Mock<ICommandAPIRepo>();
            _realProfile = new CommandProfile();
            _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(_realProfile));
            _mapper = new Mapper(_mapperConfiguration);
        }

        [Fact]
        public void GetCommands_ReturnsZeroItems_WhenDBIsEmpty()
        {
            //Arrange 
            _mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));

            //We need to create an instance of our CommandsController class
            var controller = new CommandsController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllCommands_ReturnOneItem_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.Get();

            //Assert
            OkObjectResult? okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<CommandReadDto>;

            Assert.Single(commands);
        }

        [Fact]
        public void GetAllCommands_Returns200Ok_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllCommands_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);
        }

        [Fact]
        public void GetCommandByID_Returns404NotFound_WhenNonExistentIDProvided()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
            var controller = new CommandsController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetCommandById(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetCommandByID_Returns200OK_WhenValidIDProvided()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "mock",
                Platform = "mock",
                CommandLine = "mock"
            });
            var controller = new CommandsController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetCommandById(1);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetCommandByID_ReturnsCorrectType_WhenValidIDProvided()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "mock",
                Platform = "mock",
                CommandLine = "mock"
            });
            var controller = new CommandsController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetCommandById(0);

            //Assert
            Assert.IsType<ActionResult<CommandReadDto>>(result);
        }

        private List<Command> GetCommands(int num)
        {
            var commands = new List<Command>();
            if (num > 0)
            {
                commands.Add(new Command
                {
                    Id = 0,
                    HowTo = "How to generate a migration",
                    CommandLine = "dotnet ef migrations add <Name of Migration>",
                    Platform = ".Net Core EF"
                });
            }
            return commands;
        }

        public void Dispose()
        {
            _mockRepo = null;
            _mapper = null;
            _mapperConfiguration = null;
            _realProfile = null;
        }

    }
}
