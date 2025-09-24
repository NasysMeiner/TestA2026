public class DragonBreathSkill : Skill
{
    private bool _isMainAttack;

    public DragonBreathSkill(SkillParameters skillParameters) : base(skillParameters)
    {
        _isMainAttack = skillParameters.IsMainAttack;
    }

    public override void UseSkill(DamageData damageData)
    {
        if (!CheckReady())
            return;

        damageData.Damage += _damageBonus;

        if (_isMainAttack && damageData.UseMainSkill == TypeSkill.Empty)
            damageData.UseMainSkill = TypeSkill;

        base.UseSkill(damageData);
    }
}
