public class PoisonDagger : Skill
{
    public PoisonDagger(PoisonDagger skill) : base(skill)
    {
    }

    public PoisonDagger(SkillParameters skillParameters) : base(skillParameters)
    {
    }

    public override void UseSkill(DamageData damageData)
    {
        if (!CheckReady())
            return;

        damageData.DebuffTarget = new PoisonAttribute(_debuff);

        base.UseSkill(damageData);
    }
}
