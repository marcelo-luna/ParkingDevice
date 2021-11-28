using Interfaces.ParkingDevice;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingDevice.Simulator
{
    public class GateSensorSimulator : IGateSensor
    {
        public int TotalCarsParked { get; set; } = 0;
        public int CapacityParking { get; set; }
        private Random _random;
        private CancellationTokenSource _cancellationToken;

        public GateSensorSimulator(int capacityParking)
        {
            if (capacityParking < 1)
            {
                throw new Exception("The capacity must be greater than 0");
            }

            CapacityParking = capacityParking;
            _random = new Random(); 
            _cancellationToken = new CancellationTokenSource();
        }

        public async Task GateSimulatorStart()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    await EnterGate();
                    await ExitGate();
                }
            }, _cancellationToken.Token);
        }
        public void GateSimulatorStop()
        {
            _cancellationToken.Cancel();
        }


        public async Task EnterGate(int? enterCarQuantity = null)
        {
            if (TotalCarsParked < CapacityParking)
            {
                TotalCarsParked = TotalCarsParked + enterCarQuantity ?? TotalCarsParked + _random.Next(0, 2);
            }
            await Task.Delay(1500);
        }

        public async Task ExitGate(int? exitCarQuantity = null)
        {
            if (TotalCarsParked > 0)
            {
                TotalCarsParked = TotalCarsParked - exitCarQuantity ?? TotalCarsParked - _random.Next(0, 2);
            }
            await Task.Delay(1500);
        }
    }
}
