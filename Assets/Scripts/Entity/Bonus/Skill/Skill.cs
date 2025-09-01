public abstract class Skill : IBonus
{
    private SkillVariation _skillVariation;
    private int _recharge;
    private int _currentRecharg;
    private OverlayType _overlayType;
    private TypeSkill _typeSkill;

    protected int _damageBonus;
    protected Attribute _debuff;

    public SkillVariation SkillVariation => _skillVariation;
    public OverlayType OverlayType => _overlayType;
    public TypeSkill TypeSkill => _typeSkill;

    public Skill(Skill skill)
    {
        _skillVariation = skill._skillVariation;
        _recharge = skill._recharge;
        _damageBonus = skill._damageBonus;
        _overlayType = skill._overlayType;
        _typeSkill = TypeSkill;

        _debuff = skill._debuff;

        _currentRecharg = 0;
    }

    public Skill(SkillParameters skillParameters)
    {
        _skillVariation = skillParameters.SkillVariation;
        _recharge = skillParameters.Recharg;
        _damageBonus = skillParameters.DamageBonus;
        _overlayType = OverlayType.Outside;
        _typeSkill = skillParameters.TypeSkill;

        _debuff = skillParameters.DebuffTarget;

        _currentRecharg = 0;
    }

    public void SetBaseType()
    {
        _overlayType = OverlayType.Base;
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
