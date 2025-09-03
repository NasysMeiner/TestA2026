using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwapWeaponView : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameWeaponPlayer;
    [SerializeField] private TMP_Text _typeWeaponPlayer;
    [SerializeField] private TMP_Text _damageWeaponPlayer;
    [SerializeField] private TMP_Text _nameWeaponEnemy;
    [SerializeField] private TMP_Text _typeWeaponEnemy;
    [SerializeField] private TMP_Text _damageWeaponEnemy;

    private UiWeaponData _weaponData;

    public void Init(UiWeaponData weaponData)
    {
        _weaponData = weaponData;
    }

    public void SetWeaponStats(Weapon player, Weapon enemy)
    {
        WeaponElements playerWeapon = SearchWeaponData(player.TypeWeapon);
        WeaponElements enemyWeapon = SearchWeaponData(enemy.TypeWeapon);

        _nameWeaponPlayer.text = playerWeapon.Name;
        _typeWeaponPlayer.text = player.TypeDamage.ToString();
        _damageWeaponPlayer.text = player.Damage.ToString();

        _nameWeaponEnemy.text = enemyWeapon.Name;
        _typeWeaponEnemy.text = enemy.TypeDamage.ToString();
        _damageWeaponEnemy.text = enemy.Damage.ToString();
    }

    private WeaponElements SearchWeaponData(TypeWeapon typeWeapon)
    {
        foreach (WeaponElements elements in _weaponData.WeaponElements)
            if (elements.TypeWeapon == typeWeapon)
                return elements;

        return null;
    }
}
