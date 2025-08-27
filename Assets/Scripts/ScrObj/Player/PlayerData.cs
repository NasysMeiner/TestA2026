using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public List<Player> Players;
}

[Serializable]
public struct Player
{
    public TypeClass TypeClass;
    public PlayerUi PrefabPlayer;
}