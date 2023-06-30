using C03Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C03Test
{
    public class ContravarianceTest
    {
        [Fact]
        public void Generic_Contravariance_tests()
        {
            IContravariant<Weapon> weaponSetter = new WeaponSetter();
            IContravariant<Sword> swordSetter = weaponSetter;
            Assert.Same(swordSetter, weaponSetter);

            // Contravariance: Weapon > Sword > TwoHandedSword
            weaponSetter.Set(new Weapon());
            weaponSetter.Set(new Sword());
            weaponSetter.Set(new TwoHandedSword());

            // Contravariance: Sword > TwoHandedSword
            swordSetter.Set(new Sword());
            swordSetter.Set(new TwoHandedSword());
        }
    }
}
