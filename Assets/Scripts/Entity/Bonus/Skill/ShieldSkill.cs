public class ShieldSkill : Skill
{
    public ShieldSkill(SkillParameters skillParameters) : base(skillParameters)
    {
    }

    public override void UseSkill(DamageData damageData)
    {
        if (!CheckReady())
            return;

        if (damageData.Attacker.Strength < damageData.Target.Strength)
        {
            damageData.Damage += _damageBonus;
            base.UseSkill(damageData);
        }
    }
}
