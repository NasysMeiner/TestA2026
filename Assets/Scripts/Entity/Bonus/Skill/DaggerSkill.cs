public class DaggerSkill : Skill
{
    public DaggerSkill(SkillParameters skillParameters) : base(skillParameters) { }

    public override void UseSkill(DamageData damageData)
    {
        if (!CheckReady())
            return;

        if (damageData.Attacker.Dexterity > damageData.Target.Dexterity)
            damageData.Damage += _damageBonus;

        base.UseSkill(damageData);
    }
}
