public class Weapon
{
    public TypeWeapon TypeWeapon { get; private set; }
    public TypeDamage TypeDamage { get; private set; }
    public int Damage { get; private set; }

    public Weapon(TypeWeapon typeWeapon, TypeDamage typeDamage, int damage)
    {
        TypeWeapon = typeWeapon;
        TypeDamage = typeDamage;
        Damage = damage;
    }
}
