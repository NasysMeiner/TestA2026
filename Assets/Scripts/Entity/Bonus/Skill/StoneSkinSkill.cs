public class StoneSkinSkill : Skill
{
    public StoneSkinSkill(SkillParameters skillParameters) : base(skillParameters)
    {
    }

    public override void UseSkill(DamageData damageData)
    {
        if (!CheckReady())
            return;

        damageData.Damage -= damageData.Target.Endurance;

        base.UseSkill(damageData);
    }
}
