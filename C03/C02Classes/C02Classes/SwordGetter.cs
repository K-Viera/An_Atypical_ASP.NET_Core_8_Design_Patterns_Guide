using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C03Classes
{
    public class SwordGetter : ICovariant<Sword>
    {
        private static readonly Sword _instance = new();
        public Sword Get() => _instance;
    }
}
