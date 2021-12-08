using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ParkingDevice.Simulator;
using System.Threading.Tasks;

namespace ParkingDevice.Test
{
    public class GateSensorSimulatorTest
    {
        private GateSensorSimulator _gateSensorSimulator;

        [SetUp]
        public void Setup()
        {
            var logMock = new Mock<ILogger<GateSensorSimulator>>();
            _gateSensorSimulator = new GateSensorSimulator(40);
        }

        [Test]
        public async Task CarEnter()
        {
            _gateSensorSimulator.TotalCarsParked = 0;
            await _gateSensorSimulator.EnterGate(1);

            Assert.AreEqual(_gateSensorSimulator.TotalCarsParked, 1);
        }

        [Test]
        public async Task AddNewWhenLimitReached()
        {
            _gateSensorSimulator.TotalCarsParked = _gateSensorSimulator.CapacityParking;
            await _gateSensorSimulator.EnterGate(1);

            Assert.AreEqual(_gateSensorSimulator.TotalCarsParked, _gateSensorSimulator.CapacityParking);
        }

        [Test]
        public async Task CarExit()
        {
            _gateSensorSimulator.TotalCarsParked = 10;
            await _gateSensorSimulator.ExitGate(1);

            Assert.AreEqual(_gateSensorSimulator.TotalCarsParked, 9);
        }

        [Test]
        public async Task RemoveWhenParkEmpty()
        {
            _gateSensorSimulator.TotalCarsParked = 0;
            await _gateSensorSimulator.ExitGate(1);

            Assert.AreEqual(_gateSensorSimulator.TotalCarsParked, 0);
        }
    }
}