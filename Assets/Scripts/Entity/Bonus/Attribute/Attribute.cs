public abstract class Attribute : IBonus
{
    private TypeAttribute _typeAttribute;
    private SkillVariation _attrubuteVariation;
    private int _currentMoves;

    protected int _buffValue;
    protected int _countMoves;
    protected int _valueAfterBuff;

    public SkillVariation AttrubuteVariation => _attrubuteVariation;
    public TypeAttribute TypeAttribute => _typeAttribute;

    public Attribute(TypeAttribute typeAttribute, SkillVariation attrubuteVariation, int buffValue, int countMoves, int valueAfterBuff)
    {
        _typeAttribute = typeAttribute;
        _attrubuteVariation = attrubuteVariation;
        _buffValue = buffValue;
        _countMoves = countMoves;
        _valueAfterBuff = valueAfterBuff;

        ResetBonus();
    }

    public abstract void UseBuff(DamageData damageData);

    public bool CheckBuffDuration()
    {
        if (_currentMoves != 0)
        {
            _currentMoves--;

            return true;
        }

        return false;
    }

    public void ResetBonus()
    {
        _currentMoves = _countMoves;
    }
}
