using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    public interface ICar { }
    public interface IBike { }
    public class LowEndCar : ICar { }
    public class LowEndBike : IBike { }
    public class HighEndCar : ICar { }
    public class HighEndBike : IBike { }
    public class MiddleGradeCar : ICar { }
    public class MiddleGradeBike : IBike { }
}
