using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C03Classes
{
    public class WeaponSetter : IContravariant<Weapon>
    {
        private Weapon? _weapon;
        public void Set(Weapon value)
        {
            _weapon = value;
        }
    }
}
