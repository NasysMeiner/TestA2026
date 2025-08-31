using System.Collections.Generic;
using UnityEngine;

public class EntityFactory : MonoBehaviour
{
    private PlayerData _playerData;
    private EnemyData _enemyData;
    private WeaponData _weaponData;
    private SkillData _skillData;
    private AttributeData _attributeData;

    private Dictionary<TypeEnemy, int> _lastEnemy;
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
        Player player = _playerData.Players[0];

        foreach(Player pl in _playerData.Players)
        {
            if (pl.TypeClass == typeClass)
            {
                player = pl;
                break;
            }
        }

        generated.EntityUi = Instantiate(player.PrefabPlayer);

        GeneratedParameters playerParameters = GenerateParameters(player, _playerData.MinValue, _playerData.MaxValue);
        generated.Entity = new Entity(playerParameters);

        return generated;
    }

    public GeneratedData CreateEnemy()
    {
        GeneratedData generated = new();
        Enemy enemyData = _enemyData.Enemies[0];

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

        GeneratedParameters parameters = GenerateParameters(enemyData);
        generated.Entity = new Entity(parameters);

        return generated;
    }

    private GeneratedParameters GenerateParameters(Player player, int min, int max)
    {
        GeneratedParameters parameters = new();

        WeaponParameters weaponParameters = SearchWeaponParameters(player.TypeWeapon);

        parameters.TypeEntity = TypeEntity.Player;
        parameters.Weapon = new Weapon(weaponParameters.TypeWeapon, weaponParameters.TypeDamage, weaponParameters.WeaponDamage);
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

    private GeneratedParameters GenerateParameters(Enemy enemy)
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

    private void AddSkill(GeneratedParameters parameters, SkillArray skillArrays)
    {
        foreach(TypeSkill typeSkill in skillArrays.Skills)
        {
            SkillParameters par = SearchSkillParameters(typeSkill);
            Skill newSkill = null;

            switch (typeSkill)
            {
                case TypeSkill.DaggerDarkness:
                    newSkill = new DaggerSkill(par);
                break;
                case TypeSkill.FireBreath:
                    newSkill = new DaggerSkill(par);
                break;
                case TypeSkill.Shield:
                    newSkill = new ShieldSkill(par);
                break;
                case TypeSkill.PoisonDagger:
                    par.DebuffTarget = CreateAttribute(par.TypeAttribute);
                    newSkill = new PoisonDagger(par);
                break;
            }

            if (newSkill != null)
                parameters.Skills.Add(newSkill);
        }
    }

    private void AddAttribute(GeneratedParameters parameters, SkillArray skillArrays)
    {
        foreach(TypeAttribute attribute in skillArrays.Attributes)
        {
            AttributeParameters par = SearchAttributeParameters(attribute);
            Attribute newAttribute = null;

            switch (attribute)
            {
                case TypeAttribute.Reage:
                    newAttribute = new RageAttribute(par);
                break;
                case TypeAttribute.Poison:
                    newAttribute = new PoisonAttribute(par);
                break;
            }

            if(newAttribute != null)
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

    private Attribute CreateAttribute(TypeAttribute typeAttribute)
    {
        AttributeParameters parameters = SearchAttributeParameters(typeAttribute);
        Attribute newAttribute = null;

        switch (typeAttribute)
        {
            case TypeAttribute.Reage:
                newAttribute = new RageAttribute(parameters);
                break;
            case TypeAttribute.Poison:
                newAttribute = new PoisonAttribute(parameters);
                break;
        }

        return newAttribute;
    }

    private WeaponParameters SearchWeaponParameters(TypeWeapon typeWeapon)
    {
        foreach(WeaponParameters weapon in _weaponData.Weapons)
            if (weapon.TypeWeapon == typeWeapon)
                return weapon;

        return _weaponData.Weapons[0];
    }
}

public struct GeneratedData
{
    public EntityUi EntityUi;
    public Entity Entity;
}

public class GeneratedParameters
{
    public TypeEntity TypeEntity;
    public float HealthPoint;
    public Weapon Weapon;
    public int Strength;
    public int Dexterity;
    public int Endurance;
    public List<Skill> Skills = new();
    public List<Attribute> Attributes = new();
}