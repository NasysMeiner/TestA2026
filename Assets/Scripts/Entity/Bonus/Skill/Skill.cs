public abstract class Skill : IBonus
{
    private SkillVariation _skillVariation;
    private int _recharge;
    private int _currentRecharg;

    protected int _damageBonus;
    protected Attribute _debuff;

    public SkillVariation SkillVariation => _skillVariation;

    public Skill(Skill skill)
    {
        _skillVariation = skill._skillVariation;
        _recharge = skill._recharge;
        _damageBonus = skill._damageBonus;

        _debuff = skill._debuff;

        _currentRecharg = 0;
    }

    public Skill(SkillParameters skillParameters)
    {
        _skillVariation = skillParameters.SkillVariation;
        _recharge = skillParameters.Recharg;
        _damageBonus = skillParameters.DamageBonus;

        _debuff = skillParameters.DebuffTarget;

        _currentRecharg = 0;
    }

    public virtual void UseSkill(DamageData damageData)
    {
        _currentRecharg = _recharge;
    }

    protected bool CheckReady()
    {
        if (_currentRecharg != 0)
        {
            _currentRecharg--;

            return false;
        }
        else
        {
            return true;
        }
    }

    public void ResetBonus()
    {
        _currentRecharg = 0;
    }
}
