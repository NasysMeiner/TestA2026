using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Attribute/AttributeData")]
public class AttributeData : ScriptableObject
{
    public List<AttributeParameters> AttributeParameters = new();
}

[Serializable]
public struct AttributeParameters
{
    public TypeAttribute TypeAttribute;
    public SkillVariation SkillVariation;
    public int BuffValue;
    public int CountBonus;
    public int ValueAfterBuff;
}