public class RageAttribute : Attribute
{
    public RageAttribute(TypeAttribute typeAttribute, SkillVariation attrubuteVariation, int buffValue, int countMoves, int valueAfterBuff) : base(typeAttribute, attrubuteVariation, buffValue, countMoves, valueAfterBuff)
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
