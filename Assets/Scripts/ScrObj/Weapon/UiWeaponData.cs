using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon/UiWeaponData")]
public class UiWeaponData : ScriptableObject
{
    public List<WeaponElements> WeaponElements;
}

[Serializable]
public class WeaponElements
{
    public TypeWeapon TypeWeapon;
    public string Name;
    public Sprite Icon;
}