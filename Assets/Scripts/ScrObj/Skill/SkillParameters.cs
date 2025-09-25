using System;

[Serializable]
public struct SkillParameters
{
    public bool IsMainAttack;
    public TypeSkill TypeSkill;
    public SkillVariation SkillVariation;
    public int Recharg;
    public int DamageBonus;
    public TypeAttribute TypeAttribute;
    public Attribute DebuffTarget;
}
