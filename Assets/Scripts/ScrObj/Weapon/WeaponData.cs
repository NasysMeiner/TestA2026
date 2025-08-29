using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon/WeaponData")]
public class WeaponData : ScriptableObject
{
    public List<WeaponParameters> Weapons = new();
}

[Serializable]
public struct WeaponParameters
{
    public TypeWeapon TypeWeapon;
    public TypeDamage TypeDamage;
    public int WeaponDamage;
}