using System.Collections.Generic;

public class Entity
{
    private List<ISkill> _damageSkills = new();
    private List<ISkill> _deffSkills = new();
    private List<IAttribute> _buffs = new();
    private List<IAttribute> _debuffs = new();

    public TypeEntity TypeEntity { get; private set; }
    public Weapon Weapon { get; private set; }
    public float HealPoint { get; private set; }
    public float MaxHealPoint { get; private set; }
    public int Strength { get; private set; }
    public int Dexterity { get; private set; }
    public int Endurance { get; private set; }

    public Entity(GeneratedParameters parameters)
    {
        TypeEntity = parameters.TypeEntity;
        Weapon = parameters.Weapon;
        HealPoint = parameters.HealthPoint;
        MaxHealPoint = parameters.HealthPoint;
        Strength = parameters.Strength;
        Dexterity = parameters.Dexterity;
        Endurance = parameters.Endurance;
    }

    public void CalculateDamage(DamageData data, Entity target)
    {
        data.IsDead = false;
        data.Attacker = this;
        data.Damage = Strength;
        data.TypeDamage = Weapon.TypeDamage;
        data.DamageWeapon = Weapon.Damage;
    }

    public void TakeDamage(DamageData data)
    {
        HealPoint -= data.Damage + data.DamageWeapon;

        if (HealPoint <= 0)
            data.IsDead = true;
    }

    public void RegenerateHealthPoints()
    {
        HealPoint = MaxHealPoint + 100;
    }
}
