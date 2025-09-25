using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon/UiWeaponData")]
public class UiWeaponData : ScriptableObject
{
    public List<WeaponElementsParameters> WeaponElements;
}