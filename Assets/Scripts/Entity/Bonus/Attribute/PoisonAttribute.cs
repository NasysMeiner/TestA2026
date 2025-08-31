public class PoisonAttribute : Attribute
{
    private int _startDamage = 0;
    private int _increasePerMove = 1;

    private int _currentDamage;

    public PoisonAttribute(Attribute attribute) : base(attribute)
    {
        if (attribute is not PoisonAttribute poisonAttribite)
            return;

        _startDamage = 0;
        _increasePerMove = poisonAttribite._increasePerMove;

        ResetBonus();
    }

    public PoisonAttribute(AttributeParameters parameters) : base(parameters)
    {
        _startDamage = 0;
        _increasePerMove = parameters.IncreasePerMove;

        ResetBonus();
    }

    public override void UseBuff(DamageData damageData)
    {
        if (!CheckBuffDuration())
            return;

        damageData.Damage += _currentDamage;
        _currentDamage += _increasePerMove;
    }

    public override void ResetBonus()
    {
        base.ResetBonus();

        _currentDamage = _startDamage;
    }
}
