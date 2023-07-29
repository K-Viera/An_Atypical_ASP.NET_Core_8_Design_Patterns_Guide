using Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public interface IVehicleFactory
    {
        ICar CreateCar();
        IBike CreateBike();
    }

    public class LowEndVehicleFactory : IVehicleFactory
    {
        public IBike CreateBike() => new LowEndBike();
        public ICar CreateCar() => new LowEndCar();
    }

    public class HighEndVehicleFactory : IVehicleFactory
    {
        public IBike CreateBike() => new HighEndBike();
        public ICar CreateCar() => new HighEndCar();
    }

    public class MidRangeVehicleFactory : IVehicleFactory
    {
        public IBike CreateBike() => new MiddleGradeBike();
        public ICar CreateCar() => new MiddleGradeCar();
    }
}
