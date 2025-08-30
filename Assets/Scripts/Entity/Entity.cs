using System.Collections.Generic;

public class Entity
{
    private List<Skill> _damageSkills = new();
    private List<Skill> _deffSkills = new();
    private List<Attribute> _attackAttribute = new();//DamageBonus out
    private List<Attribute> _defAttribute = new();//TakeDamageBonus in

    public int CurrentLevel { get; private set; }
    public TypeEntity TypeEntity { get; private set; }
    public Weapon Weapon { get; private set; }
    public float HealPoint { get; private set; }
    public float MaxHealPoint { get; private set; }
    public int Strength { get; private set; }
    public int Dexterity { get; private set; }
    public int Endurance { get; private set; }

    public Entity(GeneratedParameters parameters)
    {
        CurrentLevel = 1;
        TypeEntity = parameters.TypeEntity;
        Weapon = parameters.Weapon;
        HealPoint = parameters.HealthPoint;
        MaxHealPoint = parameters.HealthPoint;
        Strength = parameters.Strength;
        Dexterity = parameters.Dexterity;
        Endurance = parameters.Endurance;

        foreach(Skill skill in parameters.Skills)
        {
            if (skill.SkillVariation == SkillVariation.Attacking)
                _damageSkills.Add(skill);
            else
                _deffSkills.Add(skill);
        }

        foreach(Attribute attribute in parameters.Attributes)
        {
            if (attribute.AttrubuteVariation == SkillVariation.Attacking)
                _attackAttribute.Add(attribute);
            else
                _defAttribute.Add(attribute);
        }
    }

    public void CalculateDamage(DamageData data, Entity target)
    {
        data.IsDead = false;
        data.Attacker = this;
        data.Damage = Strength;
        data.TypeDamage = Weapon.TypeDamage;
        data.DamageWeapon = Weapon.Damage;

        foreach (Skill skill in _damageSkills)
            skill.UseSkill(data);

        foreach (Attribute attribute in _attackAttribute)
            attribute.UseBuff(data);
    }

    public void TakeDamage(DamageData data)
    {
        foreach (Skill skill in _deffSkills)
            skill.UseSkill(data);

        foreach (Attribute attribute in _defAttribute)
            attribute.UseBuff(data);

        int finalDamage = data.Damage + data.DamageWeapon;

        if (finalDamage <= 0)
            finalDamage = 1;

        HealPoint -= finalDamage;

        if (HealPoint <= 0)
            data.IsDead = true;
    }

    public void ResetEntity()
    {
        RegenerateHealthPoints();

        ResetBuffsAndSkills(new List<IBonus>(_damageSkills));
        ResetBuffsAndSkills(new List<IBonus>(_deffSkills));
        ResetBuffsAndSkills(new List<IBonus>(_attackAttribute));
        ResetBuffsAndSkills(new List<IBonus>(_defAttribute));
    }

    public void RegenerateHealthPoints()
    {
        HealPoint = MaxHealPoint;
    }

    private void ResetBuffsAndSkills(List<IBonus> arrayBonus)
    {
        foreach (IBonus bonus in arrayBonus)
            bonus.ResetBonus();
    }
}
