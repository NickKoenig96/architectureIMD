using System;
using Xunit;
using MyProject.API.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using MyProject.API.Ports;
using MyProject.API.Domain;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;
using Snapshooter.Xunit;




namespace MyProject.Tests.UnitTests
{
    // Only partially tests the controller; the other endpoints are more of a reader's exercise. PR's welcome.
    // Don't pull this on me with your own project ;-).
    public class WeatherControllerUnitTest
    {
        // Mock the logger as STDOUT/STDIN is *** slow.
        private Mock<ILogger<EventsController>> _mockedLogger = new Mock<ILogger<EventsController>>();
        // Notice that we don't care what our database implementation looks like, since we "mock" (i.e. fake) it.
        // We are testing the behaviour of our controller in this class, not of the database. 
        private Mock<IDatabase> _mockedDatabase = new Mock<IDatabase>();

        public WeatherControllerUnitTest()
        {
            _mockedDatabase.Reset();
            _mockedLogger.Reset();
        }

        [Fact]
        public async Task TestGetById_HappyPath()
        {
            // arrange
            // this is our happy flow: we ask for the id of an existing application
            var ourId = Guid.NewGuid();
            var ourEvent = new Event { Id = ourId, eventTitle = "yes", eventDate = DateTime.Now, eventDescription = "event description 1", eventAge = 16, eventParticpantCount = 7 };
            // set up the mock so that when we call the 'GetMovieById' method we return a predefined task
            // No database calls are happening here.
            _mockedDatabase.Setup(x => x.GetEventById(ourId)).Returns(Task.FromResult(ourEvent));

            // act
            var controller = new EventsController(_mockedLogger.Object, _mockedDatabase.Object);
            var actualResult = await controller.GetById(ourId.ToString()) as OkObjectResult;

            // assert
            Assert.Equal(200, actualResult.StatusCode);
            var viewModel = actualResult.Value as ViewEvent;
            Assert.Equal(ourEvent.Id.ToString(), viewModel.Id);
            Assert.Equal(ourEvent.eventTitle, viewModel.eventTitle);
            Assert.Equal(ourEvent.eventDate, viewModel.eventDate);
            Assert.Equal(ourEvent.eventDescription, viewModel.eventDescription);
            Assert.Equal(ourEvent.eventAge, viewModel.eventAge);
            //Assert.Equal(ourEvent.eventParticipants, viewModel.eventParticipants);
            Assert.Equal(ourEvent.eventParticpantCount, viewModel.eventParticpantCount);





            _mockedLogger.VerifyAll();
            _mockedDatabase.VerifyAll();
        }


        [Fact]
        public async Task TestGetById_DoesntExist()
        {
            // arrange
            var ourId = Guid.NewGuid();
            var ourEvent = new Event { Id = ourId, eventTitle = "yes", eventDate = DateTime.Now, eventDescription = "event description 1", eventAge = 16, eventParticpantCount = 7 };
            _mockedDatabase.Setup(x => x.GetEventById(ourId)).Returns(Task.FromResult(null as Event));

            // act
            var controller = new EventsController(_mockedLogger.Object, _mockedDatabase.Object);

            // assert
            var result = await new EventsController(_mockedLogger.Object, _mockedDatabase.Object).GetById(ourId.ToString());
            Assert.IsType<NotFoundResult>(result);

            _mockedLogger.VerifyAll();
            _mockedDatabase.VerifyAll();
        }

        [Fact]
        public async Task TestGetById_ErrorOnRetrievalAsync()
        {
            // arrange
            var ourId = Guid.NewGuid();
            var ourEvent = new Event { Id = ourId, eventTitle = "yees", eventDate = DateTime.Now, eventDescription = "event description 1", eventAge = 16, eventParticpantCount = 7 };
            _mockedDatabase.Setup(x => x.GetEventById(ourId)).ThrowsAsync(new Exception("drama"));

            // act
            var result = await new EventsController(_mockedLogger.Object, _mockedDatabase.Object).GetById(ourId.ToString());

            // assert
            Assert.IsType<BadRequestObjectResult>(result);
            _mockedLogger.VerifyAll();
            _mockedDatabase.VerifyAll();
        }
    }
}