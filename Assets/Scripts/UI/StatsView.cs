using TMPro;
using UnityEngine;

public class StatsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;

    [SerializeField] private TMP_Text _health;
    [SerializeField] private TMP_Text _strength;
    [SerializeField] private TMP_Text _dexterity;
    [SerializeField] private TMP_Text _endurance;

    [SerializeField] private TMP_Text _warrior;
    [SerializeField] private TMP_Text _barbarian;
    [SerializeField] private TMP_Text _robber;

    [SerializeField] private TMP_Text _weapon;
    [SerializeField] private TMP_Text _weaponDamage;

    public void SetStats(Entity entity, string name)
    {
        _name.text = name;

        _health.text = entity.HealPoint.ToString();
        _strength.text = entity.Strength.ToString();
        _dexterity.text = entity.Dexterity.ToString();
        _endurance.text = entity.Endurance.ToString();

        _warrior.text = entity.LevelData.WarriorLevel.ToString();
        _barbarian.text = entity.LevelData.BararianLevel.ToString();
        _robber.text = entity.LevelData.RobberLevel.ToString();

        _weapon.text = entity.Weapon.TypeWeapon.ToString();
        _weaponDamage.text = entity.Weapon.Damage.ToString();
    }

    public void SetStats()
    {
        _name.text = "-";

        _health.text = "-";
        _strength.text = "-";
        _dexterity.text = "-";
        _endurance.text = "-";

        _weapon.text = "-";
        _weaponDamage.text = "-";
    }
}
