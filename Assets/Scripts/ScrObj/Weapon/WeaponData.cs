using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon/WeaponData")]
public class WeaponData : ScriptableObject
{
    public List<WeaponParameters> Weapons = new();
}