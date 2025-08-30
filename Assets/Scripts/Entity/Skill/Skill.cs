public abstract class Skill
{
    private SkillVariation _skillVariation;
    private int _recharge;
    private int _currentRecharg;

    protected int _damageBonus;

    public SkillVariation SkillVariation => _skillVariation;

    public Skill(SkillVariation skillVariation, int damageBonus, int recharge)
    {
        _skillVariation = skillVariation;
        _recharge = recharge;
        _damageBonus = damageBonus;

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
}
