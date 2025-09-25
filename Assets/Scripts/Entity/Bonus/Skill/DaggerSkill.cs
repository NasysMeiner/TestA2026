public class DaggerSkill : Skill
{
    private bool _isMainAttack;

    public DaggerSkill(SkillParameters skillParameters) : base(skillParameters)
    {
        _isMainAttack = skillParameters.IsMainAttack;
    }

    public override void UseSkill(DamageData damageData)
    {
        if (!CheckReady())
            return;

        if (damageData.Attacker.Dexterity > damageData.Target.Dexterity)
            damageData.Damage += _damageBonus;

        if(_isMainAttack && damageData.UseMainSkill == TypeSkill.Empty)
            damageData.UseMainSkill = TypeSkill;

        base.UseSkill(damageData);
    }
}
