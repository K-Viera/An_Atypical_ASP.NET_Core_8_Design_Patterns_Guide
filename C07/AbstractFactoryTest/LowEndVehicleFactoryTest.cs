using Factory;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryTest
{
    public class LowEndVehicleFactoryTest:BaseAbstractFactoryTest<LowEndVehicleFactory,LowEndCar,LowEndBike>
    {
    }
}
