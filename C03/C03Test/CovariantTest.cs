using C03Classes;

namespace C03Test
{
    public class CovariantTest
    {
        [Fact]
        public void Generic_Covariance_tests()
        {
            ICovariant<Sword> swordGetter = new SwordGetter();
            ICovariant<Weapon> weaponGetter = swordGetter;
            Assert.Same(swordGetter, weaponGetter);

            Sword sword = swordGetter.Get();
            Weapon weapon = weaponGetter.Get();

            var isSwordASword = Assert.IsType<Sword>(sword);
            var isWeaponASword = Assert.IsType<Sword>(weapon);

            Assert.NotNull(isSwordASword);
            Assert.NotNull(isWeaponASword);
        }
    }
}