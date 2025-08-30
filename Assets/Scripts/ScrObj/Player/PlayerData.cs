using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int MinValue = 1;
    public int MaxValue = 3;
    public List<Player> Players = new();
}

[Serializable]
public struct Player
{
    public TypeClass TypeClass;
    public EntityUi PrefabPlayer;
    public int HealthPointPerLevel;
    public TypeWeapon TypeWeapon;
    public List<SkillArray> Skills;
}

[Serializable]
public struct SkillArray
{
    public int Strength;
    public int Dexterity;
    public int Endurance;
    public List<TypeSkill> Skills;
    public List<TypeAttribute> Attributes;
}