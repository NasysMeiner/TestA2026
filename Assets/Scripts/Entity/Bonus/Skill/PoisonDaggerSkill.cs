public class PoisonDaggerSkill : Skill
{
    public PoisonDaggerSkill(PoisonDaggerSkill skill) : base(skill)
    {
    }

    public PoisonDaggerSkill(SkillParameters skillParameters) : base(skillParameters)
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
