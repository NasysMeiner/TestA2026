using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int MinValue = 1;
    public int MaxValue = 3;
    public List<Player> Players = new();
}

[Serializable]
public struct Player
{
    public TypeClass TypeClass;
    public EntityUi PrefabPlayer;
    public int HealthPointPerLevel;
    public TypeWeapon TypeWeapon;
}