using System.Threading.Tasks;

namespace Interfaces.ParkingDevice
{
    public interface IGateSensor
    {
        public Task EnterGate(int? enterCarQuantity = null);
        public Task ExitGate(int? exitCarQuantity = null);
    }
}