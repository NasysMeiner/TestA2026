using System.Collections.Generic;
using UnityEngine;

public class EntityFactory : MonoBehaviour
{
    private PlayerData _playerData;
    private EnemyData _enemyData;
    private WeaponData _weaponData;
    private SkillData _skillData;
    private AttributeData _attributeData;

    private Dictionary<TypeEnemy, int> _lastEnemy = new();
    private int _currentEnemy = 0;

    public void Init(PlayerData playerData, EnemyData enemyData, WeaponData weaponData, SkillData skillData, AttributeData attributeData)
    {
        _playerData = playerData;
        _enemyData = enemyData;
        _weaponData = weaponData;
        _skillData = skillData;
        _attributeData = attributeData;
    }

    public GeneratedData CreatePlayer(TypeClass typeClass)
    {
        GeneratedData generated = new();
        PlayerParameters player = SearchPlayerParameters(typeClass);

        generated.EntityUi = Instantiate(player.PrefabPlayer);
        generated.EntityUi.SetNameEntity(player.TypeClass.ToString());

        GeneratedParameters playerParameters = GenerateParameters(player, _playerData.MinValue, _playerData.MaxValue);
        generated.Entity = new Entity(playerParameters);

        return generated;
    }

    public GeneratedData CreateEnemy()
    {
        GeneratedData generated = new();
        EnemyParameters enemyData = _enemyData.Enemies[0];

        if (_enemyData.IsRandom)
        {
            if (_enemyData.IsUnique && _lastEnemy.Count == _enemyData.Enemies.Count)
                _lastEnemy.Clear();

            while (true)
            {
                _currentEnemy = Random.Range(0, _enemyData.Enemies.Count);

                if (_enemyData.IsUnique)
                {
                    if (_lastEnemy.ContainsKey(_enemyData.Enemies[_currentEnemy].TypeEnemy))
                        continue;
                    else
                        _lastEnemy.Add(_enemyData.Enemies[_currentEnemy].TypeEnemy, 1);
                }

                break;
            }

            enemyData = _enemyData.Enemies[_currentEnemy];
        }
        else
        {
            enemyData = _enemyData.Enemies[_currentEnemy++];

            if (_currentEnemy >= _enemyData.Enemies.Count)
                _currentEnemy = 0;
        }

        generated.EntityUi = Instantiate(enemyData.PrefabEnemy);
        generated.EntityUi.SetNameEntity(enemyData.TypeEnemy.ToString());

        GeneratedParameters parameters = GenerateParameters(enemyData);
        generated.Entity = new Entity(parameters);

        return generated;
    }

    public void LevelUpPlayer(Entity entity, TypeClass typeClass)
    {
        int level = 0;
        PlayerParameters player = SearchPlayerParameters(typeClass);

        switch (typeClass)
        {
            case TypeClass.Warrior:
                level += entity.LevelData.WarriorLevel;
                break;
            case TypeClass.Barbarian:
                level += entity.LevelData.BararianLevel;
                break;
            case TypeClass.Robber:
                level += entity.LevelData.RobberLevel;
                break;
        }

        if (player.Skills.Count <= level)
            return;

        GeneratedParameters parameters = new();
        SkillArrayParameters skillArray = player.Skills[level];

        parameters.TypeClass = player.TypeClass;
        parameters.TypeEntity = TypeEntity.Player;
        parameters.Weapon = entity.Weapon;
        parameters.Strength = entity.Strength + skillArray.Strength;
        parameters.Dexterity = entity.Dexterity + skillArray.Dexterity;
        parameters.Endurance = entity.Endurance + skillArray.Endurance;
        parameters.HealthPoint = entity.MaxHealPoint + skillArray.Endurance + player.HealthPointPerLevel;

        AddSkill(parameters, skillArray);
        AddAttribute(parameters, skillArray);

        entity.AddParameters(parameters);
    }

    public Weapon CreateWeapon(TypeWeapon weapon)
    {
        WeaponParameters weaponParameters = SearchWeaponParameters(weapon);

        return new Weapon(weaponParameters.TypeWeapon, weaponParameters.TypeDamage, weaponParameters.WeaponDamage);
    }

    private GeneratedParameters GenerateParameters(PlayerParameters player, int min, int max)
    {
        GeneratedParameters parameters = new();

        parameters.TypeClass = player.TypeClass;
        parameters.TypeEntity = TypeEntity.Player;
        parameters.Weapon = CreateWeapon(player.TypeWeapon);
        parameters.Strength = Random.Range(min, max + 1);
        parameters.Dexterity = Random.Range(min, max + 1);
        parameters.Endurance = Random.Range(min, max + 1);
        parameters.HealthPoint = player.HealthPointPerLevel + parameters.Endurance;

        if (player.Skills.Count != 0)
        {
            AddSkill(parameters, player.Skills[0]);
            AddAttribute(parameters, player.Skills[0]);
        }

        return parameters;
    }

    private GeneratedParameters GenerateParameters(EnemyParameters enemy)
    {
        GeneratedParameters parameters = new();

        WeaponParameters weaponParameters = SearchWeaponParameters(enemy.TypeWeapon);

        parameters.TypeEntity = TypeEntity.Enemy;
        parameters.HealthPoint = enemy.Health;
        parameters.Weapon = new Weapon(weaponParameters.TypeWeapon, weaponParameters.TypeDamage, enemy.WeaponDamage);
        parameters.Strength = enemy.Strength;
        parameters.Dexterity = enemy.Dexterity;
        parameters.Endurance = enemy.Endurance;
        parameters.Skills = new();

        AddSkill(parameters, enemy.Skills);
        AddAttribute(parameters, enemy.Skills);

        return parameters;
    }

    private void AddSkill(GeneratedParameters parameters, SkillArrayParameters skillArrays)
    {
        foreach (TypeSkill typeSkill in skillArrays.Skills)
        {
            Skill newSkill = CreateSkill(typeSkill);

            if (newSkill != null)
                parameters.Skills.Add(newSkill);
        }
    }

    private void AddAttribute(GeneratedParameters parameters, SkillArrayParameters skillArrays)
    {
        foreach (TypeAttribute attribute in skillArrays.Attributes)
        {
            Attribute newAttribute = CreateAttribute(attribute);

            if (newAttribute != null)
                parameters.Attributes.Add(newAttribute);
        }
    }

    private SkillParameters SearchSkillParameters(TypeSkill typeSkill)
    {
        foreach (SkillParameters skill in _skillData.Skills)
            if (skill.TypeSkill == typeSkill)
                return skill;

        return _skillData.Skills[0];
    }

    private AttributeParameters SearchAttributeParameters(TypeAttribute typeAttribute)
    {
        foreach (AttributeParameters attribute in _attributeData.AttributeParameters)
            if (attribute.TypeAttribute == typeAttribute)
                return attribute;

        return _attributeData.AttributeParameters[0];
    }

    private Skill CreateSkill(TypeSkill typeSkill)
    {
        SkillParameters par = SearchSkillParameters(typeSkill);
        Skill newSkill = null;

        switch (typeSkill)
        {
            case TypeSkill.DaggerDarkness:
                newSkill = new DaggerSkill(par);
                break;
            case TypeSkill.FireBreath:
                newSkill = new DragonBreathSkill(par);
                break;
            case TypeSkill.Shield:
                newSkill = new ShieldSkill(par);
                break;
            case TypeSkill.PoisonDagger:
                par.DebuffTarget = CreateAttribute(par.TypeAttribute);
                newSkill = new PoisonDaggerSkill(par);
                break;
            case TypeSkill.StoneSkin:
                newSkill = new StoneSkinSkill(par);
                break;
        }

        return newSkill;
    }

    private Attribute CreateAttribute(TypeAttribute typeAttribute)
    {
        AttributeParameters parameters = SearchAttributeParameters(typeAttribute);
        Attribute newAttribute = null;

        switch (typeAttribute)
        {
            case TypeAttribute.ImpulseToAction:
                newAttribute = new ImpulseToAction(parameters);
                break;
            case TypeAttribute.Reage:
                newAttribute = new RageAttribute(parameters);
                break;
            case TypeAttribute.Poison:
                newAttribute = new PoisonAttribute(parameters);
                break;
            case TypeAttribute.ImmunSlashingDamage:
                newAttribute = new ImmuneSlashingDamageAttribute(parameters);
                break;
            case TypeAttribute.VulnerabilityCrushingDamage:
                newAttribute = new VulnerabilityCrushingDamageAttribute(parameters);
                break;
        }

        return newAttribute;
    }

    private WeaponParameters SearchWeaponParameters(TypeWeapon typeWeapon)
    {
        foreach (WeaponParameters weapon in _weaponData.Weapons)
            if (weapon.TypeWeapon == typeWeapon)
                return weapon;

        return _weaponData.Weapons[0];
    }

    private PlayerParameters SearchPlayerParameters(TypeClass typeClass)
    {
        foreach (PlayerParameters player in _playerData.Players)
            if (player.TypeClass == typeClass)
                return player;

        return _playerData.Players[0];
    }
}

public struct GeneratedData
{
    public EntityUi EntityUi;
    public Entity Entity;
}

public class GeneratedParameters
{
    public TypeClass TypeClass;
    public TypeEntity TypeEntity;
    public float HealthPoint;
    public Weapon Weapon;
    public int Strength;
    public int Dexterity;
    public int Endurance;
    public List<Skill> Skills = new();
    public List<Attribute> Attributes = new();
}