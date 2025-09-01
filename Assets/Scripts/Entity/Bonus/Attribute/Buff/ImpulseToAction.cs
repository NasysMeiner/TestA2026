public class ImpulseToAction : Attribute
{
    private int _damageMultiplier;

    public ImpulseToAction(AttributeParameters parameters) : base(parameters)
    {
        _damageMultiplier = parameters.DamageMultiplier;
    }

    public override void UseBuff(DamageData damageData)
    {
        bool isBuffValue = CheckBuffDuration();

        if (isBuffValue)
        {
            damageData.DamageWeapon = damageData.DamageWeapon * _damageMultiplier;
            damageData.IsDamageNotDodgeable = true;
        }
    }
}
