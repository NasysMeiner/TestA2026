public class ImmuneSlashingDamageAttribute : Attribute
{
    public ImmuneSlashingDamageAttribute(AttributeParameters parameters) : base(parameters)
    {
    }

    public override void UseBuff(DamageData damageData)
    {
        if (!damageData.IsDamageNotDodgeable && damageData.Attacker.Weapon.TypeDamage == TypeDamage.Slashing)
            damageData.DamageWeapon = 0;
    }
}
