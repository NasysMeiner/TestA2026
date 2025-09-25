using System;
using System.Collections.Generic;

[Serializable]
public struct SkillArrayParameters
{
    public int Strength;
    public int Dexterity;
    public int Endurance;
    public List<TypeSkill> Skills;
    public List<TypeAttribute> Attributes;
}
