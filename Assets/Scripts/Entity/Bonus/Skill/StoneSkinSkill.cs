public class StoneSkinSkill : Skill
{
    public StoneSkinSkill(SkillVariation skillVariation, int damageBonus, int recharge) : base(skillVariation, damageBonus, recharge)
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
