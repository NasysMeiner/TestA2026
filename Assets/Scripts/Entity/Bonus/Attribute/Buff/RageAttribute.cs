public class RageAttribute : Attribute
{
    public RageAttribute(AttributeParameters parameters) : base(parameters)
    {
    }

    public override void UseBuff(DamageData damageData)
    {
        bool isBuffValue = CheckBuffDuration();

        if (isBuffValue)
            damageData.Damage += _buffValue;
        else
            damageData.Damage += _valueAfterBuff;
    }
}
