using System.Security.Cryptography;

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

    public Attribute (Attribute attribute)
    {
        _typeAttribute = attribute._typeAttribute;
        _attrubuteVariation = attribute._attrubuteVariation;
        _buffValue = attribute._buffValue;
        _countMoves = attribute._countMoves;
        _valueAfterBuff = attribute._valueAfterBuff;

        ResetBonus();
    }

    public Attribute(AttributeParameters parameters)
    {
        _typeAttribute = parameters.TypeAttribute;
        _attrubuteVariation = parameters.SkillVariation;
        _buffValue = parameters.BuffValue;
        _countMoves = parameters.CountBonus;
        _valueAfterBuff = parameters.ValueAfterBuff;

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

    public virtual void ResetBonus()
    {
        _currentMoves = _countMoves;
    }
}
