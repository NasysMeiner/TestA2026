public class DaggerSkill : Skill
{
    public DaggerSkill(SkillVariation skillVariation, int damageBonus, int recharge) : base(skillVariation, damageBonus, recharge) { }

    public override void UseSkill(DamageData damageData)
    {
        if (!CheckReady())
            return;

        if (damageData.Attacker.Dexterity > damageData.Target.Dexterity)
            damageData.Damage += _damageBonus;

        base.UseSkill(damageData);
    }
}
