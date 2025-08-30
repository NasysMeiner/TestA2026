using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Skill/SkillData")]
public class SkillData : ScriptableObject
{
    public List<SkillParameters> Skills;
}

[Serializable]
public struct SkillParameters
{
    public bool IsMainAttack;
    public TypeSkill TypeSkill;
    public SkillVariation SkillVariation;
    public int Recharg;
    public int DamageBonus;
}