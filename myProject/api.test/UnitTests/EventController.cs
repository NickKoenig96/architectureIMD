using System;
using Xunit;
using MyProject.API.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using MyProject.API.Ports;
using MyProject.API.Domain;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyProject.Tests.UnitTests
{
    public class EventControllerUnitTest
    {
        private Mock<ILogger<EventsController>> _mockedLogger = new Mock<ILogger<EventsController>>();

        private Mock<IDatabase> _mockedDatabase = new Mock<IDatabase>();

        public EventControllerUnitTest()
        {
            _mockedDatabase.Reset();
            _mockedLogger.Reset();
        }

        [Fact]
        public async Task TestGetById_HappyPath()
        {

            var ourId = Guid.NewGuid();
            var ourEvent = new Event { Id = ourId, eventTitle = "yes", eventDate = DateTime.Now, eventDescription = "event description 1", eventAge = 16, eventParticpantCount = 7 };

            _mockedDatabase.Setup(x => x.GetEventById(ourId)).Returns(Task.FromResult(ourEvent));

            var controller = new EventsController(_mockedLogger.Object, _mockedDatabase.Object);
            var actualResult = await controller.GetById(ourId.ToString()) as OkObjectResult;

            Assert.Equal(200, actualResult.StatusCode);
            var viewModel = actualResult.Value as ViewEvent;
            Assert.Equal(ourEvent.Id.ToString(), viewModel.Id);
            Assert.Equal(ourEvent.eventTitle, viewModel.eventTitle);
            Assert.Equal(ourEvent.eventDate, viewModel.eventDate);
            Assert.Equal(ourEvent.eventDescription, viewModel.eventDescription);
            Assert.Equal(ourEvent.eventAge, viewModel.eventAge);
            Assert.Equal(ourEvent.eventParticpantCount, viewModel.eventParticpantCount);

            _mockedLogger.VerifyAll();
            _mockedDatabase.VerifyAll();
        }


        [Fact]
        public async Task TestGetById_DoesntExist()
        {
            var ourId = Guid.NewGuid();
            var ourEvent = new Event { Id = ourId, eventTitle = "yes", eventDate = DateTime.Now, eventDescription = "event description 1", eventAge = 16, eventParticpantCount = 7 };
            _mockedDatabase.Setup(x => x.GetEventById(ourId)).Returns(Task.FromResult(null as Event));

            var controller = new EventsController(_mockedLogger.Object, _mockedDatabase.Object);

            var result = await new EventsController(_mockedLogger.Object, _mockedDatabase.Object).GetById(ourId.ToString());
            Assert.IsType<NotFoundResult>(result);

            _mockedLogger.VerifyAll();
            _mockedDatabase.VerifyAll();
        }

        [Fact]
        public async Task TestGetById_ErrorOnRetrievalAsync()
        {
            var ourId = Guid.NewGuid();
            var ourEvent = new Event { Id = ourId, eventTitle = "yees", eventDate = DateTime.Now, eventDescription = "event description 1", eventAge = 16, eventParticpantCount = 7 };
            _mockedDatabase.Setup(x => x.GetEventById(ourId)).ThrowsAsync(new Exception("drama"));

            var result = await new EventsController(_mockedLogger.Object, _mockedDatabase.Object).GetById(ourId.ToString());

            Assert.IsType<BadRequestObjectResult>(result);
            _mockedLogger.VerifyAll();
            _mockedDatabase.VerifyAll();
        }
    }
}