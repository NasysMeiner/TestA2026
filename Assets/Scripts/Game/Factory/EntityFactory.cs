using System.Collections.Generic;
using UnityEngine;

public class EntityFactory : MonoBehaviour
{
    private PlayerData _playerData;
    private EnemyData _enemyData;
    private WeaponData _weaponData;
    private SkillData _skillData;

    private Dictionary<TypeEnemy, int> _lastEnemy;
    private int _currentEnemy = 0;

    public void Init(PlayerData playerData, EnemyData enemyData, WeaponData weaponData, SkillData skillData)
    {
        _playerData = playerData;
        _enemyData = enemyData;
        _weaponData = weaponData;
        _skillData = skillData;
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
            AddSkill(parameters, player.Skills[0]);

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

        return parameters;
    }

    private void AddSkill(GeneratedParameters parameters, SkillArray skillArrays)
    {
        foreach(TypeSkill typeSkill in skillArrays.Skills)
        {
            SkillParameters par;

            switch (typeSkill)
            {
                case TypeSkill.DaggerDarkness:
                    par = SearchSkillParameters(TypeSkill.DaggerDarkness);
                    DaggerSkill skill = new(par.SkillVariation, par.DamageBonus, par.Recharg);
                    parameters.Skills.Add(skill);
                break;
                case TypeSkill.FireBreath:
                    par = SearchSkillParameters(TypeSkill.FireBreath);
                    DaggerSkill daggerSkill = new(par.SkillVariation, par.DamageBonus, par.Recharg);
                    parameters.Skills.Add(daggerSkill);
                break;
                case TypeSkill.Shield:
                    par = SearchSkillParameters(TypeSkill.Shield);
                    ShieldSkill shieldSkill = new(par.SkillVariation, par.DamageBonus, par.Recharg);
                    parameters.Skills.Add(shieldSkill);
                break;
            }
        }
    }

    private SkillParameters SearchSkillParameters(TypeSkill typeSkill)
    {
        foreach (SkillParameters skill in _skillData.Skills)
            if (skill.TypeSkill == typeSkill)
                return skill;

        return _skillData.Skills[0];
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
}