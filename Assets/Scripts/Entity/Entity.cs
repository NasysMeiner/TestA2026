using System.Collections.Generic;

public class Entity
{
    private List<Skill> _damageSkills = new();
    private List<Skill> _deffSkills = new();
    private List<Attribute> _attackAttribute = new();//DamageBonus out
    private List<Attribute> _defAttribute = new();//TakeDamageBonus in

    private Dictionary<TypeSkill, int> _countSkill = new();
    private Dictionary<TypeAttribute, int> _countAttribute = new();

    private Dictionary<TypeAttribute, int> _countTimeAttribute = new();

    public LevelData LevelData { get; private set; }
    public TypeEntity TypeEntity { get; private set; }
    public TypeClass TypeClass { get; private set; }
    public Weapon Weapon { get; private set; }
    public float HealPoint { get; private set; }
    public float MaxHealPoint { get; private set; }
    public int Strength { get; private set; }
    public int Dexterity { get; private set; }
    public int Endurance { get; private set; }

    public Entity(GeneratedParameters parameters)
    {
        LevelData = new();
        TypeClass = parameters.TypeClass;
        AddParameters(parameters);
    }

    public void AddParameters(GeneratedParameters parameters)
    {
        TypeEntity = parameters.TypeEntity;
        Weapon = parameters.Weapon;
        HealPoint = parameters.HealthPoint;
        MaxHealPoint = parameters.HealthPoint;
        Strength = parameters.Strength;
        Dexterity = parameters.Dexterity;
        Endurance = parameters.Endurance;

        LevelData.LevelUp(parameters.TypeClass);

        AddSkill(parameters.Skills);
        AddAttribute(parameters.Attributes);
    }

    public void AddSkill(List<Skill> skills)
    {
        foreach (Skill skill in skills)
        {
            if (!_countSkill.ContainsKey(skill.TypeSkill))
            {
                skill.SetBaseType();
                _countSkill.Add(skill.TypeSkill, 1);

                if (skill.SkillVariation == SkillVariation.Attacking)
                    _damageSkills.Add(skill);
                else
                    _deffSkills.Add(skill);
            }
        }
    }

    public void AddAttribute(List<Attribute> attributes)
    {
        foreach (Attribute attribute in attributes)
        {
            if (!_countAttribute.ContainsKey(attribute.TypeAttribute))
            {
                attribute.SetBaseType();
                _countAttribute.Add(attribute.TypeAttribute, 1);

                if (attribute.AttrubuteVariation == SkillVariation.Attacking)
                    _attackAttribute.Add(attribute);
                else
                    _defAttribute.Add(attribute);
            }
        }
    }

    public void CalculateDamage(DamageData data, Entity target)
    {
        data.IsDead = false;
        data.Attacker = this;

        if (data.IsMiss)
            return;

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
        foreach (Attribute attribute in _defAttribute)
            attribute.UseBuff(data);

        if (data.IsMiss)
            goto skip;

        ApplyDebuff(data.DebuffTarget);

        foreach (Skill skill in _deffSkills)
            skill.UseSkill(data);

        skip:

        int finalDamage = data.FinalDamage;

        if (finalDamage < 0)
            finalDamage = 0;

        HealPoint -= finalDamage;

        if (HealPoint <= 0)
            data.IsDead = true;
    }

    public void ResetEntity()
    {
        RegenerateHealthPoints();

        _attackAttribute = ClearTimeBuffsAndDebuffs(_attackAttribute);
        _defAttribute = ClearTimeBuffsAndDebuffs(_defAttribute);

        ResetBuffsAndSkills(new List<IBonus>(_damageSkills));
        ResetBuffsAndSkills(new List<IBonus>(_deffSkills));
        ResetBuffsAndSkills(new List<IBonus>(_attackAttribute));
        ResetBuffsAndSkills(new List<IBonus>(_defAttribute));
    }

    public void RegenerateHealthPoints()
    {
        HealPoint = MaxHealPoint;
    }

    public void SetWeapon(Weapon weapon)
    {
        Weapon = weapon;
    }

    private List<Attribute> ClearTimeBuffsAndDebuffs(List<Attribute> attributes)
    {
        List<Attribute> cleraListAttribute = new();

        for (int i = 0; i < attributes.Count; i++)
            if (attributes[i].OverlayType != OverlayType.Outside)
                cleraListAttribute.Add(attributes[i]);

        return cleraListAttribute;
    }

    private void ResetBuffsAndSkills(List<IBonus> arrayBonus)
    {
        foreach (IBonus bonus in arrayBonus)
            bonus.ResetBonus();
    }

    private void ApplyDebuff(Attribute debuff)
    {
        if (debuff == null)
            return;

        if (!_countTimeAttribute.ContainsKey(debuff.TypeAttribute))
        {
            _defAttribute.Add(debuff);
            _countTimeAttribute.Add(debuff.TypeAttribute, 1);
        }
    }
}
