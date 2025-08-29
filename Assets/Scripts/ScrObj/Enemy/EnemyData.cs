using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public bool IsRandom;
    public bool IsUnique;
    public List<Enemy> Enemies = new();
}

[Serializable]
public struct Enemy
{
    public TypeEnemy TypeEnemy;
    public EntityUi PrefabEnemy;
    public float Health;
    public TypeWeapon TypeWeapon;
    public int WeaponDamage;
    public int Strength;
    public int Dexterity;
    public int Endurance;
}