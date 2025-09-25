using System;

[Serializable]
public struct EnemyParameters
{
    public TypeEnemy TypeEnemy;
    public EntityUi PrefabEnemy;
    public float Health;
    public TypeWeapon TypeWeapon;
    public int WeaponDamage;
    public int Strength;
    public int Dexterity;
    public int Endurance;
    public SkillArrayParameters Skills;
}
