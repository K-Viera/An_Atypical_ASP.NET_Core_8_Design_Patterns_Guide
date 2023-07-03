using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C03Classes
{
    public record class Weapon { }

    public record class Sword : Weapon { }

    public record class TwoHandedSword : Sword { }
}
