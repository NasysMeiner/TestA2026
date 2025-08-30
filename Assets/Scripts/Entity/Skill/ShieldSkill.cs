using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSkill : Skill
{
    public ShieldSkill(SkillVariation skillVariation, int damageBonus, int recharge) : base(skillVariation, damageBonus, recharge)
    {
    }

    public override void UseSkill(DamageData damageData)
    {
        if (!CheckReady())
            return;

        if (damageData.Attacker.Strength < damageData.Target.Strength)
            damageData.Damage += _damageBonus;

        base.UseSkill(damageData);
    }
}
